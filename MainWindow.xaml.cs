using ADO_EF.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ADO_EF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataContext dataContext;
        public ObservableCollection<Pair> Pairs { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            dataContext = new();
            Pairs = new();
            this.DataContext = this;
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

        private void Button1_Click(object sender, RoutedEventArgs e)
        {

            var query = dataContext.managers.Where(m => m.IdMainDep == Guid.Parse("131ef84b-f06e-494b-848f-bb4bc0604266"))
                .Select(
                m => new Pair
                {
                    Key = m.Surname,
                    Value = $"{m.Name[0]}. {m.Surname[0]}"
                }
                );
            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }

        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext.managers.Join(dataContext.Departments, m => m.IdMainDep, d => d.Id, (m, d) => new Pair
            { Key = m.Surname + " " + m.Name[0] + "." + m.Secname[0], Value = d.Name }).Take(10).Skip(3);
            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext
                .managers
                .Join(
                    dataContext.managers,
                    m => m.IdChief,
                    chief => chief.Id,
                    (m, chief) => new Pair
                    {
                        Key = m.Surname + " " + m.Name[0] + "." + m.Secname[0] + ".",
                        Value = chief.Surname + " " + chief.Name[0] + "." + chief.Secname[0] + "."
                    }
                )
                .ToList().OrderBy(pair => pair.Key);

            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button5_Click(object sender, RoutedEventArgs e)           // DZ 31.08.2023
        {
            var query = dataContext
                .managers
                .Join(
                    dataContext.Departments,
                    m => m.IdSecDep,
                    d => d.Id,
                    (m, d) => new Pair
                    {
                        Key = m.Surname + " " + m.Name[0] + "." + m.Secname[0] + ".",
                        Value = d.Name
                    }
                )
                .ToList().OrderBy(pair => pair.Value);

            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }
        private int _N;
        public int N { get => _N++; set => _N = value; }
        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            N = 1;
            var query = dataContext
                .Departments
                .OrderBy(d => d.Name)
                .Select(d=> new Pair()
                {
                    Key = (N).ToString(),
                    Value = d.Name
                })
                ;
            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            N = 1;
            var query = dataContext
                .Departments
                .OrderBy(d => d.Name)
                .AsEnumerable()
                .Select(d => new Pair()
                {
                    Key = (N).ToString(),
                    Value = d.Name
                })
                ;
            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext
                .Departments
                .GroupJoin(
                    dataContext.managers,
                    d => d.Id,
                    m => m.IdMainDep,
                    (dep, mans) => new Pair
                    {
                        Key = dep.Name,
                        Value = mans.Count().ToString()
                    }
                );
            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext
                .managers
                .GroupJoin(
                    dataContext.managers,
                    d => d.Id,
                    m => m.IdChief,
                    (dep, mans) => new Pair
                    {
                        Key = dep.Surname + " " + dep.Name[0] + ". " + dep.Secname[0] + ".",
                        Value = mans.Count().ToString()
                    }
                ).Where(m => m.Value != "0");
            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }
        private void Button10_Click(object sender, RoutedEventArgs e)
        {
            var query = dataContext.managers
                .GroupBy(m => m.Surname)
                .Select(group => new Pair
                {
                    Key = group.Key,
                    Value = group.Count().ToString()
                }).Where(p => Convert.ToInt32(p.Value) > 1);

            Pairs.Clear();
            foreach (var pair in query)
            {
                Pairs.Add(pair);
            }
        }
    }
    public class Pair
    {
        public String Key { get; set; } = null!;
        public String? Value { get; set; }
    }
}
