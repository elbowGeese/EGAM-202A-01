using UnityEngine;

public static class ScoreManager
{
    private static int score = 0;
    public static int Score { get { return score; } }

    private static bool won = false;
    public static bool Won { get { return won; } }

    public static void ResetScore()
    {
        score = 0;
    }

    public static void AddToScore(int amount)
    {
        score += amount;
    }

    public static void Win()
    {
        won = true;
    }
}
