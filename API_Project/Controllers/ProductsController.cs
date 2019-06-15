using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API_Project.DataTables;
using API_Project.Models;
using API_Project.Repository;

namespace API_Project.Controllers
{
    // [AllowCrossSiteJson]
    public class ProductsController : ApiController
    {
        private leaderEntities_API db = new leaderEntities_API();
        private IRepository<Product> iProductRepositort = null;
        public ProductsController(IRepository<Product> _iProductRepositort)
        {
            this.iProductRepositort = _iProductRepositort;
        }
        [HttpGet]
        [Route("api/Products")]
        // GET: api/Products




        public IEnumerable<Product> GetProducts()
        {
            var products = iProductRepositort.GetAll().ToArray();
            return products;

        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product;
            if (id == 0)
            {
                product = new Product();
            }
            else
                product = iProductRepositort.GetByID(id);

            if (product == null)
            {

                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut]
        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {

            SqlParameter[] paramets = new SqlParameter[8];
            paramets[0] = new SqlParameter("@Cat_id", product.Cat_id);
            paramets[1] = new SqlParameter("@Product_Name", product.Product_Name);
            paramets[2] = new SqlParameter("@Product_Price", product.Product_Price);
            paramets[3] = new SqlParameter("@Product_QTY", product.Product_QTY);
            paramets[4] = new SqlParameter("@Product_Description", product.Product_Description);
            paramets[5] = new SqlParameter("@Product_Image", product.Product_Image);
            paramets[6] = new SqlParameter("@Product_Writing_Date", product.Product_Writing_Date.HasValue ? product.Product_Writing_Date : DateTime.Now);
            paramets[7] = new SqlParameter("@Product_id", product.Product_id);
            db.Database.ExecuteSqlCommand("exec Products_SP_Update @Cat_id, @Product_Name, @Product_Price, @Product_QTY, @Product_Description, @Product_Image, @Product_Writing_Date, @Product_id", paramets);
            //  db.Products_SP_Update(product.Cat_id, product.Product_Name, product.Product_Price, product.Product_QTY, product.Product_Description, product.Product_Image, product.Product_Writing_Date, product.Product_id);
            return Ok("Product Updated successfully");

        }

        [HttpPost]
        // POST: api/Products
        // [ResponseType(typeof(Product))]
        public IHttpActionResult AddProduct(Product product)
        {
            SqlParameter[] paramets = new SqlParameter[7];
            paramets[0] = new SqlParameter("@Cat_id", product.Cat_id);
            paramets[1] = new SqlParameter("@Product_Name", product.Product_Name);
            paramets[2] = new SqlParameter("@Product_Price", product.Product_Price);
            paramets[3] = new SqlParameter("@Product_QTY", product.Product_QTY);
            paramets[4] = new SqlParameter("@Product_Description", product.Product_Description);
            paramets[5] = new SqlParameter("@Product_Image", product.Product_Image);
            paramets[6] = new SqlParameter("@Product_Writing_Date", product.Product_Writing_Date.HasValue ? product.Product_Writing_Date : DateTime.Now);

            db.Database.ExecuteSqlCommand("exec Products_SP_Insert @Cat_id, @Product_Name, @Product_Price, @Product_QTY, @Product_Description, @Product_Image, @Product_Writing_Date", paramets);

            return Ok("Product Added successfully");

        }


        [HttpDelete]

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            SqlParameter paramID = new SqlParameter("@Product_id", id);
            db.Database.ExecuteSqlCommand(" exec Products_SP_DeletByProduct_id @Product_id", paramID);
            //db.Products_SP_DeletByProduct_id(id);
            return Ok("Product Deleted successfully");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Product_id == id) > 0;
        }
    }
}