using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Tile> route = new List<Tile>();
    [SerializeField] [Range(0.1f, 5)] float speed = 1;
    [SerializeField] int enemyDamage = 10;
    GameManager gm;

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void FindPath()
    {
        route.Clear();

        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        foreach(GameObject g in waypoints)
        {
            route.Add(g.GetComponent<Tile>());
        }
    }
    
    void ReturnToStart()
    {
        transform.position = route[0].transform.position;
    }

    IEnumerator FollowPath()
    {
        foreach(Tile w in route)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = w.transform.position;
            float movePercent = 0f;

            transform.LookAt(endPos);

            //Smooth moving between two points using LERP (Linear intERPolation)
            while(movePercent < 1f)
            {
                movePercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, movePercent);
                yield return new WaitForEndOfFrame();
            }

            gameObject.SetActive(false);

            //Damage the Base
            gm.GetComponent<BaseHP>().reduceHP(enemyDamage);
        }
    }
}
