using UnityEngine;

public class BedroomDoor : Door
{
    
    protected override void EnterDoor()
    {
        if (!player.HasUsedToilet())
        {
            Debug.Log("Сначала сходи в туалет!");
            HintUI.Instance.ShowTemporary(HintMessages.GoToToilet);
            return;
        }
        if (!LampManager.Instance.AreAllLampsOff())
        {
            Debug.Log("Нельзя войти — включён свет!");
            HintUI.Instance.ShowTemporary(HintMessages.TurnOffLights);
            return;
        }
        
        GameInput.Instance.DisableMovement();
        PanelManager.Instance.Win();
        base.EnterDoor();
    }
}