using UnityEngine;
using System;
using System.Collections.Generic;
using live2d;
using live2d.framework;

public class L2DModel : L2DBaseModel
{
    private L2DModelProxy _proxy;
    private string _modelHomeDir;
    private Vector3 _modelRenderPos;
    private ModelSetting _modelSetting = null;
    public bool _IsActivate { get; private set; }
    private bool _updatedOnce = false;  // 한번이라도 업데이트 되었는가. model.draw() 前に model.update() が呼ばれていない可能性があります 메시지 방지용
    private string _curMotionName = "";
    private bool _curMotionLoop = false;

    public void Initialize(L2DModelInfo modelInfo, int modelSlotIdx)
    {
        setUpdating(true);  // 모델 갱신중
        setInitialized(false);

        _IsActivate = true;
        _modelHomeDir = FileManager.getDirName(modelInfo._SettingFullPath);
        _modelRenderPos = new Vector3(modelSlotIdx * 5.0f, 60.0f, 0.0f);

        GameObject proxyObj = new GameObject(modelInfo._modelName);
        _proxy = proxyObj.AddComponent<L2DModelProxy>();
        _proxy.Initialize(this, modelInfo._planeSize, _modelRenderPos);

        string modelJson = Live2DFramework.getPlatformManager().loadString(modelInfo._SettingFullPath);
        _modelSetting = new ModelSettingJson(modelJson);
        startTimeMSec = UtSystem.getUserTimeMSec();
        _updatedOnce = false;

        // Live2D Model
        if (_modelSetting.GetModelFile() != null)
        {
            loadModelData(_modelHomeDir + _modelSetting.GetModelFile());

            var len = _modelSetting.GetTextureNum();
            for (int i = 0; i < len; i++)
            {
                loadTexture(i, _modelHomeDir + _modelSetting.GetTextureFile(i));
            }
        }

        // 모션은 미리 읽지 않고 각 모션 재생 시도 시 읽어들임

        // Expression
        if (_modelSetting.GetExpressionNum() != 0)
        {
            var len = _modelSetting.GetExpressionNum();
            for (int i = 0; i < len; i++)
            {
                loadExpression(_modelSetting.GetExpressionName(i), _modelHomeDir + _modelSetting.GetExpressionFile(i));
            }
        }

        // Physics
        if (_modelSetting.GetPhysicsFile() != null)
        {
            loadPhysics(_modelHomeDir + _modelSetting.GetPhysicsFile());
        }

        // Pose
        if (_modelSetting.GetPoseFile() != null)
        {
            loadPose(_modelHomeDir + _modelSetting.GetPoseFile());
        }

        // 파라미터 초기값
        for (int i = 0; i < _modelSetting.GetInitParamNum(); i++)
        {
            string id = _modelSetting.GetInitParamID(i);
            float value = _modelSetting.GetInitParamValue(i);
            live2DModel.setParamFloat(id, value);
        }

        // 파츠
        for (int i = 0; i < _modelSetting.GetInitPartsVisibleNum(); i++)
        {
            string id = _modelSetting.GetInitPartsVisibleID(i);
            float value = _modelSetting.GetInitPartsVisibleValue(i);
            live2DModel.setPartsOpacity(id, value);
        }

        // 눈 자동깜빡임
        eyeBlink = new L2DEyeBlink();

        // 레이아웃. 적용 순서에 따라 결과 달라짐에 주의
        getModelMatrix().multScale(modelInfo._layout._scale, modelInfo._layout._scale);
        getModelMatrix().setY(modelInfo._layout._y);

        setUpdating(false); // 갱신상태 완료
        setInitialized(true);
    }

    public void OnRemove()
    {
        UnityEngine.Object.Destroy(_proxy._Go);
    }

