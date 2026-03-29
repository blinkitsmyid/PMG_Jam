using UnityEngine;

public class Door : MonoBehaviour
{
    protected PlayerController player;
    protected bool isPlayerInside = false;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            HintUI.Instance.ShowTemporary(HintMessages.PressRoomIn);
            if (collision.TryGetComponent(out PlayerController p))
            {
                Debug.Log(collision.gameObject.name);
                player = p;
                isPlayerInside = true;
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController p))
        {
            if (player == p)
            {
                isPlayerInside = false;
                player = null;
            }
        }
    }

    public virtual void Interact()
    {
        if (!isPlayerInside || player == null) return;
        Debug.Log("Зайти");
        EnterDoor();
    }

    protected virtual void EnterDoor()
    {
        if (player == null) return;
        
        GameInput.Instance.DisableMovement();
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.SetVisible(false);
        SoundManager.Instance.PlayOpenedDoor();
    }
    protected virtual void ExitDoor()
    {
        if (player == null) return;

        
        GameInput.Instance.EnableMovement();
        player.SetVisible(true);
        SoundManager.Instance.PlayClosedDoor();
    }
    protected void SetPlayerVisible(bool visible)
    {
        if (player == null) return;

        player.SetVisible(visible);
    }
}