using BulkeyBook.Models;
using BulkeyBook.Models.DataAccess.Modul;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;
using System.Security.Claims;
using Microsoft.Extensions.Hosting;
using static System.Net.Mime.MediaTypeNames;
using BulkeyBook.Models.Static_Roles;
using InstamojoAPI;
using System.Net;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Razorpay.Api;
using Paymentsss = BulkeyBook.Models.DataAccess.Modul.Payment;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Payment = Razorpay.Api.Payment;
using BulkeyBook.Models.Razorepay;

namespace BulkeyBook.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");
            return View(productList);
        }

        public IActionResult Details(int productId)
        {


            ShoppingCart shoppingCart = new()
            {

                Count = 1,
                ProductId = productId,
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category,CoverType")

            };

            return View(shoppingCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.UserINtoUserId = claim.Value;

            ShoppingCart cartFromDb = _unitOfWork.shoppingRepository.GetFirstOrDefault(
                u => u.UserINtoUserId == claim.Value && u.ProductId == shoppingCart.ProductId);


            if (cartFromDb == null)
            {

                _unitOfWork.shoppingRepository.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(ShoppingCartST.SessionCart,
                    _unitOfWork.shoppingRepository.GetAll(u => u.UserINtoUserId == claim.Value).ToList().Count);
            }
            else
            {
                //_unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
                //_unitOfWork.Save();
            }


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }



        public IActionResult ReasurePay()
        {
            RazorpayClient client = new RazorpayClient("rzp_test_nhrFMzGsypzvM5", "wPORnpniDaEMurRzKiT0YTm5");

            Dictionary<string, object> options = new Dictionary<string, object>();
            OrderModel order = new OrderModel()
            {
                OrderAmount = 500,
                Currency = "INR",
                Payment_Capture = 1,    // 0 - Manual capture, 1 - Auto capture
                Notes = new Dictionary<string, string>()
                {
                    { "note 1", "first note while creating order" }, { "note 2", "you can add max 15 notes" },
                    { "note for account 1", "this is a linked note for account 1" }, { "note 2 for second transfer", "it's another note for 2nd account" }
                }
            };
            options.Add("amount", 5000);
            options.Add("currency", "INR");
            options.Add("receipt", "MerchantTransactionId");
            options.Add("payment_capture", 1);
            var s = new Order();
            Order orders = s.Create(options);


            //RazorPayOptionsModel razorPayOptions = new RazorPayOptionsModel()
            //{
            //    Key = "rzp_test_nhrFMzGsypzvM5",
            //    AmountInSubUnits = 500  ,
            //    Currency = "INR",
            //    Name = "Skynet",
            //    Description = "for Dotnet",
            //    ImageLogUrl = "",
            //    OrderId = order.,
            //    ProfileName = registration.Name,
            //    ProfileContact = registration.Mobile,
            //    ProfileEmail = registration.Email,
            //    Notes = new Dictionary<string, string>()
            //    {
            //        { "note 1", "this is a payment note" }, { "note 2", "here also, you can add max 15 notes" }
            //    }
            //};
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> PrivacyAsync(Paymentsss image)
        {
            string Insta_client_id = "test_3goYA7nGppFHIIr3THm6H8lxocji0SDQyyD",
                  Insta_client_secret = "test_8lbcz6MmbYH5Zjp5LxaiLlDKzIAJE5xD9X3rIH8gYjxRXsssBjFsByUjTaO3RJt77c5289T1xDRuT15w2Sm8apgO1qkabICa3i4rbcd53WjS5RwuN3GXPrPCcqr",
                  Insta_Endpoint = InstamojoConstants.INSTAMOJO_API_ENDPOINT,
                  Insta_Auth_Endpoint = InstamojoConstants.INSTAMOJO_AUTH_ENDPOINT;
            Instamojo objClass = await InstamojoImplementation.getApi(Insta_client_id, Insta_client_secret, Insta_Endpoint, Insta_Auth_Endpoint);
            CreatePaymentOrder(objClass, image);
            return View();
        }

        
        public async Task<IActionResult> CallbackAsync()
        {
            string Insta_client_id = "test_3goYA7nGppFHIIr3THm6H8lxocji0SDQyyD",
                  Insta_client_secret = "test_8lbcz6MmbYH5Zjp5LxaiLlDKzIAJE5xD9X3rIH8gYjxRXsssBjFsByUjTaO3RJt77c5289T1xDRuT15w2Sm8apgO1qkabICa3i4rbcd53WjS5RwuN3GXPrPCcqr",
                  Insta_Endpoint = InstamojoConstants.INSTAMOJO_API_ENDPOINT,
                  Insta_Auth_Endpoint = InstamojoConstants.INSTAMOJO_AUTH_ENDPOINT;
            Instamojo objClass = await InstamojoImplementation.getApi(Insta_client_id, Insta_client_secret, Insta_Endpoint, Insta_Auth_Endpoint);
          
            return View(Getpaymebtdatils(objClass));
        }

        private PaymentOrderDetailsResponse Getpaymebtdatils(Instamojo objClass)
        {
           
           return objClass.getPaymentOrderDetailsByTransactionId(HttpContext.Session.GetString("tranid"));


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public  void CreatePaymentOrder(Instamojo objClass, Paymentsss payment)
        {
            PaymentOrder objPaymentRequest = new PaymentOrder();
            //Required POST parameters
            objPaymentRequest.name = "ABCD";
            objPaymentRequest.email = "foo@example.com";
            objPaymentRequest.phone = "9969156561";
            objPaymentRequest.description = "Test description";
            objPaymentRequest.amount = payment.amount;
            objPaymentRequest.currency = "USD";
            objPaymentRequest.allow_repeated_payments = false;
            objPaymentRequest.send_email =false;



            string randomName = Path.GetRandomFileName();
            randomName = randomName.Replace(".", string.Empty);
            objPaymentRequest.transaction_id = "test" + randomName;
            HttpContext.Session.SetString("tranid", objPaymentRequest.transaction_id);
     
            //objPaymentRequest.redirect_url = "https://swaggerhub.com/api/saich/pay-with-instamojo/1.0.0";
            objPaymentRequest.redirect_url = "https://localhost:7089/Customer/Home/Callback";
            objPaymentRequest.webhook_url = "https://your.server.com/webhook";
            //Extra POST parameters 

            if (objPaymentRequest.validate())
            {
                if (objPaymentRequest.emailInvalid)
                {
                   var msg ="Email is not valid";
                }
                if (objPaymentRequest.nameInvalid)
                {
                   var msg ="Name is not valid";
                }
                if (objPaymentRequest.phoneInvalid)
                {
                   var msg ="Phone is not valid";
                }
                if (objPaymentRequest.amountInvalid)
                {
                   var msg ="Amount is not valid";
                }
                if (objPaymentRequest.currencyInvalid)
                {
                   var msg ="Currency is not valid";
                }
                if (objPaymentRequest.transactionIdInvalid)
                {
                   var msg ="Transaction Id is not valid";
                }
                if (objPaymentRequest.redirectUrlInvalid)
                {
                   var msg ="Redirect Url Id is not valid";
                }
                if (objPaymentRequest.webhookUrlInvalid)
                {
                   var msg ="Webhook URL is not valid";
                }

            }
            else
            {
                try
                {
                    CreatePaymentOrderResponse objPaymentResponse = objClass.createNewPaymentRequest(objPaymentRequest);
                    Response.Redirect(objPaymentResponse.payment_options.payment_url);
                   //var msg ="Payment URL = " + objPaymentResponse.payment_options.payment_url;
                }
                catch (ArgumentNullException ex)
                {
                    var msg = ex.Message;
                }
                catch (WebException ex)
                {
                    var msg = ex.Message;
                }
                catch (IOException ex)
                {
                    var msg = ex.Message;
                }
                catch (InvalidPaymentOrderException ex)
                {
                    if (!ex.IsWebhookValid())
                    {
                        var msg = "Webhook is invalid";
                    }

                    if (!ex.IsCurrencyValid())
                    {
                       var msg ="Currency is Invalid";
                    }

                    if (!ex.IsTransactionIDValid())
                    {
                       var msg ="Transaction ID is Inavlid";
                    }
                }
                catch (ConnectionException ex)
                {
                   var msg =ex.Message;
                }
                catch (BaseException ex)
                {
                   var msg =ex.Message;
                }
                catch (Exception ex)
                {
                   var msg ="Error:" + ex.Message;
                }
            }
        }
    }
}