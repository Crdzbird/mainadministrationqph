using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IUserService
    {
        PagedList<User> GetUsers(UserQueryFilter filters);
        Task<User> GetUser(int id);
        Task<User> InsertUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(int id);
        Task<bool> ActivateUserAccount(string code);
        Task<bool> CheckActivationCode(string code);
        void SendMail(string code, string destination, string message);
        string GenerateActivationCode(int length = 8, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~[!]+-");
    }
}