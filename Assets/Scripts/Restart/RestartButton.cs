using Assets.Scripts.Spawn;
using Assets.Scripts.SpriteGroup;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Assets.Scripts.Restart
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button _restart;
        [SerializeField] private GameObject _panel;
        [Space (10)]
        [SerializeField] private CellSpawn _cellSpawn;
        [SerializeField] private GroupSprites _groupSprites;

        private void Awake()
        {
            _restart.onClick.AddListener(ResetGameValues);
            _restart.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_cellSpawn.LevelChange < 3)
                PanelFade(0, 0.5f);

            if (_cellSpawn.LevelChange >= 3)
            {
                _restart.gameObject.SetActive(true);

                PanelFade(0.8f, 1);
            }
        }

        private void ResetGameValues()
        {
            _cellSpawn.LevelChange = 0;
            _groupSprites.Level = 0;

            _restart.gameObject.SetActive(false);

            _groupSprites.SetGroupByLevel(0);
            _cellSpawn.CreateCell(0);
        }

        private void PanelFade(float alfa, float time) => _panel.GetComponent<Image>().DOFade(alfa, time);
    }
}