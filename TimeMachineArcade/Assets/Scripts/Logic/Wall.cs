using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wall : MonoBehaviour
{
   [SerializeField] private BoxCollider _collider;

   public Vector2 Size { get; private set; }
   private void OnValidate()
   {
      _collider=GetComponent<BoxCollider>();
   }

   public void SetPosition(Vector3 position)
   {
      transform.position = position;
   }


   public void SetRandomRotation()
   {
      int randomRotation = Random.Range(0, 4);

      transform.eulerAngles = new Vector3(0, (randomRotation) * 90);
      Vector3 size = _collider.size;

      if (randomRotation % 2 == 0)
         Size = new Vector2(size.x, size.z);
      else
         Size = new Vector2(size.z, size.x);
   }
}
