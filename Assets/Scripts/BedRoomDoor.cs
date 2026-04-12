using UnityEngine;

public class BedroomDoor : Door
{
    PlayerController playerController;
    protected override void EnterDoor()
    {
        if (!player.HasUsedToilet())
        {
            AudioManager.Instance.PlayDoorClose();
            Debug.Log("Сначала сходи в туалет!");
            Bumble.Instance.ShowBumble(PlayerController.Instance.towelSprite);
            HintUI.Instance.ShowTemporary(HintMessages.GoToToilet);
            return;
        }
        if (!LampManager.Instance.AreAllLampsOff())
        {
            Debug.Log("Нельзя войти — включён свет!");
            Bumble.Instance.ShowBumble(PlayerController.Instance.lightSprite);
            HintUI.Instance.ShowTemporary(HintMessages.TurnOffLights);
            return;
        }
        SoundManager.Instance.PlayOpenedDoor();
        GameInput.Instance.DisableMovement();
        PanelManager.Instance.Win();
        base.EnterDoor();
    }
}