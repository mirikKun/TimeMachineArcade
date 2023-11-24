using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailsSwitch : MonoBehaviour
{
    private TrailRenderer[] _trailRenderers;
    private void Start()
    {
        _trailRenderers = GetComponentsInChildren<TrailRenderer>();
    }

    public void ClearTrails()
    {
        foreach (var trailRenderer in _trailRenderers)
        {
            trailRenderer.Clear();
        }
    }
   
}
