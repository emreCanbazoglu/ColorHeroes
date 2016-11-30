using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum ColorEnum
{
    Red,
    Green,
    Blue,
    Yellow,
}

public class CharacterManager : MonoBehaviour 
{
    static CharacterManager _instance;

    public static CharacterManager Instance { get { return _instance; } }

    public List<CharacterScript> AvailableCharacterList;
    List<CharacterScript> _activeCharacterList;

    void Awake()
    {
        _instance = this;

        InitCharacterLists();
    }

    void OnDestroy()
    {
        _instance = null;
    }

    void InitCharacterLists()
    {
        _activeCharacterList = new List<CharacterScript>(); 
    }

    public CharacterScript GetAvailableCharacter()
    {
        CharacterScript availableCharacter = AvailableCharacterList[0];

        AvailableCharacterList.Remove(availableCharacter);
        _activeCharacterList.Add(availableCharacter);

        return availableCharacter;
    }
}
