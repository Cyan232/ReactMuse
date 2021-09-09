using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigthController : MonoBehaviour
{
    private Light _ligth;

    [SerializeField] private int _chanel=1;
    [HideInInspector] public int Chanel { get => _chanel; set => _chanel = value; }

    [SerializeField] private int _baseIntensity=10;
    [HideInInspector] public int BaseIntensity { get => _baseIntensity; set => _baseIntensity = value; }

    [SerializeField] private int _intensity=200;
    [HideInInspector] public int Intensity { get => _intensity; set => _intensity = value; }

    void Awake()
    {
        Setup();
    }

    void FixedUpdate()
    {
        UpdateIntensity();
    }


    private void Setup()
    {
        _ligth = GetComponent<Light>();
    }

    private void UpdateIntensity()
    {
        _ligth.intensity = _baseIntensity + _intensity * GameManager.Entity.MusicHandler.FrecuencyBandsConstantExp[_chanel];
    }
}
