using UnityEngine;
using System.Collections;

public class TestGameManager : MonoBehaviour
{
    void Start()
    {
        PlayerManager.Instance.NewPlayerJoined(1);
    }
}
