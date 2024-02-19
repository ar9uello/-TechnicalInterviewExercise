using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos;

public class TaskEntityDto
{
    [Key]
    public int TaskId {  get; set; }
    public required string TaskName { get; set; }
    public required string TaskDescription { get; set; }
    public required TaskEntityStatus TaskStatus { get; set; }
}
