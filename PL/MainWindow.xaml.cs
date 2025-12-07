using DalFacade;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            orders_ListBox.ItemsSource = Init.GetOrders();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Wow");
        }

        private void RightButton_Click(object sender, MouseButtonEventArgs e) =>
            MessageBox.Show("All Right");

        private void orders_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) =>
            new CouriersListWindow().ShowDialog();
         // new CourierWindow(103).ShowDialog();
    }
}