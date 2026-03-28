using UnityEngine;
using TMPro;
using System.Collections;

public class HintUI : MonoBehaviour
{
    public static HintUI Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI hintText;

    [Header("Settings")]
    [SerializeField] private float defaultDuration = 2f;

    private Coroutine currentCoroutine;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    // Показать (пока не скроешь вручную)
    public void Show(string message)
    {
        if (hintText == null) return;

        hintText.text = message;
        hintText.gameObject.SetActive(true);
    }

    // Скрыть
    public void Hide()
    {
        if (hintText == null) return;

        hintText.gameObject.SetActive(false);
    }

    // Показать на время
    public void ShowTemporary(string message, float duration = -1f)
    {
        if (hintText == null) return;

        if (duration <= 0)
            duration = defaultDuration;

        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(ShowRoutine(message, duration));
    }

    private IEnumerator ShowRoutine(string message, float duration)
    {
        Show(message);

        yield return new WaitForSeconds(duration);

        Hide();
    }
}