using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskList.Domain;

namespace TaskList.Repository
{
    public class TaskListRepository : ITaskListRepository
    {
        private readonly TaskListContext _context;

        public TaskListRepository(TaskListContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        #region Events
        public async Task<Tasks[]> GetAllTasksAsync(int userId, StatusTaskType status, bool includeRemarks = false)
        {
            IQueryable<Tasks> query = _context.Tasks;
            if (includeRemarks) {
                query = query
                    .Include(r => r.TaskRemarks);
            }
            query = query.OrderBy(t => t.CreationDate)
                .Where(t => t.UserId == userId && t.Status == status);
            return await query.ToArrayAsync();
        }

        public async Task<Tasks[]> GetAllTasksAsyncByTitle(int userId, StatusTaskType status, string title, bool includeRemarks = false)
        {
            IQueryable<Tasks> query = _context.Tasks;
            if (includeRemarks) {
                query = query
                    .Include( r => r.TaskRemarks );
            }
            query = query.OrderBy( t => t.CreationDate )
                .Where(t => t.UserId == userId 
                    && t.Status == status
                    && ( t.Title.Contains(title) || t.Description.Contains(title)) );
            return await query.ToArrayAsync();
        }

        public async Task<Tasks> GetTaskAsyncById(int TaskId, bool includeRemarks = false)
        {
            IQueryable<Tasks> query = _context.Tasks;
            if (includeRemarks) {
                query = query
                    .Include( r => r.TaskRemarks );
            }
            query = query.OrderBy( t => t.CreationDate )
                .Where(t => t.Id == TaskId );
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Tasks[]> Get(FilterTaskDTQ filterTasksQuery)
        {
            IQueryable<Tasks> query = _context.Tasks;
            if (filterTasksQuery.IncludeRemarks) {
                query = query
                    .Include(r => r.TaskRemarks);
            }
            query = query.OrderBy(t => t.CreationDate)
                .Where(t => t.UserId == filterTasksQuery.userId);
            if (filterTasksQuery.TaskId != null)
            {
                query = query.Where(t => t.Id == filterTasksQuery.TaskId);
            }
            else 
            {
                if (filterTasksQuery.status != null)
                    query = query.Where(t => t.Status == filterTasksQuery.status);
                if (!string.IsNullOrEmpty(filterTasksQuery.TitleDescription))
                    query = query.Where(t => t.Title.Contains(filterTasksQuery.TitleDescription) || t.Description.Contains(filterTasksQuery.TitleDescription));
            }

            return await query.ToArrayAsync();
        }
        #endregion
    }
}