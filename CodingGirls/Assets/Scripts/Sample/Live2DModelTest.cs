using UnityEngine;
using System;
using System.Collections;
using live2d;

[ExecuteInEditMode]
public class Live2DModelTest : MonoBehaviour
{
    public TextAsset mocFile;
    public Texture2D[] textureFiles;

    private Live2DModelUnity live2DModel;
    private Matrix4x4 live2DCanvasPos;

    void Start()
    {
        if (live2DModel != null) return;
        Live2D.init();

        live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);
        for (int i = 0; i < textureFiles.Length; i++)
        {
            live2DModel.setTexture(i, textureFiles[i]);
        }

        float modelWidth = live2DModel.getCanvasWidth();
        live2DCanvasPos = Matrix4x4.Ortho(0, modelWidth, modelWidth, 0, -50.0f, 50.0f);
    }

    void Update()
    {
        if (live2DModel == null) return;
        live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);

        if (!Application.isPlaying)
        {
            live2DModel.update();
            return;
        }

        double t = (UtSystem.getUserTimeMSec() / 1000.0) * 2 * Math.PI;
        live2DModel.setParamFloat("PARAM_ANGLE_X", (float)(30 * Math.Sin(t / 3.0)));

        live2DModel.update();
    }

    void OnRenderObject()
    {
        if (live2DModel == null) return;
        live2DModel.draw();
    }
}