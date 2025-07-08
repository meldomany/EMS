using AutoMapper;
using EMS.DataAccess.Interfaces;
using EMS.Services.IServices;
using EMS.Shared.DTOs;

namespace EMS.Services
{
    public class LogHistoryService : ILogHistoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public LogHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<LogHistoryDto>> GetAllLogHistorysAsync()
        {
            var logHistories = await unitOfWork.LogHistories.GetAllAsync();
            return mapper.Map<IEnumerable<LogHistoryDto>>(logHistories);
        }

        public async Task<LogHistoryDto> GetLogHistoryByIdAsync(int id)
        {
            var logHistory = await unitOfWork.LogHistories.GetByIdAsync(id);
            if (logHistory != null)
            {
                return mapper.Map<LogHistoryDto>(logHistory);
            }
            return new LogHistoryDto();
        }
    }
}
