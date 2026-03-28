using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lamp : MonoBehaviour
{
    [SerializeField] private bool isOn = false;
    [SerializeField] private Light2D lamp;

    
   
    private void Start()
    {
        LampManager.Instance.RegisterLamp(this);
        UpdateLight();
    }

    public void Toggle()
    {
        isOn = !isOn;
        UpdateLight();

        Debug.Log("Lamp: " + (isOn ? "ON" : "OFF"));
    }

    private void UpdateLight()
    {
        if (lamp != null)
        {
            lamp.enabled = isOn;
        }
    }

    public bool IsOn() => isOn;
}