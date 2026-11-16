using Interpolation_WPF.ViewModels;
using System.Collections.Specialized;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Interpolation_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainViewModel mainViewModel { get; set; }
        public MainWindow()
        {
            mainViewModel = new MainViewModel();
            InitializeComponent();
            DataContext = mainViewModel;

            mainViewModel.Points.CollectionChanged += Points_CollectionChanged;
        }
        private void Points_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateColumns();
        }

        private void UpdateColumns()
        {
            PointsGrid.Columns.Clear();

            PointsGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "X",
                Binding = new Binding("X")
            });

            int maxCount = mainViewModel.Points.Count == 0
                ? 0
                : mainViewModel.Points.Max(p => p.Derivatives.Count);

            for (int i = 0; i < maxCount; i++)
            {
                PointsGrid.Columns.Add(new DataGridTextColumn
                {
                    Header = i == 0
                        ? "f(x)"
                        : $"f{new string('\'', i)}(x)",

                    Binding = new Binding($"Derivatives[{i}].Value")
                });
            }
        }
    }
}