using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Group
{
    [CreateAssetMenu(menuName = "GroupData/GroupData", fileName = "NewGroupData")]
    public class GroupData : ScriptableObject
    {
        [SerializeField] private int _row;

        [SerializeField] private int _column;

        public int Row => _row;

        public int Column => _column;
    }
}