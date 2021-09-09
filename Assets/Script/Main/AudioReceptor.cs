using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReceptor : MonoBehaviour
{
    [SerializeField] private int _chanel = 0;
    [HideInInspector] public int Chanel { get => _chanel; private set => _chanel = value; }
   

    [SerializeField] private float _maxScale = 2;
    [SerializeField] private float _scaleSpeed = 5;

    [SerializeField] private Vector3 _scaleDirection = Vector3.one;
    [HideInInspector] public Vector3 ScaleDirection { get => _scaleDirection; private set => _scaleDirection = value; }

    // Update is called once per frame
    void Update()
    {
        ChangeScale();
    }

    private void ChangeScale()
    {
        transform.localScale = GetScale();
    }

    private Vector3 GetScale()
    {
        if(Chanel <= GameManager.Entity.MusicHandler.Bands )
        {
            return new Vector3(GetLength(transform.localScale.x, _scaleDirection.x), GetLength(transform.localScale.y, _scaleDirection.y),GetLength(transform.localScale.z, _scaleDirection.z));
        }
        else return Vector3.one;
    }

    private float GetLength(float vectorval, float vectorWeigth)
    {
        return Mathf.Lerp(vectorval, 1 + (vectorWeigth * ( GameManager.Entity.MusicHandler.FrecuencyBandsConstantExp[Chanel] * _maxScale)), Time.deltaTime * _scaleSpeed);
    }
}
