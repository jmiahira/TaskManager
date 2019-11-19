using TaskList.Domain;

namespace TaskList.Repository
{
    public class FilterTaskDTQ
    {
        private int? _taskId;
        private string _titleDescription;
        private StatusTaskType? _status { get; set; }
        private int _userId;
        private bool _includeRemarks = false;

        public int? TaskId { get; set; }
        public string TitleDescription { get; set; }
        public StatusTaskType? status { get; set; }
        public int userId { get; set; }
        public bool IncludeRemarks { get; set; }
    }
}