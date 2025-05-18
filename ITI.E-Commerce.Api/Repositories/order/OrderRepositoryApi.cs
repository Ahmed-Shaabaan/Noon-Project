using AutoMapper;
using Castle.Core.Resource;
using ITI.E_Commerce.Api.Model;
using ITI.E_Commerce.Api.Model.Order;
using ITI.E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using static NuGet.Packaging.PackagingConstants;


namespace ITI.E_Commerce.Api.Repositories
{
    public class OrderRepositoryApi : IOrderRepositoryApi
    {


        private readonly MyContext context;
        private readonly IMapper mapper;

        public OrderRepositoryApi(MyContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        //[Authorize]
        public async Task<GetData> CreateNew(OrderCreateModelApi obj)
        {
            GetData resultViewModel = new GetData();
            Customer customer = context.Customers.FirstOrDefault(i => i.Id == obj.CustomerID);
            if (customer == null)
            {
                resultViewModel.Success = false;
                resultViewModel.data = null;
                resultViewModel.Message = "User Not Found";
                return resultViewModel;

            }
            Order newOrder = new Order()

            {
                OrderDate = DateTime.Now,
                CustomerID = obj.CustomerID,
                Status = obj.Status,
            };
            Address address = new Address()
            {
                Street = obj.street,
                City = obj.city,
                State = obj.state,
                Zipcode = obj.zipcode,
                OrderID = newOrder.ID,
                Order = newOrder
            };
            newOrder.ShippingAddress = address;
            List<OrderDetails> newOrderdetails = new List<OrderDetails>();

            foreach (OrderDetailsList product in obj.OrderDetails)
            {

                var product1 = context.Products.FirstOrDefault(i => i.Id == product.ProductId);
                if (product1 == null) { }
                else
                {

                    OrderDetails new_OrderDetails = new OrderDetails();
                    if (product1.PriceOffer == null || product1.PriceOffer.NewPrice == 0)
                    {
                        new_OrderDetails.Quantity = product.Quantity;
                        new_OrderDetails.OrderID = newOrder.ID;
                        new_OrderDetails.TotalPrice = product.Quantity * product1.UnitPrice;
                        new_OrderDetails.ProductID = product.ProductId;
                    }
                    else
                    {
                        new_OrderDetails.Quantity = product.Quantity;
                        new_OrderDetails.OrderID = newOrder.ID;
                        new_OrderDetails.TotalPrice = product.Quantity * product1.PriceOffer.NewPrice;
                        new_OrderDetails.ProductID = product.ProductId;
                    }
                    newOrder.TotalPrice = newOrder.TotalPrice + new_OrderDetails.TotalPrice;
                    //reduce Quantity Product
                    product1.Quantity = product1.Quantity - new_OrderDetails.Quantity;
                    newOrderdetails.Add(new_OrderDetails);
                }
            }
            newOrder.OrderDetails = newOrderdetails;
            await context.Orders.AddAsync(newOrder);
            await context.SaveChangesAsync();
            var data = mapper.Map<Order, Order_Details_response>(newOrder);

            resultViewModel.Success = true;
            resultViewModel.data = data;
            resultViewModel.Message = "Element Create";
            return resultViewModel;

        }
        [Authorize]
        public GetData Delete(int id)
        {
            Order itemDelete = context.Orders.FirstOrDefault(i => i.ID == id);

            GetData resultViewModel = new GetData();
            if (itemDelete != null)
            {
                foreach (var Update_order_Details in itemDelete.OrderDetails)
                {
                    var product1 = context.Products.FirstOrDefault(i => i.Id == Update_order_Details.ProductID);
                    if (product1 == null)
                    {
                        continue;

                    }
                    else
                    {
                        product1.Quantity = product1.Quantity + Update_order_Details.Quantity;

                    }

                }
                //context.orderDetails.Remove(deleteitem);

                context.Orders.Remove(itemDelete);
                context.SaveChanges();
                resultViewModel.Success = true;
                resultViewModel.Message = "Element Delete";
                return resultViewModel;
            }
            else
            {
                resultViewModel.Success = false;
                resultViewModel.Message = "Element Not found";
                return resultViewModel;
            }

        }
        [Authorize]
        public async Task<GetData> Edit(int id_order, OrderCreateModelApi obj)
        {
            Order order = context.Orders.FirstOrDefault(i => i.ID == id_order);
            if (order == null)
            {
                GetData Not_Found = new GetData();
                Not_Found.Success = true;
                Not_Found.Message = "Element  Not Found";
                Not_Found.data = null;
                return Not_Found;
            }
            order.OrderDate = DateTime.Now;
            order.CustomerID = obj.CustomerID;
            order.ShipperID = obj.CustomerID;
            order.Status = obj.Status;
            Address address = new Address()
            {
                Street = obj.street,
                City = obj.city,
                State = obj.state,
                Zipcode = obj.zipcode,
                OrderID = order.ID,
                Order = order
            };
            order.ShippingAddress = address;
            List<OrderDetails> newOrderdetails = new List<OrderDetails>();
            newOrderdetails = order.OrderDetails;
            //product in order
            IEnumerable<int> product_ID = order.OrderDetails.Select(i => i.ProductID);
            //product in update
            IEnumerable<int> product_ID_Update = obj.OrderDetails.Select(i => i.ProductId);
            //test 
            IEnumerable<int> IDS = product_ID.Union(product_ID_Update);
            foreach (var Update_order_Details in IDS.ToList())
            {

                var product1 = context.Products.FirstOrDefault(i => i.Id == Update_order_Details);
                if (product1 == null)
                {
                    continue;

                }

                //if in database in updatre data
                else if (product_ID.Contains(Update_order_Details) && product_ID_Update.Contains(Update_order_Details))
                {
                    int Quantity_product = obj.OrderDetails.FirstOrDefault(i => i.ProductId == Update_order_Details).Quantity;
                    OrderDetails new_OrderDetails = context.orderDetails.FirstOrDefault(i => i.ProductID == Update_order_Details&&i.OrderID==order.ID);
                    order.TotalPrice = order.TotalPrice - new_OrderDetails.TotalPrice;
                    product1.Quantity = product1.Quantity + Quantity_product;
                    if (product1.PriceOffer == null || product1.PriceOffer.NewPrice == 0)
                    {
                        new_OrderDetails.Quantity = Quantity_product;
                        new_OrderDetails.OrderID = order.ID;
                        new_OrderDetails.TotalPrice = Quantity_product * product1.UnitPrice;
                        new_OrderDetails.ProductID = product1.Id;
                    }
                    else
                    {
                        new_OrderDetails.Quantity = Quantity_product;
                        new_OrderDetails.OrderID = order.ID;
                        new_OrderDetails.TotalPrice = Quantity_product * product1.PriceOffer.NewPrice;
                        new_OrderDetails.ProductID = product1.Id;
                    }

                    order.TotalPrice = order.TotalPrice + new_OrderDetails.TotalPrice;
                    //reduce Quantity Product
                    product1.Quantity = product1.Quantity - Quantity_product;
                    context.orderDetails.Update(new_OrderDetails).State = EntityState.Detached;
                    context.SaveChanges();
                }
                else
                {
                    OrderDetails deleteitem = context.orderDetails.FirstOrDefault(i => i.ProductID == Update_order_Details && i.OrderID == order.ID);
                    //if not found orderdetails database create
                    if (deleteitem == null)
                    {
                        OrderDetails new_OrderDetails = new OrderDetails();
                        int Quantity_product = obj.OrderDetails.FirstOrDefault(i => i.ProductId == Update_order_Details).Quantity;
                        if (product1.PriceOffer == null || product1.PriceOffer.NewPrice == 0)
                        {
                            new_OrderDetails.Quantity = Quantity_product;
                            new_OrderDetails.OrderID = order.ID;
                            new_OrderDetails.TotalPrice = Quantity_product * product1.UnitPrice;
                            new_OrderDetails.ProductID = product1.Id;
                        }
                        else
                        {
                            new_OrderDetails.Quantity = Quantity_product;
                            new_OrderDetails.OrderID = order.ID;
                            new_OrderDetails.TotalPrice = Quantity_product * product1.PriceOffer.NewPrice;
                            new_OrderDetails.ProductID = product1.Id;
                        }

                        order.OrderDetails.Add(new_OrderDetails);
                        order.TotalPrice = order.TotalPrice + new_OrderDetails.TotalPrice;
                        //reduce Quantity Product

                        product1.Quantity = product1.Quantity - new_OrderDetails.Quantity;
                        context.orderDetails.Update(new_OrderDetails).State = EntityState.Detached; ;
                    }
                    else
                    {
                        //if  found orderdetails database delete

                        order.TotalPrice = order.TotalPrice - deleteitem.TotalPrice;
                        product1.Quantity = product1.Quantity + deleteitem.Quantity;
                        //context.orderDetails.Remove(deleteitem);
                        context.orderDetails.Remove(deleteitem);
                        context.SaveChanges();
                    }
                }
            }
            //order.OrderDetails = newOrderdetails;
            context.Orders.Update(order).State = EntityState.Detached;
            //context.SaveChanges();
            context.SaveChanges();
            var data = mapper.Map<Order, Order_Details_response>(order);

            GetData resultViewModel = new GetData();
            resultViewModel.Success = true;
            resultViewModel.Message = "Element  update";
            resultViewModel.data = data;
            return resultViewModel;
        }
        //[Authorize]
        public GetData Get(string id_user)
        {
            GetData resultViewModel = new GetData();
           var orders =context.Orders.Where(i => i.CustomerID == id_user).ToList();
            var data = mapper.Map<IEnumerable<Order>, IEnumerable<Order_Details_user>>(orders);
            if (orders == null)
            {
                resultViewModel.Success = false;
                resultViewModel.Message = "Element Not found";
                return resultViewModel;
            }
            else
            {
                resultViewModel.Success = true;
                resultViewModel.Message = "Element  found";
                resultViewModel.data = data;
                return resultViewModel;
            }

        }
        public IEnumerable<ProductModel> get_best_seller()
        {
            GetData resultViewModel = new GetData();
            var products =context.Products.OrderByDescending(info => info.OrderDetails.
            Select(i => new { i.ProductID, i.Quantity }).Sum(i => i.Quantity)).Take(6).ToList();
            var data = mapper.Map<IEnumerable<Models.Product>, IEnumerable<ProductModel>>(products);
            return data;
        }
        public GetData GetProductByCaterogyName(string name)
        {


            Category _category = context.Categories.FirstOrDefault(i => i.Name == name);
            IReadOnlyList<Models.Product> Products = new List<Models.Product>();
            GetData resultViewModel = new GetData();
            if (_category != null)
            {
                Products = context.Products.Where(i => i.CategoryID == _category.Id).ToList();
                if (Products != null)
                {
                    var data = mapper.Map<IEnumerable<Models.Product>, IEnumerable<ProductModel>>(Products);
                    resultViewModel = new GetData()
                    {
                        Success = true,
                        Message = "success",
                        data = data,
                    };
                }
                else
                {
                    resultViewModel = new GetData()
                    {
                        Success = false,
                        Message = "false",
                        data = null,
                    };
                }
            }
            else
            {
                resultViewModel = new GetData()
                {
                    Success = false,
                    Message = "false",
                    data = null,
                };
            }
            return resultViewModel;
        }
    }
}
