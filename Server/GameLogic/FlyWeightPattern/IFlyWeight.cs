namespace Server.GameLogic.FlyWeightPattern
{
    public interface IFlyWeight<T>
    {
        T GetType(string key);
    }
}
