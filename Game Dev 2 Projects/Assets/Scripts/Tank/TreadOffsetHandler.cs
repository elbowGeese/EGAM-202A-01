using UnityEngine;

public class TreadOffsetHandler : MonoBehaviour
{
    public Material treadMaterial;
    public float treadSpeed = 1f;

    private TankInputs inputs;

    private void Start()
    {
        inputs = GetComponent<TankInputs>();
    }

    void Update()
    {
        if (inputs)
        {
            float newXOffset = treadMaterial.mainTextureOffset.x - (treadSpeed * inputs.ForwardInput * Time.deltaTime);

            treadMaterial.mainTextureOffset = new Vector2 (newXOffset, 0f);
        }
    }
}
