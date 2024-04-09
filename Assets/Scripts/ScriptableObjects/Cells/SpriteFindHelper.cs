using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Cells
{
    public class SpriteFindHelper : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;

        public SpriteRenderer Sprite { get => _sprite; set => _sprite = value; }
    }
}