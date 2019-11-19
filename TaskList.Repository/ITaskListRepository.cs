using System.Threading.Tasks;
using TaskList.Domain;

namespace TaskList.Repository
{
    public interface ITaskListRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task<bool> SaveChangesAsync();

        //EVENTOS
        Task<Tasks[]> GetAllTasksAsyncByTitle(int userId, StatusTaskType status, string title, bool includeRemarks = false);
        Task<Tasks[]> GetAllTasksAsync(int userId, StatusTaskType status, bool includeRemarks = false);
        Task<Tasks> GetTaskAsyncById(int taskId, bool includeRemarks = false);
        Task<Tasks[]> Get(FilterTaskDTQ filterTasksQuery);
    }
}