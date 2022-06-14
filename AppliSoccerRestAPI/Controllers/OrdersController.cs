using AppliSoccerEngine;
using AppliSoccerEngine.Orders;
using AppliSoccerObjects.Modeling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliSoccerRestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly OrdersManager _ordersManager;
        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpPut]
        public async Task<bool> CreateOrder(Order order)
        {
            try
            {
                _logger.LogInformation("Got CreateOrder request from " + order.SenderId);
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("Order details : " + JsonConvert.SerializeObject(order).ToString());
                }
                bool isSuccess = await _ordersManager.CreateOrder(order);
                _logger.LogInformation($"Is creating order succeed? ${isSuccess}");
                return isSuccess;
            }catch(Exception ex)
            {
                _logger.LogError("Creating order has encountered error", ex.Message);
                return false;
            }
        }

        [HttpGet]
        public async Task<List<Order>> GetOrders(string receiverId, DateTime fromTime, DateTime endTime)
        {
            _logger.LogInformation($"Got request for GetOrders. Parameters: receiverId: ${receiverId} fromTime:${fromTime.ToString()}" +
                $"endTime:${endTime.ToString()}");
            try
            {
                List<Order> orders = await _ordersManager.GetOrders(receiverId, fromTime, endTime);
                if(orders != null)
                {
                    _logger.LogInformation("GotOrders has succeed!");
                    if (_logger.IsEnabled(LogLevel.Debug))
                    {
                        _logger.LogDebug($"Orders that fetched from DB: ${JsonConvert.SerializeObject(orders).ToString()}");
                    }
                    return orders;
                }
            }catch(Exception ex)
            {
                _logger.LogError("Exception has occurred while trying get orders.", ex);
            }
            _logger.LogInformation("GetOrders has failed");
            return null;
        }
    }
}
