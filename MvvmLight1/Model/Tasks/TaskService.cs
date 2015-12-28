using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PixelloTools.Logging;
using PixelloTools.XmlObject;

namespace PixelloToDo.Model.Tasks
{
    public class TaskService : ObjectSerializer<TaskItem>, ITaskService
    {
        public TaskService(Ilogger ilogger) : base(ilogger)
        {
            PathBuilder = new PathBuilder("Tasks", "FileName", ".xml"); //Default values for this serialization, 
        }

        public async Task SaveTaskToFile(TaskItem taskItem)
        {
            string filename = GetCurrentDateTimeFormatedToSave();
            taskItem.AccessName = filename;
            await SaveSingleDataAsync(taskItem, filename);
        }

        private static string GetCurrentDateTimeFormatedToSave() => DateTime.Now.ToString("s").Replace(':', '_');

        public async Task<ObservableCollection<TaskItem>> GetTasksObservableCollectionAsync()
        {
            var task = new Task<ObservableCollection<TaskItem>>(
                () => new ObservableCollection<TaskItem>(GetTasksCollection()));
            task.Start();
            return await task;
        }

        public void DeleteTask(string accessName)
        {
            DeleteData(accessName);
        }

        public async void ModifyCompletedProperty(TaskItem taskItem)
        {
            await SaveSingleDataAsync(taskItem, taskItem.AccessName);
        }

        public IEnumerable<TaskItem> GetTasksCollection()
        {
            return GetEnumerableData();
        }
    }
}