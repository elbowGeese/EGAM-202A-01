using UnityEngine;
using UnityEngine.UI;

public class CoinSideIndicator : MonoBehaviour
{
    public float idleTimer = 0.2f;

    public GameObject sideIndicator;
    public Image[] indicators;
    public Color playerSide, enemySide;

    private CoinBehaviour coinBehaviour;

    private void Start()
    {
        coinBehaviour = FindAnyObjectByType<CoinBehaviour>();
    }

    void Update()
    {
        UpdateColorIndication();
    }

    private void UpdateColorIndication()
    {
        bool coinPlayerSide = coinBehaviour.IsCoinFacingUp();

        if (coinPlayerSide) { ChangeIndicationColor(playerSide); }
        else { ChangeIndicationColor(enemySide); }
    }

    private void ChangeIndicationColor(Color newColor)
    {
        foreach (Image ind in indicators)
        {
            ind.color = newColor;
        }
    }
}
