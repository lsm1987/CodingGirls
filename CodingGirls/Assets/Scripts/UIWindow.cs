using UnityEngine;

public class UIWindow : MonoBehaviour
{
    private GameObject _go;
    public GameObject _Go { get { if (_go == null) { _go = gameObject; } return _go; } }
    private Transform _trans;
    public Transform _Trans { get { if (_trans == null) { _trans = transform; } return _trans; } }
    private RectTransform _rectTrans;
    public RectTransform _RectTrans { get { if (_rectTrans == null) { _rectTrans = GetComponent<RectTransform>(); } return _rectTrans; } }

    public void SetActivate(bool activate)
    {
        _Go.SetActive(activate);
    }

    public bool IsActivate()
    {
        return _Go.activeSelf;
    }

    public void SetPosition(Vector2 pos)
    {
        _RectTrans.localPosition = new Vector3(pos.x, pos.y, _RectTrans.localPosition.z);
    }
}