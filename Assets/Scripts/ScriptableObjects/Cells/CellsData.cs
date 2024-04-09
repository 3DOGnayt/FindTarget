using System;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Cells
{
    [Serializable]
    public class CellsData
    {
        [SerializeField] private string _name;

        [SerializeField] private Sprite _sprite;

        public string Name => _name;

        public Sprite Sprite => _sprite;
    }
}