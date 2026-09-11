using NUnit.Framework;
using System;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(Skybox))]

public class SkyBoxSetter : MonoBehaviour
{
    [SerializeField] System.Collections.Generic.List<Material> _skyboxMaterials;

    Skybox _skybox;



    private void Awake()
    {
        _skybox = GetComponent<Skybox>();
    }

    void OnEnable()
    {
        ChangeSkybox(0);
    }

    void ChangeSkybox(int Skybox)
    {
        if (_skybox != null && Skybox >= 0 && Skybox < _skyboxMaterials.Count)
        {
          _skybox.material = _skyboxMaterials[Skybox];
        }
    }
}
