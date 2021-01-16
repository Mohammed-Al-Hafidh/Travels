using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Travels
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string fileName = @"..\..\trips.txt";
        List<Trip> trips = new List<Trip>();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line = sr.ReadLine();
                while (line != null)
                {
                    string[] myData = line.Split(';');
                    if (myData.Length > 5)
                    {
                        MessageBox.Show("Invalid line. Go to next line", "Error", MessageBoxButton.OK, MessageBoxImage.Error);                        
                    }
                    else
                    {
                        trips.Add(new Trip(myData[0], myData[1], myData[2], myData[3], myData[4]));
                    }                    
                    line = sr.ReadLine();
                }
            }
            lvTrips.ItemsSource = trips;

        }
        public void SaveData()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (Trip trip in trips)
                {
                    writer.WriteLine(trip.ToDataString());
                }
            }
        }
        public void AddTrip()
        {
            try
            {
                if ((txtDestination.Text.Trim() == "")
                    || (txtName.Text.Trim() == "")
                    || (txtPassport.Text.Trim() == "")
                    || (dpDeparture.SelectedDate== null)
                    || (dpReturn.SelectedDate == null)
                    || (txtDestination.Text.Contains(";"))
                    || (txtName.Text.Contains(";")))
                {
                    throw new InvalidDataException("Can not have an empty field\nDestination and Name can not contain a ';'");
                }
                trips.Add(new Trip(txtDestination.Text, txtName.Text, txtPassport.Text, dpDeparture.SelectedDate.Value.ToString(), dpReturn.SelectedDate.Value.ToString()));
                Reset();                               
            }
            catch
            {
                return;
            }           
        }

        

        public void UpdateTrip()
        {
            try
            {
                if ((txtDestination.Text.Trim() == "")
                    || (txtName.Text.Trim() == "")
                    || (txtPassport.Text.Trim() == "")
                    || (dpDeparture.SelectedDate==null)
                    || (dpReturn.SelectedDate==null)
                    || (txtDestination.Text.Contains(";"))
                    || (txtName.Text.Contains(";")))
                {
                    throw new InvalidDataException("Can not have an empty field\nDestination and Name can not contain a ';'");
                }
                Trip tripToBeUpdated = (Trip)lvTrips.SelectedItem;
                tripToBeUpdated.Destination = txtDestination.Text;
                tripToBeUpdated.Name = txtName.Text;
                tripToBeUpdated.Passport = txtPassport.Text;
                tripToBeUpdated.DepartureDate = dpDeparture.SelectedDate.Value.ToString();
                tripToBeUpdated.ReturnDate = dpReturn.SelectedDate.Value.ToString();
                Reset();
            }
            catch
            {
                return;
            }



            
        }
        public void DeleteTrip()
        {
            
            Trip tripToBeDeleted = (Trip)lvTrips.SelectedItem;
            if (tripToBeDeleted != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "CONFIRMATION", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    trips.Remove(tripToBeDeleted);                    
                    Reset();
                }
            }
        }
        private void Reset()
        {
            lvTrips.Items.Refresh();
            lvTrips.SelectedIndex = -1;
            btnUpdateTrip.IsEnabled = false;
            btnDeleteTrip.IsEnabled = false;
            txtDestination.Clear();
            txtName.Clear();
            txtPassport.Clear();
            dpDeparture.Text = "";
            dpReturn.Text = "";

        }

        private void btnAddTrip_Click(object sender, RoutedEventArgs e)
        {
            AddTrip();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveData();
        }

        private void lvTrips_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDeleteTrip.IsEnabled = true;
            btnUpdateTrip.IsEnabled = true;

            var selectedItem = lvTrips.SelectedItem;
            if (selectedItem is Trip)
            {
            
            Trip trip = (Trip)lvTrips.SelectedItem;

            txtDestination.Text = trip.Destination;
            txtName.Text = trip.Name;
            txtPassport.Text = trip.Passport;
            dpDeparture.Text = trip.DepartureDate;
            dpReturn.Text = trip.ReturnDate;
            }

            if (lvTrips.SelectedIndex == -1)
            {
                btnUpdateTrip.IsEnabled = false;
                btnDeleteTrip.IsEnabled = false;
                txtDestination.Clear();
                txtName.Clear();
                txtPassport.Clear();
                dpDeparture.Text = "";
                dpReturn.Text = "";
            }
        }

        private void btnDeleteTrip_Click(object sender, RoutedEventArgs e)
        {
            DeleteTrip();
        }

        private void btnUpdateTrip_Click(object sender, RoutedEventArgs e)
        {
            UpdateTrip();
        }

        private void btnSaveSelected_Click(object sender, RoutedEventArgs e)
        {
            SaveSelected();
        }
        public void SaveSelected()
        {
            if (lvTrips.SelectedIndex == -1)
            {
                MessageBox.Show("No items selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Trips file (*.trips)|*.trips";
                if (dialog.ShowDialog() == true)
                {
                    string allData = "";
                    foreach (Trip trip in lvTrips.SelectedItems)
                    {
                        allData += trip.ToDataString() + "\n";
                    }
                    File.WriteAllText(dialog.FileName, allData);
                }
                else
                {
                    MessageBox.Show("File error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
