using UnityEngine;
using System.Collections;

namespace Game
{
    public class Foreground : MonoBehaviour
    {
        private GameObject _go;
        protected GameObject _Go { get { if (_go == null) { _go = gameObject; } return _go; } }
        private Transform _trans;
        public Transform _Trans { get { if (_trans == null) { _trans = transform; } return _trans; } }
        [SerializeField]
        private GameObject _main;
        [SerializeField]
        private GameObject _gradationLeft;
        [SerializeField]
        private GameObject _gradationRight;
        private Renderer _mainRenderer;

        public void Initialize()
        {
            // 메인 판이 카메라를 덮도록
            float camHeight = Camera.main.orthographicSize * 2.0f;
            float camWidth = camHeight * Camera.main.aspect;
            _main.transform.localScale = new Vector3(camWidth, camHeight, 1.0f);

            float gradationLeftWidth = _gradationLeft.transform.localScale.x;
            _gradationLeft.transform.localScale = new Vector3(gradationLeftWidth, camHeight, 1.0f);
            _gradationLeft.transform.localPosition = new Vector3(-camWidth / 2.0F - gradationLeftWidth / 2.0F, 0.0F, 0.0F);

            float gradationRightWidth = _gradationRight.transform.localScale.x;
            _gradationRight.transform.localScale = new Vector3(gradationRightWidth, camHeight, 1.0f);
            _gradationRight.transform.localPosition = new Vector3(camWidth / 2.0F + gradationRightWidth / 2.0F, 0.0F, 0.0F);

            _mainRenderer = _main.GetComponent<Renderer>();
        }

        public void SetActivate(bool activate)
        {
            _Go.SetActive(activate);
        }

        public void SetAlpha(float alpha)
        {
            const string tintName = "_Color";
            Color color = _mainRenderer.material.GetColor(tintName);
            color.a = alpha;
            _mainRenderer.material.SetColor(tintName, color);
        }

        public float GetMainWidth()
        {
            return _main.transform.localScale.x;
        }

        public float GetGradationLeftWidth()
        {
            return _gradationLeft.transform.localScale.x;
        }

        public float GetGradationRightWidth()
        {
            return _gradationRight.transform.localScale.x;
        }

        // 불투명해지기
        public void FadeIn(float duration)
        {
            if (duration == 0.0f)
            {
                GameSystem._Instance._UI._Dialogue.SetActivate(false);
                SetActivate(true);
                SetAlpha(1.0f);
            }
            else
            {
                GameSystem._Instance.RegisterTask(FadeInTask(duration));
            }
        }

        private IEnumerator FadeInTask(float duration)
        {
            GameSystem._Instance._UI._Dialogue.SetActivate(false);
            SetActivate(true);
            float startTime = Time.time;
            while (Time.time - (startTime + duration) < 0.0f)
            {
                float rate = (Time.time - startTime) / duration;
                SetAlpha(Mathf.Lerp(0.0f, 1.0f, rate));
                yield return null;
            }
            SetAlpha(1.0f);
        }

        // 투명해지기
        public void FadeOut(float duration)
        {
            if (duration == 0.0f)
            {
                GameSystem._Instance._UI._Dialogue.SetActivate(false);
                SetActivate(false);
            }
            else
            {
                GameSystem._Instance.RegisterTask(FadeOutTask(duration));
            }
        }

        private IEnumerator FadeOutTask(float duration)
        {
            GameSystem._Instance._UI._Dialogue.SetActivate(false);
            SetActivate(true);
            float startTime = Time.time;
            while (Time.time - (startTime + duration) < 0.0f)
            {
                float rate = (Time.time - startTime) / duration;
                SetAlpha(Mathf.Lerp(1.0f, 0.0f, rate));
                yield return null;
            }
            SetAlpha(1.0f);
            SetActivate(false);
        }

        // 움직이며 가리기
        public void Cover(bool toLeft, float duration)
        {
            if (duration == 0.0f)
            {
                GameSystem._Instance._UI._Dialogue.SetActivate(false);
                SetActivate(true);
            }
            else
            {
                GameSystem._Instance.RegisterTask(CoverTask(toLeft, duration));
            }
        }

        private IEnumerator CoverTask(bool toLeft, float duration)
        {
            GameSystem._Instance._UI._Dialogue.SetActivate(false);
            SetActivate(true);

            float startX = 0.0f;
            if (toLeft)
            {
                startX = GetGradationRightWidth() + GetMainWidth();
            }
            else
            {
                startX = - GetGradationLeftWidth() - GetMainWidth();
            }
            float z = _main.transform.position.z;

            float startTime = Time.time;
            while (Time.time - (startTime + duration) < 0.0f)
            {
                float rate = (Time.time - startTime) / duration;
                float posX = Mathf.Lerp(startX, 0.0f, rate);
                _Trans.position = new Vector3(posX, 0.0f, z);
                yield return null;
            }
            _Trans.position = new Vector3(0.0f, 0.0f, z);
        }

        // 움직이며 치우기
        public void Sweep(bool toLeft, float duration)
        {
            if (duration == 0.0f)
            {
                GameSystem._Instance._UI._Dialogue.SetActivate(false);
                SetActivate(false);
            }
            else
            {
                GameSystem._Instance.RegisterTask(SweepTask(toLeft, duration));
            }
        }

        private IEnumerator SweepTask(bool toLeft, float duration)
        {
            GameSystem._Instance._UI._Dialogue.SetActivate(false);
            SetActivate(true);

            float endX = 0.0f;
            if (toLeft)
            {
                endX = -GetGradationLeftWidth() - GetMainWidth();
            }
            else
            {
                endX = GetGradationRightWidth() + GetMainWidth();
            }
            float z = _main.transform.position.z;

            float startTime = Time.time;
            while (Time.time - (startTime + duration) < 0.0f)
            {
                float rate = (Time.time - startTime) / duration;
                float posX = Mathf.Lerp(0.0f, endX, rate);
                _Trans.position = new Vector3(posX, 0.0f, z);
                yield return null;
            }
            _Trans.position = new Vector3(0.0f, 0.0f, z);
            SetActivate(false);
        }
    }
}