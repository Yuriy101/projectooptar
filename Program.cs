using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Client client;
        public Employee employee;

        public List<Shop> shops = new List<Shop>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.client = new Client("Michael", "lvov, shevchenko 21", "policeman911", "bomj@mail.ua");
            this.employee = new Employee("Jack", "lvov, ivana franka 12", 100500);

            this.shops.Add(
                new Shop("Auchan", new Product[3] {
                    new Product("Baltica 9", 20, 0.5, 0.3),
                    new Product("gun-don", 50, 0.01, 0.1),
                    new Product("test na beremenost", 15, 0.01, 0.01)
                })
            );
            this.shops.Add(
                new Shop("Silpo", new Product[3] {
                    new Product("jack danielson", 70, 0.5, 0.3),
                    new Product("lays", 30, 0.25, 0.5),
                    new Product("parlament", 55, 0.01, 0.01)
                })
            );

            productsListView.Columns.Add("Title", 100);
            productsListView.Columns.Add("Price", 100);
            productsListView.Columns.Add("Weight", 100);
            productsListView.Columns.Add("Shop", 100);

            for (int i = 0; i < this.shops[0].GetProducts().Length; i++)
            {
                productsListView.Items.Add(new ListViewItem(new string[3] { shops[0].GetProducts()[i].GetTitle(), "100", "1" }));
                productsListView.Items.Add(new ListViewItem(new string[3] { shops[0].GetProducts()[i].GetPrice().ToString(), "100", "1" }));
                productsListView.Items.Add(new ListViewItem(new string[3] { shops[0].GetProducts()[i].GetWeight().ToString(), "100", "1" }));
                productsListView.Items.Add(new ListViewItem(new string[3] { shops[0].GetName(), "100", "10" }));
            }
        }

        private void productsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }

    public delegate void WorkCompletedCallBack(Order[] orders);

    public class Person
    {
        protected string name;
        protected string address;

        public Person(string name, string address)
        {
            this.name = name;
            this.address = address;
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetAddress()
        {
            return this.address;
        }
    }

    public class Employee : Person
    {
        private double salary = 0;

        public Employee(string name, string address, double salary) : base(name, address)
        {
            this.salary = salary;
        }
    }

    public class Client : Person
    {
        private string username = "";
        private string email = "";

        private List<Order> orders = new List<Order>();

        public Client(string name, string address, string username, string email) : base(name, address)
        {
            this.username = username;
            this.email = email;
        }

        public void SetUsername(string username)
        {
            this.username = username;
        }

        public void SetEmail(string email)
        {
            this.email = email;
        }

        public void SetAddress(string address)
        {
            this.address = address;
        }

        public Order[] GetOrders()
        {
            return this.orders.ToArray();
        }

        public void ReserveOrder(Order newOrder)
        {
            this.orders.Add(newOrder);
        }
public void RemoveOrder(string orderId)
        {
            try
            {
                this.orders.RemoveAt(this.orders.FindIndex(order => orderId == order.GetID()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void ConfirmOrder(WorkCompletedCallBack callback)
        {
            callback(this.orders.ToArray());
            this.orders.Clear();
        }
    }
    public class Order
    {
        protected string id;

        private Shop shop;
        private List<Product> reservedProducts = new List<Product>();

        public Order()
        {
            this.id = "order-" + DateTime.Now;
        }

        public string GetID()
        {
            return this.id;
        }

        public void Reserve(Product product)
        {
            this.reservedProducts.Add(product);
        }

        public Product[] GetReserved()
        {
            return this.reservedProducts.ToArray();
        }
    }

    public class Product
    {
        private string title;
        private double price;
        private double weight;
        private double volume;

        public Product(string title, double price, double weight, double volume)
        {
            this.title = title;
            this.price = price;
            this.weight = weight;
            this.volume = volume;
        }

        public string GetTitle()
        {
            return this.title;
        }

        public double GetPrice()
        {
            return this.price;
        }

        public double GetWeight()
        {
            return this.weight;
        }
        public double GetVolume()
        {
            return this.volume;
        }

    }

    public class Shop
    {
        private string name;
        private List<Product> products = new List<Product>();

        public Shop(string name)
        {
            this.name = name;
        }

        public Shop(string name, Product[] products)
        {
            this.name = name;

            for (int i = 0; i < products.Length; i++)
            {
                this.products.Add(products[i]);
            }
        }

        public string GetName()
        {
            return this.name;
        }

        public Product[] GetProducts()
        {
            return this.products.ToArray();
        }

        public Product GetProduct(string productName)
        {
            return this.products.Find(product => product.GetTitle() == productName);
        }
    }    
}