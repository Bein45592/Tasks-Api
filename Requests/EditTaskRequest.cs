using WebApplication3.Enums;

namespace WebApplication3.Requests
{
    public class EditTaskRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public EPriority? Priority { get; set; }
        public DateTime? DeadLine { get; set; }
        public EStatus? Status { get; set; }
    }
}
