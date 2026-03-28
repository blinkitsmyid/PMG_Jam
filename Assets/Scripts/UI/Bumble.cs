using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bumble : MonoBehaviour
{
    [SerializeField]SpriteRenderer _spriteRenderer;
    [SerializeField]SpriteRenderer _currentSprite;
    [SerializeField] private float defaultDuration = 3f;
    public static Bumble Instance;
    void Awake()
    {
        Instance = this;
      
        HideBumble();
    }
    public void ShowBumble(Sprite sprite)
    {
        if (_currentSprite == null || _spriteRenderer == null) return;
        
        _currentSprite.sprite = sprite;
        Debug.Log($"ShowBumble: {_currentSprite}");
        _currentSprite.enabled = true;
        _spriteRenderer.enabled = true;
    }

    public void HideBumble()
    {
        Debug.Log($"HideBumble: {_currentSprite}");
        if (_spriteRenderer == null) return;
        _currentSprite.enabled = false;
        _spriteRenderer.enabled = false;
    }

 
 

}
