namespace ShootEmUp.GameCycle.Models
{
    public interface IGameEventListener : IRegistable<IGameEventListener>
    { }

    public interface IGameStartListener : IGameEventListener
    {
        public void OnGameStart();
    }

    public interface IGameEndListener : IGameEventListener
    {
        public void OnGameEnd();
    }

    public interface IGamePauseListener : IGameEventListener
    {
        public void OnGamePause();
    }

    public interface IGameResumeListener : IGameEventListener
    {
        public void OnGameResume();
    }
}