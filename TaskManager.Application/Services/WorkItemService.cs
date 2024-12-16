using AutoMapper;
using TaskManager.Application.DTOs;
using TaskManager.Core.Entities;
using TaskManager.Core.Enums;

namespace TaskManager.Application.Services
{
    public class WorkItemService : IWorkItemService
    {
        private readonly IWorkItemRepository _workItemRepository;
        private readonly IMapper _mapper;

        public WorkItemService(IWorkItemRepository workItemRepository, IMapper mapper)
        {
            _workItemRepository = workItemRepository;
            _mapper = mapper;
        }

        public async Task<WorkItemDto> GetByIdAsync(int id)
        {
            var workItem = await _workItemRepository.GetByIdAsync(id);
            if (workItem == null)
                throw new NotFoundException($"WorkItem with id {id} not found");
            
            return _mapper.Map<WorkItemDto>(workItem);
        }

        public async Task<IEnumerable<WorkItemDto>> GetAllAsync()
        {
            var workItems = await _workItemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<WorkItemDto>>(workItems);
        }

        public async Task<IEnumerable<WorkItemDto>> GetByStatusAsync(WorkItemStatus status)
        {
            var workItems = await _workItemRepository.GetByStatusAsync(status);
            return _mapper.Map<IEnumerable<WorkItemDto>>(workItems);
        }

        public async Task<WorkItemDto> CreateAsync(WorkItemDto workItemDto)
        {
            var workItem = _mapper.Map<WorkItem>(workItemDto);
            workItem.CreatedAt = DateTime.UtcNow;
            
            var result = await _workItemRepository.AddAsync(workItem);
            return _mapper.Map<WorkItemDto>(result);
        }

        public async Task UpdateAsync(int id, WorkItemDto workItemDto)
        {
            var existingWorkItem = await _workItemRepository.GetByIdAsync(id);
            if (existingWorkItem == null)
                throw new NotFoundException($"WorkItem with id {id} not found");

            _mapper.Map(workItemDto, existingWorkItem);
            existingWorkItem.UpdatedAt = DateTime.UtcNow;
            
            await _workItemRepository.UpdateAsync(existingWorkItem);
        }

        public async Task DeleteAsync(int id)
        {
            await _workItemRepository.DeleteAsync(id);
        }
    }
} 