using UnityEngine;
using System;
using System.Collections;
using Unity.Cinemachine;

public class GameState_Idle : GameState
{
    // game state data
    public event Action OnStateOver;
    public string buttonMessage { get; set; }
    public Camera Camera { get; set; }
    public CharacterData mario { get; set; }
    public CharacterData goomba { get; set; }

    public void BeginState()
    {
        Debug.Log("Began Idle State!");
    }

    public IEnumerator StateRoutine()
    {
        yield return null;
    }

    public void StateButton()
    {
        OnStateOver?.Invoke();
    }

    public void EndState()
    {
        Debug.Log("Ended Idle State!");
    }
}
