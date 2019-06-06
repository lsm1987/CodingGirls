using UnityEngine;

namespace Game
{
    public class UIMenu : UIWindow
    {
        [SerializeField]
        private GameObject _container = null;

        private void Awake()
        {
            MoveAnchorToSafeArea();
        }

        public override bool OnKeyInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnOptionClicked();
                return true;
            }

            return false;
        }

        private void MoveAnchorToSafeArea()
        {
            // 우상단이 Safe Area 우상단과 일치하도록 이동
            // 예) iPhone X
            // Screen w:2436 h:1125
            // SafeArea x:132.00, y:63.00, width:2172.00, height:1062.00

            Vector2 rightTopAnchor = new Vector2(
                (Screen.safeArea.x + Screen.safeArea.width) / (float)Screen.width,
                (Screen.safeArea.y + Screen.safeArea.height) / (float)Screen.height
            );

            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.anchorMin = rightTopAnchor;
            rectTransform.anchorMax = rightTopAnchor;
        }

        public void OnOptionClicked()
        {
            ToggleContainerActive();
        }

        public void OnTitleClicked()
        {
            GameSystem._Instance.LoadTitleScene();
        }

        private void ToggleContainerActive()
        {
            _container.SetActive(!_container.activeSelf);
        }
    }
}