using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chart_Business_Layers;
using Chart_Leader.Models;
using Chart_Leader.Repository;
using Chart_Leader.Repository.ViewModels;

namespace Chart_Leader.Infrastrucutre
{
    public class AutomapperWebProfile : AutoMapper.Profile
    {
        public AutomapperWebProfile()
        {
            //CreateMap<ProductsDomaineModel, ProductsViewModel>()
            //    .ForMember(dest => dest.Product_Writing_Date, opt => opt.MapFrom(src => src.Product_Writing_Date.ToString("MM/dd/yyy hh:mm")));
            //Reverese mapping


            CreateMap<CategoriesViewModel, Categories>();
            CreateMap<Categories, CategoriesViewModel>();

            CreateMap<Products, ProductsViewModel>();
            CreateMap<ProductsViewModel, Products>();



            CreateMap<Products, ProductViewModel>();
            CreateMap<ProductViewModel, Products>();


        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<AutomapperWebProfile>();

            });
        }
    }
}