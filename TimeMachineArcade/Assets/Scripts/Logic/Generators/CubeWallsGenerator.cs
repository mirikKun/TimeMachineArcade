using UnityEngine;

namespace Logic.Generators
{
    public class CubeWallsGenerator
    {
        private Transform _wallPrefab;
        private readonly float _planeWidth;
        private readonly float _miSize;
        private readonly float _maxSize;
        private readonly float _spread;
        
        private float _leftLastPoint;
        private float _rightLastPoint;
        
        
        public CubeWallsGenerator(Transform wallPrefab,float planeWidth, float start,float miSize,float maxSize,float spread)
        {
            _wallPrefab = wallPrefab;
            _planeWidth = planeWidth;
            _leftLastPoint = start;
            _rightLastPoint = start;
            _miSize = miSize;
            _maxSize = maxSize;
            _spread = spread;
        }

        public void GenerateWallsForPlane(Transform plane,float planeLenght)
        {
            while (_leftLastPoint-plane.position.z<planeLenght/2)
            {
                float nextSize = Random.Range(_miSize, _maxSize);
                _leftLastPoint += nextSize;
                CreateWall(plane, nextSize,_leftLastPoint,-1);
            }
            while (_rightLastPoint-plane.position.z<planeLenght/2)
            {
                float nextSize = Random.Range(_miSize, _maxSize);
                _rightLastPoint += nextSize;
                CreateWall(plane, nextSize,_rightLastPoint,1);
            }
        }

        private void CreateWall(Transform plane, float nextSize,float lastPoint,int sign)
        {
            float x = plane.position.x + sign*(_planeWidth + nextSize / 2f+Random.Range(0,_spread));
            float y = plane.position.y + nextSize / 2f;
            float z = lastPoint-nextSize/2;
            
            Vector3 position = new Vector3(x, y, z);
            Transform wall = Object.Instantiate(_wallPrefab, position, Quaternion.identity, plane);
            wall.localScale = Vector3.one * nextSize;
        }
    }
}