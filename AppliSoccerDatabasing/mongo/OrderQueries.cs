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

        public Task<List<OrderReceiving>> FetchOrdersMetadata(DateTime upperBoundDate, int ordersQuantity, string receiverId)
        {
            return Task.Run(() => {
                var sameIdFilter = Builders<OrderReceivingDBModel>.Filter.Eq(orderReceiving => orderReceiving.ReceiverId, receiverId);
                var desSort = Builders<OrderReceivingDBModel>.Sort.Descending("Order.SendingDate");
                var filters = sameIdFilter;
                var orderReceivingDbModels =
                    _orderReceivingCollection
                    .Find(filters)
                    .Sort(desSort)
                    .ToList();

                // Work around :
                var filterExactTime = orderReceivingDbModels.Where(o => o.Order.SendingDate.Ticks < upperBoundDate.Ticks).Take(ordersQuantity).ToList();
                var output = DBModelConverter.ConvertOrderReceivingList(filterExactTime);
                return output;
            });
        }

        public Task<List<OrderReceiving>> PullNewOrdersMetadata(DateTime lowerBoundDate, int ordersQuantity, string receiverId)
        {
            return Task.Run(() => {
                var sameIdFilter = Builders<OrderReceivingDBModel>.Filter.Eq(orderReceiving => orderReceiving.ReceiverId, receiverId);
                var afterDateFilter = Builders<OrderReceivingDBModel>.Filter.Gt(OrderReceiving => OrderReceiving.Order.SendingDate, lowerBoundDate);
                var ascSort = Builders<OrderReceivingDBModel>.Sort.Ascending("Order.SendingDate");
                var filters = sameIdFilter & afterDateFilter;
                var orderReceivingDbModels =
                    _orderReceivingCollection
                    .Find(filters)
                    .Sort(ascSort)
                    .Limit(ordersQuantity).ToList();

                // Work around :
                var filterExactTime = orderReceivingDbModels.Where(o => o.Order.SendingDate.Ticks > lowerBoundDate.Ticks).ToList();
                var output = DBModelConverter.ConvertOrderReceivingList(filterExactTime);
                return output;
            });
        }

        public void MarkOrderAsRead(string orderId, string askerId)
        {
            var orderIdFilter = Builders<OrderReceivingDBModel>.Filter.Eq(orderRec => orderRec.Order.Id, orderId);
            var orderRecOfAsker = Builders<OrderReceivingDBModel>.Filter.Eq(orderRec => orderRec.ReceiverId, askerId);
            var orderRecFilter = orderIdFilter & orderRecOfAsker;
            OrderReceivingDBModel matchOrderRec = _orderReceivingCollection.Find(orderRecFilter).FirstOrDefault();
            matchOrderRec.WasRead = true;
            _orderReceivingCollection.ReplaceOneAsync(orderRecFilter, matchOrderRec);

            var orderFromDB = GetOrderFromDB(orderId);
            if (orderFromDB == null || !orderFromDB.MemberIdsReceivers.Contains(askerId))
                return;
            if (!orderFromDB.MembersIdsAlreadyRead.Contains(askerId))
            {
                orderFromDB.MembersIdsAlreadyRead.Add(askerId);
            }
            var filter = Builders<OrderDBModel>.Filter.Eq(order => order.Id, orderId);
            _ordersCollection.ReplaceOneAsync(filter, orderFromDB);
        }

        public Order GetOrder(string orderId)
        {
            OrderDBModel orderDBModel = _ordersCollection.Find(o => o.Id.Equals(orderId)).FirstOrDefault();
            if (orderDBModel == null)
                return null;
            return DBModelConverter.ConvertOrder(orderDBModel);
        }

        // TODO - unify upper & lower date search
        public List<Order> GetOrdersFromDateUp(DateTime lowerBoundDate, int ordersQuantity, string senderId)
        {
            var senderIdFilter = Builders<OrderDBModel>.Filter.Eq(odm => odm.SenderId, senderId);
            var ascSort = Builders<OrderDBModel>.Sort.Ascending("SendingDate");
            List<OrderDBModel> orderDBModelList = _ordersCollection.Find(senderIdFilter).Sort(ascSort).ToList();

            var filteredByExactTime = orderDBModelList.Where(o => o.SendingDate.Ticks > lowerBoundDate.Ticks).Take(ordersQuantity).ToList();
            var output = DBModelConverter.ConvertOrders(filteredByExactTime);
            return output;
        }

        public List<Order> GetOrders(DateTime upperBoundDate, int ordersQuantity, string senderId)
        {
            var senderIdFilter = Builders<OrderDBModel>.Filter.Eq(odm => odm.SenderId, senderId);
            var descSort = Builders<OrderDBModel>.Sort.Descending("SendingDate");
            List<OrderDBModel> orderDBModelList = _ordersCollection.Find(senderIdFilter).Sort(descSort).ToList();

            var filteredByExactTime = orderDBModelList.Where(o => o.SendingDate.Ticks < upperBoundDate.Ticks).Take(ordersQuantity).ToList();
            var output = DBModelConverter.ConvertOrders(filteredByExactTime);
            return output;

        }

        public Task<Order> GetOrder(string orderId, string askerId)
        {
            return Task.Run( () => {

                var orderFromDB = GetOrderFromDB(orderId);
                if (orderFromDB == null)
                    return null;
                return DBModelConverter.ConvertOrder(orderFromDB);
            });
        }

        private OrderDBModel GetOrderFromDB(string orderId)
        {
            List<OrderDBModel> orderFromDBList = _ordersCollection.Find(o => o.Id.Equals(orderId)).ToList();
            if (orderFromDBList == null || orderFromDBList.Count == 0)
            {
                return null;
            }
            var orderFromDB = orderFromDBList.First();
            return orderFromDB;
        }
    }
}