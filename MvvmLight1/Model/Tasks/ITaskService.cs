using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PixelloToDo.Model.Tasks
{
    public interface ITaskService
    {
        IEnumerable<TaskItem> GetTasksCollection();
        Task SaveTaskToFile(TaskItem taskItem);
        Task<ObservableCollection<TaskItem>> GetTasksObservableCollectionAsync();
        void DeleteTask(string accessName);
        void ModifyCompletedProperty(TaskItem taskItem);
    }
}