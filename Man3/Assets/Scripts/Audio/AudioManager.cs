using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    void Awake() { instance = this; }
    //Sound Effects
    public AudioClip sfx_landing, sfx_cherry,sfx_attack;
    //Music
    public AudioClip music_tiktok, music;
    //Current Music Object
    public GameObject currentMusicObject;

    //Sound Object
    public GameObject soundObject;

    public void PlaySFX(string sfxName)
    {
        switch (sfxName)
        {
            case "landing":
                SoundObjectCreation(sfx_landing);
                break;
            case "cherry":
                SoundObjectCreation(sfx_cherry);
                break;
            case "attack":
                SoundObjectCreation(sfx_attack);
                break;
            default:
                break;
        }
    }

    void SoundObjectCreation(AudioClip clip)
    {
        //Create SoundsObject gameobject
        GameObject newObject = Instantiate(soundObject, transform);
        //Assign audioclip to its audiosource
        newObject.GetComponent<AudioSource>().clip = clip;
        //Play the audio
        newObject.GetComponent<AudioSource>().Play();
    }

    public void PlayMusic(string musicName)
    {
        switch (musicName)
        {
            case "tiktok":
                MusicObjectCreation(music_tiktok);
                break;
            case "nhac":
                MusicObjectCreation(music);
                break;
            default:
                break;
        }
    }

    void MusicObjectCreation(AudioClip clip)
    {
        //Check if there's an existing music object, if so delete it
        if (currentMusicObject)
            Destroy(currentMusicObject);
        //Create SoundsObject gameobject
        currentMusicObject = Instantiate(soundObject, transform);
        //Assign audioclip to its audiosource
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        //Make the audio source looping
        currentMusicObject.GetComponent<AudioSource>().loop = true;
        //Play the audio
        currentMusicObject.GetComponent<AudioSource>().Play();
    }
}
