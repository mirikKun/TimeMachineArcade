using System;
using DefaultNamespace;
using UnityEngine;
using Zenject;

public class CarMover : MonoBehaviour
{
    [SerializeField] private DriftingEffect _driftingEffect;
    [SerializeField] private AnimationCurve _dragOverDirection;
    [SerializeField] private float _moveSpeed = 8;
    [SerializeField] private float _maxSpeed = 16;
    [SerializeField] private float _drag = 0.98f;
    [SerializeField] private float _steerAngle = 50;
    [SerializeField] private float _traction = 0.01f;
    [SerializeField] private float _angleToMinDrag = 60;

    [SerializeField] private float _angleToDriftProve = 40f;
    [SerializeField] private float _minSpeedToDrift = 5;
    public event Action OnDrifting;
    private float _currentDrag;

    private Vector3 _moveForce;
    private IInput _input;
    private Transform _transform;
    private Vector3 _startPosition;
    private float _forceAngle;


    [Inject]
    private void Construct(IInput input)
    {
        _input = input;
    }

    private void Start()
    {
        _transform = transform;
        _currentDrag = _drag;
        _startPosition = _transform.position;
    }

    private void FixedUpdate()
    {
       
        CarMoving();
        OnDriftingInvoking(_forceAngle);
    }
    
    private void CarMoving()
    {
        _moveForce += GetForwardForce();
        _transform.position += _moveForce * Time.deltaTime;
        _transform.Rotate(GetRotation());
        FixMoveForce();
        _forceAngle = Vector3.Angle(_moveForce.normalized, _transform.forward);
        CalculateDrag(_forceAngle);
    }

    private void FixMoveForce()
    {
        _moveForce *= _currentDrag;
        _moveForce = Vector3.ClampMagnitude(_moveForce, _maxSpeed);
        _moveForce = LerpForceToMoveDirection();
    }

    public void Reset()
    {
        _moveForce = Vector3.zero;
        _transform.rotation = Quaternion.identity;
        _transform.position = _startPosition;
        _currentDrag = _drag;
    }

    private void CalculateDrag(float forceAngle)
    {
        float forceDiff = forceAngle / _angleToMinDrag;
        _currentDrag = _drag + (1 - _drag) * _dragOverDirection.Evaluate(forceDiff);
    }

    private Vector3 LerpForceToMoveDirection() =>
        Vector3.Lerp(_moveForce.normalized, _transform.forward, TractionDelta()) *
        _moveForce.magnitude;

    private void OnDriftingInvoking(float forceAngle)
    {
        if (forceAngle > _angleToDriftProve && _moveForce.magnitude > _minSpeedToDrift)
        {
            OnDrifting?.Invoke();
            _driftingEffect.EnableEffect();
        }
    }

    private float TractionDelta() => _traction * Time.deltaTime;


    private Vector3 GetRotation() => Vector3.up * (_input.RotateInput * _steerAngle * Time.deltaTime);

    private Vector3 GetForwardForce()
    {
        return transform.forward * (_moveSpeed * _input.GasInput * Time.deltaTime);
    }
}