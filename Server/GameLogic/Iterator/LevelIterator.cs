using Server.Helpers;
using Server.Models;

namespace Server.GameLogic.Iterator
{
    public class LevelIterator : IIterator<GameLevel>
    {
        LevelCollection levelsCollection;
        int current = 0;
        bool isEndless = false;

        public LevelIterator(LevelCollection levelsCollection)
        {
            this.levelsCollection=levelsCollection;
        }

        public GameLevel CurrentItem()
        {
            return levelsCollection.Items[current];
        }

        public GameLevel First()
        {
            return levelsCollection.Items[0];
        }

        public bool IsDone()
        {
            return current >= levelsCollection.Items.Count;
        }

        public GameLevel Next()
        {
            if (isEndless && IsDone())
                current = 0;
            levelsCollection.Items[current].Level = current+1;
            return levelsCollection.Items[current++];
        }

        /// <summary>
        /// Sets if the iterator should loop back to the first item when collection was iterated through
        /// </summary>
        /// <param name="isEndless"></param>
        public void IsEndless(bool isEndless)
        {
            this.isEndless = isEndless;
        }
    }

    public class LevelCollection : IterableCollection<GameLevel>
    {
        public List<GameLevel> Items { get; set; }

        public IIterator<GameLevel> CreateIterator()
        {
            Items = new List<GameLevel>()
            {
                FactoryPresets.GetLevel(1),
                FactoryPresets.GetLevel(2),
                FactoryPresets.GetLevel(3)
            };
            
            return new LevelIterator(this);
        }
    }

    public interface IterableCollection<T>
    {
        List<T> Items { get; set; }
        public IIterator<T> CreateIterator();
    }
}
