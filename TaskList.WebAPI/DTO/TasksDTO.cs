using System.Collections.Generic;

namespace TaskList.WebAPI.DTO
{
    public class TasksDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public string Priority { get; set; }
        public string LastUpdateDateTime { get; set; }
        public List<TaskRemarksDTO> TaskRemarks { get; }
    }
}