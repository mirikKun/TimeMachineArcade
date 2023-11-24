using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Logic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinsAnimator : MonoBehaviour
{
    [SerializeField] private RectTransform _coinPrefab;
    [SerializeField] private RectTransform _targetPosition;
    [SerializeField] private FloatRange _animationTime;
    [SerializeField] private Ease _easeType;
    [SerializeField] private float _spread;
    
    
    private CoinsPool _coinsPool;

    private Camera _camera;
    private CoinsCounter _coinsCounter;

    private void Start()
    {
        _camera = Camera.main;
        _coinsPool = new CoinsPool(_coinPrefab, transform, 6);
    }

    public void Init(CoinsCounter coinsCounter)
    {
        _coinsCounter = coinsCounter;
    }
    

    public void PlayCoinsAnimation(int coins, Vector3 position)
    {
        Debug.Log(coins);
        for (int i = 0; i < coins; i++)
        {
            float duration = _animationTime.RandomValueInRange;
            Vector3 spread = new Vector3(Random.Range(-_spread, _spread), 0, Random.Range(-_spread, _spread));

            RectTransform coin = _coinsPool.GetElement();
            Vector3 screenPosition = _camera.WorldToScreenPoint(position);

            coin.position = screenPosition + spread;
            coin.DOMove(_targetPosition.position, duration)
                .SetEase(_easeType)
                .OnComplete((() =>
                {
                    _coinsPool.RevertToPool(coin);
                    _coinsCounter.AddVisualCoins(1);
                }));
        }
    }
}