namespace BlazorVendingMachine.Events
{
    public class CardEvent:Event
    {
    }

    public class ActivateCardReaderEvent : CardEvent
    {
        public double PaymentSum { get; set; }
    }

    public class PaymentSuccessfulEvent:CardEvent
    {
        public double PaymentSum { get; set; }
    }

    public class PaymentFailedEvent:CardEvent {}

    public class DeactivateReaderEvent:CardEvent { }
}