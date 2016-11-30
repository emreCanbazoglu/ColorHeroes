using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour 
{
    static InputManager _instance;

    public static InputManager Instance { get { return _instance; } }

    List<PlayerInputInfo> _inputControllerList;

    #region Player1
    public const string P1_MAIN_HOR_AXIS = "P1_Main_Horizontal";
    public const string P1_MAIN_VER_AXIS = "P1_Main_Vertical";
    public const string P1_SEC_HOR_AXIS = "P1_Sec_Horizontal";
    public const string P1_SEC_VER_AXIS = "P1_Sec_Vertical";
    #endregion

    void Awake()
    {
        _instance = this;

        InitControllers();
    }

    void OnDestroy()
    {
        _instance = null;
    }

    void InitControllers()
    {
        _inputControllerList = new List<PlayerInputInfo>();

        PlayerInputInfo p1 = new PlayerInputInfo(
            KeyCode.Joystick1Button0,
            KeyCode.Joystick1Button5,
            P1_MAIN_HOR_AXIS, P1_MAIN_VER_AXIS,
            P1_SEC_HOR_AXIS, P1_SEC_VER_AXIS);

        _inputControllerList.Add(p1);
    }

    public PlayerInputInfo GetPlayerControls(int playerID)
    {
        Debug.Log("player id: " + playerID);

        return _inputControllerList[playerID - 1];
    }
}
