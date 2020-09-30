using KFC_Clone.Models;
using KFC_Clone.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KFC_Clone.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private static List<Order> _orders = new List<Order>();

        public ActionResult CartOrders()
        {
            return View();
        }


        public ActionResult GetPromotionsPage()
        {
            ViewBag.Amount = _orders.Count;

            return View();
        }

        [HttpPost]
        public ActionResult GetPromotionsPage(int Id, decimal Price, string Title, int Quantity = 1)
        {
            try
            {
                Order order = new Order()
                {
                    OrderId = Id,
                    Price = Price,
                    Title = Title,
                    Quantity = Quantity
                };

                var currOrder = _orderService.GetOrderById(Id, _orders);

                if (currOrder != null)
                {
                    AddOrdersToSessions(_orders, currOrder);

                    return Json(new { success = true, message = "Order Updated", amount = _orders.Count }, JsonRequestBehavior.AllowGet);
                }

                _orders.Add(order);

                AddOrdersToSessions(_orders);

                return Json(new { success = true, message = "Order Added", amount = _orders.Count }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult RemoveOrder(int id)
        {
            var order = _orderService.GetOrderById(id, _orders);

            if (order != null)
            {
                _orders.Remove(order);

                AddOrdersToSessions(_orders);

                return Json(new
                {
                    success = true,
                    message = "Order Removed",
                    html = RazorViewManager.RenderRazorViewToString(this, "CartOrders")
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, message = "Error Occurred"}, JsonRequestBehavior.AllowGet);
        }

        private void AddOrdersToSessions(IEnumerable<Order> orders, Order existingOrder = null)
        {
            if (existingOrder != null)
            {
                orders = _orderService.UpdateExistingOrder(orders, existingOrder);
            }

            _orders = orders as List<Order>;

            Session["orders"] = orders;
            Session["totalPrice"] = _orderService.CalculateTotalOrderPrice(orders);
            Session["count"] = orders.Count();
        }
    }
}