    public void Update()
    {
        if (!isInitialized() || isUpdating())
        {
            return;
        }

        long timeMSec = UtSystem.getUserTimeMSec() - startTimeMSec;
        double timeSec = timeMSec / 1000.0;
        double t = timeSec * 2 * Math.PI;// 2πt

        // 루프 체크
        if (_curMotionLoop && mainMotionManager.isFinished())
        {
            StartMotion(_curMotionName, false);
        }

        live2DModel.loadParam();// 이전 프레임에 세이브한 상태 로드
        bool update = mainMotionManager.updateParam(live2DModel);// モーションを更新

        if (!update)
        {
            // 재생중인 모션이 없을 때. 애니 재생 후 마지막 프레임이 유지되고 있을 때도 update == false
            eyeBlink.updateParam(live2DModel);
        }
        live2DModel.saveParam();// 상태 보존

        // 표정 파라미터 갱신(상대변화)
        expressionManager.updateParam(live2DModel);

        // 호흡 등
        live2DModel.addToParamFloat(L2DStandardID.PARAM_ANGLE_X, (float)(15 * Math.Sin(t / 6.5345)), 0.5f);
        live2DModel.addToParamFloat(L2DStandardID.PARAM_ANGLE_Y, (float)(8 * Math.Sin(t / 3.5345)), 0.5f);
        live2DModel.addToParamFloat(L2DStandardID.PARAM_ANGLE_Z, (float)(10 * Math.Sin(t / 5.5345)), 0.5f);
        live2DModel.addToParamFloat(L2DStandardID.PARAM_BODY_ANGLE_X, (float)(4 * Math.Sin(t / 15.5345)), 0.5f);
        live2DModel.setParamFloat(L2DStandardID.PARAM_BREATH, (float)(0.5f + 0.5f * Math.Sin(t / 3.2345)), 1);

        // 물리연산 파라미터 갱신
        if (physics != null) physics.updateParam(live2DModel);

        // 포즈 설정
        if (pose != null) pose.updateParam(live2DModel);

        live2DModel.update();

        if (!_updatedOnce)
        {
            _updatedOnce = true;
        }
    }

    public void Draw()
    {
        if (!_updatedOnce)
        {
            return;
        }

        Matrix4x4 modelMatrix = Matrix4x4.identity;
        var array = getModelMatrix().getArray();
        for (int i = 0; i < 16; i++)
        {
            modelMatrix[i] = array[i];
        }

        Quaternion rotation = Quaternion.identity;
        Vector3 scale = Vector3.one;
        Matrix4x4 m = Matrix4x4.TRS(_modelRenderPos, rotation, scale);
        GetLive2DModelUnity().setMatrix(m * modelMatrix);

        live2DModel.draw();
    }

    /// <summary>
    /// 지정한 모션 재생
    /// <para>우선순위, 동일 애니 재생여부 고려하지 않고 진행중인 애니메이션 덮어 씀</para>
    /// </summary>
    private void StartMotion(string group, bool noFade)
    {
        const int no = 0;
        string motionName = _modelSetting.GetMotionFile(group, no);
        if (string.IsNullOrEmpty(motionName))
        {
            Debug.LogError("Motion name is invalid. group:" + group + " no:" + no.ToString());
            return;
        }

        AMotion motion = null;
        string name = group + "_" + no;
        if (motions.ContainsKey(name))
        {
            motion = motions[name];
        }
        if (motion == null)
        {
            motion = loadMotion(name, _modelHomeDir + motionName);
        }
        if (motion == null)
        {
            Debug.LogError("Failed to read the motion." + motionName);
            return;
        }

        int fadeIn = (noFade) ? 0 : _modelSetting.GetMotionFadeIn(group, no);
        int fadeOut = (noFade) ? 0 : _modelSetting.GetMotionFadeOut(group, no);
        motion.setFadeIn(fadeIn);
        motion.setFadeOut(fadeOut);

        mainMotionManager.startMotionPrio(motion, Define.L2D._priorityForce);
    }

    public void SetMotion(string name, bool loop, bool noFade)
    {
        _curMotionName = name;
        _curMotionLoop = loop;
        StartMotion(name, noFade);
    }

    public void SetExpression(string name, bool noFade)
    {
        if (!expressions.ContainsKey(name))
        {
            Debug.LogError("[L2DModel.InvalidExpresssion]" + name);
            return;
        }
        AMotion motion = expressions[name];
        int fadeIn = (noFade) ? 0 : Define.L2D._expressionFadeDefault;
        int fadeOut = (noFade) ? 0 : Define.L2D._expressionFadeDefault;
        motion.setFadeIn(fadeIn);
        motion.setFadeOut(fadeOut);
        expressionManager.startMotion(motion, false);
    }

    public void ClearExpression(bool noFade)
    {
        SetExpression("empty", noFade);
    }

    public Live2DModelUnity GetLive2DModelUnity()
    {
        return (Live2DModelUnity)live2DModel;
    }

    public void SetActivate(bool activate)
    {
        if (activate && !_IsActivate)
        {
            // 비활성화 상태에서 활성화 될 때
            startTimeMSec = UtSystem.getUserTimeMSec();
        }
        _IsActivate = activate;
        _proxy.SetActivate(activate);
    }

    public void SetAlpha(float alpha)
    {
        _proxy.SetAlpha(alpha);
    }

    public void SetEyeBlinkEnabled(bool enabled)
    {
        eyeBlink.SetEnabled(enabled);
    }

    public void SetPosition(Vector3 position)
    {
        _proxy._Trans.position = position;
    }
}