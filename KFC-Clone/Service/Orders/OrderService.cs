using KFC_Clone.Models.DBModels;
using KFC_Clone.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KFC_Clone.Models
{
    public class OrderService : IOrderService
    {
        public decimal CalculateTotalOrderPrice(IEnumerable<Order> orders)
        {
            decimal _totalPrice = 0;
            var _orders = orders as List<Order>;

            foreach (var order in _orders)
            {
                _totalPrice += order.Price;
            }
            return _totalPrice;
        }

        public IEnumerable<Order> UpdateExistingOrder(IEnumerable<Order> orders, Order existingOrder)
        {
            var _orders = orders as List<Order>;
            int _orderPos = _orders.IndexOf(existingOrder);

            _orders[_orderPos].Quantity += 1;
            _orders[_orderPos].Price += existingOrder.Price;

            return _orders;
        }

        public Order GetOrderById(int id, IEnumerable<Order> orders)
        {
            var order = orders.Where(x => x.OrderId == id).SingleOrDefault();

            return order;
        }

        public void SaveOrderingHistory(IEnumerable<Order> orders, IRepository<User> repository)
        {
            string currentUserName = HttpContext.Current.User.Identity.Name;
            var currentUserId = HttpContext.Current.Request.Cookies[currentUserName].Values.Get("Id");
            
            if (currentUserId != null)
            {
                var currentUser = repository.TableNoTracking.Where(
                    user => user.Id == int.Parse(currentUserId)).FirstOrDefault();

                foreach (var item in orders)
                {
                    currentUser.Orders.Add(item);
                }
                repository.Save();
            }
        }
    }
}