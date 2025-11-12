using UnityEngine;
using System;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    public GameState[] gameStates = new GameState[3];
    public GameState currentState;
    public Coroutine currentStateRoutine;

    public TMP_Text masterButtonText;
    public string[] stateButtonMessages;

    public CharacterData mario;
    public CharacterData goomba;

    private void OnDisable()
    {
        if (currentState != null)
        {
            currentState.OnStateOver -= GoToNextState;
        }
    }

    void Start()
    {
        InitStates();
        SetState(gameStates[0]);
    }

    void InitStates()
    {
        gameStates[0] = new GameState_Idle();
        gameStates[0].buttonMessage = stateButtonMessages[0];
        gameStates[0].mario = mario;
        gameStates[0].goomba = goomba;

        gameStates[1] = new GameState_Jump();
        gameStates[1].buttonMessage = stateButtonMessages[1];
        gameStates[1].mario = mario;
        gameStates[1].goomba = goomba;

        gameStates[2] = new GameState_Block();
        gameStates[2].buttonMessage = stateButtonMessages[2];
        gameStates[2].mario = mario;
        gameStates[2].goomba = goomba;
    }

    public void SetState(GameState state)
    {
        if (currentState == state) { return; }

        // end previous state
        if(currentState != null) 
        {
            currentState.OnStateOver -= GoToNextState;
            currentState.EndState();
            StopCoroutine(currentStateRoutine);
        }

        // set state
        currentState = state;

        // begin new state
        currentState.OnStateOver += GoToNextState;
        currentState.BeginState();
        currentStateRoutine = StartCoroutine(currentState.StateRoutine());
        masterButtonText.text = currentState.buttonMessage;
    }

    private int GetCurrentStateIndex()
    {
        // get current state index
        int currentStateIndex = 0;
        for (int i = 0; i < gameStates.Length; i++)
        {
            if (gameStates[i] == currentState)
            {
                currentStateIndex = i; break;
            }
        }

        return currentStateIndex;
    }

    public void GoToNextState()
    {
        // get current state index
        int currentStateIndex = GetCurrentStateIndex();

        // add one and modulo to find next index
        int nextStateIndex = (currentStateIndex + 1) % gameStates.Length;

        // set state
        SetState(gameStates[nextStateIndex]);
    }

    void Update()
    {
        if (currentState != null)
        {
            
        }
    }

    public void MasterButtonPress()
    {
        currentState.StateButton();
    }
}
