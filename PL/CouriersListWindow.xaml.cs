using DalFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for CouriersListWindow.xaml
    /// </summary>
    public partial class CouriersListWindow : Window
    {
        public CouriersListWindow()
        {
            InitializeComponent();
            DataContext = Init.GetCouriers(); //BL.Factory.Get().Courier().GetAll();
        }

        private void Couriers_ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            new CourierWindow((e.AddedItems[0] as DO.Courier).Id).ShowDialog();
        //new CourierWindow(((DO.Courier)Couriers_ListView.SelectedItem).Id).ShowDialog();
        // new CourierWindow().ShowDialog();
    }
}
