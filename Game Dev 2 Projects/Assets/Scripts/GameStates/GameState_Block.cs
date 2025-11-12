using UnityEngine;
using System;
using System.Collections;

public class GameState_Block : GameState
{
    // game state data
    public event Action OnStateOver;
    public string buttonMessage { get; set; }
    public CharacterData mario { get; set; }
    public CharacterData goomba { get; set; }

    public void BeginState()
    {
        Debug.Log("Began Blocking State!");
    }

    public IEnumerator StateRoutine()
    {
        yield return null;
    }

    public void StateButton()
    {

    }

    public void EndState()
    {
        Debug.Log("Ended Blocking State!");
    }
}
