using KFC_Clone.Models.DBModels;
using KFC_Clone.Models.Repository;
using System.Collections.Generic;

namespace KFC_Clone.Models
{
    public interface IOrderService
    {
        Order GetOrderById(int id, IEnumerable<Order> orders);
        decimal CalculateTotalOrderPrice(IEnumerable<Order> orders);
        IEnumerable<Order> UpdateExistingOrder(IEnumerable<Order> orders, Order existingOrder);
        void SaveOrderingHistory(IEnumerable<Order> orders, IRepository<User> repository);
    }
}