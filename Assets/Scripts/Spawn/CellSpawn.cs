using Assets.Scripts.ScriptableObjects.Cells;
using Assets.Scripts.ScriptableObjects.Levels;
using Assets.Scripts.SpriteGroup;
using Assets.Scripts.Target;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Spawn
{
    public class CellSpawn : MonoBehaviour
    {
        [SerializeField] private GroupSprites _groupSprites;
        [SerializeField] private SetTarget _setTarget;
        [SerializeField] private FindTarget _findTarget;
        [Space(10)]
        [SerializeField] private LevelDataList _levelDataList;
        [SerializeField] private CellBundleData _cellBundleData;
        [Space(10)]
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private GameObject _parentPositionToSpawn;
        [SerializeField] private List<GameObject> _spriteList;
        [SerializeField] private List<Sprite> _spriteListName;

        private int _levelChange;

        internal LevelDataList LevelDataList => _levelDataList;

        public CellBundleData CellBundleData { get => _cellBundleData; set => _cellBundleData = value; }

        public List<GameObject> SpriteList => _spriteList;

        public int LevelChange { get => _levelChange; set => _levelChange = value; }

        public Action<GameObject> _onStartBounce;

        private void Start() => CreateCell(0);

        private void OnEnable() => _findTarget._onFindedTargetCreateCells += DestroyCell;

        private void OnDisable() => _findTarget._onFindedTargetCreateCells -= DestroyCell;

        public void CreateCell(int level)
        {
            var dataList = _levelDataList.LevelData;

            for (int i = 0; i < dataList[level].Element; i++)
            {
                var cell = Instantiate(_cellPrefab, _parentPositionToSpawn.transform, false);

                var sprite = cell.GetComponentInChildren<SpriteFindHelper>().Sprite;

                _spriteList.Add(cell);

                sprite.sprite = _cellBundleData.CellsDatas[UnityEngine.Random.Range(0, _cellBundleData.CellsDatas.Length)].Sprite;

                _spriteListName.Add(sprite.sprite);

                if (_spriteListName.Count >= 2)
                    CheckDuplicate(i);

                if (sprite.sprite.rect.height < sprite.sprite.rect.width && sprite.sprite.name != "10" && sprite.sprite.name != "M" && sprite.sprite.name != "W" )
                {
                    sprite.transform.eulerAngles = new Vector3(0, 0, -90);
                }                
            }

            Replase();
        }

        private void CheckDuplicate(int elenemt)
        {
            for (int i = 0; i < _spriteListName.Count - 1; i++)
            {
                if (_spriteListName[elenemt] == _spriteListName[i])
                {
                    _spriteListName[elenemt] = _cellBundleData.CellsDatas[UnityEngine.Random.Range(0, _cellBundleData.CellsDatas.Length)].Sprite;

                    _spriteList[elenemt].GetComponentInChildren<SpriteFindHelper>().Sprite.sprite = _spriteListName[elenemt];

                    CheckDuplicate(elenemt);
                }
            }            
        }

        private void Replase()
        {
            for (int i = 0; i < _spriteList.Count; i++)
                _spriteList[i].transform.position = _groupSprites.CellPositions[i].position;

            _setTarget.SetTargetToLevel();

            foreach (var item in _spriteList)
                _onStartBounce?.Invoke(item);
        }

        private void DestroyCell()
        {
            foreach (var item in _spriteList)
                Destroy(item);

            _spriteList.Clear();

            _spriteListName.Clear();

            _levelChange += 1;

            if (_levelChange >= 3)
                return;
            
            CreateCell(_levelChange);
        }
    }
}