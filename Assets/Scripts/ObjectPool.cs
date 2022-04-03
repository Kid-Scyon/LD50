using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] public List<GameObject> enemySpawners;
    [SerializeField][Range(0.1f, 5)] float spawnTimer = 1f;

    GameObject[] pool;
    public bool inWave = false;

    public void FillPool(int poolSize)
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < pool.Length; i++)
        {
            pool[i] = createNewEnemy();
            pool[i].SetActive(false);
        }

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
        while(inWave)
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

        inWave = false;
    }
}
