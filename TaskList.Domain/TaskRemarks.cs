using System;

namespace TaskList.Domain
{
    public class TaskRemarks
    {
        public int Id { get; set; }
        public int TasksId { get; set; }
        public DateTime InsertDateTime { get; set; }
        public string Remarks { get; set; }
        public Tasks Tasks { get; }
    }
}