using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource sfxSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip coinSound;
    public AudioClip damageSound;
    public AudioClip healSound;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
        }
    }

    public void PlayHover()
    {
        if (hoverSound != null && sfxSource != null)
            sfxSource.PlayOneShot(hoverSound);
    }

    public void PlayClick()
    {
        if (clickSound != null && sfxSource != null)
            sfxSource.PlayOneShot(clickSound);
    }

    public void PlayWinSound()
    {
        if (winSound != null && sfxSource != null)
            sfxSource.PlayOneShot(winSound);
    }

    public void PlayLoseSound()
    {
        if (loseSound != null && sfxSource != null)
            sfxSource.PlayOneShot(loseSound);
    }
   
    public void PlayDamageSound()
    {
        if (damageSound != null && sfxSource != null)
            sfxSource.PlayOneShot(damageSound);
    }
    
}