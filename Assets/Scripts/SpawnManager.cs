﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning;

    // Start is called before the first frame update
    void Start()
    {
        _stopSpawning = false;
        StartCoroutine("SpawnRoutine");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn enemy every 5 seconds
    IEnumerator SpawnRoutine(){
        while(!_stopSpawning){
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield  return new WaitForSeconds(5.0f);
        }
    }

    public void StopSpawning() {
        _stopSpawning = true;
    }
}
