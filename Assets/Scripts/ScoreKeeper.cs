using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour {

    public static int score { get; set; }

    private float lastEnemyKilledTime;
    private int streakCount;
    private float streakExpiryTime = 3f;

    private void Start () {
        score = 0;
        Coin.OnPickUpCoin += AddScore;
    }

    private void AddScore () {
        if (Time.time < lastEnemyKilledTime + streakExpiryTime) {
            streakCount++;
        } else {
            streakCount = 0;
        }

        lastEnemyKilledTime = Time.time;
        score += 5 + streakCount;
    }

}