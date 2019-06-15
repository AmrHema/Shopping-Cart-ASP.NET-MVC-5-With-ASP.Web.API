using Chart_Leader.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chart_Leader.Repository.ViewModels;
using System.Web;

namespace Chart_Business_Layers.Interface
{
    public interface IproductsBusiness
    {
        List<Products> GetAllProducts();
        Products GetProductByID(int? id);
        void Delete (int? id);
        void Add(Products product);
        void Update(Products product);


    }
}
