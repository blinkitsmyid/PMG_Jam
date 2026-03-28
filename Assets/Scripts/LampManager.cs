using System.Collections.Generic;
using UnityEngine;

public class LampManager : MonoBehaviour
{
    public static LampManager Instance { get; private set; }

    private List<Lamp> lamps = new List<Lamp>();

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterLamp(Lamp lamp)
    {
        lamps.Add(lamp);
    }

    public bool AreAllLampsOff()
    {
        foreach (var lamp in lamps)
        {
            if (lamp.IsOn())
                return false;
        }
        return true;
    }
    public int GetActiveLampsCount()
    {
        int count = 0;

        foreach (var lamp in lamps)
        {
            if (lamp.IsOn())
                count++;
        }

        return count;
    }
}