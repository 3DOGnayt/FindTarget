using Assets.Scripts.ScriptableObjects.Cells;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Target
{
    public class FindTarget : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LayerMask _layerMask;
        [Space (10)]
        [SerializeField] private SetTarget _setTarget;

        private RaycastHit Hit;

        public Action _onFindedTargetCreateGroup;
        public Action _onFindedTargetCreateCells;
        public Action _onFindedTargetSetTarget;

        public Action<GameObject> _onFindedRightTarget;
        public Action<GameObject> _onFindedWrongTarget;

        private void Start() => _camera = Camera.main;

        private void Update() => FinderTarget();

        private void RightTarget(SpriteFindHelper example)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _onFindedRightTarget?.Invoke(example.gameObject);

                var particle = example.gameObject.GetComponentInChildren<ParticleSystem>();
                particle.emissionRate = 10;

                StartCoroutine(WaitEffects());
            }
        }

        private void WrongTarget(SpriteFindHelper example)
        {
            if (Input.GetMouseButtonDown(0)) 
                _onFindedWrongTarget?.Invoke(example.gameObject);
        }

        public void FinderTarget()
        {
            Ray _cameraRay = _camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(_camera.transform.position, _cameraRay.direction * 20f, Color.green);

            Physics.Raycast(_cameraRay, out Hit, 50f, _layerMask);

            if (Hit.collider == null)
                return;
            if(Hit.collider.gameObject.TryGetComponent(out SpriteFindHelper component))
            {
                if (component.Sprite.sprite.name == _setTarget.TargetName)
                    RightTarget(component);
                else WrongTarget(component);
            }
        }

        private IEnumerator WaitEffects()
        {
            yield return new WaitForSeconds(1.5f);

            _onFindedTargetCreateGroup?.Invoke();
            _onFindedTargetCreateCells?.Invoke();
            _onFindedTargetSetTarget?.Invoke();
        }
    }
}