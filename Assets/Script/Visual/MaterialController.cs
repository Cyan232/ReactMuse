using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    private Material _material;
    private Shader _shader;

    private void Awake()
    {
        Setup();
    }

    private void FixedUpdate()
    {
        UpdateShaderValues();
    }

    private void UpdateShaderValues()
    {
        if(_material != null)
        {
            _material.SetFloat("_Ch00", GameManager.Entity.MusicHandler.FrecuencyBandsConstantExp[0]);
            _material.SetFloat("_Ch01", GameManager.Entity.MusicHandler.FrecuencyBandsConstantExp[1]);
            _material.SetFloat("_Ch02", GameManager.Entity.MusicHandler.FrecuencyBandsConstantExp[2]);
            _material.SetFloat("_Ch03", GameManager.Entity.MusicHandler.FrecuencyBandsConstantExp[3]);
        }
    }

    private void Setup()
    {
        _material = GetComponent<Renderer>().sharedMaterial;
        _shader = _material == null ? _shader = null : _shader = _material.shader;
    }
}
