using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    /// <summary>
    /// 인게임 프레젠테이션
    /// </summary>
    public class Presentation : MonoBehaviour
    {
        private PresentationLayout _activeLayout;   // 활성화된 레이아웃
        private Dictionary<string, Text> _texts = new Dictionary<string, Text>();    // 등록된 텍스트들

        /// <summary>
        /// 레이아웃을 비운다.
        /// </summary>
        public void ClearLayout()
        {
            if (_activeLayout != null)
            {
                _activeLayout.UnregisterTexts(this);
                Destroy(_activeLayout.gameObject);
                _activeLayout = null;
            }
        }

        /// <summary>
        /// 레이아웃을 지정한다.
        /// </summary>
        public void SetLayout(string layoutName)
        {
            ClearLayout();

            var prefab = Resources.Load<PresentationLayout>(Define._presentationRoot + "/Layout/" + layoutName);
            if (prefab == null)
            {
                Debug.LogError("[Presentation.SetLayout.CannotLoadLayout]" + layoutName);
                return;
            }

            var layout = Instantiate(prefab);
            layout.transform.SetParent(transform, false);
            layout.RegisterTexts(this);
            _activeLayout = layout;
        }

        /// <summary>
        /// 텍스트 등록
        /// </summary>
        public void RegisterText(string name, Text text)
        {
            _texts.Add(name, text);
        }

        /// <summary>
        /// 텍스트 등록 해제
        /// </summary>
        public void UnregisterText(string name)
        {
            _texts.Remove(name);
        }

        /// <summary>
        /// 등록된 텍스트 찾기
        /// </summary>
        public Text GetText(string name)
        {
            Text text;
            _texts.TryGetValue(name, out text);
            return text;
        }
    }
}