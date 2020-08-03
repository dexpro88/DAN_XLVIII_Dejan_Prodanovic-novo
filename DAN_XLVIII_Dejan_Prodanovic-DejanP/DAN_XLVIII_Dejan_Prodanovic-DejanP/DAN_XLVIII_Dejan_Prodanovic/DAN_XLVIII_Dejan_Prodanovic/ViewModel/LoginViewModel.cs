using DAN_XLVIII_Dejan_Prodanovic.Command;
using DAN_XLVIII_Dejan_Prodanovic.Validation;
using DAN_XLVIII_Dejan_Prodanovic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_XLVIII_Dejan_Prodanovic.ViewModel
{
    class LoginViewModel:ViewModelBase
    {
        LoginView view;

        public LoginViewModel(LoginView loginView)
        {
            view = loginView;
        }

        private string userName;
        public string UserName
        {

            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("UserName");
            }
        }

        private ICommand submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                if (submitCommand == null)
                {
                    submitCommand = new RelayCommand(SubmitCommandExecute,
                        param => CanSubmitCommandExecute());
                }
                return submitCommand;
            }
        }

        private void SubmitCommandExecute(object obj)
        {
            try
            {
                string password = (obj as PasswordBox).Password;
                if (ValidationClass.JMBGisValid(UserName) && password.Equals("Gost"))
                {
                    GuestMainView guestMain = new GuestMainView(UserName);
                    view.Close();
                    guestMain.Show();
                    return;
                }
                else if (UserName.Equals("Zaposleni") && password.Equals("Zaposleni"))
                {
                    EmployeeView employeeView = new EmployeeView();
                    view.Close();
                    employeeView.Show();
                }
                else
                {
                    MessageBox.Show("Wrong usename or password");
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanSubmitCommandExecute()
        {
            return true;
        }
    }
}
