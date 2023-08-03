using System.Threading.Tasks;

namespace Zero.Abp.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}