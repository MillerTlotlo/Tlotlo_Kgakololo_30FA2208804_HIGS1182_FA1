using System;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{
    [SerializeField][Required] Transform _Target;
    private void LateUpdate()
    {
        transform.rotation = _Target.rotation;  
    }

    private class RequiredAttribute : Attribute
    {
    }
}
