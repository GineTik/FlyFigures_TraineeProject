using System;
using System.Windows;
using FlyFiguresTraineeProject.ViewModels;

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