using UnityEngine;

namespace Logic.Generators.Data
{
    [CreateAssetMenu(fileName = "SettingData", menuName = "SettingData")]
    public class SettingObjectsData:ScriptableObject
    {
        public Wall[] Walls;
        public Plane[] Planes;
        
        public float WallSpread = 0.5f;
        public float WallOffset = 0.5f;
        public float ObstaclesSpawnRate = 7;
        public float ObstaclesSpread = 0.4f;
        public float ObjectsSpawnRange = 7;
    }
}