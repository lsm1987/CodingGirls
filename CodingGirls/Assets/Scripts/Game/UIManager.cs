using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    public class UIManager : global::UIManager
    {
        public UIInput _Input { get; private set; }
        public UIMenu _Menu { get; private set; }
        public UIDialogue _Dialogue { get; private set; }
        public UISelect _Select { get; private set; }
        private Dictionary<string, UIWindow> _dynamicUIs = new Dictionary<string, UIWindow>(); // <UI명, UI>

        public void Initialize()
        {
            CreateInput();
            CreateMenu();
            CreateDialogue();
        }

        private void CreateInput()
        {
            _Input = OpenWindow(Define._uiInputPrefabPath) as UIInput;
        }

        private void CreateMenu()
        {
            _Menu = OpenWindow(Define._uiMenuPrefabPath) as UIMenu;
            _Menu.SetActivate(false);
        }

        private void CreateDialogue()
        {
            _Dialogue = OpenWindow(Define._uiDialoguePrefabPath) as UIDialogue;
            _Dialogue.SetActivate(false);
        }

        public void CreateSelect()
        {
            if (_Select != null)
            {
                Debug.LogError("[UIManager.AlreadSelectUICreated]");
                return;
            }

            _Select = OpenWindow(Define._uiSelectPrefabPath) as UISelect;
        }

        public void RemoveSelect()
        {
            if (_Select != null)
            {
                CloseWindow(_Select);
                _Select = null;
            }
        }

        public UIWindow GetDynamicUI(string name)
        {
            UIWindow ui;
            if (_dynamicUIs.TryGetValue(name, out ui))
            {
                return ui;
            }
            else
            {
                return null;
            }
        }

        public UIWindow AddDynamicUI(string name, string prefab)
        {
            if (GetDynamicUI(name) != null)
            {
                Debug.LogError("[UIManager.AddDynamicUI.AlreadyExistName]" + name);
                return null;
            }

            UIWindow ui = OpenWindow(prefab) as UIWindow;
            _dynamicUIs.Add(name, ui);
            return ui;
        }

        public void RemoveDynamicUI(string name)
        {
            UIWindow ui = GetDynamicUI(name);
            if (ui == null)
            {
                Debug.LogError("[UIManager.RemoveDynamicUI.NotExistName]" + name);
                return;
            }

            _dynamicUIs.Remove(name);
            CloseWindow(ui);
        }
    }
}