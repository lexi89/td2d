﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<Transform> Spawnpoints;
    public GameObject CreepPrefab;
    [SerializeField] Canvas UICanvas;
    [SerializeField] GameObject SlamDownNoticePrefab;
    Dictionary<int, Wave> _activeWaves;

    void Awake()
    {
        _activeWaves= new Dictionary<int, Wave>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(NextWave());
        }
    }
    
    public void SpawnWave()
    {
        int numberOfCreepsToSpawn = GameController.Instance.Difficulty * 10;
        // use the difficulty for the id;
        Wave newWave = new Wave(GameController.Instance.Difficulty, numberOfCreepsToSpawn);
        _activeWaves[newWave.ID] = newWave;
        StartCoroutine(ReleaseCreepsForWave(newWave));
    }

    IEnumerator ReleaseCreepsForWave(Wave newWave)
    {
        for (int i = 0; i < newWave.NumberOfCreepsToSpawn; i++)
        {
            Vector3 newSpawnPoint = Spawnpoints[Random.Range(0, Spawnpoints.Count)].position;
            GameObject newCreepGO = Instantiate(CreepPrefab, newSpawnPoint, Quaternion.identity);
            Creep newCreep = newCreepGO.GetComponent<Creep>();
            newCreep.WaveID = newWave.ID;
            newCreep.WaveManager = this;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void OnCreepDeath(Creep deadCreep)
    {
        if (!_activeWaves.ContainsKey(deadCreep.WaveID))
        {
            Debug.LogWarning("Creep died that had no wave.");
            return;
        }
//        print("creep wave id: " + deadCreep.Wave.ID);
//        print("creeps left in the wave: " + _activeWaves[deadCreep.Wave.ID].CreepsRemaining);
        _activeWaves[deadCreep.WaveID].CreepsRemaining--;
//        print("after: " + _activeWaves[deadCreep.Wave.ID].CreepsRemaining);
        if (_activeWaves[deadCreep.WaveID].CreepsRemaining == 0)
        {
//            print("killed all creeps in the wave.");
            _activeWaves.Remove(deadCreep.WaveID);
            StartCoroutine(NextWave());
        }
    }
    
    public IEnumerator NextWave()
    {
        GameController.Instance.IncrementDifficulty();
        GameObject newSlamNotice = Instantiate(SlamDownNoticePrefab, UICanvas.transform);
        SlamDownNotice sdn = newSlamNotice.GetComponent<SlamDownNotice>();
        sdn.Text.text = string.Concat ("Wave: ", GameController.Instance.Difficulty.ToString ());
        sdn.SlamDown();
        yield return new WaitForSeconds (2f);
        SpawnWave();
    }
}

public class Wave
{
    public int ID;
    public int NumberOfCreepsToSpawn;
    public float DelayBetweenSpawns;
    public List<Creep> Creeps;
    public GameObject CreepPrefab;
    public int CreepsRemaining;

    public Wave(int newId, int numberOfCreeps)
    {
        ID = newId;   
        NumberOfCreepsToSpawn = numberOfCreeps;
        CreepsRemaining = numberOfCreeps;
    }
}
