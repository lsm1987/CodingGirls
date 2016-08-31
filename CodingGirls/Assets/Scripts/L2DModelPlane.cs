using UnityEngine;

public class L2DModelPlane : MonoBehaviour
{
    [SerializeField]
    private float _size;    // 메쉬 한 변의 길이
    private float _Size { get { return _size; } }
    [SerializeField]
    private Renderer _renderer;
    private Transform _trans;
    private Transform _Trans { get { if (_trans == null) { _trans = transform; } return _trans; } }

    public void Initialize(Transform proxyTrans, Texture texture)
    {
        _Trans.parent = proxyTrans;
        _Trans.localPosition = new Vector3(0.0f, _Size / 2.0f, 0.0f);   // 캐릭터 중앙바닥이 모델 원점 되도록
        _Trans.localEulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);
        _Trans.localScale = Vector3.one;

        _renderer.material.SetTexture("_MainTex", texture);
    }

    public void SetAlpha(float alpha)
    {
        const string tintName = "_Color";
        Color color = _renderer.material.GetColor(tintName);
        color.a = alpha;
        _renderer.material.SetColor(tintName, color);
    }
}