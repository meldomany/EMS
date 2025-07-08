using EMS.Shared.DTOs;

namespace EMS.Services.IServices
{
    public interface ILogHistoryService
    {
        Task<IEnumerable<LogHistoryDto>> GetAllLogHistorysAsync();
        Task<LogHistoryDto> GetLogHistoryByIdAsync(int id);
    }
}
