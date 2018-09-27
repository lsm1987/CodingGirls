using UnityEngine;

namespace App
{
    /// <summary>
    /// 어플리케이션 전체에서 사용하는 정보
    /// </summary>
    public static class AppSystem
    {
        private static GameSystemParam _gameSystemParam = new GameSystemParam();
        public static GameSystemParam _GameSystemParam { get { return _gameSystemParam; } }

        private static bool _isApplicationInitialized = false;

        public static void TryInitializeApplication()
        {
            if (_isApplicationInitialized)
            {
                return;
            }

            Debug.Log("Initialize Application");
            _isApplicationInitialized = true;

            // 최대한 프레임 높이기. 모바일의 기본 프레임은 30이라고 함.
            // https://docs.unity3d.com/kr/current/ScriptReference/Application-targetFrameRate.html
            Application.targetFrameRate = 300;
        }
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