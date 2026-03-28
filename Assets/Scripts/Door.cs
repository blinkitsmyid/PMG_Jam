using UnityEngine;

public class Door : MonoBehaviour
{
    protected PlayerController player;
    protected bool isPlayerInside = false;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
            HintUI.Instance.ShowTemporary(HintMessages.PressRoomIn);
            if (collision.TryGetComponent(out PlayerController p))
            {
                Debug.Log(collision.gameObject.name);
                player = p;
                isPlayerInside = true;
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
        SetPlayerVisible(false);
    }
    protected virtual void ExitDoor()
    {
        SetPlayerVisible(true);
    }
    protected void SetPlayerVisible(bool visible)
    {
        if (player == null) return;

        player.SetVisible(visible);
    }
}