using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviourPun
{
    public string enemyPrefabPath;
    public float maxEnemies;
    public float spawnRadius;
    public float spawnCheckTime;
    private float lastSpawnCheckTime;
    private List<GameObject> curEnemies = new List<GameObject>();

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;
        if (Time.time - lastSpawnCheckTime > spawnCheckTime)
        {
            lastSpawnCheckTime = Time.time;
            TrySpawn();
        }
    }

    void TrySpawn()
    {
        // remove any dead enemies from the curEnemies list
        for (int x = 0; x < curEnemies.Count; ++x)
        {
            if (!curEnemies[x])
                curEnemies.RemoveAt(x);
        }
        // if we have maxed out our enemies, return
        if (curEnemies.Count >= maxEnemies)
            return;
        // otherwise, spawn an enemy
        //Vector3 randomInCircle = Random.insideUnitCircle * spawnRadius;
        GameObject enemy = PhotonNetwork.Instantiate(enemyPrefabPath, transform.position + randomInsideCircle(spawnRadius), Quaternion.identity);
        curEnemies.Add(enemy);
    }


    public Vector3 randomInsideCircle(float maxValue)
    {
        float randomX = UnityEngine.Random.Range(0f, maxValue);
        float randomY = UnityEngine.Random.Range(0f, maxValue);
        return new Vector3(randomX, randomY, 0f); // z is 0 for 2D
    }

}
