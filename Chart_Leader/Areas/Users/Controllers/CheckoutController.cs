using Chart_Leader.Controllers;
using Chart_Leader.Models;
using Chart_Leader.Repository;
using Chart_Leader.Repository.RepositoryS_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Customers = Chart_Leader.Repository.Customers;

namespace Chart_Leader.Areas.Users.Controllers
{
    [HandleError]

    public class CheckoutController : BaseController
    {
        leader_Entities db = new leader_Entities();
        IRepository<Customers> CustomersRepository;
        IRepository<Orders> OrdersRepository;
        IRepository<Order_Products> Order_ProductsRepository;
        IRepository<Products> ProductsRepository;
        IRepository<Categories> categoriesRepository;


        private List<object> cards;

        public CheckoutController(IRepository<Customers> CustomersRepository, IRepository<Categories> categoriesRepository, IRepository<Orders> OrdersRepository, IRepository<Order_Products> Order_ProductsRepository, IRepository<Products> ProductsRepository)
        {
            this.CustomersRepository = CustomersRepository;
            this.OrdersRepository = OrdersRepository;
            this.Order_ProductsRepository = Order_ProductsRepository;
            this.ProductsRepository = ProductsRepository;
            this.categoriesRepository = categoriesRepository;

            cards = new List<object> {
                new { Type = "VISA" },
                new { Type = "Master Card" },
                new { Type = "AMEX" }
            };
        }

        // GET: Checkout
        public ActionResult Index()
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                Session["cart"] = cart;
                return View(Session["cart"]);
            }
            else
                return View(Session["cart"]);
        }

        public ActionResult Purchase()
        {
            ViewBag.Cards = cards;/* new SelectList(cards, "Type", "Type");*/
            ViewBag.category = new SelectList(categoriesRepository.GetAll(), "Cat_id", "Cat_Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Purchase(Customers customer)
        {
            ViewBag.Cards = cards;

            if (ModelState.IsValid)
            {
                //if (customer.ExpDate <= DateTime.Now)
                //{
                //    ModelState.AddModelError("", "Credit card has already expired");
                //}

                if (customer.Ctype == "AMEX")
                {
                    if (customer.CardNo.Length != 15)
                    {
                        ModelState.AddModelError("", "AMEX must be 15 digits");
                    }
                }
                else
                {
                    if (customer.CardNo.Length != 16)
                    {
                        ModelState.AddModelError("", customer.Ctype + "must be 16 digits");
                    }
                }

                if (ModelState.IsValid)
                {
                    CustomersRepository.Add(customer);
                    //Orders && Cutsomer
                    Orders o = new Orders
                    {
                        Order_date = DateTime.Now,
                        DeliveryDate = DateTime.Now.AddDays(5),
                        CID = customer.CID
                    };
                    OrdersRepository.Add(o);

                    // save Order_Products
                    List<Item> carts = (List<Item>)Session["cart"];
                    foreach (var item in carts)
                    {

                        Order_ProductsRepository.Add(new Order_Products
                        {
                            OrderID = o.Order_id,
                            PID = item.product.Product_id,
                            Qty = item.quantity,
                            TotalSale = item.quantity * item.product.Product_Price
                        });
                        //update Product store quantity
                        Products product = ProductsRepository.GetFirstorDefaultByParameter(x => x.Product_id == item.product.Product_id);
                        product.Product_QTY -= item.quantity;
                        ProductsRepository.Update(product);
                    }
                    db.SaveChanges();

                    //Send Email
                    string message = string.Format("Thanks for your shopping from our site"+ "\n" + "Your Products will arrive in 5 days");
                    SendEmail(message);

                    // remove Session
                    Session.Remove("cart");
                    ViewBag.category = new SelectList(categoriesRepository.GetAll(), "Cat_id", "Cat_Name");
                    return RedirectToAction("PurchasedSuccess");

                }
            }

            List<ModelError> errors = new List<ModelError>();
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errors.Add(error);
                }
            }
            return View(customer);


        }

        public ActionResult PurchasedSuccess()
        {
            return View();
        }
        public void SendEmail(string Message="")
        {
            MailMessage mailMessage = new MailMessage("From@gmail.com", "To@gmail.com", "Report", Message);

            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Send(mailMessage);
        }
        //

    }
}