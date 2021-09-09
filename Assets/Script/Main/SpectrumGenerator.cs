using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpectrumGenerator : MonoBehaviour
{
    public AudioSource _audioSource;
    
    public Texture2D _texture,_texture2;

    public int SpectrumSize = 1024;

    // Start is called before the first frame update
    void Start()
    {
        //_sprite.
       /* GameObject go = new GameObject();
        SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
        renderer.sprite = _sprite;*/
        Setup();
        GenerateSpectrumImage();
    }

    private void GenerateSpectrumImage()
    {
        AudioSource temp = new AudioSource();
        temp = _audioSource;
        

        for(int i = 0; i < SpectrumSize; i++)
        {
            
            temp.time += .1f;    
            temp.Play();
            float[] samples = new float[512];
            temp.GetSpectrumData(samples,0, FFTWindow.Blackman);
            temp.Pause();

            for(int j = 0; j < 512; j++)
            {
               _texture.SetPixel(i,j, new Color(Mathf.Clamp(samples[j],0,1),0.5f,0.5f));
               
            }

        }
        //_audioSource.time = 0;
        _texture.Apply();
        temp.time = 0;
        temp.Play();
    }

    private void Setup()
    {
        _texture = new Texture2D(SpectrumSize,512);
    }

    /*private IEnumerator DrawSpectrum()
    {
        //float currentTime = 0;

        for(int i = 0; i < SpectrumSize; i++)
        {
            _audioSource.time += .1f;
            //currentTime += .1f;
            float[] samples = new float[512];
            _audioSource.GetSpectrumData(samples,0, FFTWindow.Blackman);

            for(int j = 0; j < 512; j++)
            {
               _texture.SetPixel(i,j, new Color(Mathf.Clamp(samples[j],0,1),0.5f,0.5f));
                return YieldInstruction
            }

        }
        _audioSource.time = 0;
        _texture.Apply();
    }s
    }*/


}
