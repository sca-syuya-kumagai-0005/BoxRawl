using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        MENU,
        MAINGAME,
        STATUS,
        CONTROL,
        TITLE,
    }

    public static GameState state;

    // Start is called before the first frame update
    void Start()
    {
        state=GameState.MENU;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
    }
}
