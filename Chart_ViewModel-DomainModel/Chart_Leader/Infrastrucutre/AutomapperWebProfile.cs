using Chart_DomaineModel;
using Chart_Leader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chart_Leader.Infrastrucutre
{
    public class AutomapperWebProfile:AutoMapper.Profile
    {
        public AutomapperWebProfile()
        {
            //CreateMap<ProductsDomaineModel, ProductsViewModel>()
            //    .ForMember(dest => dest.Product_Writing_Date, opt => opt.MapFrom(src => src.Product_Writing_Date.ToString("MM/dd/yyy hh:mm")));
            //Reverese mapping

            CreateMap<ProductsDomaineModel, ProductsViewModel>();
            CreateMap<ProductsViewModel, ProductsDomaineModel>();


            CreateMap<Products_DomainModel, ProductsViewModel>();
            CreateMap<ProductsViewModel, Products_DomainModel>();

            
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