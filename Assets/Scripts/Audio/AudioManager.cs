using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip levelMusic;

    [Header("SFX")]
    public AudioClip hoverSound;
    public AudioClip clickSound;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip keySound;
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public AudioClip lampSwitchSound;
    public AudioClip whistleSound;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    private bool isMusicOn = true;
    private bool isSoundOn = true;

    void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // создаём AudioSource автоматически
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!isMusicOn) return;

        if (scene.buildIndex <= 1)
            PlayMusic(menuMusic);
        else
            PlayMusic(levelMusic);
    }

    // 🎵 Музыка
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        if (musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    // 🔊 SFX
    public void PlaySFX(AudioClip clip)
    {
        if (!isSoundOn || clip == null) return;

        sfxSource.PlayOneShot(clip);
    }

    // быстрые методы
    public void PlayClick() => PlaySFX(clickSound);
    public void PlayHover() => PlaySFX(hoverSound);
    public void PlayWin() => PlaySFX(winSound);
    public void PlayLose() => PlaySFX(loseSound);
    public void PlayKey() => PlaySFX(keySound);
    public void PlayDoorOpen() => PlaySFX(doorOpenSound);
    public void PlayDoorClose() => PlaySFX(doorCloseSound);
    public void PlayLamp() => PlaySFX(lampSwitchSound);
    public void PlayWhistle() => PlaySFX(whistleSound);

    // ⚙️ настройки
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