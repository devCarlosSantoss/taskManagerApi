namespace taskManagerApi.Dtos
{
    public record UpdateTaskDto(string Description, Models.TaskStatus Status)
    {
        
    }
}