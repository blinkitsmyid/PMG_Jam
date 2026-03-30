using UnityEngine;

public class BedroomDoor : Door
{
    PlayerController playerController;
    protected override void EnterDoor()
    {
        if (!player.HasUsedToilet())
        {
            Debug.Log("Сначала сходи в туалет!");
            Bumble.Instance.ShowBumble(playerController.towelSprite);
            HintUI.Instance.ShowTemporary(HintMessages.GoToToilet);
            Bumble.Instance.HideBumble();
            return;
        }
        if (!LampManager.Instance.AreAllLampsOff())
        {
            Debug.Log("Нельзя войти — включён свет!");
            Bumble.Instance.ShowBumble(playerController.lightSprite);
            HintUI.Instance.ShowTemporary(HintMessages.TurnOffLights);
            Bumble.Instance.HideBumble();
            return;
        }

        GameInput.Instance.DisableMovement();
        PanelManager.Instance.Win();
        base.EnterDoor();
    }
}