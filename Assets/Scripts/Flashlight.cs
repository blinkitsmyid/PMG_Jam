using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light2D flashlight;
    private bool _isFlashlightOn = true;
    
    void Start()
    {
        flashlight.enabled = _isFlashlightOn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
