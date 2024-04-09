using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Levels
{
    [CreateAssetMenu(menuName = "LevelData/LevelDataList", fileName = "NewLevelDataList")]
    public class LevelDataList : ScriptableObject
    {
        [SerializeField] private List<LevelData> _levelData;

        public List<LevelData> LevelData => _levelData;
    }
}