using Assets.Scripts.ScriptableObjects.Cells;
using Assets.Scripts.Spawn;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Target
{
    public class SetTarget : MonoBehaviour
    {
        [SerializeField] private FindTarget _findTarget;
        [SerializeField] private List<CellBundleData> _cellBundleDataList;
        [SerializeField] private CellBundleData _cellBundleData;
        [SerializeField] private CellSpawn _cellSpawn;
        [SerializeField] private Text _textTarget;

        private string _targetName;

        public string TargetName => _targetName;

        public Action<GameObject> _onFadeIn;

        private void Awake() => SetDataTarget();

        private void OnEnable() => _findTarget._onFindedTargetSetTarget += SetDataTarget;

        private void OnDisable() => _findTarget._onFindedTargetSetTarget -= SetDataTarget;

        private void SetDataTarget()
        {
            var random = _cellBundleDataList[UnityEngine.Random.Range(0, 2)];

            _cellBundleData = random;

            _cellSpawn.CellBundleData = _cellBundleData;

            if (_cellSpawn.LevelChange >= 3)
                _textTarget.gameObject.SetActive(false);
        }

        public void SetTargetToLevel()
        {
            _textTarget.gameObject.SetActive(true);

            _textTarget.DOFade(0, 0);

            var target = _cellSpawn.SpriteList[UnityEngine
                .Random.Range(0, _cellSpawn.SpriteList.Count)]
                .GetComponentInChildren<SpriteFindHelper>()
                .Sprite;

            _targetName = target.sprite.name;

            _textTarget.text = $"Find: {_targetName}";

            _onFadeIn?.Invoke(_textTarget.gameObject);
        }
    }
}