using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassified.Web.Utilities.Interfaces
{
    public interface IVeryBasicEmail
    {
        void SendEmail(string emailAddress, string url);
    }
}