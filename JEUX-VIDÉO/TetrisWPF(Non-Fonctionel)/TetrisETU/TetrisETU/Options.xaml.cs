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


namespace Tetris
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        
        
        public Options()
        {
            InitializeComponent();
            
        }
        static public bool acceptHit = false;
       
        static public bool musicChecked = true;

        static public bool accept()
        {
            return acceptHit;
        }
        void MusicCheck_Checked(object sender, RoutedEventArgs e)
        {
            musicChecked = true;
        }

        void MusicCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            musicChecked = false;
        }

        void Cancel_Click(object sender, RoutedEventArgs e)
        {
            acceptHit = false;
            App.Current.Windows[1].Close();

        }

        public void Accept_Click(object sender, RoutedEventArgs e)
        {
            acceptHit = true;
        }
        static public bool MusicOrNot()
        {
            if (musicChecked)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //static public int NombreColonne()
        //{
        //    int col = Convert.ToInt16(nbcolonne.Value);
        //    return col;
        //}
        //static public int NombreRanger()
        //{
        //    int ran = Convert.ToInt16(nbligne.Value);
        //    return ran;
        //}
    }
}

