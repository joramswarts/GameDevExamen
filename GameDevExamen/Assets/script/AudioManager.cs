using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    // Dit zijn de audio sources voor achtergrondmuziek en geluidseffecten
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    // Hier staan de verschillende audioclips die we gaan gebruiken
    public AudioClip coinClip;
    public AudioClip destroyClip;
    public AudioClip menuMusic;
    public AudioClip inGameMusic;

    // Singleton zodat we maar één AudioManager hebben in het spel
    public static AudioManager instance;

    private void Awake()
    {
        // Checkt of er al een AudioManager bestaat
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Zorgt ervoor dat dit object niet verdwijnt bij het laden van een nieuwe scene
        }
        else
        {
            Destroy(gameObject); // Als er al een is, deze weghalen om dubbele audio te voorkomen
        }
    }

    // Wordt één keer aangeroepen bij de start van het spel
    void Start()
    {
        PlayMusic(inGameMusic); // Start meteen met het afspelen van de in-game muziek
    }

    // Speelt een meegegeven muziekclip af als achtergrondmuziek
    public void PlayMusic(AudioClip musicClip)
    {
        if (musicSource != null && musicClip != null)
        {
            musicSource.clip = musicClip;
            musicSource.loop = true; // Muziek blijft herhalen
            musicSource.Play();
        }
    }

    // Speelt een geluidseffect één keer af
    public void PlaySFX(AudioClip sfxClip)
    {
        if (sfxSource != null && sfxClip != null)
        {
            sfxSource.PlayOneShot(sfxClip); // Speelt het geluid af zonder andere SFX te onderbreken
        }
    }

    // Stopt de huidige achtergrondmuziek
    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
}
