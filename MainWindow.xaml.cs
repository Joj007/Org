using System;
using System.Collections.Generic;
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
using System.IO;

namespace orgWPF
{
    ///<summary>
    /// Interaction logic for MainWindow.xaml
    ///</summary>
    public partial class MainWindow : Window
    {
        List<Organization> organizations = new List<Organization>();


        public MainWindow()
        {

            InitializeComponent();

            foreach (string line in File.ReadAllLines("organizations-100.csv").Skip(1))
            {
                organizations.Add(new Organization(line.Split(';')));
            }
            dgAdatok.ItemsSource = organizations;

            cbOrszag.Items.Add("Minden ország");
            organizations.Select(n => n.Country).Distinct().OrderBy(n => n).ToList().ForEach(n => cbOrszag.Items.Add(n));

            cbEv.Items.Add("Minden évszám");
            organizations.Select(n => n.Founded).Distinct().OrderBy(n => n).ToList().ForEach(n => cbEv.Items.Add(n));

            cbOrszag.SelectedIndex = 0;
            cbEv.SelectedIndex = 0;

        }


        private void dgAdatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAdatok.SelectedItem is Organization)
            {

                Organization kivalasztott = dgAdatok.SelectedItem as Organization;
                lblId.Content = kivalasztott.Id;
                lblWeb.Content = kivalasztott.Website;
                lblIsm.Content = kivalasztott.Description;
            }



        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Organization> filteredList = organizations;
            filteredList = evszures(filteredList);
            filteredList = orszagszures(filteredList);
            dgAdatok.ItemsSource = filteredList;

            lblLetszam.Content = filteredList.Sum(n => n.EmployeesNumber);
        }

        private List<Organization> orszagszures(List<Organization> szurendoLista)
        {
            if (cbOrszag.SelectedItem == null || cbOrszag.SelectedItem.ToString() == "Minden ország")
            {
                return szurendoLista;
            }
            else
            {
                return szurendoLista.Where(n => n.Country == cbOrszag.SelectedItem.ToString()).ToList();
            }
        }
        private List<Organization> evszures(List<Organization> szurendoLista)
        {
            if (cbEv.SelectedItem==null || cbEv.SelectedItem.ToString() == "Minden évszám")
            {
                return szurendoLista;
            }
            else
            {
                return szurendoLista.Where(n => n.FoundedString == cbEv.SelectedItem.ToString()).ToList();
            }
            
        }

    }
}

