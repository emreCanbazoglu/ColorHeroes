using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour 
{
    public ColorEnum CharacterType;

    public CharMovementController MovementController; 

    public PlayerScript PlayerScript { get; private set; }

    public void SetPlayerScript(PlayerScript playerScript)
    {
        PlayerScript = playerScript;

        MovementController.SetPlayerInputController(playerScript.InputController);
    }

    public void UpdateFrame()
    {
        MovementController.UpdateFrame();
    }
}
