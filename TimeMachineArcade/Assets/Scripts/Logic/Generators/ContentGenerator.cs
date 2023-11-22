using UnityEngine;

namespace Logic.Generators
{
    public class ContentGenerator
    {
        private readonly Transform _obstaclePrefab;
        private readonly float _planeWidth;
        private readonly float _minSize;
        private readonly float _maxSize;
        private readonly float _spawnRate;
        private readonly float _spawnSpread;
        private float _curPoint;

        public ContentGenerator(Transform obstaclePrefab,float startPoint,float planeWidth,float minSize,float maxSize, float spawnRate, float spawnSpread)
        {
            _obstaclePrefab = obstaclePrefab;
            _planeWidth = planeWidth;
            _minSize = minSize;
            _maxSize = maxSize;
            _spawnRate = spawnRate;
            _spawnSpread = spawnSpread;
            _curPoint = startPoint;
        }

        public void GenerateContent(Transform plane, float planeLenght)
        {

            while (_curPoint<plane.position.z+planeLenght/2)
            {
                float randomScale = Random.Range(_minSize, _maxSize);
                
                float x =plane.position.x+ Random.Range(-_planeWidth, _planeWidth);
                float y = plane.position.y+randomScale/2;
                float z = _curPoint;
                Vector3 point = new Vector3(x, y, z);
                
                Transform wall = Object.Instantiate(_obstaclePrefab, point, Quaternion.identity, plane);
                wall.localScale = Vector3.one * randomScale;
                wall.gameObject.name = "obstacle";
                _curPoint += Random.Range(-_spawnSpread, _spawnSpread) + _spawnRate;

            }
        }
    }
}