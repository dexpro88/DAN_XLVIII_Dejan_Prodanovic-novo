using DAN_XLVIII_Dejan_Prodanovic.Command;
using DAN_XLVIII_Dejan_Prodanovic.Model;
using DAN_XLVIII_Dejan_Prodanovic.Service;
using DAN_XLVIII_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLVIII_Dejan_Prodanovic.ViewModel
{
    class GuestMainViewModel:ViewModelBase
    {
        GuestMainView guestMainView;
        private string JMBG;
        IPizzeriaService pizzeriaService;

        #region Constructors
        public GuestMainViewModel(GuestMainView guestMainViewOpen, string JMBG)
        {
            pizzeriaService = new PizzeriaService();
            guestMainView = guestMainViewOpen;
            this.JMBG = JMBG;

            ordersOfUser = pizzeriaService.GetOrdersOfGuest(JMBG);

            if (ordersOfUser.Any())
            {
                if (ordersOfUser.Last().OrderStatus == "A")
                {
                    //ViewAprovedOrder = Visibility.Visible;
                    //ViewMainMenu = Visibility.Hidden;
                    //Thread.Sleep(3000);
                    //ViewAprovedOrder = Visibility.Hidden;
                    //ViewMainMenu = Visibility.Visible;
                    ApprovedWindow approvedWindow = new ApprovedWindow();
                    approvedWindow.ShowDialog();


                    MessageBox.Show("Your last order is aproved");
                }
                if (ordersOfUser.Last().OrderStatus == "R")
                {
                    //ViewAprovedOrder = Visibility.Visible;
                    //ViewMainMenu = Visibility.Hidden;
                    //Thread.Sleep(3000);
                    //ViewAprovedOrder = Visibility.Hidden;
                    //ViewMainMenu = Visibility.Visible;
                    //RefusedWindow refusedWindow = new RefusedWindow();
                    //refusedWindow.ShowDialog();
                    //Thread.Sleep(2000);
                    //refusedWindow.Close();
                    MessageBox.Show("Your last order is refused");

                }
            }

        }
        #endregion

        private Visibility viewAprovedOrder = Visibility.Hidden;
        public Visibility ViewAprovedOrder
        {
            get
            {
                return viewAprovedOrder;
            }
            set
            {
                viewAprovedOrder = value;
                OnPropertyChanged("ViewAprovedOrder");
            }
        }

        private Visibility viewMainMenu = Visibility.Visible;
        public Visibility ViewMainMenu
        {
            get
            {
                return viewMainMenu;
            }
            set
            {
                viewMainMenu = value;
                OnPropertyChanged("ViewMainMenu");
            }
        }

        private List<tblOrder> ordersOfUser;

        #region Commands
        private ICommand showMenu;
        public ICommand ShowMenu
        {
            get
            {
                if (showMenu == null)
                {
                    showMenu = new RelayCommand(param => ShowMenuExecute(), param => CanShowMenuExecute());
                }
                return showMenu;
            }
        }

        private void ShowMenuExecute()
        {
            try
            {
                MenuView menuView = new MenuView(JMBG);
                menuView.Show();
                guestMainView.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanShowMenuExecute()
        {
            return true;
        }

        private ICommand logoutCommmand;
        public ICommand LogoutCommmand
        {
            get
            {
                if (logoutCommmand == null)
                {
                    logoutCommmand = new RelayCommand(param => LogoutExecute(), param => CanLogoutExecute());
                }
                return logoutCommmand;
            }
        }

        private void LogoutExecute()
        {
            try
            {
                LoginView loginView = new LoginView();
                guestMainView.Close();
                loginView.ShowDialog();
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

                guestMainView.Close();

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
    }
}
