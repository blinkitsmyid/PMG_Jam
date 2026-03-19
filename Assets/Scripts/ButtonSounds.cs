using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonSoundManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    void Awake()
    {
        // если AudioSource не назначен — найти его автоматически
        if (sfxSource == null)
        {
            sfxSource = GetComponent<AudioSource>();
        }
    }

    void Start()
    {
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry hoverEntry = new EventTrigger.Entry();
            hoverEntry.eventID = EventTriggerType.PointerEnter;
            hoverEntry.callback.AddListener((data) => { PlayHover(); });
            trigger.triggers.Add(hoverEntry);

            EventTrigger.Entry clickEntry = new EventTrigger.Entry();
            clickEntry.eventID = EventTriggerType.PointerClick;
            clickEntry.callback.AddListener((data) => { PlayClick(); });
            trigger.triggers.Add(clickEntry);
        }
    }

    void PlayHover()
    {
        if (hoverSound != null)
            sfxSource.PlayOneShot(hoverSound);
    }

    void PlayClick()
    {
        if (clickSound != null)
            sfxSource.PlayOneShot(clickSound);
    }
}