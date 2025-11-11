using UnityEngine;

public class ObjectType : MonoBehaviour
{
    public enum Type
    {
        HERB,
        SALVE
    }
    public Type objType;
    public enum Name
    {
        BASIL,
        CHAMOMILE,
        DANDELION,
        MINT,
        MYSTERY,
        BURN,
        COUGH,
        ITCH,
        HANGOVER,
        HEADACHE,
        SCRAPE,
        ACNE,
        POISON,
        CRAMP
    }
    public Name objName;
}
