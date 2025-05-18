using System;
using AutoMapper;
using ITI.E_Commerce.Api.Model;
using ITI.E_Commerce.Api.Model.Order;
using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Helper
{
	public class MappingProfiles:Profile
	{
		public MappingProfiles()
		{
            CreateMap<Product, ProductModel>()
            .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
            .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
            .ForMember(d => d.PriceOffer, o => o.MapFrom(s => s.PriceOffer.PromotionalText))
            .ForMember(d => d.Discount, o => o.MapFrom(s => s.Discount))
            .ForMember(d => d.NewPrice, o => o.MapFrom(s => s.PriceOffer.NewPrice))
            .ForMember(d => d.Sail, o => o.MapFrom(s =>s.Discount / (double)s.UnitPrice * 100))
            .ForMember(d => d.ProductImages, o => o.MapFrom(s => s.ProductImages.Select(i => i.Path)));
            CreateMap<Order, Order_Details_response>()
                .ForMember(d => d.OrderDetails, o => o.MapFrom(s => s.OrderDetails))
                .ForMember(d => d.ID, o => o.MapFrom(s => s.ID))
                .ForMember(d => d.CustomerID, o => o.MapFrom(s => s.CustomerID))
                .ForMember(d => d.OrderDate, o => o.MapFrom(s => s.OrderDate))
                .ForMember(d => d.zipcode, o => o.MapFrom(s => s.ShippingAddress.Zipcode))
                .ForMember(d => d.street, o => o.MapFrom(s => s.ShippingAddress.Street))
                .ForMember(d => d.city, o => o.MapFrom(s => s.ShippingAddress.City))
                .ForMember(d => d.state, o => o.MapFrom(s => s.ShippingAddress.State));
            CreateMap<OrderDetails, OrderDetailsModel>()
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Quantity))
                .ForMember(d => d.Product_Name, o => o.MapFrom(s => s.Products.Name))
                .ForMember(d => d.Product_Price, o => o.MapFrom(s => s.Products.UnitPrice))
                .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.TotalPrice))
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductID));
            CreateMap<Brand, BrandModel>();
            CreateMap<Category, CategoryModel>();
            CreateMap<Order, Order_Details_user>()
                .ForMember(d => d.ShippedDate, o => o.MapFrom(s => s.ShippedDate))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status))
                .ForMember(d => d.DeliveryCost, o => o.MapFrom(s => s.DeliveryCost))
                .ForMember(d => d.TotalPrice, o => o.MapFrom(s => s.TotalPrice))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.Shippers.FirstName))
                .ForMember(d => d.zipcode, o => o.MapFrom(s => s.ShippingAddress.Zipcode))
                .ForMember(d => d.street, o => o.MapFrom(s => s.ShippingAddress.Street))
                .ForMember(d => d.city, o => o.MapFrom(s => s.ShippingAddress.City))
                .ForMember(d => d.state, o => o.MapFrom(s => s.ShippingAddress.State))
                .ForMember(d => d.OrderDetails, o => o.MapFrom(s => s.OrderDetails));
          
















        CreateMap<Rate, RateModel>()
            .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.Customer.UserName));


        }
    }
}

