namespace TaskManager.Application.DTOs;

using TaskManager.Core.Enums;

public record WorkItemDto(
    int Id,
    string Title,
    string Description,
    WorkItemStatus Status,
    int? AssignedUserId,
    DateTime CreatedAt,
    DateTime? UpdatedAt); 