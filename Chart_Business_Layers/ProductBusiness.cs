using Chart_Business_Layers.Interface;
using Chart_Leader.Repository;
using Chart_Leader.Repository.RepositoryS_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chart_Leader.Repository.ViewModels;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace Chart_Business_Layers
{
    public class ProductBusiness : IproductsBusiness
    {
        /// <summary>
        /// Constructor with parameter unitOfWork db_Entities
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// 
        ProductsRepository productsRepository;
        public ProductBusiness(ProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }


        public Products GetProductByID(int? id)
        {
            Products product = new Products();
            if (id != null)
            {
                //product = productsRepository.GetByID(id);
                SqlParameter ID = new SqlParameter("@Product_id", id);
                product = productsRepository.StoreProcedure_SelectByParameters("EXEC Products_sp_SelectByID @Product_id", ID).ToList<Products>().FirstOrDefault();
            }
            return product;
        }

        List<Products> IproductsBusiness.GetAllProducts()
        {
            //  List<Products> productslist = productsRepository.GetAll().ToList<Products>();
            List<Products> productslist = productsRepository.StoreProcedure_SelectALL("Products_SP_SelectAll_").ToList<Products>();

            return productslist;
        }


        public void Delete(int? id)
        {
            // Products product;
            if (id != null)
            {
                //product = productsRepository.GetByID(id);
                //productsRepository.Remove(product);


                SqlParameter paramID = new SqlParameter("@Product_id", id);
                productsRepository.Excute_Store_CUD(" exec Products_SP_DeletByProduct_id @Product_id", paramID);

            }
        }

        public void Add(Products product)
        {
            SqlParameter[] paramets = new SqlParameter[7];
            paramets[0] = new SqlParameter("@Cat_id", product.Cat_id);
            paramets[1] = new SqlParameter("@Product_Name", product.Product_Name);
            paramets[2] = new SqlParameter("@Product_Price", product.Product_Price);
            paramets[3] = new SqlParameter("@Product_QTY", product.Product_QTY);
            paramets[4] = new SqlParameter("@Product_Description", product.Product_Description);
            paramets[5] = new SqlParameter("@Product_Image", product.Product_Image);
            paramets[6] = new SqlParameter("@Product_Writing_Date", product.Product_Writing_Date.HasValue ? product.Product_Writing_Date : DateTime.Now);

            productsRepository.Excute_Store_CUD("exec Products_SP_Insert @Cat_id, @Product_Name, @Product_Price, @Product_QTY, @Product_Description, @Product_Image, @Product_Writing_Date", paramets);
        }

        public void Update(Products product)
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
            productsRepository.Excute_Store_CUD("exec Products_SP_Update @Cat_id, @Product_Name, @Product_Price, @Product_QTY, @Product_Description, @Product_Image, @Product_Writing_Date, @Product_id", paramets);
        }
    }
}
