using System;
using System.Collections;
using UnityEngine;

public interface GameState
{
    public event Action OnStateOver;
    public string buttonMessage { get; set; }
    public CharacterData mario { get; set; }
    public CharacterData goomba { get; set; }

    public void BeginState();
    public IEnumerator StateRoutine();
    public void StateButton();
    public void EndState();
}
