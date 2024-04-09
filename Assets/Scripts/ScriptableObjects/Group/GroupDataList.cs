using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Group
{
    [CreateAssetMenu(menuName = "GroupData/GroupDataList", fileName = "NewGroupDataList")]
    public class GroupDataList : ScriptableObject
    {
        [SerializeField] private List<GroupData> _groupDatas;

        public List<GroupData> GroupDatas => _groupDatas;
    }
}