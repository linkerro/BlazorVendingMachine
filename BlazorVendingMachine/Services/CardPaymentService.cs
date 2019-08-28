using BlazorVendingMachine.Events;
using Microsoft.JSInterop;
using System;
using System.Reactive.Subjects;
using System.Threading;

namespace BlazorVendingMachine.Services
{
    public class CardPaymentService
    {
        private IJSRuntime _jsRuntime;

        public Subject<CardEvent> CardSystem { get; } = new Subject<CardEvent>();

        public CardPaymentService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            var cardValue = 0d;
            CardSystem.Subscribe(e =>
            {
                switch (e)
                {
                    case ActivateCardReaderEvent activateCardReader:
                        cardValue = activateCardReader.PaymentSum;
                        WaitForPayment(cardValue);
                        break;
                }
            });
        }

        private void WaitForPayment(double cardValue)
        {
            Thread.Sleep(2000);

            _jsRuntime.InvokeAsync<object>("console.log", "Payment request");

            if (new Random().Next(100) % 2==0)
            {
                _jsRuntime.InvokeAsync<object>("console.log", $"Payment success {cardValue}");
                CardSystem.OnNext(new PaymentSuccessfulEvent { PaymentSum = cardValue });
            }
            else
            {
                _jsRuntime.InvokeAsync<object>("console.log", $"Payment fail {cardValue}");
                CardSystem.OnNext(new PaymentFailedEvent());
            }
        }
    }
}
