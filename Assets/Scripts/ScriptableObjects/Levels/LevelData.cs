using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Levels
{
    [CreateAssetMenu(menuName = "LevelData/LevelData", fileName = "NewLevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int _level;

        [SerializeField] private int _element;

        public int level => _level;

        public int Element => _element;
    }
}