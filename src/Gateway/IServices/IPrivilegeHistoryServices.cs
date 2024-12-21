using ModelDTO.Bonus;

namespace Gateway.IServices
{
    public interface IPrivilegeHistoryServices
    {
        Task<List<PrivilegeHistoryDTO>> GetAll(int page, int size);
        Task AddNew(PrivilegeHistoryDTO history);
    }
}
