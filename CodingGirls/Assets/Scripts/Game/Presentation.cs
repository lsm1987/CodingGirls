using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 인게임 프레젠테이션
    /// </summary>
    public partial class Presentation : MonoBehaviour
    {
        /// <summary>
        /// 정의된 레이아웃들
        /// </summary>
        [SerializeField]
        private List<Layout> _layouts;

        private Layout _activeLayout;   // 활성화된 레이아웃

        /// <summary>
        /// 지정한 이름의 레이아웃을 찾는다.
        /// </summary>
        /// <param name="name">레이아웃 이름</param>
        /// <returns>찾지 못하면 null</returns>
        private Layout GetLayout(string name)
        {
            if (_layouts != null)
            {
                for (int i = 0; i < _layouts.Count; ++i)
                {
                    if (_layouts[i]._Name == name)
                    {
                        return _layouts[i];
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 레이아웃을 비운다.
        /// </summary>
        public void ClearLayout()
        {
            if (_activeLayout != null)
            {
                _activeLayout.SetActive(false);
                _activeLayout = null;
            }
        }

        /// <summary>
        /// 레이아웃을 지정한다.
        /// </summary>
        public void SetLayout(string name)
        {
            ClearLayout();

            var layout = GetLayout(name);
            if (layout == null)
            {
                Debug.LogError("[Presentation.SetLayout.NotExistLayout]" + name);
                return;
            }

            layout.SetActive(true);
            _activeLayout = layout;
        }
    }
}