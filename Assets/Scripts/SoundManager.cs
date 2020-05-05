using System;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource EfxSource;

    public SoundType SoundType;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public void PlayShot()
    {
        if (gameObject.activeInHierarchy)
        {
            EfxSource.clip = SoundType.Fire;
            EfxSource.PlayOneShot(EfxSource.clip);
        }
    }



    //public Button AudioUnmuted;
    //public Button AudioMuted;
    //private bool _toggle;

    //public bool Toggle
    //{
    //    get => _toggle;
    //    set
    //    {
    //        _toggle = value;
    //        AudioMuted.gameObject.SetActive(_toggle);
    //        AudioUnmuted.gameObject.SetActive(!_toggle);
    //        gameObject.SetActive(!_toggle);
    //    }
    //}

    //public Button MainAudioUnmuted;
    //public Button MainAudioMuted;
    //private bool _toggleMain;

    //public bool ToggleMain
    //{
    //    get => _toggleMain;
    //    set
    //    {
    //        _toggleMain = value;
    //        MainAudioMuted.gameObject.SetActive(_toggleMain);
    //        MainAudioUnmuted.gameObject.SetActive(!_toggleMain);
    //        gameObject.SetActive(!_toggleMain);
    //    }
    //}


    //public void Mute()
    //{
    //    Toggle = !Toggle;
    //}

    //public void MainMute()
    //{
    //    ToggleMain = !ToggleMain;
    //}

}

public class SoundType
{
    public AudioClip Fire;
    public AudioClip Reload;

    public class Rifle : SoundType 
    {
        public Rifle()
        {
            Fire = Resources.Load("Sounds/Rifle") as AudioClip;
        }
    }

    public class Shotgun : SoundType { 
        public Shotgun() 
        { 
            Fire = Resources.Load("Sounds/Shotgun") as AudioClip;
        }
    }
    public class Hangdun : SoundType { }
    public class Lasergun : SoundType { }

}