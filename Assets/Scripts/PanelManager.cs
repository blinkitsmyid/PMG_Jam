using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PanelManager : MonoBehaviour
{
    public static PanelManager Instance;
    private PlayerInputActions _playerInputActions;
    public GameObject pausePanel;
    public GameObject losePanel;
    public GameObject winPanel;

    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
        _playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _playerInputActions.Enable();
        _playerInputActions.UI.Pause.performed += _ => Pause();
    }
    private void OnDisable()
    {
        _playerInputActions.Disable();
    }
    // ⏸️ PAUSE
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    
    public void Lose()
    {
        AudioManager.Instance.musicSource.Stop();
        losePanel.SetActive(true);
        AudioManager.Instance.PlayLose();
        Time.timeScale = 0f;
    }

    // 🏆 WIN
    public void Win()
    {
        AudioManager.Instance.musicSource.Stop();
        winPanel.SetActive(true);
        AudioManager.Instance.PlayWin();
        Time.timeScale = 0f;
    }
    
}