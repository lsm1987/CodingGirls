using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// 배경 이미지
    /// </summary>
    public class Background : MonoBehaviour
    {
        [SerializeField]
        private MeshRenderer _renderer = null;
        [SerializeField]
        private Material _matColorOri = null;
        private Material _matColor = null;
        [SerializeField]
        private Material _matTextureOri = null;
        private Material _matTexture = null;
        private Dictionary<string, Texture> _loadedTexture = new Dictionary<string, Texture>();

        public void Initialize()
        {
            _matColor = new Material(_matColorOri);
            _matTexture = new Material(_matTextureOri);
            InitializeScale();
        }

        private void InitializeScale()
        {
            float camRatio = Camera.main.aspect;
            float camHeight = Camera.main.orthographicSize * 2.0f;
            float camWidth = camHeight * camRatio;

            float oriWidth = transform.localScale.x;
            float oriHeight = transform.localScale.y;
            float oriRatio = oriWidth / oriHeight;

            float scale = 1.0f;

            if (camRatio >= oriRatio)
            {
                // 화면의 가로 비율이 원본 배경 가로 비율보다 큼
                // 예: 아이폰X = 19.5:9, 배경 = 16:9
                // 원본 배경 가로가 화면을 채우도록 원본 스케일 조정
                scale = camWidth / oriWidth;
            }
            else
            {
                // 예: 아이패드 프로 = 4:3, 배경 = 16:9
                // 원본 배경 세로가 기준 세로를 채우도록 원본 스케일 조정
                scale = GameSystem._ReferenceWorldHeight / oriHeight;
            }

            float scaledWidth = oriWidth * scale;
            float scaledHeight = oriHeight * scale;

            transform.localScale = new Vector3(scaledWidth, scaledHeight, 1.0f);
        }

        public Texture LoadTexture(string textureName)
        {
            Texture texture = null;
            if (_loadedTexture.TryGetValue(textureName, out texture))
            {
                return texture;
            }

            texture = Resources.Load<Texture>(Define._backgroundRoot + "/" + textureName);
            if (texture == null)
            {
                Debug.LogError("[Background.LoadTexture.InvalidName]" + textureName);
                return null;
            }

            _loadedTexture.Add(textureName, texture);
            return texture;
        }

        public void SetColor(Color color)
        {
            _matColor.SetColor("_Color", color);
            _renderer.material = _matColor;
        }

        public void SetTexture(string textureName)
        {
            Texture texture = LoadTexture(textureName);
            _matTexture.SetTexture("_MainTex", texture);
            _renderer.material = _matTexture;
        }
    }
}