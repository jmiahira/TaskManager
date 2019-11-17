using System;

namespace TaskList.Api.Model
{
    public class TaskRemarks
    {
        public int TaskRemarksId { get; set; }
        public int TasksId { get; set; }
        public DateTime InsertDateTime { get; set; }
        public int InsertedUserId { get; set; }
        public string Remarks { get; set; }
    }
}