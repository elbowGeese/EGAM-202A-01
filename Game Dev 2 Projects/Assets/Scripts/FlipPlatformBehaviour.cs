using UnityEngine;

public class FlipPlatformBehaviour : MonoBehaviour
{
    private CoinBehaviour coinBehaviour;

    public MeshRenderer mr;
    public Material redMaterial, greenMaterial;

    public bool invertAngle = false;
    public float tippingAngle = 30f;
    public float tippingSpeed = 5f;

    void Start()
    {
        coinBehaviour = FindAnyObjectByType<CoinBehaviour>();
    }

    void Update()
    {
        UpdateCoinFacing();
    }

    void UpdateCoinFacing()
    {
        Vector3 angle = transform.eulerAngles;

        if (coinBehaviour.IsCoinFacingUp())
        {
            mr.material = greenMaterial;

            if (invertAngle) { angle.z = 360f - tippingAngle; }
            else { angle.z = tippingAngle; }
        }
        else
        {
            mr.material = redMaterial;

            if (invertAngle) { angle.z = tippingAngle; }
            else { angle.z = 360f - tippingAngle; }
        }

        if(Mathf.Abs(angle.z - transform.eulerAngles.z) > 0.1f)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, angle, tippingSpeed * Time.deltaTime);
        }
    }
}
