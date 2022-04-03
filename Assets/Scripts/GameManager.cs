using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int curTowerSelect = 0;
    [SerializeField] public List<Tower> towerPrefabs;
    [SerializeField] public GameObject tileList;

    [SerializeField] int waveTimer = 60;
    int timeRemaining;
    [SerializeField] int waveSize = 4;
    [SerializeField] int waveIncrease = 4;
    [SerializeField] TextMeshProUGUI timer;
    [SerializeField] TextMeshProUGUI scoreText;


    ObjectPool pool;
    Pathfinding pathfinder;
    GridManager gridmanager;
    bool isRunning = false;
    int score = 0;
    public int Score { get { return score; } }

    // Start is called before the first frame update
    void Start()
    {
        pool = FindObjectOfType<ObjectPool>();
        pathfinder = pool.GetComponent<Pathfinding>();
        gridmanager = FindObjectOfType<GridManager>();
        timeRemaining = waveTimer;
        scoreText.text = "Score\n" + score;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isRunning)
        {
            StartCoroutine(Wave());
        }
    }

    IEnumerator Wave()
    {
        isRunning = true;

        while (timeRemaining > 0)
        {
            timeRemaining--;
            timer.text = timeRemaining.ToString();
            //Wait for a number of seconds between waves
            yield return new WaitForSeconds(1);
        }

        int randSpawner = Random.Range(0, pool.enemySpawners.Count);
        Vector2Int coord = gridmanager.CoordFromPos(pool.enemySpawners[randSpawner].transform.position);
        pathfinder.UpdateStartCoord(coord);
        pool.inWave = true;

        //Fill the Object pool with the correct number of enemies for the wave
        pool.FillPool(waveSize);

        //Increase the pool size over time for a difficulty ramp
        waveSize += waveIncrease;

        isRunning = false;
        timeRemaining = 60;
    }

    public void ChangeSelectedTower(int towerCode)
    {
        curTowerSelect = towerCode;
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = "Score\n" + score;
    }
}
