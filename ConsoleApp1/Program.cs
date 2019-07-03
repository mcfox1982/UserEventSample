using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();
            Waiter waiter = new Waiter();
            customer.Order += waiter.Action;
            customer.Action();
            customer.Action();
            customer.PayBill();
        }
    }

    public class Customer
    {
        public double Bill { get; set; }

        public void PayBill()
        {
            Console.WriteLine("i will pay ${0}.", this.Bill);
        }

        //private OrderEventHandler _orderEventHandler;

        //public event OrderEventHandler Order
        //{
        //    add { this._orderEventHandler += value; }
        //    remove { this._orderEventHandler -= value; }
        //}
        public event EventHandler Order;

        public void Walkin()
        {
            Console.WriteLine("I am walking in");
        }

        public void Sitdown()
        {
            Console.WriteLine("I sit down");
        }

        public void Think()
        {
            Console.WriteLine("let me think");
            Thread.Sleep(2000);

            if (this.Order != null)
            {
                OrderEventArgs e = new OrderEventArgs();
                e.DishName = "Dish A";
                e.Size = "big";
                this.Order.Invoke(this, e);
            }
        }

        public void Action()
        {
            Console.ReadLine();
            this.Walkin(); this.Sitdown(); this.Think();
        }
    }

    public class OrderEventArgs : EventArgs
    {


        public string DishName { get; set; }
        public string Size { get; set; }
    }

    //public delegate void OrderEventHandler(Customer customer, OrderEventArgs e);

    public class Waiter
    {
        public void Action(object sender, EventArgs e1)
        {
            Customer customer = (Customer) sender;
            OrderEventArgs e = (OrderEventArgs) e1;
            Console.WriteLine("I will serve u the dish--{0}", e.Size);
            double price = 10;
            switch (e.Size)
            {
                case "small":
                    price = price * 0.5;
                    break;
                case "big":
                    price = price * 1.5;
                    break;
                default:
                    break;
            }

            customer.Bill += price;
        }
    }
}
