using UnityEngine;
using System;

public class L2DModelProxy : MonoBehaviour
{
    private GameObject _go;
    public GameObject _Go { get { if (_go == null) { _go = gameObject; } return _go; } }
    private Transform _trans;
    public Transform _Trans { get { if (_trans == null) { _trans = transform; } return _trans; } }
    private L2DModel _model;
    private L2DModelCamera _modelCam;
    private RenderTexture _renderTex;
    private L2DModelPlane _plane;

    public void Initialize(L2DModel model, float planeSize, Vector3 modelRenderPos)
    {
        _model = model;
        _Trans.position = new Vector3(0.0f, -1.0f, 0.0f);
        _renderTex = RenderTexture.GetTemporary(Define.L2D._modelRenderTexSize, Define.L2D._modelRenderTexSize, 16, RenderTextureFormat.ARGB32);

        _plane = CreateModelPlane(planeSize);
        _plane.Initialize(_Trans, _renderTex);

        _modelCam = CreateModelCamera();
        _modelCam.Initialize(gameObject.name, modelRenderPos, _renderTex);
    }

    private void OnRenderObject()
    {
        if (_model == null)
        {
            return;
        }

        _model.Draw();
    }

    private void Update()
    {
        if (_model == null)
        {
            return;
        }

        _model.Update();
    }

    private void OnDestroy()
    {
        RenderTexture.ReleaseTemporary(_renderTex);
        if (_modelCam != null && _modelCam._Go != null)
        {
            Destroy(_modelCam.gameObject);
        }
    }

    private L2DModelPlane CreateModelPlane(float size)
    {
        L2DModelPlane prefab = Resources.Load<L2DModelPlane>(Define.L2D._modelPlanPrefix + size.ToString());
        if (prefab == null)
        {
            Debug.LogError("Size " + size.ToString() + " plane does not exist");
            return null;
        }
        else
        {
            return GameObject.Instantiate<L2DModelPlane>(prefab);
        }
    }

    private L2DModelCamera CreateModelCamera()
    {
        L2DModelCamera prefab = Resources.Load<L2DModelCamera>(Define.L2D._modelCam);
        return GameObject.Instantiate<L2DModelCamera>(prefab);
    }

    public void SetActivate(bool activate)
    {
        _Go.SetActive(activate);
        _modelCam.SetActivate(activate);
    }

    public void SetAlpha(float alpha)
    {
        _plane.SetAlpha(alpha);
    }
}