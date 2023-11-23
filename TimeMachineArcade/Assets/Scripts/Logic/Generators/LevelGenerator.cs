using System.Collections.Generic;
using Logic.Generators.Data;
using UnityEngine;
using Zenject;

namespace Logic.Generators
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private SettingObjectsData _cityObjectsData;

        [SerializeField] private Transform _startOfLevelPoint;
        [SerializeField] private float _generationStartLenght = 100;
        [SerializeField] private float _wallSpread = 0.3f;
        [SerializeField] private float _wallOffset = 0.5f;
        [SerializeField] private float _obstaclesSpawnRate = 3;
        [SerializeField] private float _obstaclesSpread = 0.4f;
        [SerializeField] private float _aheadLenghtOfGeneration = 80;
        [SerializeField] private float _objectsSpawnRange = 7;
        private float _obstaclesAppearingRate;


        private int _lastPlaneIndex = 0;
        private float _currentLevelThreshold;
        private Vector3 _lastPlanePoint;
        private WallsGenerator _cubeWallsGenerator;

        private ContentGenerator _contentGenerator;

        private List<Plane> _placedPlanes = new();

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
            _contentGenerator = new ContentGenerator(_startOfLevelPoint.position.z + _generationStartLenght / 4, _obstaclesSpawnRate, _obstaclesSpread);
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
            while (currentPosition + _aheadLenghtOfGeneration > _currentLevelThreshold)
            {
                Plane plane = GetRandomPlane();
                _currentLevelThreshold += plane.Lenght;
                _lastPlanePoint.z += plane.Lenght/2;
                SpawnPlane(plane);
                ClearPastPlanes(currentPosition);
                _lastPlanePoint.z += plane.Lenght/2;

            }
        }

        public Plane GetRandomPlane()
        {
            Plane plane = _cityObjectsData.Planes[Random.Range(0, _cityObjectsData.Planes.Length)];
            while (_lastPlaneIndex != plane.EnterIndex)
            {
                plane = _cityObjectsData.Planes[Random.Range(0, _cityObjectsData.Planes.Length)];
            }

            _lastPlaneIndex = plane.ExitIndex;

            return plane;
        }

        private void SpawnPlane(Plane planePrefab)
        {
            Plane plane = Instantiate(planePrefab, _lastPlanePoint, Quaternion.identity);
            _cubeWallsGenerator.GenerateWallsForPlane(plane);
            _contentGenerator.GenerateContent(plane);
            _placedPlanes.Add(plane);
        }


        private void ClearPastPlanes(float currentPosition)
        {
            for (int i = 0; i < _placedPlanes.Count; i++)
            {
                var plane = _placedPlanes[i];
                if (plane.Position().z <= currentPosition - _generationStartLenght / 2)
                {
                    _placedPlanes.Remove(plane);
                    i--;
                    Destroy(plane.gameObject);
                }
            }
        }
    }
}