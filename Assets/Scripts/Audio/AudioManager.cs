using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip MenuMusic;
    public AudioClip LevelMusic;
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public static AudioManager Instance;

    private bool isMusicOn = true;
    private bool isSoundOn = true;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex <= 1)
            PlayMusic(MenuMusic);
        else
            PlayMusic(LevelMusic);
    }

    void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        musicSource.mute = !isMusicOn;
    }

    public void ToggleSFX()
    {
        isSoundOn = !isSoundOn;
        sfxSource.mute = !isSoundOn;
    }

    public bool IsMusicOn() => isMusicOn;
    public bool IsSFXOn() => isSoundOn;
}