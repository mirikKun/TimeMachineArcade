using System.Collections.Generic;
using Logic.Generators.Data;
using UnityEngine;
using Zenject;

namespace Logic.Generators
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private SettingObjectsData[] _allSettingObjectsData;
        private SettingObjectsData _currentGenerationData;

        [SerializeField] private TimePortal _timePortal;
        
        [SerializeField] private Transform _startOfLevelPoint;
        [SerializeField] private float _obstacleGenerationStart = 100;
        [SerializeField] private float _aheadLenghtOfGeneration = 80;
        [SerializeField] private float _preGenerationStartLenght = 20;

        [SerializeField] private float _portalSpawnPeriod=200;
        private float _currentPortalSpawnThreshold;
        

        private float _obstaclesAppearingRate;


        private int _lastPlaneIndex = -1;
        private int _lastSettingIndex = 0;
        private float _currentLevelThreshold;
        private Vector3 _lastPlanePoint;
        private WallsGenerator _wallsGenerator;

        private ContentGenerator _contentGenerator;

        private List<Plane> _placedPlanes = new();

        [Inject]
        private void Construct(CarMover carMover)
        {
            carMover.OnMoving += UpdateLevelGeneration;
        }

        public void ChangeSetting()
        {
            GetNewSetting();
            ResetAll();
        }

        public void SetStartSetting()
        {
            _lastSettingIndex = 0;
            _currentGenerationData = _allSettingObjectsData[_lastSettingIndex];

        }
        private void GetNewSetting()
        {
            _lastSettingIndex++;
            if (_lastSettingIndex >= _allSettingObjectsData.Length)
            {
                _lastSettingIndex = 0;
            }

            SettingObjectsData newSettingObjectsData = _allSettingObjectsData[_lastSettingIndex];
            _currentGenerationData = newSettingObjectsData;
        }

        private void InitGenerators()
        {
            _wallsGenerator = new WallsGenerator(_currentGenerationData, _startOfLevelPoint.position.z - _preGenerationStartLenght);
            _contentGenerator = new ContentGenerator(_startOfLevelPoint.position.z + _obstacleGenerationStart / 4,
            _currentGenerationData.ObstaclesSpawnRate, _currentGenerationData.ObstaclesSpread);
        }

        private void InitialGeneration()
        {
            _lastPlaneIndex = -1;
            _lastPlanePoint = _startOfLevelPoint.position;
            _lastPlanePoint.z -= _preGenerationStartLenght;
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

            if (currentPosition > _currentPortalSpawnThreshold)
            {
                Instantiate(_timePortal,_lastPlanePoint,Quaternion.identity, _placedPlanes[^1].transform);
                _currentPortalSpawnThreshold += _portalSpawnPeriod;
            }
        }

        public Plane GetRandomPlane()
        {
            Plane plane = _currentGenerationData.Planes[Random.Range(0, _currentGenerationData.Planes.Length)];
            while (_lastPlaneIndex != plane.EnterIndex&&_currentGenerationData.Planes.Length>1)
            {
                plane = _currentGenerationData.Planes[Random.Range(0, _currentGenerationData.Planes.Length)];
            }

            _lastPlaneIndex = plane.ExitIndex;

            return plane;
        }

        private void SpawnPlane(Plane planePrefab)
        {
            Plane plane = Instantiate(planePrefab, _lastPlanePoint, Quaternion.identity);
            _wallsGenerator.GenerateWallsForPlane(plane);
            _contentGenerator.GenerateContent(plane);
            _placedPlanes.Add(plane);
        }

        public void ResetAll()
        {
            for (int i = 0; i < _placedPlanes.Count; i++)
            {
                var plane = _placedPlanes[i];
               
                _placedPlanes.Remove(plane);
                i--;
                Destroy(plane.gameObject);
                
            }

            InitGenerators();
            InitialGeneration();
            _currentPortalSpawnThreshold = _portalSpawnPeriod;
        }

        private void ClearPastPlanes(float currentPosition)
        {
            for (int i = 0; i < _placedPlanes.Count; i++)
            {
                var plane = _placedPlanes[i];
                if (plane.Position().z <= currentPosition - _obstacleGenerationStart)
                {
                    _placedPlanes.Remove(plane);
                    i--;
                    Destroy(plane.gameObject);
                }
            }
        }
    }
}