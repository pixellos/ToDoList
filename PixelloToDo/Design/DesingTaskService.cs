using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using PixelloToDo.Model.Tasks;
using PixelloTools.Logging;
using PixelloTools.XmlObject;

namespace PixelloToDo.Design
{
    public class DesingTaskService : ObjectSerializer<TaskItem>, ITaskService
    {
        public DesingTaskService(Ilogger ilogger) : base(ilogger){}

        public Task SaveTaskToFile(TaskItem taskItem)
        {
            MessageBox.Show("Saving...");
            return Task.Delay(0);
        }

        public Task<ObservableCollection<TaskItem>> GetTasksObservableCollectionAsync()
        {
            return  new Task<ObservableCollection<TaskItem>>(() =>
            {
                return new ObservableCollection<TaskItem>(GetTasksCollection());
            });

        }

        public void DeleteTask(string accessName)
        {
            MessageBox.Show("Deleting...");
        }

        public void ModifyCompletedProperty(TaskItem taskItem)
        {
            MessageBox.Show("Modifying");
        }

        public void OpenFile(string AccessName)
        {
            MessageBox.Show("Opening..");
        }

        public IEnumerable<TaskItem> GetTasksCollection()
        {
            return new ObservableCollection<TaskItem>(new List<TaskItem>()
                {
                    new TaskItem() {TaskPirority = TaskPirority.Average,Name = "it's Name",Tags = "There are tags", IsCompleted =true,AccessName = "ThereIsNot"}
                });
        }
    }
}