using UnityEngine;

public class L2DModelCamera : MonoBehaviour
{
    [SerializeField]
    private Camera _cam;
    private Camera _Cam { get { return _cam; } }
    private Transform _trans;
    private Transform _Trans { get { if (_trans == null) { _trans = transform; } return _trans; } }
    private GameObject _go;
    public GameObject _Go { get { if (_go == null) { _go = gameObject; } return _go; } }

    public void Initialize(string modelName, Vector3 modelRendererPos, RenderTexture targetTexture)
    {
        _Go.name = modelName + "_Cam";

        Vector3 pos = modelRendererPos;
        pos.z -= 10.0f;
        _Trans.position = pos;

        _Cam.targetTexture = targetTexture;
    }

    public void SetActivate(bool activate)
    {
        _Go.SetActive(activate);
    }
}