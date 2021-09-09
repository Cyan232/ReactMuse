using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [HideInInspector] public AudioSource AudioSource { get => _audioSource; set => _audioSource = value; }

    private void Start()
    {
        _audioSource = transform.GetComponentInChildren<AudioSource>();
        //GameManager.Entity.SetMainMusic(_audioSource);
    }
}
