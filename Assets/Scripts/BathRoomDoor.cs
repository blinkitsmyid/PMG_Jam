using UnityEngine;

public class BathroomDoor : Door
{
    private bool isInsideDoor =  false;
    [SerializeField] private float toiletTime = 3f;
    protected override void EnterDoor()
    {
        base.EnterDoor();
        
        player.DoToiletRoutine(toiletTime); // 3 секунды
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