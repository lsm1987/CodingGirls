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