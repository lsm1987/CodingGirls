using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    /// <summary>
    /// 인게임 프레젠테이션 레이아웃 관련 정의
    /// </summary>
    public class PresentationLayout : MonoBehaviour
    {
        /// <summary>
        /// 레이아웃에 속한 텍스트들
        /// </summary>
        [SerializeField]
        private List<Text> _texts;

        /// <summary>
        /// 프레젠테이션에 레이아웃 텍스트 등록
        /// </summary>
        public void RegisterTexts(Presentation presentation)
        {
            if (_texts != null)
            {
                for (int i = 0; i < _texts.Count; ++i)
                {
                    var text = _texts[i];
                    presentation.RegisterText(text.name, text);
                }
            }
        }

        /// <summary>
        /// 프레젠테이션에서 레이아웃 텍스트 등록 해제
        /// </summary>
        public void UnregisterTexts(Presentation presentation)
        {
            if (_texts != null)
            {
                for (int i = 0; i < _texts.Count; ++i)
                {
                    var text = _texts[i];
                    presentation.UnregisterText(text.name);
                }
            }
        }
    }
}