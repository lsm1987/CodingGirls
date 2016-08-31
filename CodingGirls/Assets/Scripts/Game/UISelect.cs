using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// 선택지 UI
    /// </summary>
    public class UISelect : UIWindow
    {
        public class Item
        {
            public UISelectItem _UI { get; private set; }
            public string _LabelName { get; private set; }

            public Item(UISelectItem ui, string labelName)
            {
                _UI = ui;
                _LabelName = labelName;
            }
        }

        [SerializeField]
        private GameObject _itemUIPrefab;
        [SerializeField]
        private float _selectableDelay; // 선택가능상태 되기까지 딜레이
        [SerializeField]
        private float _closeDelay;
        private float _startTime;
        private List<Item> _items = new List<Item>();
        private const int _invalidIdx = -1;
        private List<int> _pointeredIdxs = new List<int>(); // 가장 최근에 Enter 한 순서대로
        private int _selectedIdx = _invalidIdx;
        public bool _SelectEnded { get; private set; }

        private void Awake()
        {
            _startTime = Time.time;
        }

        public void AddItem(string text, string labelName)
        {
            int itemIdx = _items.Count;
            UISelectItem itemUI = UIManager.OpenWindow(_itemUIPrefab, _Trans) as UISelectItem;
            itemUI.SetText(text);
            itemUI.SetEventListener(
                delegate { OnPointerEnterItem(itemIdx); }
                , delegate { OnPointerExitItem(itemIdx); }
                , delegate { OnClickItem(itemIdx); }
                );

            _items.Add(new Item(itemUI, labelName));
        }

        private int _PointeredIdx
        {
            get
            {
                return (_pointeredIdxs.Count > 0) ? _pointeredIdxs[0] : _invalidIdx;
            }
        }

        private bool IsSelectableDelay
        {
            get
            {
                return (Time.time - _startTime - _selectableDelay < 0.0f);
            }
        }

        private void OnPointerEnterItem(int idx)
        {
            if (IsSelectableDelay || _IsSelected || _pointeredIdxs.Contains(idx))
            {
                return;
            }

            int prevPointeredIdx = _PointeredIdx;
            _pointeredIdxs.Insert(0, idx);
            SetItemPointerExited(prevPointeredIdx);
            SetItemPointerEntered(_PointeredIdx);
        }

        private void OnPointerExitItem(int idx)
        {
            if (IsSelectableDelay || _IsSelected || !_pointeredIdxs.Remove(idx))
            {
                return;
            }

            SetItemPointerExited(idx);
            SetItemPointerEntered(_PointeredIdx);
        }

        private bool _IsSelected
        {
            get
            {
                return (_selectedIdx != _invalidIdx);
            }
        }

        public string _SelectedLabelName
        {
            get
            {
                if (_IsSelected)
                {
                    return _items[_selectedIdx]._LabelName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private void OnClickItem(int idx)
        {
            if (IsSelectableDelay
                || _IsSelected
                || idx != _PointeredIdx
                )
            {
                return;
            }

            //Debug.Log(idx.ToString() + " clicked " + _items[idx]._LabelName.ToString());
            SoundManager._Instance.PlaySound(Define._selectSound);
            _selectedIdx = idx;
            SetItemSelected(idx);
            StartCoroutine(WaitForClose());
        }

        private void SetItemPointerEntered(int idx)
        {
            if (idx == _invalidIdx)
            {
                return;
            }

            _items[idx]._UI.SetPointerEntered();
        }

        private void SetItemPointerExited(int idx)
        {
            if (idx == _invalidIdx)
            {
                return;
            }

            _items[idx]._UI.SetPointerExited();
        }

        private void SetItemSelected(int idx)
        {
            _items[idx]._UI.SetSelected();
        }

        private IEnumerator WaitForClose()
        {
            yield return new WaitForSeconds(_closeDelay);
            _SelectEnded = true;
        }
    }
}