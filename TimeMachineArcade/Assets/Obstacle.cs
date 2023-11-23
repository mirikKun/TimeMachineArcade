using Logic;
using UnityEngine;

[RequireComponent(typeof(ExplosionEffect))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _acceleration=1;
    [SerializeField] private int _coinsOnDestroy = 1;
    private ExplosionEffect _explosionEffect;
    private void Start()
    {
        _explosionEffect = GetComponent<ExplosionEffect>();
    }

    private void OnTriggerEnter(Collider other)
    {

        CarMover carMover = other.GetComponent<CarMover>();
        if (carMover)
        {
            _explosionEffect.ChangeParent(transform.parent);
            _explosionEffect.PlayEffect();
            carMover.OperateHit(_acceleration,_coinsOnDestroy,transform.position);
            Destroy(gameObject);
        }
        
    }
}
