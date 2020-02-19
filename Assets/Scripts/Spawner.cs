using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Transform obstaclePrefabs;
    public Transform coinPrefabs;

    public float obstacleSpwanRate = 1f;
    private float obstacleNextTimeToSpawn = 0f;
    public float coinSpwanRate = 1f;
    private float coinNextTimeToSpawn = 0f;
    public float width = 10f;
    public float spawnRangeOffset = 0.5f;

    private float spawnPosX;

    private Player player;

    private void Start () {
        spawnPosX = transform.position.x;
        player = FindObjectOfType<Player>();

        StartCoroutine (GenerateObstacle ());
        StartCoroutine (GenerateCoin ());
    }

    private void Update() {
        if (player == null){
            StopAllCoroutines();
        }
    }

    private IEnumerator GenerateObstacle () {
        while (true) {
            float rand = Random.Range (-width / 2f + spawnRangeOffset, width / 2f - spawnRangeOffset);

            if (Time.time >= obstacleNextTimeToSpawn) {
                Transform prefabs = Instantiate (
                    obstaclePrefabs,
                    new Vector3 (rand, transform.position.y, transform.position.z),
                    Quaternion.identity
                );
                prefabs.parent = transform;
                obstacleNextTimeToSpawn = Time.time + 1f / obstacleSpwanRate;
            }
            yield return null;
        }
    }

    private IEnumerator GenerateCoin () {
        while(true){
            float rand = Random.Range (-width / 2f + spawnRangeOffset, width / 2f - spawnRangeOffset);

            if (Time.time >= coinNextTimeToSpawn){
                Transform prefabs = Instantiate(
                    coinPrefabs,
                    new Vector3(rand, transform.position.y, transform.position.y),
                    Quaternion.identity
                );
                prefabs.parent = transform;
                coinNextTimeToSpawn = Time.time + 1f / coinSpwanRate;
            }
            yield return null;
        }
        
    }
}