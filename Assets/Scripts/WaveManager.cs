using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<Transform> Spawnpoints;
    int numberOfCreepsToSpawn;
    int numberOfCreepsSpawned;
    int numberOfCreepsKilled;
    public GameObject CreepPrefab;
    Dictionary<int, int> WaveCreepCounts;

    public void SpawnWave()
    {
        Wave newWave = new Wave();
        newWave.NumberOfCreepsToSpawn = GameController.Instance.Difficulty * 10;
        StartCoroutine(ReleaseCreepsForWave(newWave));
    }

    IEnumerator ReleaseCreepsForWave(Wave newWave)
    {
        for (int i = 0; i < newWave.NumberOfCreepsToSpawn; i++)
        {
            Vector3 newSpawnPoint = Spawnpoints[Random.Range(0, Spawnpoints.Count)].position;
            Instantiate(CreepPrefab, newSpawnPoint, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }
}

public class Wave {

    public int NumberOfCreepsToSpawn;
    public float DelayBetweenSpawns;
    public List<Creep> Creeps;
    public GameObject CreepPrefab;
}
