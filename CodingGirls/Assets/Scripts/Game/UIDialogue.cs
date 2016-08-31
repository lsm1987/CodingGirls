using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Game
{
    public class UIDialogue : UIWindow
    {
        [SerializeField]
        private GameObject _name;
        [SerializeField]
        private Text _nameText;
        [SerializeField]
        private Text _text;
        [SerializeField]
        private GameObject _waitIcon;
        private Coroutine _blinkWaitIconCoroutine = null;

        private void OnEnable()
        {
            if (_blinkWaitIconCoroutine != null)
            {
                StartBlinkWaitIcon();
            }
        }

        public void SetText(string text)
        {
            GameSystem._Instance.RegisterTask(SetTextTask(text));
        }

        private IEnumerator SetTextTask(string text)
        {
            SetActivate(true);
            StopBlinkWaitIcon();

            // 줄바꿈 발생할 인덱스 미리 계산하여 텍스트에 반영
            string lineBreakedText = GetLineBreakedText(text);
            int len = 0;
            float lastTime = Time.time;
            while (len < lineBreakedText.Length)
            {
                if (GameSystem._Instance._IsClicked)
                {
                    len = lineBreakedText.Length;
                }
                else
                {
                    float deltaTime = (Time.time - lastTime);
                    int count = (int)Mathf.Max(1.0f, deltaTime / Define._textInterval);
                    len = Mathf.Min(len + count, lineBreakedText.Length);
                    //Debug.Log("deltaTime:" + deltaTime.ToString() + " count:" + count.ToString() + " len:" + len.ToString());
                }
                _text.text = lineBreakedText.Substring(0, len);
                lastTime = Time.time;
                yield return new WaitForSeconds(Define._textInterval);
            }
            StartBlinkWaitIcon();
            while (true)
            {
                if (GameSystem._Instance._IsClicked)
                {
                    break;
                }
                else
                {
                    yield return null;
                }
            }
            StopBlinkWaitIcon();
        }

        private string GetLineBreakedText(string text)
        {
            string lineBreakedText = text;
            Vector2 extents = _text.rectTransform.rect.size;
            var settings = _text.GetGenerationSettings(extents);
            _text.cachedTextGenerator.Populate(text, settings);
            var lines = _text.cachedTextGenerator.lines;
            for (int i = lines.Count - 1; i > 0; --i)   // 첫 줄 시작 인덱스는 제외
            {
                int startCharIdx = lines[i].startCharIdx;
                if (lineBreakedText[startCharIdx - 1] == '\n')
                {
                    continue;   // 실제 줄바꿈 문자에 의한 줄은 넘어감
                }
                lineBreakedText = lineBreakedText.Insert(startCharIdx, "\n");
            }
            return lineBreakedText;
        }

        private void StartBlinkWaitIcon()
        {
            _blinkWaitIconCoroutine = StartCoroutine(BlinkWaitIcon());
        }

        private void StopBlinkWaitIcon()
        {
            if (_blinkWaitIconCoroutine != null)
            {
                StopCoroutine(_blinkWaitIconCoroutine);
                _blinkWaitIconCoroutine = null;
            }
            _waitIcon.SetActive(false);
        }

        private IEnumerator BlinkWaitIcon()
        {
            _waitIcon.SetActive(true);
            while (true)
            {
                yield return new WaitForSeconds(Define._waitIconInterval);
                _waitIcon.SetActive(!_waitIcon.activeSelf);
            }
        }

        public void SetName(string name)
        {
            if (name != "")
            {
                _name.SetActive(true);
                _nameText.text = name;
                _nameText.color = GameSystem._Instance._ScenarioInfo.GetNameColor(name);
            }
            else
            {
                _name.SetActive(false);
            }
        }
    }
}