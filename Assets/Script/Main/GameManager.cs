using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _entity;
    public static GameManager Entity 
    {
        get
        {
            if(_entity == null)
            {
                GameObject gameManager = new GameObject("GameManager", new System.Type[]{typeof(GameManager), typeof(MusicHandler)});
                //_entity = gameManager.GetComponent<GameManager>();
                _entity.Setup();
                return _entity;
            }
            else
            {
                return _entity;
            }
        }
    }
    #endregion

    private AudioSource _mainMusic = null;
    public AudioSource MainMusic { get => _mainMusic; set => _mainMusic = value; }

    private MusicHandler _musicHandler;
    public MusicHandler MusicHandler { get => _musicHandler; set => _musicHandler = value; }

    private void Awake()
    {
        _entity = this;
        
    }
   
    public void SetMainMusic(AudioSource music)
    {
        this._mainMusic = music;
    }

    private void Setup()
    {
        _musicHandler = this.GetComponent<MusicHandler>();
        _mainMusic = FindObjectOfType<AudioSource>();
        _mainMusic.time += 10;
    }
}
