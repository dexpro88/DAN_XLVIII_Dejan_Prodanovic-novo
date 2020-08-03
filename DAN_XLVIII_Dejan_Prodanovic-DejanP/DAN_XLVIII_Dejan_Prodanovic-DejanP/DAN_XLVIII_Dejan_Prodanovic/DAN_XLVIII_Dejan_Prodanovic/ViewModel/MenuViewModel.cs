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
    class MenuViewModel:ViewModelBase
    {
        MenuView menuView;
        private decimal totalAmountNum = 0;
        private bool orderConfirmed = false;
        private string JMBG;
        IPizzeriaService pizzeriaService;
        bool CanAdd = true;

        #region Constructors
        public MenuViewModel(MenuView menuViewOpen)
        {
            menuView = menuViewOpen;
            pizzeriaService = new PizzeriaService();
            PizzaList = pizzeriaService.GetPizzas();
            orederedPizzas = new List<tblPizzaOrder>();
        }

        public MenuViewModel(MenuView menuViewOpen, string JMBG)
        {
            menuView = menuViewOpen;
            pizzeriaService = new PizzeriaService();
            PizzaList = pizzeriaService.GetPizzas();
            orederedPizzas = new List<tblPizzaOrder>();
            ordersOfUser = pizzeriaService.GetOrdersOfGuest(JMBG);

            if (ordersOfUser.Any())
            {
                if (ordersOfUser.Last().OrderStatus == "W")
                {
                    ViewMakeOrder = Visibility.Hidden;
                    ViewShowOrder = Visibility.Visible;
                    CanAdd = false;
                }
            }
            this.JMBG = JMBG;
        }
        #endregion

        #region Properties
        private tblPizza selectedPizza;
        public tblPizza SelectedPizza
        {
            get
            {
                return selectedPizza;
            }
            set
            {
                selectedPizza = value;
                OnPropertyChanged("SelectedPizza");
            }
        }

        private string totalAmount = "Total order amount: 0";
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

        private int currentAmount = 0;
        public int CurrentAmount
        {
            get
            {
                return currentAmount;
            }
            set
            {
                currentAmount = value;
                OnPropertyChanged("CurrentAmount");

            }
        }

        private Visibility viewMakeOrder = Visibility.Visible;
        public Visibility ViewMakeOrder
        {
            get
            {
                return viewMakeOrder;
            }
            set
            {
                viewMakeOrder = value;
                OnPropertyChanged("ViewMakeOrder");
            }
        }

        private Visibility viewShowOrder = Visibility.Collapsed;
        public Visibility ViewShowOrder
        {
            get
            {
                return viewShowOrder;
            }
            set
            {
                viewShowOrder = value;
                OnPropertyChanged("ViewShowOrder");
            }
        }


        private List<tblPizza> pizzaList;
        public List<tblPizza> PizzaList
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


        private List<tblOrder> ordersOfUser;



        private List<tblPizzaOrder> orederedPizzas;
        public List<tblPizzaOrder> OrederedPizzas
        {
            get
            {
                return orederedPizzas;
            }
            set
            {
                orederedPizzas = value;
                OnPropertyChanged("OrederedPizzas");
            }
        }
        #endregion

        #region Commands

        private ICommand addToOrder;
        public ICommand AddToOrder
        {
            get
            {
                if (addToOrder == null)
                {
                    addToOrder = new RelayCommand(param => AddToOrderExecute(), param => CanAddToOrderExecute());
                }
                return addToOrder;
            }
        }

        private void AddToOrderExecute()
        {
            try
            {
                tblPizzaOrder thisPizza = FindPizzaByName(SelectedPizza.PizzaType);

                if (thisPizza != null && currentAmount == 0)
                {
                    CurrentAmount = (int)thisPizza.Amount;
                }
                if (CurrentAmount <= 0 || CurrentAmount > 50)
                {
                    MessageBox.Show("You have to order between 1 and 50 pizzas of one type");
                    return;
                }
                tblPizzaOrder newOrder = new tblPizzaOrder();
                newOrder.PizzaID = SelectedPizza.ID;
                newOrder.tblPizza = SelectedPizza;
                //newOrder.tblPizza.Price = SelectedPizza.Price;

                newOrder.Amount = CurrentAmount;
                //MessageBox.Show(newOrder.tblPizza.PizzaType);
                //SelectedPizza.Amount = currentAmount;

                //PizzaClass newPizza = new PizzaClass(SelectedPizza.Name, SelectedPizza.Price) { Amount = currentAmount};

                if (thisPizza != null)
                {
                    //MessageBox.Show(thisPizza.Amount.ToString());
                    totalAmountNum -= ((int)thisPizza.Amount * (decimal)thisPizza.tblPizza.Price);
                    OrederedPizzas.Remove(thisPizza);
                }


                totalAmountNum += (CurrentAmount * (int)SelectedPizza.Price);
                //OrederedPizzas.Add(newPizza);
                orederedPizzas.Add(newOrder);

                TotalAmount = string.Format("Total order price {0}", totalAmountNum);
                string outputStr = string.Format("Your order will contain {0} {1}", CurrentAmount, SelectedPizza.PizzaType);
                CurrentAmount = 0;
                MessageBox.Show(outputStr);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddToOrderExecute()
        {
            if (orderConfirmed|| !CanAdd)
            {
                return false;
            }
            return true;
        }

        private ICommand makeOrder;
        public ICommand MakeOrder
        {
            get
            {
                if (makeOrder == null)
                {
                    makeOrder = new RelayCommand(param => MakeOrderExecute(), param => CanMakeOrderExecute());
                }
                return makeOrder;
            }
        }

        private void MakeOrderExecute()
        {
            try
            {
                OrderView orderView = new OrderView(orederedPizzas, totalAmountNum, JMBG);
                orderView.ShowDialog();

                if ((orderView.DataContext as OrderViewModel).OrderConfirmed == true)
                {
                    ViewMakeOrder = Visibility.Hidden;
                    ViewShowOrder = Visibility.Visible;
                    orderConfirmed = true;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanMakeOrderExecute()
        {
            if (!orederedPizzas.Any() || orderConfirmed)
            {
                return false;
            }
            if (ordersOfUser.Any())
            {
                if (ordersOfUser.Last().OrderStatus == "W")
                {
                    return false;
                }
            }
            return true;
        }

        private ICommand showOrder;
        public ICommand ShowOrder
        {
            get
            {
                if (showOrder == null)
                {
                    showOrder = new RelayCommand(param => ShowOrderExecute(), param => CanShowOrderExecute());
                }
                return showOrder;
            }
        }

        private void ShowOrderExecute()
        {
            try
            {
                ShowOrderView orderView = new ShowOrderView(orederedPizzas, totalAmountNum, JMBG);
                orderView.ShowDialog();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanShowOrderExecute()
        {
            if (!orederedPizzas.Any())
            {
                return false;
            }
            return true;
        }

        private ICommand logout;
        public ICommand Logout
        {
            get
            {
                if (logout == null)
                {
                    logout = new RelayCommand(param => LogoutExecute(), param => CanLogoutExecute());
                }
                return logout;
            }
        }

        private void LogoutExecute()
        {
            try
            {
                LoginView loginView = new LoginView();
                loginView.Show();
                menuView.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanLogoutExecute()
        {
            return true;
        }

        private ICommand back;
        public ICommand Back
        {
            get
            {
                if (back == null)
                {
                    back = new RelayCommand(param => BackExecute(), param => CanBackExecute());
                }
                return back;
            }
        }

        private void BackExecute()
        {
            try
            {
                GuestMainView guestMain = new GuestMainView(JMBG);
                guestMain.Show();
                menuView.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanBackExecute()
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

                menuView.Close();

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
        #endregion

        #region Methods
        private tblPizzaOrder FindPizzaByName(string name)
        {
            foreach (var pizza in orederedPizzas)
            {
                if (pizza.tblPizza.PizzaType.Equals(name))
                {
                    return pizza;
                }
            }
            return null;
        }
        #endregion
    }
}
