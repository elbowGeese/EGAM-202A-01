using UnityEngine;

public class CameraGeometryFade : MonoBehaviour
{
    public float radius = 1.5f;
    public float distance = 2f;
    public Vector3 offset = Vector3.zero;

    public void FadeBlockingGeometry()
    {
        RaycastHit[] blockingGeometry = Physics.SphereCastAll(transform.position + offset, radius, -Vector3.up, distance);
        foreach(RaycastHit blockingHit in blockingGeometry)
        {
            if(blockingHit.transform.GetComponent<CoinBehaviour>() == null)
            {
                if (blockingHit.transform.GetComponent<MeshRenderer>() != null)
                {
                    Debug.Log("HIT!");
                    SetMaterialAlpha(blockingHit.transform.GetComponent<MeshRenderer>());
                }
            }
        }
    }

    private void SetMaterialAlpha(MeshRenderer blockingRenderer)
    {
        Color fadeColor = blockingRenderer.materials[0].color;
        fadeColor.a = 0.3f;
        blockingRenderer.materials[0].color = fadeColor;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawSphere(transform.position + offset + -Vector3.up * distance, radius);
    }
}
