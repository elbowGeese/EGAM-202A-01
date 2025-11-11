using UnityEngine;

public class ChangeMaterialOnCollision : MonoBehaviour
{
    [System.Serializable]
    public class Prop
    {
        public MeshRenderer mr;
        public Material nonCollisionMaterial, collisionMaterial;
    }
    public Prop[] props;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach(Prop prop in props)
            {
                prop.mr.material = prop.collisionMaterial;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (Prop prop in props)
            {
                prop.mr.material = prop.nonCollisionMaterial;
            }
        }
    }
}
