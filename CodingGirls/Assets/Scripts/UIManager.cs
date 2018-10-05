using UnityEngine;
using System.Collections.Generic;

public abstract class UIManager : MonoBehaviour
{
    private Transform _trans;
    public Transform _Trans { get { if (_trans == null) { _trans = transform; } return _trans; } }
    private const string _prefabRoot = "UI";
    
    // 활성화된 윈도우 목록
    // 생성된 순서로 추가됨
    private List<UIWindow> _windows = new List<UIWindow>();

    public UIWindow OpenWindow(string prefabPath)
    {
        return OpenWindow(prefabPath, _Trans);
    }

    public UIWindow OpenWindow(string prefabPath, Transform parent)
    {
        Object prefabWindow = Resources.Load(_prefabRoot + "/" + prefabPath);
        return OpenWindow(prefabWindow, parent);
    }

    public UIWindow OpenWindow(Object prefabWindow, Transform parent)
    {
        UIWindow window = CreateWidget<UIWindow>(prefabWindow, parent);

        if (window)
        {
            _windows.Add(window);
        }

        return window;
    }

    public void CloseWindow(UIWindow ui)
    {
        _windows.Remove(ui);

        Destroy(ui._Go);
    }

    public static T CreateWidget<T>(Object prefabWidget, Transform parent)
        where T : MonoBehaviour
    {
        GameObject objWidget = Instantiate(prefabWidget) as GameObject;
        objWidget.name = prefabWidget.name; // 오브젝트명의 (Clone) 삭제
        objWidget.transform.SetParent(parent, false);

        RectTransform rectTrans = objWidget.GetComponent<RectTransform>();

        if (rectTrans)
        {
            rectTrans.localScale = Vector3.one;
        }
        else
        {
            Debug.LogErrorFormat("[UIManager] Created widget doesn't have RectTransform. prefabWidget: {0}"
                , prefabWidget.name);
        }

        T widgetComponent = objWidget.GetComponent<T>();

        if (!widgetComponent)
        {
            Debug.LogErrorFormat("[UIManager] Created widget doesn't have target component. prefabWidget: {0}, component type:{1}"
                , prefabWidget.name, typeof(T).Name);
        }

        return widgetComponent;
    }

    /// <summary>
    /// UI들에게서 키입력 처리를 시도한다.
    /// </summary>
    /// <returns>true: UI 내에서 키입력 처리가 완료되어 추가 처리하지 않음</returns>
    public bool OnKeyInput()
    {
        if (OnKeyInputOnComponent())
        {
            return true;
        }

        if (OnKeyInputOnWindows())
        {
            return true;
        }

        return false;
    }

    protected virtual bool OnKeyInputOnComponent()
    {
        return false;
    }

    private bool OnKeyInputOnWindows()
    {
        if (_windows.Count > 0)
        {
            // 역순(나중에 추가된 것부터) 순회
            for (int i = _windows.Count - 1; i >= 0; --i)
            {
                UIWindow window = _windows[i];
                if (window != null && window.OnKeyInput())
                {
                    // 키입력 전달하지 않는 윈도우 발생
                    return true;
                }
            }
        }

        return false;
    }
}