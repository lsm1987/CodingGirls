using UnityEngine;

public abstract class UIManager : MonoBehaviour
{
    private Transform _trans;
    public Transform _Trans { get { if (_trans == null) { _trans = transform; } return _trans; } }
    private const string _prefabRoot = "UI";

    public UIWindow OpenWindow(string prefabPath)
    {
        return OpenWindow(prefabPath, _Trans);
    }

    public static UIWindow OpenWindow(string prefabPath, Transform parent)
    {
        Object prefabWindow = Resources.Load(_prefabRoot + "/" + prefabPath);
        return OpenWindow(prefabWindow, parent);
    }

    public static UIWindow OpenWindow(Object prefabWindow, Transform parent)
    {
        GameObject objWindow = Instantiate(prefabWindow) as GameObject;
        objWindow.name = prefabWindow.name; // 오브젝트명의 (Clone) 삭제
        UIWindow window = objWindow.GetComponent<UIWindow>();
        window._Trans.SetParent(parent, false);
        window._RectTrans.localScale = Vector3.one;
        return window;
    }

    public void CloseWindow(UIWindow ui)
    {
        Destroy(ui._Go);
    }
}