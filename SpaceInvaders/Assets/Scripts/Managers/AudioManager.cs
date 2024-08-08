using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum SoundType { Music, SFX };

/// <summary>
/// Classe que define um som para ser usado pelo AudioManager.
/// </summary>
[Serializable]
public class Sound
{

    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3)]
    public float minPitch = 1;
    [Range(.1f, 3)]
    public float maxPitch = 1;

    public bool loop;

    public SoundType type;

    [HideInInspector]
    public AudioSource source;
}

/// <summary>
/// Classe que gerencia o audio do jogo.
/// </summary>
public class AudioManager : PersistentSingleton<AudioManager>
{
    public Sound[] sounds;
    Sound currentMusic;

    public float mainVolume = 1;
    public float musicVolume = .3f;
    public float effectsVolume = 1;

    protected override void Awake()
    {
        base.Awake();

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }

        UpdateVolumes();
        Play("Music");
    }

    public void Play(string name)
    {
        Sound sound = GetSound(name);
        if (sound == null) return;

        sound.source.pitch = UnityEngine.Random.Range(sound.minPitch, sound.maxPitch);
        if (sound.loop)
        {
            if (sound.source.isPlaying) return;
            sound.source.Play();
        }
        else
            sound.source.PlayOneShot(sound.clip);

        if (sound.type == SoundType.Music)
        {
            if (currentMusic != null)
                currentMusic.source.Stop();

            currentMusic = sound;
        }
    }

    public void Stop(string name)
    {
        Sound sound = GetSound(name);
        sound.source.Stop();
    }

    public Sound GetSound(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            try
            {
                Debug.LogWarning("Sound: " + sound.name + " null!");
            }
            catch
            {
                Debug.LogWarning("Sound: " + name + " not found!");
            }
            return null;
        }
        return sound;
    }

    public float getVolume(SoundType soundType)
    {
        float volume = mainVolume;
        switch (soundType)
        {
            case SoundType.Music:
                volume *= musicVolume;
                break;
            case SoundType.SFX:
                volume *= effectsVolume;
                break;
        }
        return volume;
    }

    public void UpdateVolumes()
    {
        foreach (Sound sound in sounds)
        {
            sound.source.volume = getVolume(sound.type);
        }
    }

    private void OnEnable()
    {
        OnSceneLoaded(SceneManager.GetActiveScene());
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        OnSceneLoaded(scene);
        //ReloadSoundsSource();
    }
    private void OnSceneLoaded(Scene scene)
    {
        //Debug.Log(scene.name);
        switch(scene.name) 
        {
            case ("MainMenu"):
                Play("MenuMusic");
                break;
            case ("MAPA1"):
                Play("SwampMusic");
                break;
            case ("MAPA2"):
                Play("BossMusic");
                break;
        }
    }

    private void ReloadSoundsSource()
    {
        int index = 0;
        foreach (AudioSource oldSource in gameObject.GetComponents<AudioSource>())
        {
            Destroy(oldSource);

            sounds[index].source = gameObject.AddComponent<AudioSource>();
            sounds[index].source.clip = sounds[index].clip;
            sounds[index].source.volume = sounds[index].volume;
            sounds[index].source.loop = sounds[index].loop;
            index++;
            Debug.Log(index + " " + sounds[index].source == null);
            if (index >= sounds.Length)
                return;
        }


    }
}
