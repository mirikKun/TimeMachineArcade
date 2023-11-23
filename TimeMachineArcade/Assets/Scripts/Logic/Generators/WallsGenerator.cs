using Logic.Generators.Data;
using UnityEngine;
namespace Logic.Generators
{
    public class WallsGenerator
    {
        private SettingObjectsData _settingObjectsData;
        private readonly float _planeWidth;

        private readonly float _spread;
        
        private float _leftLastPoint;
        private float _rightLastPoint;
        
        
        public WallsGenerator(SettingObjectsData settingObjectsData,float planeWidth, float start,float spread)
        {
            _settingObjectsData = settingObjectsData;
            _planeWidth = planeWidth;
            _leftLastPoint = start;
            _rightLastPoint = start;
    
            _spread = spread;
        }

        public void GenerateWallsForPlane(Plane plane)
        {
            while (_leftLastPoint-plane.Position().z<plane.Lenght/2)
            {
                Wall wall = GetRandomWallObject();
                float wallLenght=CreateWall(plane, wall,_leftLastPoint,-1);
                _leftLastPoint += wallLenght;

            }
            
            
            while (_rightLastPoint-plane.Position().z<plane.Lenght/2)
            {
                Wall wall = GetRandomWallObject();
                float wallLenght=CreateWall(plane, wall,_rightLastPoint,1);
                _rightLastPoint += wallLenght;

            }
        }

        private Wall GetRandomWallObject()
        {
            int index = Random.Range(0, _settingObjectsData.Walls.Length);
            Wall wall = _settingObjectsData.Walls[index];
            return wall;
        }

        private float CreateWall(Plane plane, Wall wallPrefab,float lastPoint,int sign)
        {
            
            Wall wall = Object.Instantiate(wallPrefab,plane.transform);
            wall.SetRandomRotation();
            
            float x = plane.Position().x + sign*(_planeWidth + wall.Size.x / 2f+Random.Range(0,_spread));
            float z = lastPoint+wall.Size.y/2;
            Vector3 position = new Vector3(x, 0, z);
            wall.SetPosition(position);
            return wall.Size.y;
        }
    }
}