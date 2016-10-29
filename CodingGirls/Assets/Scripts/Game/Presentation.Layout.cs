using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    /// <summary>
    /// 인게임 프레젠테이션 레이아웃 관련 정의
    /// </summary>
    public partial class Presentation
    {
        [Serializable]
        public class Layout
        {
            /// <summary>
            /// 레이아웃 최상위
            /// </summary>
            [SerializeField]
            private GameObject _root;

            /// <summary>
            /// 레이아웃에 속한 텍스트들
            /// </summary>
            [SerializeField]
            private List<Text> _texts;

            /// <summary>
            /// 레이아웃 이름
            /// </summary>
            public string _Name
            {
                get
                {
                    return _root.name;
                }
            }

            /// <summary>
            /// 지정한 이름의 텍스트를 찾는다.
            /// </summary>
            /// <param name="name">텍스트 이름</param>
            /// <returns>찾지 못하면 null</returns>
            public Text GetText(string name)
            {
                if (_texts != null)
                {
                    for (int i = 0; i < _texts.Count; ++i)
                    {
                        if (_texts[i].name == name)
                        {
                            return _texts[i];
                        }
                    }
                }

                return null;
            }
        }
    }
}