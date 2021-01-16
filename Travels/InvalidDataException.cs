using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Travels
{
    class InvalidDataException : Exception
    {
        public InvalidDataException(string msg) : base()
        {
            MessageBox.Show(msg,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
        }
    }
}
