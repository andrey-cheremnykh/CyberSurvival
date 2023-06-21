using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    EnemySpawnPoint[] points;
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        points = FindObjectsOfType<EnemySpawnPoint>();
        StartCoroutine(SpawnNewEnemy());
        
    }

    // Update is called once per frame
    IEnumerator SpawnNewEnemy()
    {
        yield return new WaitForSeconds(2);
        int r = Random.Range(0, points.Length);
        Vector3 spawnPos = points[r].transform.position;
        PhotonNetwork.Instantiate(enemyPrefab.name, spawnPos, Quaternion.identity);
        StartCoroutine(SpawnNewEnemy());
    }
}
