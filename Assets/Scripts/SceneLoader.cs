using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevelSelect()
    {
        AudioManager.Instance.PlayClick();
        SceneManager.LoadScene("01_SelectLevel");
    }
    
    public void LoadLevel(int levelIndex)
    {
       
        SceneManager.LoadScene(levelIndex);
        Time.timeScale = 1f;
    }
    
    public void LoadMenu()
    {
        AudioManager.Instance.PlayClick();
        SceneManager.LoadScene("00_Menu");
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    
    
}