using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
    public class ModelManager
    {
        private List<string> _modelSlots = new List<string>();  // <모델정보명>
        private Dictionary<string, L2DModel> _models = new Dictionary<string, L2DModel>(); // <모델정보명, 생성된 모델>

        public L2DModel LoadModel(string modelInfoName)
        {
            L2DModel model = null;
            if (_models.TryGetValue(modelInfoName, out model))
            {
                return model;
            }

            string filepath = Define.L2D._modelInfoRoot + "/" + modelInfoName;
            TextAsset infoText = Resources.Load<TextAsset>(filepath);
            if (infoText == null)
            {
                Debug.LogError("'" + modelInfoName + "' ModelInfo does not exist");
                return null;
            }

            int slotIdx = RegisterToSlot(modelInfoName);
            L2DModelInfo info = JsonUtility.FromJson<L2DModelInfo>(infoText.text);
            model = new L2DModel();
            model.Initialize(info, slotIdx);
            model.SetActivate(false);

            _models.Add(modelInfoName, model);
            return model;
        }

        public void RemoveModel(string modelInfoName)
        {
            L2DModel model = null;
            if (!_models.TryGetValue(modelInfoName, out model))
            {
                Debug.LogError("[ModelManager.RemoveModel.CannotFindModel]" + modelInfoName);
                return;
            }

            RemoveFromSlot(modelInfoName);
            _models.Remove(modelInfoName);
            model.OnRemove();
        }

        /// <summary>
        /// 빈 슬롯에 모델정보명을 등록한다.
        /// </summary>
        /// <returns>등록된 인덱스</returns>
        private int RegisterToSlot(string modelInfoName)
        {
            for (int i = 0; i < _modelSlots.Count; ++i)
            {
                if (_modelSlots[i] == null)
                {
                    // 빈 슬롯에 할당
                    _modelSlots[i] = modelInfoName;
                    return i;
                }
            }

            // 맨 끝에 추가
            _modelSlots.Add(modelInfoName);
            return (_modelSlots.Count - 1);
        }

        private void RemoveFromSlot(string modelInfoName)
        {
            for (int i = 0; i < _modelSlots.Count; ++i)
            {
                if (_modelSlots[i] == modelInfoName)
                {
                    _modelSlots[i] = null;
                    return;
                }
            }
        }

        public void ShowModel(string name, string motionName, bool motionLoop, string expressionName, Vector3 position, float duration)
        {
            GameSystem._Instance.RegisterTask(ShowModelTask(name, motionName, motionLoop, expressionName, position, duration));
        }

        private IEnumerator ShowModelTask(string name, string motionName, bool motionLoop, string expressionName, Vector3 position, float duration)
        {
            L2DModel model = LoadModel(name);
            if (model == null)
            {
                yield break;
            }

            model.SetActivate(true);
            model.SetEyeBlinkEnabled(false);
            if (expressionName == "")
            {
                model.ClearExpression(true);
            }
            else
            {
                model.SetExpression(expressionName, true);
            }
            model.SetMotion(motionName, motionLoop, true);
            model.SetPosition(position);
            float startTime = Time.time;
            while (Time.time - (startTime + duration) < 0.0f)
            {
                float rate = (Time.time - startTime) / duration;
                model.SetAlpha(Mathf.Lerp(0.0f, 1.0f, rate));
                yield return null;
            }
            model.SetAlpha(1.0f);
        }

        public void HideModel(string name, float duration)
        {
            GameSystem._Instance.RegisterTask(HideModelTask(name, duration));
        }

        private IEnumerator HideModelTask(string name, float duration)
        {
            L2DModel model = GetActiveModel(name);
            if (model == null)
            {
                Debug.LogError("Not exist activated model " + name);
                yield break;
            }

            float startTime = Time.time;
            while (Time.time - (startTime + duration) < 0.0f)
            {
                float rate = (Time.time - startTime) / duration;
                model.SetAlpha(Mathf.Lerp(1.0f, 0.0f, rate));
                yield return null;
            }
            model.SetActivate(false);
        }

        public L2DModel GetActiveModel(string name)
        {
            L2DModel model = null;
            if (_models.TryGetValue(name, out model) && model._IsActivate)
            {
                return model;
            }
            else
            {
                return null;
            }
        }
    }
}