using System;
using System.Windows;
using MvvmLight1.ViewModel;

namespace MvvmLight1
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