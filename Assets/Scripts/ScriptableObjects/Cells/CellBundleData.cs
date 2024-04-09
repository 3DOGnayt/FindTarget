using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Cells
{
    [CreateAssetMenu(menuName = "Cells Bundle Data", fileName = "New Cells Data")]
    public class CellBundleData : ScriptableObject
    {
        [SerializeField] private CellsData[] _cellsDatas;

        public CellsData[] CellsDatas => _cellsDatas;
    }
}