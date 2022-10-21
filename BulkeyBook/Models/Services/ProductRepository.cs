using BulkeyBook.Models.DataAccess;
using BulkeyBook.Models.DataAccess.Modul;
using BulkyBook.DataAccess.Repository.IRepository;

using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Encoder = System.Drawing.Imaging.Encoder;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDBContext _db;

        public ProductRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Price100 = obj.Price100;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Author = obj.Author;
                objFromDb.CoverTypeId = obj.CoverTypeId;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;  
                }
            }
        }

        public byte[] aaaa(IFormFile formFile = null)
        {


            Bitmap myBitmap;
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            if (formFile != null)
            {
                myImageCodecInfo = GetEncoderInfo("image/jpeg");

                myEncoder = Encoder.Quality;
                myBitmap = new Bitmap(formFile.OpenReadStream());
                myEncoderParameters = new EncoderParameters(1);

                byte[] retriveImage;
                //myEncoderParameter = new EncoderParameter(myEncoder, 70L);
                //myEncoderParameters.Param[0] = myEncoderParameter;
                //myBitmap.Save(@"E:/BulkeyBook/BulkeyBook/wwwroot\images/products/Shapes050.jpg", myImageCodecInfo, myEncoderParameters);




                using (MemoryStream ms = new MemoryStream())
                {
                    myEncoderParameter = new EncoderParameter(myEncoder, 75L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    myBitmap.Save(ms, myImageCodecInfo, myEncoderParameters);
                    retriveImage = ms.ToArray();
                    Console.WriteLine();


                }

                return retriveImage;
            }
            else
            {
                return null;
            }
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

    }
}
