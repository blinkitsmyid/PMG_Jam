using UnityEngine;

public class LampSwitch : MonoBehaviour
{
    [SerializeField] private Lamp lamp;

    public void Interact()
    {
        AudioManager.Instance.PlayLamp();
        lamp.Toggle();
    }
}