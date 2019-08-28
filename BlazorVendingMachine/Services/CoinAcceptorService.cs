using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace BlazorVendingMachine.Services
{
    public class CoinAcceptorService
    {
        public Subject<double> CoinInsertion { get; } = new Subject<double>();
    }
}
