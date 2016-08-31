using UnityEngine;

namespace Game
{
    /// <summary>
    /// URL 링크 UI
    /// </summary>
    public class UILink : UIWindow
    {
        private string _url;

        public void SetUrl(string url)
        {
            _url = url;
        }

        public void OnClicked()
        {
            OpenUrl();
        }

        private void OpenUrl()
        {
            Application.OpenURL(_url);
        }
    }
}