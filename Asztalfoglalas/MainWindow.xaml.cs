using Microsoft.EntityFrameworkCore;
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

namespace Asztalfoglalas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AsztalfoglalasContext context = new AsztalfoglalasContext();
        public MainWindow()
        {
            InitializeComponent();
            SetStatus(true);
            context.Foglalas.Load();
            context.Asztal.Load();

            listaDG.ItemsSource = context.Foglalas.Local.ToObservableCollection();
            asztalCb.ItemsSource = context.Asztal.Local.ToObservableCollection();
            asztalCb.DisplayMemberPath = "Megnevezes";
        }

        private void SetStatus(bool listazas)
        {
            listaDG.IsEnabled = gombokSp.IsEnabled = listazas;
            adatokGrid.IsEnabled = !listazas;
        }

        private void ujBtn_Click(object sender, RoutedEventArgs e)
        {
            SetStatus(false);
            adatokGrid.DataContext = new Foglalas()
            {
                Datum = DateTime.Today
            };
        }

        private void modositasBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listaDG.SelectedItem != null)
            {
                SetStatus(false);
            }
        }

        private void torlesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listaDG.SelectedItem != null)
            {
                if (MessageBox.Show("Biztosan tötölni akarja a foglalást?", "Megerősítés", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Foglalas f = (Foglalas)listaDG.SelectedItem;
                    context.Foglalas.Remove(f);
                    context.SaveChanges();
                }
            }
        }

        private bool KotelezoE(Foglalas f)
        {
            if (String.IsNullOrWhiteSpace(f.Nev))
            {
                nevTb.Focus();
                return false;
            }
            if (f.Asztal == null)
            {
                asztalCb.IsDropDownOpen = true;
                return false;
            }
            if (datumDp.SelectedDate == null)
            {
                datumDp.Focus();
                return false;
            }
            if (!(int.TryParse(letszamTb.Text, out int letszam) && letszam > 0 && f.Asztal.Ferohely >= letszam))
            {
                letszamTb.Focus();
                return false;
            }
            return true;
        }

        private void mentesBtn_Click(object sender, RoutedEventArgs e)
        {
            Foglalas f = (Foglalas)adatokGrid.DataContext;
            if (KotelezoE(f))
            {
                if (f.ID == 0)
                {
                    context.Foglalas.Add(f);
                }
                else
                {
                    context.Entry(f).State = EntityState.Modified;
                }
                context.SaveChanges();
                SetStatus(true);
                listaDG.SelectedItem = f;
            }
        }

        private void megseBtn_Click(object sender, RoutedEventArgs e)
        {
            Foglalas f = (Foglalas)adatokGrid.DataContext;
            if (f.ID != 0)
            {
                context.Entry(f).State = EntityState.Unchanged;
                listaDG.Items.Refresh();
            }
            SetStatus(true);
            listaDG.SelectedItem = null;
            adatokGrid.DataContext = null;
        }

        private void listaDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            adatokGrid.DataContext = listaDG.SelectedItem;
        }
    }
}
