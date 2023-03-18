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
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<OrderDetail,OrderItemModel>().ReverseMap();
            CreateMap<Cart, CartModel>().ReverseMap();
            CreateMap<CartItem, CartItemModel>().ReverseMap();
        }
    }
}
