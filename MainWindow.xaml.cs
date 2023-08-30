using ADO_EF.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADO_EF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataContext dataContext;

        public MainWindow()
        {
            InitializeComponent();
            dataContext = new();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DepartmentsCountLabel.Content = dataContext.Departments.Count().ToString();
            TopChiefCountLabel.Content = dataContext.managers.Where(manager => manager.IdChief == null).Count().ToString();
            ChiefCountLabel.Content = dataContext.managers.Where(manager => manager.IdChief != null).Count().ToString();
            MainDepCountLabel.Content = dataContext.managers.Where(manager => manager.IdMainDep != null).Count().ToString();
            SecondDepCountLabel.Content = dataContext.managers.Where(manager => manager.IdSecDep != null).Count().ToString();
            MainAndSecondDepCountLabel.Content = dataContext.managers.Where(manager => manager.IdMainDep != null && manager.IdSecDep != null).Count().ToString();
        }
    }
}
