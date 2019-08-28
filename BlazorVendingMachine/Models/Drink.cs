using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorVendingMachine.Models
{
    public class Drink
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }

    public class DrinkComparer : IEqualityComparer<Drink>
    {
        public bool Equals(Drink x, Drink y)
        {
            return x.Name == y.Name && x.Price==x.Price;
        }

        public int GetHashCode(Drink obj)
        {
            return obj.Name.GetHashCode()+obj.Price.ToString().GetHashCode();
        }
    }
}
