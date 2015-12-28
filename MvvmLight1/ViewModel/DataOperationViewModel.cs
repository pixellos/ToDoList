using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MvvmLight1.Model.Tasks;
using PixelloTools.Logging;

namespace MvvmLight1.ViewModel
{
    public class DataOperationViewModel : ViewModelBase
    {
        private Ilogger _logger;
        private readonly ITaskService _taskService;
        public DataOperationViewModel(ITaskService taskService,Ilogger logger)
        {
            _logger = logger;
            _taskService = taskService;
            try
            {
                TaskItems = new ObservableCollection<TaskItem>(_taskService.GetTasksCollection());
            }
            catch (Exception ex)
            {
                _logger.LogIt(ex.Message);
            }

            TaskAddCommand = new RelayCommand(SaveTask);
            DeleteCommand = new RelayCommand<object>(DeleteTask);
            ModifyCommand = new RelayCommand<object>(ModifyTask);
            _logger.LogIt("DataOperationViewModel initialized");

        }
        public RelayCommand<object> DeleteCommand { get; set; }
        public RelayCommand TaskAddCommand { get; set; }
        public RelayCommand<object> ModifyCommand { get; set; }

        private void ModifyTask(object TaskItem)
        {
            _taskService.ModifyCompletedProperty(TaskItem as TaskItem);
        }

        private async void DeleteTask(object accessName)
        {
            try
            {
                _taskService.DeleteTask(accessName as string);
            }
            catch (Exception ex)
            {
                _logger.LogIt(ex.Message + ex.Data);
                throw;
            }

            TaskItems = await _taskService.GetTasksObservableCollectionAsync();
        }

        private async void SaveTask()
        {
            try
            {
                TaskItem item = new TaskItem()
                {
                    IsCompleted = IsCompleted,
                    Name = TaskName,
                    Tags = Tags,
                    TaskPirority = TaskPirority
                };

                await Task.WhenAll(_taskService.SaveTaskToFile(item)); //Solution to file read lock during _taskService.GetTasksObservableCollectionAsync()
                TaskItems = await _taskService.GetTasksObservableCollectionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogIt(System.Reflection.MethodBase.GetCurrentMethod().Name + ex.Message);
            }

            _logger.LogIt("Saved and Loaded");
        }

        private ObservableCollection<TaskItem> _taskItems;
        public ObservableCollection<TaskItem> TaskItems
        {
            get { return _taskItems; }
            set { Set(ref _taskItems, value); }
        }

        private bool _isCompleted;
        public bool IsCompleted
        {
            get	{ return _isCompleted;	}
            set	{ Set(ref _isCompleted, value);	}
        }

        private TaskPirority _taskPirority;
        public TaskPirority TaskPirority
        {
            get	{		return _taskPirority;	}
            set	{		Set(ref _taskPirority, value);	}
        }

        private string _tags;
        public string Tags
        {
            get	{ return _tags;	}
            set	{ Set(ref _tags, value); }
        }

        private string _taskName;
        public string TaskName
        {
            get { return _taskName; }
            set { Set(ref _taskName, value); }
        }
    }
}