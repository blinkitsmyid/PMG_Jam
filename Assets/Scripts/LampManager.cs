using System.Collections.Generic;
using UnityEngine;

public class LampManager : MonoBehaviour
{
    public static LampManager Instance { get; private set; }

    private List<Lamp> lamps = new List<Lamp>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterLamp(Lamp lamp)
    {
        if (!lamps.Contains(lamp))
        {
            lamps.Add(lamp);
            Debug.Log("Lamp registered: " + lamp.name);
        }
    }

    public bool AreAllLampsOff()
    {
        foreach (var lamp in lamps)
        {
            Debug.Log("AreAllLampsOff: " + lamp.name);
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