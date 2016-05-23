[CmdletBinding(PositionalBinding=$True)]

Param
      (
        [parameter(Mandatory=$False)]
        [String] $AppDatabaseName = "CarClassifiedDb",

        [parameter(Mandatory=$False)]
        [String] $UserName = "dbuser",

        # Required
        [parameter(Mandatory=$True)]
        [String] $Password,

        [parameter(Mandatory=$False)]
        [String] $FirewallRuleName = "WebsiteRule",

        [parameter(Mandatory=$False)]
        [String] $StartIPAddress,

        [parameter(Mandatory=$False)]
        [String]$EndIPAddress,

        [parameter(Mandatory=$False)]
        [String]$Location = "West US"
      )

# Create firewall rule for the website using Windows Azure REST API and Set Firewall Rule
function New-FirewallRuleForWebsite
{
    Param
          (
             [parameter(Mandatory=$True)]
             [String]
             $FirewallRuleName,

             [parameter(Mandatory=$True)]
             [String]
             $DatabaseServerName
          )

    Write-Verbose "[start] Creating a new firewall rule $FirewallRuleName for the website in database server $DatabaseServerName."

    $s = Get-AzureSubscription -Current –ExtendedDetails
    $subscriptionID = $s.SubscriptionId
    $thumbprint = $s.Certificate.Thumbprint
    if (!($subscriptionID -and $thumbprint)) {throw "Error: Cannot get Azure subscription ID and thumbprint. Failed in New-FirewallRuleForWebsite in CarClassified-Sql.ps1"}

    [string]$queryString = "https://management.database.windows.net:8443/$subscriptionID/servers/$DatabaseServerName/firewallrules/$FirewallRuleName" + '?op=AutoDetectClientIP'
    [string]$contenttype = "application/xml;charset=utf-8"
    $headers = @{"x-ms-version" = "1.0"}

    Write-Verbose "Calling 'Set Firewall Rule' Azure SQL REST API"
    [xml]$responseXml = $(Invoke-RestMethod -Method POST -Uri $queryString -CertificateThumbprint $thumbprint -Headers $headers -ContentType $contenttype -Verbose).Remove(0,1)
    if(!$responseXml) {throw "Error: Initial firewall rule was not created. Failed in New-FirewallRuleForWebsite in CarClassified-Sql.ps1"}

    $rule = Get-AzureSqlDatabaseServerFirewallRule -ServerName $DatabaseServerName -RuleName $FirewallRuleName
    if(!$rule) {throw "Error: Cannot get initial firewall rule. Failed in New-FirewallRuleForWebsite in CarClassified-Sql.ps1"}

    #Create the start and end IP addresses by substituting 0 and 255
    $ipInRule = $rule.StartIpAddress
    $split = $ipInRule.split(".")
    $StartIP = ($split[0..2] -join "."), 0   -join "."
    $EndIP =   ($split[0..2] -join "."), 255 -join "."

    Write-Verbose "Editing firewall rule to add IP address range: $StartIP - $EndIP"
    $newrule = Set-AzureSqlDatabaseServerFirewallRule -ServerName $DatabaseServerName -RuleName $FirewallRuleName -StartIpAddress $StartIP -EndIpAddress $EndIP
    if (!$newrule) {throw "Error: Cannot add start and end IP addresses to website firewall rule. Failed in New-FirewallRuleForWebsite in CarClassified-Sql.ps1"}
    Write-Verbose "[finish] Created firewall rule $FirewallRuleName for IP Address $ipAddressinRule"

    return $newrule
}

# Create a PSCrendential object from plain text password.
# The PS Credential object will be used to create a database context, which will be used to create database.
Function New-PSCredentialFromPlainText
{
    Param(
        [String]$UserName,
        [String]$Password
    )

    $securePassword = ConvertTo-SecureString -String $Password -AsPlainText -Force

    Return New-Object System.Management.Automation.PSCredential($UserName, $securePassword)
}

# Generate connection string of a given SQL Azure database
Function Get-SQLAzureDatabaseConnectionString
{
    Param(
        [String]$DatabaseServerName,
        [String]$DatabaseName,
        [String]$UserName,
        [String]$Password
    )

    Return "Server=tcp:$DatabaseServerName.database.windows.net,1433;Database=$DatabaseName;User ID=$UserName@$DatabaseServerName;Password=$Password;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;"
}
$VerbosePreference = "Continue"
$ErrorActionPreference = "Stop"

