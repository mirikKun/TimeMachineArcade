using System;
using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;

public class TimePortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CarMover car = other.GetComponent<CarMover>();
        if (car)
        {
            car.EnterPortal();
        }
    }
}
