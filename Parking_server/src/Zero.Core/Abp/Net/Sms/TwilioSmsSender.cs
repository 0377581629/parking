using System.Threading.Tasks;
using Abp.Dependency;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Zero.Net.Sms;

namespace Zero.Abp.Net.Sms
{
    public class TwilioSmsSender : ISmsSender, ITransientDependency
    {
        private TwilioSmsSenderConfiguration _twilioSmsSenderConfiguration;
        
        public TwilioSmsSender(TwilioSmsSenderConfiguration twilioSmsSenderConfiguration)
        {
            _twilioSmsSenderConfiguration = twilioSmsSenderConfiguration;
        }

        public async Task SendAsync(string number, string message)
        {
            TwilioClient.Init(_twilioSmsSenderConfiguration.AccountSid, _twilioSmsSenderConfiguration.AuthToken);
            
            var messageOptions = new CreateMessageOptions(
                new PhoneNumber(number))
            {
                From = new PhoneNumber(_twilioSmsSenderConfiguration.SenderNumber),
                Body = message
            };

            await MessageResource.CreateAsync(messageOptions);
        }
    }
}
