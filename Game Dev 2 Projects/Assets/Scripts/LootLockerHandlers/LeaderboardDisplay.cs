using UnityEngine;
using TMPro;

public class LeaderboardDisplay : MonoBehaviour
{
    public GameObject leaderboardItemPrefab;
    public TMP_Text status;

    void Start()
    {
        ClearDisplay();
    }

    public void ClearDisplay()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddDisplayItem(string playerName, string playerScore)
    {
        GameObject entry = Instantiate(leaderboardItemPrefab, transform);
        entry.GetComponent<LeaderboardItem>().SetItemData(playerName, playerScore);
    }

    public void PopulateDisplay()
    {
        int count = transform.childCount;
        for (int i = 0; i < (10 - count); i++)
        {
            int rank = count + i + 1;
            AddDisplayItem(rank.ToString() + ". ----------", "----------");
        }
    }

    public void DisplayStatus(string currentStatus)
    {
        status.text = currentStatus;
    }
}
