using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TransitionEffect : MonoBehaviour
{
    [SerializeField] private RectTransform _image;
    [SerializeField] private Ease _easeType;
    [SerializeField] private float _duration;
    [SerializeField] private float _scale = 100;
    private Camera _camera;

    private void Start()
    {
        _camera=Camera.main;
    }

    public void AnimateTransition(Vector3 pointOnScene)
    {
        _image.position = _camera.WorldToScreenPoint(pointOnScene);
        _image.localScale = Vector3.one * _scale;
        _image.gameObject.SetActive(true);
        _image.DOScale(Vector3.zero, _duration)
            .SetEase(_easeType)
            .OnComplete((() =>
            {
                _image.gameObject.SetActive(false);
            }));

    }
    
}
