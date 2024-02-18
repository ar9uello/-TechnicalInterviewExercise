using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class TaskEntity
{
    [Key]
    public int TaskId { get; set; }
    public required string TaskName { get; set; }
    public required string TaskDescription { get; set; }
    public required TaskEntityStatus TaskStatus { get; set; }
}
