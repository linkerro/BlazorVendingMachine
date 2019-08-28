using BlazorVendingMachine.Events;
using BlazorVendingMachine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace BlazorVendingMachine.Services
{
    public class StockService
    {
        List<Rack> Racks { get; set; }

        public Subject<StockEvent> StockEvents { get; set; } = new Subject<StockEvent>();

        public StockService()
        {
            StockEvents.Subscribe(x =>
            {
                switch (x)
                {
                    case RestockEvent restock:
                        Racks = restock.Racks.ToList();
                        break;
                    case RequestStockStatus request:
                        StockEvents.OnNext(new StockStatusResponse
                        {
                            AvailableDrinks = Racks.Where(r => r.Count > 0)
                            .Select(r => r.Drink)
                            .Distinct(new DrinkComparer())
                            .ToArray()
                        });
                        break;
                    case RequestDrinkEvent drinkRequest:
                        var drinkRack = Racks
                        .Where(r => r.Drink.Name == drinkRequest.Drink.Name && r.Count > 0)
                        .FirstOrDefault();
                        if (drinkRack != null)
                        {
                            drinkRack.Count -= 1;
                            StockEvents.OnNext(new DrinkDeliverySuccess { Drink = drinkRequest.Drink });
                        }
                        else
                        {
                            StockEvents.OnNext(new DrinkDeliveryFailure { Reason = DrinkDeliveryFailureReasons.OutOfDrink });
                        }
                        break;
                }
            });
        }
    }
}
