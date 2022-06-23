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
            _ordersManager = new OrdersManager();
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
                _logger.LogError(ex, "Creating order has encountered error");
                return false;
            }
        }

        [HttpGet]
        public async Task<SentOrderWithReceiversInfo> GetSentOrder(string orderId)
        {
            _logger.LogInformation($"Got request for GetOrder. Order Id : {orderId}");
            try
            {
                SentOrderWithReceiversInfo sentOrder = await _ordersManager.GetOrder(orderId);
                if(sentOrder != null)
                {
                    _logger.LogInformation("Gor order successfully!");
                    if (_logger.IsEnabled(LogLevel.Debug))
                    {
                        _logger.LogDebug($"Order that fetched from DB: {JsonConvert.SerializeObject(sentOrder)}");
                    }
                    return sentOrder;
                }
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Got order has failed");
            }
            return null;
        }

        [HttpGet]
        public async Task<List<Order>> GetOrders(string receiverId, DateTime fromTime, DateTime endTime)
        {
            _logger.LogInformation($"Got request for GetOrders. Parameters: receiverId: {receiverId} fromTime: {fromTime}" +
                $"endTime:${endTime.ToString()}");
            try
            {
                List<Order> orders = await _ordersManager.GetOrders(receiverId, fromTime, endTime);
                if(orders != null)
                {
                    _logger.LogInformation("GotOrders has succeed!");
                    if (_logger.IsEnabled(LogLevel.Debug))
                    {
                        _logger.LogDebug($"Orders that fetched from DB: {JsonConvert.SerializeObject(orders)}");
                    }
                    return orders;
                }
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Exception has occurred while trying get orders.");
            }
            _logger.LogInformation("GetOrders has failed");
            return null;
        }

        [HttpGet]
        public async Task<List<OrderMetadata>> FetchOrdersMetadata(DateTime upperBoundDate, int ordersQuantity, String receiverId)
        {
            _logger.LogInformation($"Got request of FetchOrdersMetadata for receiver ID: ${receiverId}");
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Upper Date: ${upperBoundDate}. Orders quantity: ${ordersQuantity}");
            }
            try
            {
                List<OrderMetadata> output = await _ordersManager.FetchOrdersMetadata(upperBoundDate, ordersQuantity, receiverId);
                if(output != null)
                {
                    if (_logger.IsEnabled(LogLevel.Debug))
                    {
                        _logger.LogDebug($"Fetched ${output.Count} orders metadata: ${JsonConvert.SerializeObject(output)}");
                    }
                    _logger.LogInformation($"Fetched successfully ${output.Count} orders metadata.");
                    return output;
                }
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error has occurred durring trying fetch order metadata");
            }
            return null;
        }

        [HttpGet]
        public async Task<List<OrderMetadata>> PullNewOrdersMetadata(DateTime lowerBoundDate, int ordersQuantity, String receiverId)
        {
            _logger.LogInformation($"Got request of PullNewOrdersMetadata for receiver ID: ${receiverId}");
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Lower Date: ${lowerBoundDate}. Orders quantity: ${ordersQuantity}");
            }
            try
            {
                List<OrderMetadata> output = await _ordersManager.PullNewOrdersMetadata(lowerBoundDate, ordersQuantity, receiverId);
                if(output != null)
                {
                    if (_logger.IsEnabled(LogLevel.Debug))
                    {
                        _logger.LogDebug($"Fetched ${output.Count} orders metadata: ${JsonConvert.SerializeObject(output)}");
                    }
                    _logger.LogInformation($"Fetched successfully ${output.Count} orders metadata.");
                    return output;
                }
            }catch(Exception ex)
            {
                _logger.LogError(ex, "Error has occurred durring trying fetch order metadata");
            }
            return null;
        }


        [HttpGet]
        public async Task<OrderPayload> GetOrderPayload(string orderId, string askerId)
        {
            _logger.LogInformation($"Got request for GotOrder. Order Id : ${orderId}. asker ID ${askerId}");
            try
            {
                OrderPayload orderPayload = await _ordersManager.GetOrder(orderId, askerId);
                if(orderPayload == null)
                {
                    _logger.LogInformation("Order is null");
                    return null;
                }
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug($"order details: ${JsonConvert.SerializeObject(orderPayload)}");
                }
                return orderPayload;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error has occured while trying to get orderId ${orderId}");
            }
            return null;
        }
        

        [HttpGet]
        public async Task<List<OrderMetadata>> FetchOrdersMetadataForSender(DateTime upperBoundDate, int ordersQuantity, String senderId)
        {
            _logger.LogInformation($"Got request of FetchOrdersMetadataForSender . Sender ID: ${senderId}");
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Upper Date: ${upperBoundDate}. Orders quantity: ${ordersQuantity}");
            }
            try
            {
                List<OrderMetadata> output = await _ordersManager.FetchOrdersMetadataForSender(upperBoundDate, ordersQuantity, senderId);
                if (output != null)
                {
                    if (_logger.IsEnabled(LogLevel.Debug))
                    {
                        _logger.LogDebug($"Fetched ${output.Count} orders metadata: ${JsonConvert.SerializeObject(output)}");
                    }
                    _logger.LogInformation($"Fetched successfully ${output.Count} orders metadata.");
                    return output;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error has occurred durring trying fetch order metadata for sender");
            }
            return null;
        }

        [HttpGet]
        public async Task<List<OrderMetadata>> PullNewOrdersMetadataForSender(DateTime lowerBoundDate, int ordersQuantity, String senderId)
        {
            _logger.LogInformation($"Got request of PullNewOrdersMetadataForSender for sender ID: ${senderId}");
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"Lower Date: ${lowerBoundDate}. Orders quantity: ${ordersQuantity}");
            }
            try
            {
                List<OrderMetadata> output = await _ordersManager.PullNewOrdersMetadataForSender(lowerBoundDate, ordersQuantity, senderId);
                if (output != null)
                {
                    if (_logger.IsEnabled(LogLevel.Debug))
                    {
                        _logger.LogDebug($"Fetched ${output.Count} orders metadata: ${JsonConvert.SerializeObject(output)}");
                    }
                    _logger.LogInformation($"Fetched successfully ${output.Count} orders metadata.");
                    return output;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error has occurred durring trying fetch order metadata");
            }
            return null;
        }
    }
}
