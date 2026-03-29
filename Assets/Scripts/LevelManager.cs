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
    public int GetRequiredKeys()
    {
        if (level == 3) return 3;

        if (level >= 2) return 1;

        return 0;
    }
    public bool NeedKey()
    {
        return GetRequiredKeys() > 0;
    }
    public bool CanRun()
    {
        return level >= 2;
    }
}