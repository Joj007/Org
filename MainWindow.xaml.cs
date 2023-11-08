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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Organization> organizations = new List<Organization>();
        List<Organization> filteredList;


        public MainWindow()
        {
            
            InitializeComponent();

            foreach (string line in File.ReadAllLines("organizations-100.csv").Skip(1)) {
                organizations.Add(new Organization(line.Split(';')));
            }
            dgAdatok.ItemsSource = organizations;

            cbOrszag.Items.Add("Minden ország");
            organizations.Select(n => n.Country).Distinct().OrderBy(n=>n).ToList().ForEach(n=> cbOrszag.Items.Add(n));
            cbOrszag.SelectedIndex = 0;

            cbEv.Items.Add("Minden évszám");
            organizations.Select(n => n.Founded).Distinct().OrderBy(n => n).ToList().ForEach(n => cbEv.Items.Add(n));
            cbEv.SelectedIndex = 0;

        }


        private void dgAdatok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgAdatok.SelectedItem is Organization)
            {
                
                Organization kivalasztott = dgAdatok.SelectedItem as Organization;
                lblId.Content = kivalasztott.Id;
                lblWeb.Content = kivalasztott.Website;
                lblIsm.Content = kivalasztott.Description;
            }


            
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filteredList = organizations;
            
            filteredList = evszures(filteredList);
            filteredList = orszagszures(filteredList);
    
            
            lblLetszam.Content = filteredList.Sum(n=>n.EmployeesNumber);
        }

        private List<Organization> orszagszures(List<Organization> szurendoLista)
        {
            return (string)cbOrszag.SelectedItem != "Minden ország" ? szurendoLista.Where(n => n.Country == (string)cbOrszag.SelectedItem).ToList() : szurendoLista;
        }
        private List<Organization> evszures(List<Organization> szurendoLista)
        {
            
            return (string)cbEv.Text != "Minden évszám" ? szurendoLista.Where(n => n.FoundedString == cbEv.Text).ToList() : szurendoLista;
        }

    }
}
