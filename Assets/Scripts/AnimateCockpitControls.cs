using System.Collections.Generic;
using UnityEngine;

public class AnimateCockpitControls : MonoBehaviour
{
    [SerializeField] Transform _joystick;
    [SerializeField] Vector3 _joystickRange = Vector3.zero;

    [SerializeField] List<Transform> _throttles;
    [SerializeField] Vector3 _throttleRange = Vector3.zero;

    [SerializeField] ShipMovementInput _movementInput;

    IMovementControls ControlInput => _movementInput.movementControls;

    void Update()
    {
        // Animate joystick
        _joystick.localRotation = Quaternion.Euler(
            x: ControlInput.pitchAmount * _joystickRange.x,
            y: ControlInput.yawAmount * _joystickRange.y,
            z: ControlInput.rollAmount * _joystickRange.z
        );

        // Animate throttles
        if (_throttles == null || _throttles.Count == 0)
            return;

        foreach (Transform throttle in _throttles)
        {
            if (throttle == null)
                continue;

            Vector3 throttleRotation = throttle.localEulerAngles;
            throttleRotation.x = ControlInput.thrustAmount * _throttleRange.x;

            throttle.localRotation = Quaternion.Euler(throttleRotation);
        }
    }
}
