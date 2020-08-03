using DAN_XLVIII_Dejan_Prodanovic.Command;
using DAN_XLVIII_Dejan_Prodanovic.Model;
using DAN_XLVIII_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLVIII_Dejan_Prodanovic.ViewModel
{
    class ShowOrderViewModel:ViewModelBase
    {
        ShowOrderView orderView = new ShowOrderView();
        private string JMBG;
        #region Constructors
        public ShowOrderViewModel(ShowOrderView orderViewOpen, List<tblPizzaOrder> pizzas, decimal totalAmountPar)
        {
            orderView = orderViewOpen;
            PizzaList = pizzas;

            totalAmount = String.Format("Total order price: {0}", totalAmountPar);

        }

        public ShowOrderViewModel(ShowOrderView orderViewOpen, List<tblPizzaOrder> pizzas, decimal totalAmountPar, string JMBG)
        {
            orderView = orderViewOpen;
            PizzaList = pizzas;

            totalAmount = String.Format("Total order price: {0}", totalAmountPar);
            this.JMBG = JMBG;

        }
        #endregion

        private List<tblPizzaOrder> pizzaList;
        public List<tblPizzaOrder> PizzaList
        {
            get
            {
                return pizzaList;
            }
            set
            {
                pizzaList = value;
                OnPropertyChanged("PizzaList");
            }
        }

        private string totalAmount;
        public string TotalAmount
        {
            get
            {
                return totalAmount;
            }
            set
            {
                totalAmount = value;
                OnPropertyChanged("TotalAmount");
            }
        }

       

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        private void CloseExecute()
        {
            try
            {

                orderView.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCloseExecute()
        {
            return true;
        }
    }
}
