using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Plane : MonoBehaviour
{
    public float Lenght;
    public float Width;
    public Obstacle[] Obstacles;

    public int EnterIndex;
    public int ExitIndex;
    public Vector3 Position() => transform.position;


}
