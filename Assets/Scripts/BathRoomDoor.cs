using UnityEngine;

public class BathroomDoor : Door
{
    private bool isInsideDoor =  false;
    [SerializeField] private float toiletTime = 3f;
    protected override void EnterDoor()
    {
        // 🔑 проверка ключа
        if (LevelManager.Instance.NeedKey() && !player.HasKey())
        {
            Debug.Log("Нужен ключ!");
            HintUI.Instance.ShowTemporary(HintMessages.NeedKey);
            return;
        }

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

        Debug.Log("Player left bathroom");
    }
}