using Assets.Scripts.ScriptableObjects.Group;
using Assets.Scripts.Spawn;
using Assets.Scripts.Target;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SpriteGroup
{
    public class GroupSprites : MonoBehaviour
    {
        [SerializeField] private GroupDataList _groupDatas;
        [SerializeField] private CellSpawn _cellSpawn;
        [SerializeField] private FindTarget _findTarget;
        [Space (10)]
        [SerializeField] private GameObject _positionListParentContainer;
        [SerializeField] private GameObject _positionsListParent;
        [SerializeField] private GameObject _positions;
        [Space(10)]
        [SerializeField] private List<Transform> _cellPositions;

        private float _positionXOnStart = -1;
        private float _positionYOnStart = 1;

        private int _level;

        public List<Transform> CellPositions => _cellPositions;

        public int Level { get => _level; set => _level = value; }

        private void Awake() => SetGroupByLevel(_level);

        private void OnEnable() => _findTarget._onFindedTargetCreateGroup += NextLevel;

        private void OnDisable() => _findTarget._onFindedTargetCreateGroup -= NextLevel;

        private void CreateContainerForList()
        {
            var container = new GameObject();

            _positionsListParent = Instantiate(container, _positionListParentContainer.transform, false);

            _positionsListParent.name = "ListPositions";

            Destroy(container);
        }

        public void SetGroupByLevel(int level)
        {
            var group = _groupDatas.GroupDatas;

            SetListPositions(group[level].Row, group[level].Column, level);
        }

        private void SetListPositions(int rowCount, int columnCount, int level)
        {
            var levelData = _cellSpawn.LevelDataList.LevelData;

            CreateContainerForList();

            for (int i = 0; i < levelData[level].Element; i++)
            {
                var container = new GameObject();

                _positions = Instantiate(container, _positionsListParent.transform, false);

                _positions.name = $"Position {i + 1}";

                _cellPositions.Add(_positions.transform);

                Destroy(container);
            }

            SetGroupPositions(rowCount, columnCount);
        }

        private void SetGroupPositions(int row, int column)
        {
            for (int i = 0; i < _cellPositions.Count; i++)
            {
                if (_cellPositions.Count <= column)
                {
                    _cellPositions[i].position = new Vector3(_positionXOnStart + i, _positionYOnStart, 0);
                }

                if (_cellPositions.Count > column)
                {
                    _cellPositions[i].position = new Vector3(_positionXOnStart + i, _positionYOnStart, 0);

                    if (i >= column * (row + 1)) row++;

                    if (i >= column * row)
                    {
                        _cellPositions[i].position = new Vector3(_cellPositions[0].position.x + (i - (column * row)), _positionYOnStart - row, 0);                        
                    }
                }
            }
        }

        private void NextLevel()
        {
            _cellPositions.Clear();

            Destroy(_positionsListParent);

            _level += 1;

            if (_level >= 3)
                return;

            SetGroupByLevel(_level);
        }
    }
}