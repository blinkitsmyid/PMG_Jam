using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bumble : MonoBehaviour
{
    [SerializeField]SpriteRenderer _spriteRenderer;
    [SerializeField] private float defaultDuration = 3f;
    public static Bumble Instance;
    void Awake()
    {
        Instance = this;
        HideBumble();
    }
    public void ShowBumble(Sprite sprite)
    {
        if (_spriteRenderer == null) return;
        _spriteRenderer.sprite = sprite;
        _spriteRenderer.enabled = true;
    }

    public void HideBumble()
    {
        if (_spriteRenderer == null) return;
        _spriteRenderer.enabled = false;
    }

 
 

}
