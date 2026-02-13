using UnityEngine;
using UnityEngine.UI;

public class GroundBehaviour : MonoBehaviour
{
    public float timeToConfirm = 2f;
    private float timer = 0f;
    private bool groundedCoin = false;
    private bool gameOver = false;

    public Slider sliderTimer;

    public AudioSource tickingSFX;

    private void Start()
    {
        sliderTimer.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!gameOver && groundedCoin)
        {
            timer += Time.deltaTime;
            sliderTimer.value = timer / timeToConfirm;

            if (timer > timeToConfirm)
            {
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        tickingSFX.Stop();
        gameOver = true;
        sliderTimer.gameObject.SetActive(false);

        Debug.Log("Game Ended!");
        FindAnyObjectByType<GameStateManager>().SetState(GameStateManager.GameState.Ended);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<CoinBehaviour>() != null)
        {
            groundedCoin = true;
            timer = 0f;
            sliderTimer.gameObject.SetActive(true);
            tickingSFX.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<CoinBehaviour>() != null)
        {
            groundedCoin = false;
            timer = 0f;
            sliderTimer.gameObject.SetActive(false);
            tickingSFX.Stop();
        }
    }
}
