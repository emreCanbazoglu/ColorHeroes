using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour 
{
    public int PlayerID;
    public int ControllerIndex;

    public PlayerInputController InputController;

    public CharacterScript Character { get; private set; }

    public void InitNewPlayer(int playerID, int controllerIndex)
    {
        PlayerID = playerID;

        InputController.SetPlayerInputInfo(InputManager.Instance.GetPlayerControls(controllerIndex));
    }

    public void SetCharacterScript(CharacterScript character)
    {
        Character = character;

        Character.SetPlayerScript(this);
    }

    public void UpdateFrame()
    {
        Character.UpdateFrame();
    }
}
