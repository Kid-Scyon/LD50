using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 5)] float speed = 1;
    [SerializeField] int enemyDamage = 10;
    List<Node> route = new List<Node>();
    GameManager gm;
    public Enemy enemy;
    GridManager gridManager;
    Pathfinding pathfinder;

    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gm = FindObjectOfType<GameManager>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinding>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coord = new Vector2Int();

        if(resetPath)
        {
            coord = pathfinder.StartCoord;
        }
        else
        {
            coord = gridManager.CoordFromPos(transform.position);
        }

        StopAllCoroutines();
        route.Clear();
        route = pathfinder.GetNewPath(coord);
        StartCoroutine(FollowPath());
    }
    
    void ReturnToStart()
    {
        transform.position = gridManager.PosFromCoord(pathfinder.StartCoord);
    }

    IEnumerator FollowPath()
    {
        for(int i = 1; i < route.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.PosFromCoord(route[i].coord);
            float movePercent = 0f;

            transform.LookAt(endPos);

            //Smooth moving between two points using LERP (Linear intERPolation)
            while(movePercent < 1f)
            {
                movePercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, movePercent);
                yield return new WaitForEndOfFrame();
            }

        }

        gameObject.GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);

        //Damage the Base
        gm.GetComponent<BaseHP>().reduceHP(enemyDamage);

    }
}
