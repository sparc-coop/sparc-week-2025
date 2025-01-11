namespace Sparc.Blossom.Template
{
    public class ToDos(BlossomAggregateOptions<ToDo> options) : BlossomAggregate<ToDo>(options)
    {
        public BlossomQuery<ToDo> AllTodosForUser(string userId)
            => Query().Where(x => x.UserId == userId);

        public BlossomQuery<ToDo> AllMedTodosForUser(string userId)
        {
            var user = Query().Where(x => x.UserId == userId);
            return user?.Where(x => x.Type == TodoTypes.Medication);
        }

        public BlossomQuery<ToDo> AllApptTodosForUser(string userId)
        {
            var user = Query().Where(x => x.UserId == userId);
            return user?.Where(x => x.Type == TodoTypes.Appointment);
        }

        public BlossomQuery<ToDo> AllMiscTodosForUser(string userId)
        {
            var user = Query().Where(x => x.UserId == userId);
            return user?.Where(x => x.Type == TodoTypes.Misc);
        }

        public BlossomQuery<ToDo> AllUpcomingTodosForUser(string userId)
        {
            var user = Query().Where(x => x.UserId == userId);
            return user?.Where(x => x.Status == TodoStatus.Upcoming);
        }

        public BlossomQuery<ToDo> AllPendingTodosForUser(string userId)
        {
            var user = Query().Where(x => x.UserId == userId);
            return user?.Where(x => x.Status == TodoStatus.Pending);
        }

        public BlossomQuery<ToDo> AllCompletedTodosForUser(string userId)
        {
            var user = Query().Where(x => x.UserId == userId);
            return user?.Where(x => x.Status == TodoStatus.Completed);
        }

        public BlossomQuery<ToDo> AllOverdueTodosForUser(string userId)
        {
            var user = Query().Where(x => x.UserId == userId);
            return user?.Where(x => x.Status == TodoStatus.Overdue);
        }
    }
}