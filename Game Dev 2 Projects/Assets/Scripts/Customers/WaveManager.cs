using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public int numCustomers;
        public float spawnTimer;
        public enum Ailment
        {
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
        public Ailment[] possibleAilments;

        public enum Herb
        {
            BASIL,
            CHAMOMILE,
            DANDELION,
            MINT,
            NONE
        }
        public Herb unlockedHerb;

        public int passingScore;
    }

    [Header("VARIABLES")]
    public List<Wave> waves = new List<Wave>();
    public int currentWave = 0;
    private int customerCount = 0;

    private float spawnTimer = 3f;
    private float spawnTime = 0f;


    [Header("REFERENCES")]
    public SaleCounter saleCounter;
    public GameObject customerPrefab;
    public Transform exitPos;
    public WaveDisplay waveDisplay;
    private SceneChanges sceneChanges;

    public AudioSource bellAudio, nextWaveAudio;

    void Start()
    {
        sceneChanges = GetComponent<SceneChanges>();
        Cursor.lockState = CursorLockMode.Locked;

        SetWave(0);
    }

    void Update()
    {
        if(customerCount < waves[currentWave].numCustomers)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime >= spawnTimer)
            {
                SpawnCustomer();
                spawnTime = 0f;
            }
        }
        else
        {
            // check, go to next wave
            Customer[] customersInScene = FindObjectsByType<Customer>(FindObjectsSortMode.None);
            if(customersInScene.Length <= 0)
            {
                // if the next wave exists
                if (currentWave + 1 < waves.Count)
                {
                    // and the player got a high enough score
                    if (ScoreManager.Score >= waves[currentWave].passingScore)
                    {
                        // go to the next wave
                        SetWave(currentWave + 1);
                    }
                    else // player did not get a high enough score
                    {
                        // lose the game
                        Cursor.lockState = CursorLockMode.None;
                        sceneChanges.LoadToSceneByIndex(2);
                    }
                }
                else // no next wave = done
                {
                    // and the player got a high enough score
                    if (ScoreManager.Score >= waves[currentWave].passingScore)
                    {
                        // set the player's status to winning
                        ScoreManager.Win(); 
                    }

                    Cursor.lockState = CursorLockMode.None;
                    sceneChanges.LoadToSceneByIndex(2); // go to end scene
                }
            }
        }
    }

    private void SpawnCustomer()
    {
        bellAudio.Play();

        GameObject newCustomer = Instantiate(customerPrefab);
        newCustomer.transform.position = transform.position;

        Customer c = newCustomer.GetComponent<Customer>();
        c.homePos = exitPos.position;
        c.SetAilment(GetRandomAilment());
        saleCounter.AddCustomerToQueue(c);

        customerCount++;
    }

    private string GetRandomAilment()
    {
        Wave wave = waves[currentWave];
        int ailmentIndex = Random.Range(0, wave.possibleAilments.Length);
        return wave.possibleAilments[ailmentIndex].ToString();
    }

    private void SetWave(int waveNum)
    {
        currentWave = waveNum;

        // reset counters
        spawnTime = 0f;
        spawnTimer = waves[currentWave].spawnTimer;
        customerCount = 0;

        // activate herb spawners
        HerbSpawner[] herbSpawners = FindObjectsByType<HerbSpawner>(FindObjectsSortMode.None);
        foreach(HerbSpawner herbSpawner in herbSpawners)
        {
            string herb = herbSpawner.herbPrefab.GetComponent<ObjectType>().objName.ToString();
            if (herb == waves[currentWave].unlockedHerb.ToString())
            {
                herbSpawner.canSpawn = true;
            }
        }

        string unlockedHerb = "";
        if(waves[currentWave].unlockedHerb != Wave.Herb.NONE)
        {
            unlockedHerb = waves[currentWave].unlockedHerb.ToString();
        }

        // check for final wave
        bool isFinalWave = false;
        if (currentWave == waves.Count - 1)
        {
            isFinalWave = true;
        }

        // queue fanfare
        waveDisplay.DisplayWaveCount(currentWave + 1, isFinalWave, unlockedHerb);
        nextWaveAudio.Play();
    }
}
