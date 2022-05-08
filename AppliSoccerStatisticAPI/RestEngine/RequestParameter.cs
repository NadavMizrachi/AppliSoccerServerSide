﻿/**
 * This class encapsulates data in form of key-vale that should be send to server as parameter
 */
namespace AppliSoccerStatisticAPI.RestEngine
{ 
    public class RequestParameter
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}