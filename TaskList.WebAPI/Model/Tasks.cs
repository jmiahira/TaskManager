using System;
using TaskList.Api.DTO;

namespace TaskList.Api.Model
{
    public class Tasks
    {
        public int TasksId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public StatusType Status { get; set; }
        public int AssignedUserId { get; set; }
        public PriorityType Priority { get; set; }
        public string LastUpdateDateTime { get; set; }
    }
}