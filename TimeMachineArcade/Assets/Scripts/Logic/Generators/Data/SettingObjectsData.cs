using UnityEngine;

namespace Logic.Generators.Data
{
    [CreateAssetMenu(fileName = "SettingData", menuName = "SettingData")]
    public class SettingObjectsData:ScriptableObject
    {
        public Wall[] Walls;
    }
}