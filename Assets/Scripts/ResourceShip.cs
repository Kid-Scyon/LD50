using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceShip : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 5)] float speed = 1;
    List<Node> route = new List<Node>();
    GameManager gm;
    GridManager gridManager;
    Pathfinding pathfinder;
    ResourceTower owner;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinding>();

        RecalculatePath();
    }

    public void SetOwner(ResourceTower tower)
    {
        owner = tower;
    }

    void RecalculatePath()
    {
        Vector2Int coord = new Vector2Int();

        coord = gridManager.CoordFromPos(transform.position);

        StopAllCoroutines();
        route.Clear();
        route = pathfinder.GetNewPath(coord);
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < route.Count; i++)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.PosFromCoord(route[i].coord);
            float movePercent = 0f;

            transform.LookAt(endPos);

            //Smooth moving between two points using LERP (Linear intERPolation)
            while (movePercent < 1f)
            {
                movePercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, endPos, movePercent);
                yield return new WaitForEndOfFrame();
            }

        }

        gm.GetComponent<Bank>().ChangeBalance(100);
        //Create a new ship, as this one will be destroyed
        owner.NewShip();
        Destroy(gameObject);

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colliding with enemy!");
        owner.NewShip();
        Destroy(gameObject);
    }
}
