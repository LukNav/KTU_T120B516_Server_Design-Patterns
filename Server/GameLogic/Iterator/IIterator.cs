namespace Server.GameLogic.Iterator
{
    public interface IIterator<T>
    {
        public abstract void IsEndless(bool isEndless);
        public abstract T First();
        public abstract T Next();
        public abstract bool IsDone();
        public abstract T CurrentItem();
    }
}
