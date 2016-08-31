using UnityEngine;
using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// 인게임 스프라이트들의 관리
    /// </summary>
    public class SpriteManager
    {
        private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>(); // <스프라이트명, 스프라이트>

        public Sprite GetOrCreate(string name)
        {
            Sprite spr;
            if (!_sprites.TryGetValue(name, out spr))
            {
                UnityEngine.Sprite spriteResource = Resources.Load<UnityEngine.Sprite>(Define._spriteRoot + "/" + name);
                if (spriteResource == null)
                {
                    Debug.LogError("[SpriteManager.GetOrCreate.InvalidSpriteResourceName]" + name);
                    return null;
                }

                Sprite prefab = Resources.Load<Sprite>(Define._spritePrefabPath);
                spr = Object.Instantiate(prefab);
                spr.Initialize(spriteResource);
                _sprites.Add(name, spr);
            }
            return spr;
        }

        /// <summary>
        /// 지정한 이름의 스프라이트 삭제
        /// </summary>
        public void Remove(string name)
        {
            Sprite spr;
            if (!_sprites.TryGetValue(name, out spr))
            {
                Debug.LogError("[SpriteManager.Remove.InvalidName]" + name);
                return;
            }

            _sprites.Remove(name);
            Object.DestroyObject(spr.gameObject);
        }

        /// <summary>
        /// 생성된 모든 스프라이트 삭제
        /// </summary>
        public void Remove()
        {
            foreach(var spr in _sprites.Values)
            {
                Object.DestroyObject(spr.gameObject);
            }
            _sprites.Clear();
        }
    }
}