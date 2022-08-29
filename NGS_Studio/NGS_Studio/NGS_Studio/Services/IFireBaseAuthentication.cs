using System.Threading.Tasks;

namespace NGS_Studio.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFireBaseAuthentication
    {
        bool IsSignIn();
        Task<bool> CreateUser(string username, string email, string password);
        void SignOut();
        Task<string> SignIn(string email, string password);
        Task<string> GetToken();
        Task ResetPassword(string email);
    }
}
