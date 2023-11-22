using System.Collections.Generic;
using Logic.Generators.Data;
using UnityEngine;
using Zenject;

namespace Logic.Generators
{
    public class LevelGenerator : MonoBehaviour
    {

        [SerializeField] private Transform _obstacles;
        [SerializeField] private SettingObjectsData _cityObjectsData;
        [SerializeField] private Transform _plane;

        [SerializeField] private Transform _startOfLevelPoint;
        [SerializeField] private float _generationStartLenght=100;
        [SerializeField] private FloatRange _wallSizes;
        [SerializeField]private float _planeLenght=30;
        [SerializeField]private float _wallSpread=0.3f;
        [SerializeField] private float _wallOffset=0.5f;
        [SerializeField] private FloatRange _obstaclesSizes;
        [SerializeField] private float _obstaclesSpawnRate = 3;
        [SerializeField] private float _obstaclesSpread=0.4f;
        [SerializeField] private float _aheadLenghtOfGeneration=80;
        private float _obstaclesAppearingRate;

        [SerializeField] private float _objectsSpawnRange = 7;

        private float _currentLevelThreshold;
        private Vector3 _lastPlanePoint;
        private WallsGenerator _cubeWallsGenerator;

        private ContentGenerator _contentGenerator;
    
        private List<Transform> _placedPlanes=new();

        [Inject]
        private void Construct(CarMover carMover)
        {
            carMover.OnMoving += UpdateLevelGeneration;
        }
        private void Start()
        {
            InitGenerators();
            InitialGeneration();
        }

        private void InitGenerators()
        {
            _cubeWallsGenerator = new WallsGenerator(_cityObjectsData, _objectsSpawnRange + _wallOffset,
                _startOfLevelPoint.position.z - _generationStartLenght / 2, _wallSpread);
            _contentGenerator = new ContentGenerator(_obstacles, _startOfLevelPoint.position.z + _generationStartLenght / 4,
                _objectsSpawnRange, _obstaclesSizes.Min, _obstaclesSizes.Max, _obstaclesSpawnRate, _obstaclesSpread);
        }

        private void InitialGeneration()
        {
            _lastPlanePoint = _startOfLevelPoint.position;
            _lastPlanePoint.z -= _generationStartLenght / 2;
            _currentLevelThreshold = _lastPlanePoint.z;

            UpdateLevelGeneration(_startOfLevelPoint.position.z);
        }

        public void UpdateLevelGeneration(float currentPosition)
        {
            while (currentPosition+_aheadLenghtOfGeneration > _currentLevelThreshold)
            {
                _currentLevelThreshold += _planeLenght;
                _lastPlanePoint.z += _planeLenght;
                SpawnPlane();
                ClearPastPlanes(currentPosition);
            }
        
        }

        private void SpawnPlane()
        {
            Transform plane = Instantiate(_plane, _lastPlanePoint, Quaternion.identity);
            _cubeWallsGenerator.GenerateWallsForPlane(plane,_planeLenght);
            _contentGenerator.GenerateContent(plane,_planeLenght);
            _placedPlanes.Add(plane);
        }
    

        private void ClearPastPlanes(float currentPosition)
        {
            for (int i = 0; i < _placedPlanes.Count; i++)
            {
                var plane = _placedPlanes[i];
                if (plane.position.z <= currentPosition - _generationStartLenght/2)
                {
                    _placedPlanes.Remove(plane);
                    i--;
                    Destroy(plane.gameObject);
                }
            }
        }


    }
}
