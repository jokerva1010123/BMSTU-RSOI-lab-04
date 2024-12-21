using ModelDTO.Bonus;

namespace Gateway.IServices
{
    public interface IPrivilegeServices
    {
        Task<PrivilegeDTO> GetByUsername(string username);
        Task UpdatePrivilege(int id, PrivilegeDTO privilege);
    }
}
