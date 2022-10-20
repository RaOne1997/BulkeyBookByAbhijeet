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
                Product = _unitOfWork.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category,CoverType")

            };

            return View(shoppingCart);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize]
        //public IActionResult Details(ShoppingCart shoppingCart)
        //{
        //    //var claimsIdentity = (ClaimsIdentity)User.Identity;
        //    //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        //    //shoppingCart.ApplicationUserId = claim.Value;

        //    //ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
        //    //    u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);


        //    //if (cartFromDb == null)
        //    //{

        //    //    _unitOfWork.ShoppingCart.Add(shoppingCart);
        //    //    _unitOfWork.Save();
        //    //    HttpContext.Session.SetInt32(SD.SessionCart,
        //    //        _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);
        //    //}
        //    //else
        //    //{
        //    //    _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, shoppingCart.Count);
        //    //    _unitOfWork.Save();
        //    //}


        //    //return RedirectToAction(nameof(Index));
        //}


        public ImageUplode aaaa(ImageUplode formFile = null)
        {


            Bitmap myBitmap;
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            if (formFile != null)
            {

                // Create a Bitmap object based on a BMP file.


                // Get an ImageCodecInfo object that represents the JPEG codec.
                myImageCodecInfo = GetEncoderInfo("image/jpeg");

                // Create an Encoder object based on the GUID

                // for the Quality parameter category.
                myEncoder = Encoder.Quality;
                myBitmap = new Bitmap(formFile.ulodeImage.OpenReadStream());
                // Create an EncoderParameters object.

                // An EncoderParameters object has an array of EncoderParameter

                // objects. In this case, there is only one

                // EncoderParameter object in the array.
          


                myEncoderParameters = new EncoderParameters(1);


                myEncoderParameter = new EncoderParameter(myEncoder, 70L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                myBitmap.Save(@"E:/BulkeyBook/BulkeyBook/wwwroot\images/products/Shapes050.jpg", myImageCodecInfo, myEncoderParameters);




                using (MemoryStream ms = new MemoryStream())
                {
                    myEncoderParameter = new EncoderParameter(myEncoder, 75L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    myBitmap.Save(ms, myImageCodecInfo, myEncoderParameters);
                    formFile.retriveImage = ms.ToArray();
                    Console.WriteLine();


                }

                return formFile;
            }
            else
            {
                return null;
            }
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        public IActionResult Privacy()
        {
            return View(aaaa());
        }


        [HttpPost]
        public IActionResult Privacy(ImageUplode image)
        {
            //foreach (var file in Request.Form.Files)
            //{


            //    using (var ms = new MemoryStream())
            //    {
            //        file.CopyTo(ms);
            //        image.retriveImage = ms.ToArray();
            //    }


            //}



            return View(aaaa(image));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}