using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController player))
        {
            AudioManager.Instance.PlayKey();
            player.GiveKey();
            Destroy(gameObject);
            Debug.Log($"Keys: {player.GetKeys()} / {LevelManager.Instance.GetRequiredKeys()}");
        }
    }
}