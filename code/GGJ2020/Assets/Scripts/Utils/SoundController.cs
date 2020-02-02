using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public enum Sounds {
        BULB_BREAK,
        COLLIDER_HIT,
        PICK_UP_OBJ,
        PUT_DOWN_OBJ
    }

    [Serializable]
    public class ListWrapper {
        public List<AudioClip> Clips = new List<AudioClip>();
    }        
    
    [HideInInspector]
    public List<ListWrapper> _AudioClipses = new List<ListWrapper>();

    [HideInInspector]
    public List<float> _SourcesBaseVolumes = new List<float>();

    public AudioSource[] _AudioSources;

    public static SoundController Instance;
    
    protected void Awake() {
        Instance = this;
    }

    protected void OnEnable() {
        TinyTokenManager
            .Instance
            .Register<Msg.PlaySound>(this, PlaySound);
    }

    protected void OnDisable() {
        TinyTokenManager
            .Instance
            .UnregisterAll(this);
    }

    private void PlaySound(Msg.PlaySound msg) {

        foreach(var audioSource in _AudioSources)
            if(!audioSource.isPlaying){                                                        
                
                audioSource.PlayOneShot(
                    _AudioClipses[(int)msg.Sound].Clips.Count == 0 ? 
                        null : 
                        _AudioClipses[(int)msg.Sound].Clips.GetRandom(), 
                    msg.Volume * _SourcesBaseVolumes[(int)msg.Sound]);
                break;
            }
    }

    public AudioSource PlayLooped(AudioClip clip, float volume) {
        foreach(var audioSource in _AudioSources)
            if(!audioSource.isPlaying) {

                audioSource.loop = true;
                audioSource.clip = clip;
                audioSource.volume = volume;
                
                audioSource.Play();
                
                return audioSource;
            }

        return null;
    }
}
