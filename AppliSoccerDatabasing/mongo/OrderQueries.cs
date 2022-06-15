using AppliSoccerDatabasing.DBModels;
using AppliSoccerObjects.Modeling;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppliSoccerDatabasing.mongo
{
    public class OrderQueries
    {
        
        private IMongoCollection<OrderDBModel> _ordersCollection;
        private IMongoCollection<OrderReceivingDBModel> _orderReceivingCollection;
        
        public OrderQueries(IMongoCollection<OrderDBModel> ordersCollection,
                            IMongoCollection<OrderReceivingDBModel> orderReceivingCollection)
        {
            _ordersCollection = ordersCollection;
            _orderReceivingCollection = orderReceivingCollection;
        }

        public async Task<string> InsertOrder(Order order)
        {
            OrderDBModel orderDBModel = DBModelConverter.ConvertOrder(order);
            await _ordersCollection.InsertOneAsync(orderDBModel);
            return orderDBModel.Id;
        }

        public Task InsertOrderReceiving(List<OrderReceiving> orderReceivings)
        {
            return Task.Run( () => 
            {
                List<OrderReceivingDBModel> orderDBModelList = DBModelConverter.ConvertOrderReceivingList(orderReceivings);
                return _orderReceivingCollection.InsertManyAsync(orderDBModelList);
            });
        }

        public async Task<List<Order>> GetOrders(string receiverId, DateTime fromDate, DateTime endDate)
        {
            var sameIdFilter = Builders<OrderReceivingDBModel>
                .Filter.Eq(orderReceiving => orderReceiving.ReceiverId, receiverId);
            var afterDateFilter = Builders<OrderReceivingDBModel>
                .Filter.Gt(orderReceiving => orderReceiving.Order.SendingDate, fromDate);
            var beforeDateFilter = Builders<OrderReceivingDBModel>
                .Filter.Lt(orderReceiving => orderReceiving.Order.SendingDate, endDate);
            
            FilterDefinition<OrderReceivingDBModel> totalFilters = sameIdFilter & afterDateFilter & beforeDateFilter;

            List<OrderReceivingDBModel> orderReceivingDBModelList =
                (await _orderReceivingCollection.FindAsync(totalFilters)).ToList();

            List<OrderDBModel> orderDBModelList = 
                orderReceivingDBModelList
                .Select(orderReceivingDBModel => orderReceivingDBModel.Order)
                .ToList();

            return DBModelConverter.ConvertOrders(orderDBModelList);
        }

        public Task RemoveOrder(Order order)
        {
            return _ordersCollection.DeleteOneAsync(order => order.Id.Equals(order.Id));
        }
    }
}