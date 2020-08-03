using DAN_XLVIII_Dejan_Prodanovic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLVIII_Dejan_Prodanovic.Service
{
    class PizzeriaService:IPizzeriaService
    {
        public List<tblPizza> GetPizzas()
        {
            try
            {
                using (PizzeriaDatBEntities1 context = new PizzeriaDatBEntities1())
                {
                    List<tblPizza> list = new List<tblPizza>();
                    list = (from x in context.tblPizzas select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblOrder> GetOrdersOfGuest(string JMBG)
        {
            try
            {
                using (PizzeriaDatBEntities1 context = new PizzeriaDatBEntities1())
                {
                    List<tblOrder> list = new List<tblOrder>();
                    list = (from x in context.tblOrders where x.JMBG == JMBG select x).ToList();

                    list.Sort((x, y) => DateTime.Compare((DateTime)x.DateAndTimeOfOrder, (DateTime)y.DateAndTimeOfOrder));

                    
                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblOrder AddOrder(tblOrder order)
        {
            try
            {
                using (PizzeriaDatBEntities1 context = new PizzeriaDatBEntities1())
                {

                    tblOrder newOrder = new tblOrder();
                    newOrder.JMBG = order.JMBG;
                    newOrder.OrderStatus = order.OrderStatus;
                    newOrder.DateAndTimeOfOrder = order.DateAndTimeOfOrder;
                    newOrder.TotalPrice = order.TotalPrice;

                    context.tblOrders.Add(newOrder);
                    context.SaveChanges();
                    order.ID = newOrder.ID;


                    return order;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblPizzaOrder AddPizzaOrder(tblPizzaOrder pizzaOrder)
        {
            try
            {
                using (PizzeriaDatBEntities1 context = new PizzeriaDatBEntities1())
                {

                    tblPizzaOrder newPizzaOrder = new tblPizzaOrder();
                    newPizzaOrder.Amount = pizzaOrder.Amount;
                    newPizzaOrder.OrderID = pizzaOrder.OrderID;
                    newPizzaOrder.PizzaID = pizzaOrder.PizzaID;
                    

                    context.tblPizzaOrders.Add(newPizzaOrder);
                    context.SaveChanges();
                    pizzaOrder.ID = newPizzaOrder.ID;


                    return pizzaOrder;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblOrder> GetOrders()
        {
            try
            {
                using (PizzeriaDatBEntities1 context = new PizzeriaDatBEntities1())
                {
                    List<tblOrder> list = new List<tblOrder>();
                    list = (from x in context.tblOrders select x).ToList();

                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public List<tblPizzaOrder> GetPizzaOrdersByOrderID(int orderID)
        {
            try
            {
                using (PizzeriaDatBEntities1 context = new PizzeriaDatBEntities1())
                {
                    List<tblPizzaOrder> list = new List<tblPizzaOrder>();
                    list = (from x in context.tblPizzaOrders where x.OrderID == orderID select x).ToList();
                    foreach (var item in list)
                    {
                        tblPizza pizza = (from x in context.tblPizzas where x.ID == item.PizzaID select x).First();
                        tblOrder order = (from x in context.tblOrders where x.ID == item.OrderID select x).First();
                        item.tblPizza = pizza;
                        item.tblOrder = order;

                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        public tblOrder GetOrderByID(int id)
        {
            try
            {
                using (PizzeriaDatBEntities1 context = new PizzeriaDatBEntities1())
                {


                    tblOrder order = (from x in context.tblOrders where x.ID == id select x).First();

                    return order;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        public void EditOrder(tblOrder order)
        {
            try
            {
                using (PizzeriaDatBEntities1 context = new PizzeriaDatBEntities1())
                {
                    tblOrder orderDB = (from x in context.tblOrders where x.ID == order.ID select x).First();

                    orderDB.OrderStatus = order.OrderStatus;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());

            }
        }

        public void DeleteOrder(int id)
        {
            try
            {
                using (PizzeriaDatBEntities1 context = new PizzeriaDatBEntities1())
                {
                    tblOrder orderToDelete = (from r in context.tblOrders where r.ID == id select r).First();
                    context.tblOrders.Remove(orderToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
