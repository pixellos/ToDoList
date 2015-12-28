using System.Collections.ObjectModel;
using System.Threading;
using GalaSoft.MvvmLight;
using MvvmLight1.Model.UIContent;
using PixelloTools.Logging;

namespace MvvmLight1.ViewModel
{
    public class InterfaceDataViewModel : ViewModelBase
    {
        private IUIContentService _uiContentService;
        public InterfaceDataViewModel(Ilogger logger, IUIContentService uiContentService)
        {
            _uiContentService = uiContentService;
            SetupDataFromUiContentService();
        }

        private void SetupDataFromUiContentService()
        {
            _uiContentService.GetData(
                (item, error) =>
                {
                    if (error != null) { return; }
                    TaskName = item.TaskNameDescription;
                    TaskTags = item.TaskTagsDescription;
                    Pirority = new ObservableCollection<string>(item.TaskPirorityList);
                    AddButtonName = item.TaskAddButtonName;

                    ListTagsDescription = item.ListTagsDescription;
                    ListTaskPirorityDescription = item.ListTaskPirorityDescription;
                    ListIsDoneDescription = item.ListIsCompletedDescription;
                    ListNameDescription = item.ListNameDescription;
                });
        }
#region properties
        private ObservableCollection<string> _pirority;
        public ObservableCollection<string> Pirority
        {
            get { return _pirority; }
            set { Set(ref _pirority, value); }
        }

        private string _taskName = string.Empty;
        public string TaskName
        {
            get { return _taskName; }
            set { Set(ref _taskName, value); }
        }

        private string _taskTags = string.Empty;
        public string TaskTags
        {
            get { return _taskTags; }
            set { Set(ref _taskTags, value); }
        }

        private string _addButtonName;
        public string AddButtonName
        {
            get { return _addButtonName; }
            set { Set(ref _addButtonName, value); }
        }

        private string _listNameDescription;
        public string ListNameDescription
        {
            get	{	return _listNameDescription; }
            set	{	Set(ref _listNameDescription, value); }
        }

        private string _listTagsDescription;
        public string ListTagsDescription
        {
            get	{	return _listTagsDescription; }
            set	{	Set(ref _listTagsDescription, value); }
        }

        private string _listTaskPirorityDescription;
        public string ListTaskPirorityDescription
        {
            get	{	return _listTaskPirorityDescription; }
            set	{	Set(ref _listTaskPirorityDescription, value); }
        }

        private string _listIsDoneDescription;
        public string ListIsDoneDescription
        {
            get	{	return _listIsDoneDescription; }
            set	{	Set(ref _listIsDoneDescription, value); }
        }
#endregion
    }
}