using Domain.Enums;

namespace Application.ViewModels;

public class CreateTaskVm
{
    public required string TaskName { get; set; }
    public required string TaskDescription { get; set; }
    public required TaskEntityStatus TaskStatus { get; set; }
}
