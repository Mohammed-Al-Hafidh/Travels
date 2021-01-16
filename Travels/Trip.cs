using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Travels
{
    class Trip
    {
        private string _destination;
        private string _name;
        public String Destination
        {
            get
            {
                return _destination;
            }
            set 
            {
                try
                {
                    if (value.Contains(";"))
                    {
                        throw new InvalidDataException("Destination can not contains a ';'\nGo to next line...");
                    }
                    _destination = value;
                }
                catch
                {

                }
            }
        }
        public string Name {
            get
            {
                return _name;
            }
            set
            {
                try
                {
                    if (value.Contains(";"))
                    {
                        throw new InvalidDataException("Name can not contains a ';'\nGo to next line...");
                    }
                    _name = value;
                }
                catch
                {

                }
            }
        }
        public string Passport { get; set; }
        public string DepartureDate { get; set; }
        public string ReturnDate { get; set; }
        public Trip(string destination,string name,string passport,string departureDate,string returnDate)
        {
            this.Destination = destination;
            this.Name = name;
            this.Passport = passport;
            this.DepartureDate = departureDate;
            this.ReturnDate = returnDate;
        }
        public Trip(string dataLine)
        {
            string[] myData = dataLine.Split(';');
            if (myData.Length > 5)
            {
                MessageBox.Show("Invalid line. Go to next line", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.Destination = myData[0];
                this.Name = myData[1];
                this.Passport = myData[2];
                this.DepartureDate = myData[3];
                this.ReturnDate = myData[4];
            }
        }
        public override string ToString()
        {
            return string.Format("Destination: {0}, Name: {1}, Passport Number: {2}, Departure Date: {3}, Return Date: {4}", this.Destination, this.Name, this.Passport, this.DepartureDate, this.ReturnDate);
        }
        public string ToDataString()
        {
            return string.Format("{0};{1};{2};{3};{4}", this.Destination, this.Name, this.Passport, this.DepartureDate, this.ReturnDate);
        }
    }
}
