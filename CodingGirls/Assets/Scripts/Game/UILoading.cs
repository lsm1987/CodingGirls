using UnityEngine;

namespace Game
{
    public class UILoading : UIWindow
    {
        private void Awake()
        {
            MoveAnchorToSafeArea();
        }

        private void MoveAnchorToSafeArea()
        {
            // 우하단이 Safe Area 우하단과 일치하도록 이동
            Vector2 rightBottomAnchor = new Vector2(
                (Screen.safeArea.x + Screen.safeArea.width) / (float)Screen.width,
                Screen.safeArea.y / (float)Screen.height
            );

            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.anchorMin = rightBottomAnchor;
            rectTransform.anchorMax = rightBottomAnchor;
        }
    }
}