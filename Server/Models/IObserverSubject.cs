namespace Server.Models
{
    public interface IObserverSubject
    {
        List<Player> Observers { get; set; }

        string RegisterObserver(Player player);
        void UnregisterObserver(Player player);
        void NotifyAllObservers();
    }
}

