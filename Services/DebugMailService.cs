using System;
using System.Diagnostics;

namespace WebApplication1.Services
{
    public class DebugMailService : IMailService
    {
        void IMailService.SendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending mail to: {to} from: {from} with the subject: {subject} and body: {body}");
        }
    }
}