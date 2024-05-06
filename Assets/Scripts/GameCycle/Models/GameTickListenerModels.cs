namespace ShootEmUp.GameCycle.Models
{
    public interface IGameTickListener : IRegistable<IGameTickListener>
    {
    }

    public interface IGameFixedTickableListener : IGameTickListener
    {
        public void FixedTick();
    }

    public interface IGameTickableListener : IGameTickListener
    {
        public void Tick();
    }
}