using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.ViewModels;

public class UpdateTaskVm
{
    public required int TaskId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TaskName { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TaskDescription { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TaskEntityStatus? TaskStatus { get; set; }
}
