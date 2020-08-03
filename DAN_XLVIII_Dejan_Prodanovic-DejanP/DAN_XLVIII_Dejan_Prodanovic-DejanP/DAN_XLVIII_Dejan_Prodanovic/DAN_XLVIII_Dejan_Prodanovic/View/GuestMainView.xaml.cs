using DAN_XLVIII_Dejan_Prodanovic.ViewModel;
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

namespace DAN_XLVIII_Dejan_Prodanovic.View
{
    /// <summary>
    /// Interaction logic for GuestMainView.xaml
    /// </summary>
    public partial class GuestMainView : Window
    {
        public GuestMainView()
        {
            InitializeComponent();
        }

        public GuestMainView(string JMBG)
        {
            InitializeComponent();
            DataContext = new GuestMainViewModel(this, JMBG);
        }
    }
}
