using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    private AudioSource _mainMusic;
    public AudioSource MainMusic { get => _mainMusic; set => _mainMusic = value; }

    //Value must be a power of 2 between 64 and 8192
    private const int _samples = 512, _bands = 8;

    public int Bands{get{return _bands;}}

    //Raw values of the filter
    private float[] _sampleRaw = new float[_samples];
    public float[] SampleRaw{get => _sampleRaw;}

    //Median of all the filter
    private float[] _frecuencyBandsConstant = new float[_bands];
    public float[] FrecuencyBandsConstant{get => _frecuencyBandsConstant;}

    //Median of all the filter
    private float[] _frecuencyBandsExp = new float[_bands];
    public float[] FrecuencyBandsConstantExp{get => _frecuencyBandsExp;}
    

    private void Start()
    {
        Setup();
    }

    private void FixedUpdate()
    {
        UpdateRaw();
        UpdateFilterBandsConstant();
        UpdateFilterBandsExp();
    }

    private void Setup()
    {
        _mainMusic = GameManager.Entity.MainMusic;
    }


    private void UpdateRaw()
    {
        MainMusic.GetSpectrumData(_sampleRaw,0,FFTWindow.Blackman);
    }

    private void UpdateFilterBandsConstant()
    {
        float[] TempBands = new float[_bands];

        

        int SpaceBetween = _samples / _bands;

        for(int i = 0; i < _bands; i++)
        {
            for(int j = i*SpaceBetween; j < (i*SpaceBetween) + SpaceBetween; j++)
            {
                TempBands[i] += _sampleRaw[j];
            }
            TempBands[i] = 10 * (TempBands[i]/SpaceBetween);
        }

        _frecuencyBandsConstant = TempBands;
        //_frecuencyBands = GetScaling(TempBands,0,1);
    }

    private void UpdateFilterBandsExp()
    {
        //float[] TempBands = new float[_bands];
        int count = 0;

        for(int i = 0; i < _bands; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2,i) * 2;
            if(i == 7)
            {
                sampleCount += 2;
            }
            for(int j = 0; j < sampleCount; j++)
            {
                average += _sampleRaw[count] * (count +1);
                count++;
            }
            average /= count;
            _frecuencyBandsExp[i] = average;
        }

        //_frecuencyBands = GetScaling(TempBands,0,1);
    }

    public float[] GetScaling(float[] arr, float min, float max)
    {
    float m = (max-min)/(Mathf.Max(arr) - Mathf.Min(arr));
    float c = min - Mathf.Min(arr) *m;
    float[] newarr = new float[arr.Length];
    for(int i=0; i< newarr.Length; i++)
       newarr[i]= m * arr[i] + c;
    return newarr;
    }

}
