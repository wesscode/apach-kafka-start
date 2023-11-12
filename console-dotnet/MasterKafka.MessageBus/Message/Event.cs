namespace MasterKafka.MessageBus.Message
{
    public class Event
    {
        public DateTime TimeStamp { get; set; }
        protected Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}