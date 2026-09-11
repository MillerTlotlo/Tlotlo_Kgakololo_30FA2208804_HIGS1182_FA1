using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class DesktopMovementControls : MovementControlsBase
{
    [SerializeField] float _deadZoneRadius = 0.1f;

    Vector2 ScreenCenter => new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

    public override float yawAmount
    {
        get
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();

            float yaw = (mousePosition.x - ScreenCenter.x) / ScreenCenter.x;

            return Mathf.Abs(yaw) > _deadZoneRadius ? yaw : 0f;
        }
    }

    public override float pitchAmount
    {
        get
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();

            float pitch = (mousePosition.y - ScreenCenter.y) / ScreenCenter.y;

            return Mathf.Abs(pitch) > _deadZoneRadius ? pitch : 0f;
        }
    }

    public override float rollAmount
    {
        get
        {
            if (Keyboard.current.aKey.isPressed)
            {
                return 1f;
            }

            return Keyboard.current.dKey.isPressed ? -1f : 0f;
        }
    }

    public override float thrustAmount
    {
        get
        {
            if (Keyboard.current.wKey.isPressed)
            {
                return 1f;
            }

            return Keyboard.current.sKey.isPressed ? -1f : 0f;
        }
    }
}