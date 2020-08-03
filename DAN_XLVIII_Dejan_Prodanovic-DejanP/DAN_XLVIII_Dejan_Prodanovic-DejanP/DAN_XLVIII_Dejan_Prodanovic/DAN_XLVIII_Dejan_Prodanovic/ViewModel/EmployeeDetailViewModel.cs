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
    class EmployeeDetailViewModel:ViewModelBase
    {
        EmployeeDetail employeeDetail;
        IPizzeriaService service;

        #region
        public EmployeeDetailViewModel(EmployeeDetail employeeDetailOpen)
        {
            employeeDetail = employeeDetailOpen;
        }
        public EmployeeDetailViewModel(EmployeeDetail employeeDetailOpen, tblOrder orderPar)
        {
            employeeDetail = employeeDetailOpen;
            order = orderPar;
            totalPrice = orderPar.TotalPrice.ToString();

            service = new PizzeriaService();
            if (orderPar.OrderStatus == "W")
            {
                ViewApprove = Visibility.Visible;
                ViewDelete = Visibility.Hidden;
                Status = "Status: Waiting";
            }
            else if (orderPar.OrderStatus == "A")
            {
                ViewDelete = Visibility.Visible;
                ViewApprove = Visibility.Hidden;
                Status = "Status: Approved";
            }
            else
            {
                ViewDelete = Visibility.Visible;
                ViewApprove = Visibility.Hidden;
                Status = "Status: Refused";
            }
            PizzaOrders = service.GetPizzaOrdersByOrderID(orderPar.ID);

        }
        #endregion

        #region Propertires
        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        private tblOrder order;

        private List<tblPizzaOrder> pizzaOrders;
        public List<tblPizzaOrder> PizzaOrders
        {
            get
            {
                return pizzaOrders;
            }
            set
            {
                pizzaOrders = value;
                OnPropertyChanged("PizzaOrders");
            }
        }
        private Visibility viewDelete;
        public Visibility ViewDelete
        {
            get
            {
                return viewDelete;
            }
            set
            {
                viewDelete = value;
                OnPropertyChanged("ViewDelete");
            }
        }

        private string totalPrice;
        public string TotalPrice
        {
            get
            {
                return totalPrice;
            }
            set
            {
                totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        private Visibility viewApprove;
        public Visibility ViewApprove
        {
            get
            {
                return viewApprove;
            }
            set
            {
                viewApprove = value;
                OnPropertyChanged("ViewApprove");
            }
        }


        #endregion

        private ICommand refuse;
        public ICommand Refuse
        {
            get
            {
                if (refuse == null)
                {
                    refuse = new RelayCommand(param => RefuseExecute(), param => CanRefuseExecute());
                }
                return refuse;
            }
        }

        private void RefuseExecute()
        {
            try
            {
                tblOrder orderDB = service.GetOrderByID(order.ID);
                orderDB.OrderStatus = "A";
                service.EditOrder(orderDB);
                employeeDetail.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanRefuseExecute()
        {
            return true;
        }

        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                if (delete == null)
                {
                    delete = new RelayCommand(param => DeleteExecute(), param => CanDeleteExecute());
                }
                return delete;
            }
        }

        private void DeleteExecute()
        {
            try
            {

                int id = order.ID;
                service.DeleteOrder(id);
                employeeDetail.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteExecute()
        {
            return true;

        }

        private ICommand approve;
        public ICommand Approve
        {
            get
            {
                if (approve == null)
                {
                    approve = new RelayCommand(param => ApproveExecute(), param => CanApproveExecute());
                }
                return approve;
            }
        }

        private void ApproveExecute()
        {
            try
            {
                tblOrder orderDB = service.GetOrderByID(order.ID);
                orderDB.OrderStatus = "R";
                service.EditOrder(orderDB);
                employeeDetail.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanApproveExecute()
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

                employeeDetail.Close();

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
