using AutoMapper;
using MyApiWebCore.Data;
using MyApiWebCore.Models;

namespace MyApiWebCore.Helpers
{
    public class ApplicationMapper : Profile 
    {
        public ApplicationMapper()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
        }
    }
}