Write-Verbose "[Start] creating SQL Azure database server in $Location location with username $UserName and password $Password"
$databaseServer = New-AzureSqlDatabaseServer -AdministratorLogin $UserName -AdministratorLoginPassword $Password -Location $Location
if (!$databaseServer) {throw "Did not create database server. Failure in New-AzureSqlDatabaseServer in CarClassified-Sql.ps1"}
$databaseServerName = $databaseServer.ServerName
Write-Verbose "[Finish] creating SQL Azure database server $databaseServerName in location $Location with username $UserName and password $Password"

# Create firewall rules.
If ($StartIPAddress -and $EndIPAddress)
{
    # Create a SQL Azure database server firewall rule for the IP address of the machine in which this script will run
    # This will also whitelist all the Azure IP so that the website can access the database server
    Write-Verbose "[Start] creating firewall rule $FirewallRuleName in database server $databaseServerName for IP addresses $StartIPAddress - $EndIPAddress"
    $rule1 = New-AzureSqlDatabaseServerFirewallRule -ServerName $databaseServer.ServerName -RuleName $FirewallRuleName -StartIpAddress $StartIPAddress -EndIpAddress $EndIPAddress -Verbose
}
else
{
    # Use the Azure REST API to create a firewall rule for the local machine range.
    $rule1 = New-FirewallRuleForWebsite -FirewallRuleName $FirewallRuleName -DatabaseServerName $databaseServerName
    $StartIPAddress = $rule1.StartIpAddress; $EndIPAddress = $rule1.EndIpAddress
}
if (!$rule1) {throw "Failed to create $FirewallRuleName. Failure in CarClassified-Sql.ps1"}
Write-Verbose "[Finish] creating $FirewallRuleName firewall rule in database server $databaseServerName for IP addresses $StartIPAddress - $EndIPAddress"

Write-Verbose "[Start] creating firewall rule AllowAllAzureIP in database server $databaseServerName for IP addresses 0.0.0.0 - 0.0.0.0"
$rule2 = New-AzureSqlDatabaseServerFirewallRule -AllowAllAzureServices -ServerName $databaseServerName -RuleName "AllowAllAzureIP" -Verbose
if (!$rule2) {throw "Failed to create AllowAllAzureIP firewall rule. Failure in CarClassified-Sql.ps1"}
Write-Verbose "[Finish] creating AllowAllAzureIP firewall rule in database server $databaseServerName for IP addresses 0.0.0.0 - 0.0.0.0"

# Create a database context which includes the server name and credential
# These are all local operations. No API call to Windows Azure
$credential = New-PSCredentialFromPlainText -UserName $UserName -Password $Password
if (!$credential) {throw "Failed to create secure credentials. Failure in New-PSCredentialFromPlainText function in CarClassified-Sql.ps1"}

$context = New-AzureSqlDatabaseServerContext -ServerName $databaseServerName -Credential $credential
if (!$context) {throw "Failed to create db server context for $databaseServerName. Failure in call to New-AzureSqlDatabaseServerContext in CarClassified-Sql.ps1"}

# Use the database context to create app database
Write-Verbose "[Start] creating database  $AppDatabaseName in database server $databaseServerName"
$CarClassifiedDb = New-AzureSqlDatabase -DatabaseName $AppDatabaseName -Context $context -Verbose
if (!$CarClassifiedDb) {throw "Failed to create $AppDatabaseName application database. Failure in New-AzureSqlDatabase in CarClassified-Sql.ps1"}
Write-Verbose "[Finish] creating database $AppDatabaseName in database server $databaseServerName"

Write-Verbose "Creating database connection string for $appDatabaseName in database server $databaseServerName"
$appDatabaseConnectionString = Get-SQLAzureDatabaseConnectionString -DatabaseServerName $databaseServerName -DatabaseName $AppDatabaseName -UserName $UserName -Password $Password
if (!$appDatabaseConnectionString) {throw "Failed to create application database connection string for $AppDatabaseName. Failure in Get-SQLAzureDatabaseConnectionString function in CarClassified-Sql.ps1"}

Write-Verbose "Creating hash table to return..."
Return @{ `
    Server = $databaseServerName; UserName = $UserName; Password = $Password; `
    AppDatabase = @{Name = $AppDatabaseName; ConnectionString = $appDatabaseConnectionString}; `
}
