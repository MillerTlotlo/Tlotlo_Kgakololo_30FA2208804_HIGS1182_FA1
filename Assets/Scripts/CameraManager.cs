using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    private enum VirtualCameras
    {
        NoCamera,
        CockpitCamera,
        FollowCamera
    }

    [SerializeField]
    private List<GameObject> virtualCameras;

    private void Start()
    {
        SetActiveCamera(VirtualCameras.CockpitCamera);
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SetActiveCamera(VirtualCameras.CockpitCamera);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            SetActiveCamera(VirtualCameras.FollowCamera);
        }
    }

    private void SetActiveCamera(VirtualCameras activeCamera)
    {
        if (activeCamera == VirtualCameras.NoCamera)
            return;

        foreach (GameObject cameraObject in virtualCameras)
        {
            cameraObject.SetActive(
                cameraObject.CompareTag(activeCamera.ToString())
            );
        }
    }
}
