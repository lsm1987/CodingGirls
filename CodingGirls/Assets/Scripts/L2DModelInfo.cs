using System;

/// <summary>
/// 모델 생성을 위해 필요한 정보
/// </summary>
[Serializable]
public class L2DModelInfo
{
    [Serializable]
    public class Layout
    {
        public float _scale = 1.0f;
        public float _y = 1.0f;
    }

    public string _modelName = string.Empty;
    public string _settingPath = string.Empty;
    public float _planeSize = 2.0f;
    public Layout _layout = new Layout();

    public string _SettingFullPath
    {
        get { return Define._modelRoot + "/" + _settingPath; }
    }

    // 테스트용
    public bool IsSame(L2DModelInfo other)
    {
        if (_modelName != other._modelName)
        {
            return false;
        }
        if (_settingPath != other._settingPath)
        {
            return false;
        }
        if (_planeSize != other._planeSize)
        {
            return false;
        }
        if (_layout._scale != other._layout._scale)
        {
            return false;
        }
        if (_layout._y != other._layout._y)
        {
            return false;
        }
        return true;
    }
}