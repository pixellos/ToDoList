using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MvvmLight1.Model.Tasks;
using MvvmLight1.Model.UIContent;
using PixelloTools.Logging;

namespace MvvmLight1.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<Ilogger>(ConsoleLogger.GetLoger);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<ITaskService,DesingTaskService>();
                SimpleIoc.Default.Register<IUIContentService, DesingUIContentService>();
            }
            else
            {
                SimpleIoc.Default.Register<ITaskService, TaskService>();
                SimpleIoc.Default.Register<IUIContentService, UIContentService>();
            }
            
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DataOperationViewModel>();
            SimpleIoc.Default.Register<InterfaceDataViewModel>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]
        public InterfaceDataViewModel InterfaceDataViewModel => ServiceLocator.Current.GetInstance<InterfaceDataViewModel>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public DataOperationViewModel DataOperationViewModel => ServiceLocator.Current.GetInstance<DataOperationViewModel>();


        public static void Cleanup(){}
    }
}