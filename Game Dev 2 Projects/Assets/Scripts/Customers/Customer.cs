using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Customer : MonoBehaviour, Interactable
{
    // VARIABLES
    public enum CustomerState { IN_QUEUE, GOING_HOME }
    public CustomerState state;

    public Vector3 queuePos;
    public Vector3 homePos;

    private string ailment;

    public float patience = 200f;
    private float patienceTimer = 0f;

    public GameObject scorePopupPrefab;

    // REFERENCES
    private NavMeshAgent navMeshAgent;
    public NameDisplay nameDisplay;
    private Animator anim;

    public Slider patienceSlider;

    public AudioSource voiceAudio, effectAudio;
    public AudioClip laughClip, growlClip, noClip;

    public ParticleSystem happyParticle;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        HideNameDisplay();
    }

    void Update()
    {
        switch (state)
        {
            case CustomerState.IN_QUEUE:
                UpdatePatience(Time.deltaTime);
                MoveCustomer(queuePos);
                break;
            case CustomerState.GOING_HOME:
                MoveCustomer(homePos);
                break;
            default:
                Debug.Log("Unknown customer state set.");
                break;
        }
    }

    public void SetAilment(string newAilment)
    {
        ailment = newAilment;
        nameDisplay.SetText(string.Format("I have a <color=red>{0}</color>", ailment));
    }

    private void MoveCustomer(Vector3 targetPos)
    {
        if(Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            return;
        }

        navMeshAgent.SetDestination(targetPos);
    }

    private void UpdatePatience(float timePassed)
    {
        patienceTimer += timePassed;
        patienceSlider.value = Mathf.Clamp01(1 - (patienceTimer / patience));
        if (patienceTimer >= patience)
        {
            anim.SetTrigger("no");

            voiceAudio.pitch = 1.1f;
            voiceAudio.clip = growlClip;
            voiceAudio.Play();

            state = CustomerState.GOING_HOME;
        }
    }

    public void Interact(Transform hold)
    {
        if (state != CustomerState.IN_QUEUE) { return; }

        // check if player is holding anything
        if (hold.childCount == 0) { return; }

        // check if player is holding salve
        if (hold.GetChild(0).GetComponent<ObjectType>().objType != ObjectType.Type.SALVE) 
        {
            DenyPlayer();
            return; 
        }

        // check if player is holding correct salve
        if (hold.GetChild(0).GetComponent<ObjectType>().objName.ToString() != ailment) 
        {
            DenyPlayer();
            return; 
        }

        Destroy(hold.GetChild(0).gameObject);
        CureAilment();
    }

    private void DenyPlayer()
    {
        anim.SetTrigger("no");

        voiceAudio.pitch = 1.5f;
        voiceAudio.clip = noClip;
        voiceAudio.Play();
    }

    private void CureAilment()
    {
        state = CustomerState.GOING_HOME;
        float timerPercentage = Mathf.Clamp01(1f - (patienceTimer / patience));
        int score = (int)(timerPercentage * 200f);
        string scoreString = "+" + score.ToString();
        string rankString = RankPatience(timerPercentage);

        GameObject scorePopup = Instantiate(scorePopupPrefab);
        scorePopup.transform.position = transform.position;
        scorePopup.GetComponent<ScorePopup>().SetText(scoreString, rankString, GetColorFromRank(rankString));

        ScoreManager.AddToScore(score);

        voiceAudio.pitch = 1f;
        voiceAudio.clip = laughClip;
        voiceAudio.Play();
        effectAudio.Play();

        happyParticle.Play();
    }

    private string RankPatience(float p)
    {
        if(p > 0.66f)
        {
            return "FAST!";
        }
        else if(p > 0.33f)
        {
            return "OKAY!";
        }
        else
        {
            return "SLOW!";
        }
    }

    private Color GetColorFromRank(string rank)
    {
        switch (rank)
        {
            case "FAST!":
                return Color.green;
            case "OKAY!":
                return Color.yellow;
            default:
                return Color.red;
        }
    }

    public void ShowNameDisplay()
    {
        nameDisplay.ShowDisplay();
    }

    public void HideNameDisplay()
    {
        nameDisplay.HideDisplay();
    }
}
