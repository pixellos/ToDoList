using System;
using System.Windows;
using System.Windows.Input;
using PixelloToDo.ViewModel;

namespace PixelloToDo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            this.Topmost = true;
        }

    }
}