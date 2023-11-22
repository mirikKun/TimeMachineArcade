using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{

    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _timeToDestroy;

    public void PlayEffect()
    {
        _particleSystem.Play();
    }

    public void ChangeParent(Transform newParent)
    {
        _particleSystem.transform.SetParent(newParent);
        Destroy(gameObject,_timeToDestroy);
    }
    
}
