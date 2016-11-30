using UnityEngine;
using System;
using System.Collections;

public class PlayerInputInfo
{
    public KeyCode ButtonA;
    public KeyCode ButtonRB;
    public string MainHorAxis;
    public string MainVerAxis;
    public string SecHorAxis;
    public string SecVerAxis;

    public PlayerInputInfo
        (
        KeyCode buttonA,
        KeyCode buttonRB,
        string mainHorAxis,
        string mainVerAxis,
        string secHorAxis,
        string secVerAxis
        )
    {
        ButtonA = buttonA;
        ButtonRB = buttonRB;
        MainHorAxis = mainHorAxis;
        MainVerAxis = mainVerAxis;
        SecHorAxis = secHorAxis;
        SecVerAxis = secVerAxis;
    }
}

public class PlayerInputController : MonoBehaviour
{
    public PlayerInputInfo InputInfo { get; private set; }

    #region Events
    public Action<KeyCode> OnButtonDown;

    void FireOnButtonDown(KeyCode keyCode)
    {
        if (OnButtonDown != null)
            OnButtonDown(keyCode);
    }

    public Action<KeyCode> OnButton;

    void FireOnButton(KeyCode keyCode)
    {
        if (OnButton != null)
            OnButton(keyCode);
    }

    public Action<KeyCode> OnButtonUp;

    void FireOnButtonUp(KeyCode keyCode)
    {
        if (OnButtonUp != null)
            OnButtonUp(keyCode);
    }
    #endregion

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (InputInfo == null)
            return;

        if (Input.GetKeyDown(InputInfo.ButtonA))
            FireOnButtonDown(InputInfo.ButtonA);
        if (Input.GetKeyDown(InputInfo.ButtonRB))
            FireOnButtonDown(InputInfo.ButtonRB);

        if (Input.GetKey(InputInfo.ButtonA))
            FireOnButton(InputInfo.ButtonA);
        if (Input.GetKey(InputInfo.ButtonRB))
            FireOnButton(InputInfo.ButtonRB);

        if (Input.GetKeyUp(InputInfo.ButtonA))
            FireOnButtonUp(InputInfo.ButtonA);
        if (Input.GetKeyUp(InputInfo.ButtonRB))
            FireOnButtonUp(InputInfo.ButtonRB);
    }

    public void SetPlayerInputInfo(PlayerInputInfo inputInfo)
    {
        InputInfo = inputInfo;
    }
}
