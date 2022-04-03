using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 4;
    [SerializeField] List<GameObject> enemySpawners;
    [SerializeField][Range(0.1f, 5)] float spawnTimer = 1f;
    [SerializeField] [Range(5, 120)] int waveTimer = 60;

    GameObject[] pool;

    void Awake()
    {
        FillPool();
    }

    public void FillPool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = createNewEnemy();
        }
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    GameObject createNewEnemy()
    {
        int randSpawner = Random.Range(0, enemySpawners.Count);
        GameObject newEnemy = Instantiate(enemyPrefab, enemySpawners[randSpawner].transform);
        return newEnemy;
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void EnableObjectInPool()
    {
        for(int i = 0; i < pool.Length; i++)
        {
            if(pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
