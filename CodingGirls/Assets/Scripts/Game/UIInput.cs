using UnityEngine.EventSystems;

namespace Game
{
    public class UIInput : UIWindow, IPointerDownHandler
    {
        private bool _isClicked = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isClicked = true;
        }

        public bool PopIsClicked()
        {
            bool isClicked = _isClicked;
            _isClicked = false;
            return isClicked;
        }
    }
}