using System;
using System.Collections.Generic;

namespace TaskList.Domain
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public StatusTaskType Status { get; set; }
        public int UserId { get; set; }
        public PriorityTaskType Priority { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public List<TaskRemarks> TaskRemarks { get; }
    }
}