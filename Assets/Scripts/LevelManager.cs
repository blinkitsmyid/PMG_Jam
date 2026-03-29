using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private int level = 1;

    private void Awake()
    {
        Instance = this;
    }

    public int GetLevel()
    {
        return level;
    }

    public bool NeedKey()
    {
        return level >= 2;
    }
    public bool CanRun()
    {
        return level >= 4;
    }
}