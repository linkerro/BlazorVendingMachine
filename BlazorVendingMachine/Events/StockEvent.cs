using BlazorVendingMachine.Models;
using BlazorVendingMachine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVendingMachine.Events
{
    public class StockEvent:Event
    {
    }

    public class RestockEvent:StockEvent
    {
        public IEnumerable<Rack> Racks { get; set; }
    }

    public class RequestDrinkEvent:StockEvent
    {
        public Drink Drink { get; set; }
    }

    public class DrinkDeliverySuccess:StockEvent
    {
        public Drink Drink { get; set; }
    }

    public class DrinkDeliveryFailure : StockEvent
    {
        public DrinkDeliveryFailureReasons Reason { get; set; }
    }

    public class RequestStockStatus : StockEvent { }

    public class StockStatusResponse : StockEvent
    {
        public IEnumerable<Drink> AvailableDrinks { get; set; } 
    }

    public enum DrinkDeliveryFailureReasons
    {
        Jam,
        OutOfDrink
    }
}
