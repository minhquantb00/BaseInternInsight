using BaseInsightDotNet.Business.Payloads.RequestModels;
using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseInsightDotNet.Business.InterfaceServices
{
    public interface IEmailService
    {
        string SendEmail(Request_Message message);
    }
}
