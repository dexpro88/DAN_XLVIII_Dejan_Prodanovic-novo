using DAN_XLVIII_Dejan_Prodanovic.Command;
using DAN_XLVIII_Dejan_Prodanovic.Model;
using DAN_XLVIII_Dejan_Prodanovic.Service;
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
    class OrderViewModel:ViewModelBase
    {
        OrderView orderView = new OrderView();
        IPizzeriaService pizzaService;
        private string JMBG;
        private decimal totalPrice;

        #region Constructors
        public OrderViewModel(OrderView orderViewOpen, List<tblPizzaOrder> pizzas, decimal totalAmountPar, string JMBG)
        {
            orderView = orderViewOpen;
            PizzaList = pizzas;

            totalAmount = String.Format("Total order price: {0}", totalAmountPar);
            this.JMBG = JMBG;
            totalPrice = totalAmountPar;
            pizzaService = new PizzeriaService();
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

        private bool orderConfirmed;
        public bool OrderConfirmed
        {
            get
            {
                return orderConfirmed;
            }
            set
            {
                orderConfirmed = value;
                OnPropertyChanged("OrderConfirmed");
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

        

        private ICommand confirmOrder;
        public ICommand ConfirmOrder
        {
            get
            {
                if (confirmOrder == null)
                {
                    confirmOrder = new RelayCommand(param => ConfirmOrderExecute(), param => CanConfirmOrderExecute());
                }
                return confirmOrder;
            }
        }

        private void ConfirmOrderExecute()
        {
            try
            {
                OrderConfirmed = true;
                tblOrder newOrder = new tblOrder();
                newOrder.JMBG = JMBG;
                newOrder.OrderStatus = "W";
                newOrder.DateAndTimeOfOrder = DateTime.Now;
                newOrder.TotalPrice = totalPrice;
;

                newOrder = pizzaService.AddOrder(newOrder);
                

                foreach (var pizza in PizzaList)
                {
                    tblPizzaOrder pizzaOrder = new tblPizzaOrder();
                    pizzaOrder.PizzaID = pizza.tblPizza.ID;
                    pizzaOrder.OrderID = newOrder.ID;
                    pizzaOrder.Amount = pizza.Amount;

                    pizzaService.AddPizzaOrder(pizzaOrder);
                    
                }
                orderView.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanConfirmOrderExecute()
        {
            return true;
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
