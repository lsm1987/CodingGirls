public static class Define
{
    public static class SceneName
    {
        public const string _title = "Title";
        public const string _game = "Game";
    }

    public const string _systemRoot = "System";
    public const string _soundManagerPrefabPath = _systemRoot + "/SoundManager";
    public const string _bgmRoot = "BGM";
    public const string _soundRoot = "Sound";
    private const string _scenarioInfoFileName = "ScenarioInfo";
    public const string _scenarioInfoPath = _systemRoot + "/" + _scenarioInfoFileName;
    private const string _scenarioListFileName = "ScenarioList";
    public const string _scenarioListPath = _systemRoot + "/" + _scenarioListFileName;
    public const string _modelRoot = "Model";

    // Live2D 시스템
    public static class L2D
    {
        public const string _root = "Live2D";
        public const string _modelCam = _root + "/ModelCam";
        public const string _modelPlanPrefix = _root + "/Plane_";
        public const string _modelInfoRoot = "ModelInfo";
        public const int _modelRenderTexSize = 1024;
        public const int _priorityNone = 0;
        public const int _priorityIdle = 1;
        public const int _priorityNormal = 2;
        public const int _priorityForce = 3;
        public const int _expressionFadeDefault = 500;
    }

    // 사운드 리소스
    public const string _titleBGM = "tam-n13";
    public const string _menuSelectSound = "decide1";
    public const string _selectSound = "Select";
    // UI 리소스
    public const string _uiTitleMenuPrefabPath = "Title/TitleMenu";
    public const string _uiScenarioListPrefabPath = "Title/ScenarioList";
    public const string _uiAboutPrefabPath = "Title/About";
    public const string _uiInputPrefabPath = "Game/Input";
    public const string _uiMenuPrefabPath = "Game/Menu";
    public const string _uiDialoguePrefabPath = "Game/Dialogue";
    public const string _uiSelectPrefabPath = "Game/Select";
    public const string _uiLinkPrefabPath = "Game/Link";
    public const string _uiLoadingPrefabPath = "Game/Loading";
    // 배경 리소스
    public const string _backgroundPrefabPath = _systemRoot + "/Background";
    public const string _backgroundRoot = "Texture";
    public const string _spritePrefabPath = _systemRoot + "/Sprite";
    public const string _spriteRoot = "Texture/Sprite";
    public const string _presentationRoot = _systemRoot + "/Presentation";

    public const string _mainModelName = "Epsilon";
    public static class Motion
    {
        public const string _idle = "idle";
        public const string _nod = "nod";
        public const string _deny = "deny";
    }

    public const string _foregroundPrefabPath = _systemRoot + "/Foreground";
    public const string _scenarioRoot = "Scenario";
    public const int _invalidLabelIndex = -1;

    // 게임 규칙
    private const int _textSpeed = 120;   // 초당 출력 글자 수
    public const float _textInterval = 1.0f / _textSpeed;   // 글자간 출력 시간
    public const float _waitIconInterval = 0.5f;
    public const float _foregroundCoverDefaultDuration = 1.0f;
}