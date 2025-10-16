using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PacmanController>() != null)
        {
            FindFirstObjectByType<PowerUpHandler>().PowerUp();
        }
    }
}
