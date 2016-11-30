using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerManager : MonoBehaviour 
{
    static PlayerManager _instance;

    public static PlayerManager Instance { get { return _instance; } }

    List<PlayerScript> _deactivePlayerList;
    List<PlayerScript> _activePlayerList;

    void Awake()
    {
        _instance = this;

        InitPlayerLists();
    }

    void OnDestroy()
    {
        _instance = null;
    }

    void InitPlayerLists()
    {
        _deactivePlayerList = GetComponentsInChildren<PlayerScript>().ToList();
        _activePlayerList = new List<PlayerScript>();
    }

    public void NewPlayerJoined(int controllerIndex)
    {
        if (_activePlayerList.Any(val => val.ControllerIndex == controllerIndex))
            return;

        PlayerScript newPlayer = _deactivePlayerList[0];

        _deactivePlayerList.Remove(newPlayer);
        _activePlayerList.Add(newPlayer);

        newPlayer.InitNewPlayer(_activePlayerList.Count, controllerIndex);
        newPlayer.SetCharacterScript(CharacterManager.Instance.GetAvailableCharacter());

        Debug.Log("New Player Assigned!");
    }

    public PlayerScript GetPlayerWithID(int playerID)
    {
        return _activePlayerList.Single(val => val.PlayerID == playerID);
    }

    void Update()
    {
        _activePlayerList.ForEach(val => val.UpdateFrame());
    }
}
