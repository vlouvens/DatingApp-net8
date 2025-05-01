using API.DTOs;
using API.Entities;
namespace API.Interfaces
{
    public interface IUserRespository
    {
        Task<IEnumerable<MemberDto>>GetAllUserAsync();
        Task<MemberDto> GetByUsernameAsync(string username);
        Task<MemberDto> GetUserByIdAsync(int id);
       // Task<AppUser> GetUserByIdAsync(int id);
       // Task<AppUser> GetUserByUsernameAsync(string username);
       // Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<bool> SaveAllAsync();
        void Update(AppUser user);
    }
}