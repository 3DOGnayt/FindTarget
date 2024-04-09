using Assets.Scripts.Spawn;
using Assets.Scripts.Target;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameEffects
{
    public class Effects : MonoBehaviour
    {
        [SerializeField] private CellSpawn _cellSpawn;
        [SerializeField] private SetTarget _setTarget;
        [SerializeField] private FindTarget _findTarget;

        private void OnEnable()
        {
            _cellSpawn._onStartBounce += BounceEffect;
            _setTarget._onFadeIn += FadeInEffect;
            _findTarget._onFindedWrongTarget += WrongTargetEffect;
            _findTarget._onFindedRightTarget += RightTargetEffect;
        }

        private void OnDisable()
        {
            _cellSpawn._onStartBounce -= BounceEffect;
            _setTarget._onFadeIn -= FadeInEffect;
            _findTarget._onFindedWrongTarget -= WrongTargetEffect;
            _findTarget._onFindedRightTarget -= RightTargetEffect;
        }

        private void BounceEffect(GameObject gameObject) => gameObject.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.8f, 4, 0.2f);

        private void FadeInEffect(GameObject gameObject) => gameObject.GetComponent<Text>().DOFade(1, 2);

        private void WrongTargetEffect(GameObject gameObject) => gameObject.transform.DOShakePosition(1, Vector3.left, 4, 1, false, true);

        private void RightTargetEffect(GameObject gameObject) => gameObject.transform.DOShakePosition(1, Vector3.up, 4, 1, false, true);
    }
}