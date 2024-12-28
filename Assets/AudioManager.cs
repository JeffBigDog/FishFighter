using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    // TODO: Add sound catgeories and METHODS for calling sounds from specific lists so that sounds can have duplicate names
    // Example: combat_start can be the same sound, but called in a different list so that it's different based on which area the player is in.

    [SerializeField] AudioMixerGroup masterGroup;
    [SerializeField] AudioMixerGroup sfxGroup;
    [SerializeField] AudioMixerGroup musicGroup;

    public Sound[] sounds;
    public Sound[] combatSFX_generic;
    public Sound[] music;

    private string activeMusic;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        // Check if there's an existing AudioManager
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject); // Destroy the previous instance
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = sfxGroup;
        }

        foreach (Sound s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = musicGroup;
        }

        foreach (Sound s in combatSFX_generic)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = sfxGroup;
        }

        Play("Fish type beat");
    }

    public string GetActiveMusic()
    {
        return activeMusic;
    }

    public void StopActiveMusic()
    {
        Stop(activeMusic);
    }


    public void Play (string name) // REWRITE THIS FUNCTION SO IT ONLY QUERIES A SPECIFIC LIST 
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            s = Array.Find(music, sound => sound.name == name);
            if (s == null)
            {
                s = Array.Find(combatSFX_generic, sound => sound.name == name);
                if (s == null)
                {
                    
                }
            }
        }
        s.source.Play();
    }

    public void ChangePitch (string name, float value)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            s = Array.Find(music, sound => sound.name == name);
            if (s == null)
            {
                s = Array.Find(combatSFX_generic, sound => sound.name == name);
                if (s == null)
                    return;
            }
        }
        s.source.pitch = value;
    }
    public void UpdateVolume (string name, float value)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            s = Array.Find(music, sound => sound.name == name);
            if (s == null)
            {
                s = Array.Find(combatSFX_generic, sound => sound.name == name);
                if (s == null)
                    return;
            }
        }
        s.source.volume *= value;
    }

    public float GetVolume (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            s = Array.Find(music, sound => sound.name == name);
            if (s == null)
            {
                s = Array.Find(combatSFX_generic, sound => sound.name == name);
            }
        }
        return (s.source.volume);
    }

    IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        audioSource.volume = 0f;
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += Time.deltaTime / duration;
            yield return null;
        }
    }

    // Function to fade in a specific sound over 2 seconds
    public void FadeIn(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            s = Array.Find(music, sound => sound.name == name);
            if (s == null)
            {
                s = Array.Find(combatSFX_generic, sound => sound.name == name);
                if (s == null)
                {
                    Debug.Log("Could not find sound " + name);
                    return;
                }
            }
        }

        StartCoroutine(FadeIn(s.source, 2f));
    }

    public void ChangeVolume (string name, float value)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            s = Array.Find(music, sound => sound.name == name);
            if (s == null)
            {
                s = Array.Find(combatSFX_generic, sound => sound.name == name);
                if (s == null)
                    return;
            }
        }
        s.source.volume = value;
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            s = Array.Find(music, sound => sound.name == name);
            if (s == null)
            {
                s = Array.Find(combatSFX_generic, sound => sound.name == name);
                if (s == null)
                    return;
            }
        }
        s.source.Pause();
    }

    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            s = Array.Find(music, sound => sound.name == name);
            if (s == null)
            {
                s = Array.Find(combatSFX_generic, sound => sound.name == name);
                if (s == null)
                    return;
            }
        }
        s.source.Stop();
    }

    public void UpdateMasterVolume(int level)
    {
        float updatedValue = (10 - level) * -6f;
        if (level == 0)
            updatedValue = -80f;
        masterGroup.audioMixer.SetFloat("Master", updatedValue);
    }

    public void UpdateSFXVolume(int level)
    {
        float updatedValue = (10 - level) * -6f;
        if (level == 0)
            updatedValue = -80f;
        masterGroup.audioMixer.SetFloat("SFX", updatedValue);
    }

    public void UpdateMusicVolume(int level)
    {
        float updatedValue = (10 - level) * -6f;
        if (level == 0)
            updatedValue = -80f;
        masterGroup.audioMixer.SetFloat("Music", updatedValue);
    }
}