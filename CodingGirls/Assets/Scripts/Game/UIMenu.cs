using UnityEngine;

namespace Game
{
    public class UIMenu : UIWindow
    {
        [SerializeField]
        private GameObject _container;

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