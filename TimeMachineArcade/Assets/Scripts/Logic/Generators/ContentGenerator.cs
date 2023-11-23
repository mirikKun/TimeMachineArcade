using UnityEngine;

namespace Logic.Generators
{
    public class ContentGenerator
    {
        private readonly float _spawnRate;
        private readonly float _spawnSpread;
        private float _curPoint;

        public ContentGenerator(float startPoint, float spawnRate, float spawnSpread)
        {
       
            _spawnRate = spawnRate;
            _spawnSpread = spawnSpread;
            _curPoint = startPoint;
        }

        public void GenerateContent(Plane plane)
        {

            while (_curPoint<plane.Position().z+plane.Lenght/2)
            {
                int randomObstacle = Random.Range(0, plane.Obstacles.Length);
                
                float x =plane.Position().x+ Random.Range(-plane.Width, plane.Width);
                float y = plane.Position().y;
                float z = _curPoint;
                Vector3 point = new Vector3(x, y, z);
                
                Obstacle obstacle=Object.Instantiate(plane.Obstacles[randomObstacle], point, Quaternion.identity, plane.transform);
                _curPoint += Random.Range(-_spawnSpread, _spawnSpread) + _spawnRate;
                obstacle.transform.eulerAngles = new Vector3(0, Random.Range(0, 360f), 0);
            }
        }
    }
}