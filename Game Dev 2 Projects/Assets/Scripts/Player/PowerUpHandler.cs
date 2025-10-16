using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{
    private bool isPoweredUp = false;
    public bool IsPoweredUp { get { return isPoweredUp; } }

    public float poweredTime;
    private float timer;

    private void Update()
    {
        if (isPoweredUp)
        {
            timer += Time.deltaTime;

            if(timer > poweredTime)
            {
                isPoweredUp = false;
            }
        }
    }

    public void PowerUp()
    {
        isPoweredUp = true;
        timer = 0f;
    }
}
