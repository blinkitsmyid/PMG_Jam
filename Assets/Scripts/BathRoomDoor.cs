using UnityEngine;

public class BathroomDoor : Door
{
    PlayerController playerController;
    private bool isInsideDoor = false;
    [SerializeField] private float toiletTime = 3f;
    protected override void EnterDoor()
    {
        // 🔑 проверка ключа
        int requiredKeys = LevelManager.Instance.GetRequiredKeys();

        if (player.GetKeys() < requiredKeys)
        {
            Debug.Log("Недостаточно ключей!");
            Bumble.Instance.ShowBumble(PlayerController.Instance.keySprite);
            AudioManager.Instance.PlayDoorClose();
            HintUI.Instance.ShowTemporary(HintMessages.NeedKey);
            
            return;
        }
        AudioManager.Instance.PlayDoorOpen();
        base.EnterDoor();
        player.DoToiletRoutine(toiletTime);
    }

    public override void Interact()
    {
        if (!isPlayerInside || player == null) return;

        isInsideDoor = !isInsideDoor;

        if (isInsideDoor)
            EnterDoor();
        else
            ExitDoor();
    }
    protected override void ExitDoor()
    {
        base.ExitDoor();
        AudioManager.Instance.PlayDoorOpen();
        Debug.Log("Player left bathroom");
    }
}