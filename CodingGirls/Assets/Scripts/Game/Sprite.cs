using UnityEngine;

namespace Game
{
    /// <summary>
    /// 인게임 스프라이트
    /// </summary>
    public class Sprite : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _renderer;
        private const float _defaultZ = 0.5f;

        public void Initialize(UnityEngine.Sprite sprite)
        {
            SetSprite(sprite);
            SetPosition(Vector2.zero);
            SetScale(1.0f);
        }

        private void SetSprite(UnityEngine.Sprite sprite)
        {
            _renderer.sprite = sprite;
        }

        public void SetPosition(Vector2 pos)
        {
            transform.localPosition = new Vector3(pos.x, pos.y, _defaultZ);
        }

        public void SetScale(float scale)
        {
            transform.localScale = new Vector3(scale, scale, 1.0f);
        }
    }
}