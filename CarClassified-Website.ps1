[CmdletBinding(PositionalBinding=$True)]

Param(
	[Parameter(Mandatory =$true)]
	[ValidatePattern("^[a-z0-9]*$")]
	[String]$Name,
	[String]$Location= "West US",
	[String]$SqlDatabaseUserName="dbuser",
	[String]$SqlDatabasePassword	
)

function Verify-Subscription ()
{
    $sub = Get-AzureSubscription
    If(!$sub.SubscriptionId )
    {
        {Throw "You do not have a subscription or your account information has expired"}
    }

    Select-AzureSubscription  -SubscriptionName $sub.SubscriptionName

    $website = Get-AzureWebsite -Name $Name

    if($website)
    {
        Write-Verbose ("Deleting website : {0} and recreating {0}" -f $website.Name) 
        New-AzureWebsite -Name $Name -Location $Location
    }else
    {
        Write-Verbose ("Creating your new website: {0}" -f $Name)
        New-AzureWebsite -Name $Name -Location $Location
    }


}


Write-Verbose "Verifying subscription and website"
Verify-Subscription