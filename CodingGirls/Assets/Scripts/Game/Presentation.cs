using UnityEngine;

namespace Game
{
    /// <summary>
    /// 인게임 프레젠테이션
    /// </summary>
    public class Presentation : MonoBehaviour
    {
        private PresentationLayout _activeLayout;   // 활성화된 레이아웃

        /// <summary>
        /// 레이아웃을 비운다.
        /// </summary>
        public void ClearLayout()
        {
            if (_activeLayout != null)
            {
                Destroy(_activeLayout.gameObject);
                _activeLayout = null;
            }
        }

        /// <summary>
        /// 레이아웃을 지정한다.
        /// </summary>
        public void SetLayout(string name)
        {
            ClearLayout();

            var prefab = Resources.Load<PresentationLayout>(Define._presentationRoot + "/Layout/" + name);
            if (prefab == null)
            {
                Debug.LogError("[Presentation.SetLayout.CannotLoadLayout]" + name);
                return;
            }

            var layout = Instantiate(prefab);
            layout.transform.SetParent(transform, false);
            _activeLayout = layout;
        }
    }
}