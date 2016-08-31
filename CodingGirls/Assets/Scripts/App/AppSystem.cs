namespace App
{
    /// <summary>
    /// 어플리케이션 전체에서 사용하는 정보
    /// </summary>
    public static class AppSystem
    {
        private static GameSystemParam _gameSystemParam = new GameSystemParam();
        public static GameSystemParam _GameSystemParam { get { return _gameSystemParam; } }
    }

    /// <summary>
    /// 게임 시스템 시작에 사용할 인자
    /// </summary>
    public class GameSystemParam
    {
        public string _ScenarioName { get; private set; }

        public void Set(string scenarioName)
        {
            _ScenarioName = scenarioName;
        }
    }
}