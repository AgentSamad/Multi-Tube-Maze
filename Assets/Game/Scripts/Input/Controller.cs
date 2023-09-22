using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Controller : MonoBehaviour
{
    [SerializeField] private ControlConfig controlConfig;
    private TwistInput _mTwistInput;
    private float _rotationSpeed;
    private float _minThreshold;
    private Transform _tube;
    private bool canControl;

    private void Awake()
    {
        _rotationSpeed = controlConfig.rotationSpeed;
        _minThreshold = controlConfig.minimumThresholdInput;

        GameEvents.SetControl += SetControl;
    }

    void Start()
    {
        _mTwistInput = new TwistInput();
        _tube = FindObjectOfType<TubeGenerator>().transform;
    }

    private void OnDisable()
    {
        GameEvents.SetControl -= SetControl;
    }

    void SetControl(bool c)
    {
        canControl = c;
    }


    void Update()
    {
        if (!canControl) return;
        Movement();
    }

    void Movement()
    {
        _mTwistInput.Calculate();

        if (Mathf.Abs(_mTwistInput.AngleDifference) < _minThreshold) return;


        var targetRotation =
            _tube.transform.rotation * Quaternion.Euler(Vector3.forward * (_mTwistInput.AngleDifference));
        _tube.localRotation =
            Quaternion.Slerp(_tube.localRotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }
}