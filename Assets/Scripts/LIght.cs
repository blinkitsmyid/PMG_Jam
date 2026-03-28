using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lamp : MonoBehaviour
{
    [SerializeField] private bool isOn = false;
    [SerializeField] private Light2D lamp;

    
    private void Awake()
    {
        LampManager.Instance.RegisterLamp(this);
    }
    private void Start()
    {
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