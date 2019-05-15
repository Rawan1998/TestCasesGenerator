using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;

namespace TestCasesGenerator.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            this.DataContext = new MainWindowViewModel();
            InitializeComponent();
        }

        private void FileUploadDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string filePath = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                (this.DataContext as MainWindowViewModel).GenerateTests(filePath);
            }
        }

        private void FileUploadClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                (this.DataContext as MainWindowViewModel).GenerateTests(filePath);
            }
        }

        private void CustomTest(object sender, RoutedEventArgs e)
        {

        }
    }
}
