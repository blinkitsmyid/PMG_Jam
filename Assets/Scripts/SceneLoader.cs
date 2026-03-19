using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("01_SelectLevel");
    }
    
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
    
    public void LoadMenu()
    {
        SceneManager.LoadScene("00_Menu");
    }
    
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
    
    
}