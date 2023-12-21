using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using FlyFiguresTraineeProject.ViewModels;
using WPFLocalizeExtension.Engine;
using WPFLocalizeExtension.Providers;

namespace FlyFiguresTraineeProject.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}