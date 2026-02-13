using UnityEngine;

public class Crowd : MonoBehaviour
{
    public float defaultSpeed, cheeringSpeed;
    public float maximumHeight;
    public float randomnessFactor;

    public bool coinRed = true;

    private CoinBehaviour coinBehaviour;

    private void Awake()
    {
        coinBehaviour = FindAnyObjectByType<CoinBehaviour>();
    }

    private void Update()
    {
        coinRed = !coinBehaviour.IsCoinFacingUp();
    }
}
