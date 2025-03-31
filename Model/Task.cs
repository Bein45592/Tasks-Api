using WebApplication3.Enums;

namespace TasksApi.Model
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public EPriority Priority { get; set; }
        public DateTime DeadLine { get; set; }
        public EStatus Status { get; set; }
    }
}
