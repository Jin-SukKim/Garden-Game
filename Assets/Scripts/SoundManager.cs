/**
 * Authors: Sagar Sidhu & Amrit Badesha 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SoundManager : MonoBehaviour
{
    private static AudioClip gunShotSound;
    static Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();

    public static AudioSource audioSourceFX;

        void Start()
    {
        //gunShotSound = Resources.Load<AudioClip>("sounds/refreshAudio1");
        audioSourceFX = GetComponent<AudioSource>();
        AudioClip[] allSounds = Resources.LoadAll("Sounds", typeof(AudioClip)).Cast<AudioClip>().ToArray();
        for (int i = 0; i < allSounds.Length; i++)
        {
            sounds.Add(allSounds[i].name, allSounds[i]);
        }

        foreach (KeyValuePair<string, AudioClip> item in sounds)
        {
            Debug.Log("Key: {0}, Value: {1}" + item.Key + item.Value);
        }
    }
    public static void PlaySound(string key)
    {
        if(sounds.ContainsKey(key)) {
            audioSourceFX.PlayOneShot(sounds[key]);
        }
    }
        public static void PlaySound(string key, AudioSource source)
    {
        if(sounds.ContainsKey(key)) {
            source.PlayOneShot(sounds[key]);
        } else {
            Debug.LogError("Error in playing sound");
        }
    }

    public static void PlaySoundatLocation(string key, Vector3 position) {
        if(sounds.ContainsKey(key)) {
            AudioSource.PlayClipAtPoint(sounds[key], position);
        }
    }
    
    public static void PlaySoundatLocation(AudioClip key, Vector3 position) {
            AudioSource.PlayClipAtPoint(key, position);
        
    }

}
