using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] List<Tower> towerPrefabs;
    [SerializeField] bool isPlaceable;
    [SerializeField] bool isResource;
    public bool IsPlaceable { get { return isPlaceable; } }

    GameManager gm;
    GridManager gridManager;
    Pathfinding pathfinder;
    Node valid;
    Vector2Int coord = new Vector2Int();
    public Vector2Int Coord { get { return coord; } }

    PlayerMovement player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        gm = FindObjectOfType<GameManager>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinding>();

        //Grab the list of available towers from the Game Manager
        towerPrefabs = gm.towerPrefabs;
    }

    private void Start()
    {
        if(gridManager != null)
        {
            coord = gridManager.CoordFromPos(transform.position);

            if(!isPlaceable)
            {
                gridManager.BlockNode(coord);
            }

            if(isResource)
            {
                gridManager.MakeResource(coord);
            }
        }
    }

    private void OnMouseDown()
    {
        //Check we're in build mode before constructing anything
        if(player.IsBuildMode)
        {
            //If we can place a tower (financially and pathfinding-wise) AND a tower is selected, place it
            if(gridManager.GetNode(coord).isRoutable && gm.curTowerSelect != 0 && !pathfinder.WillBlockPath(coord))
            {
                //If we're building a resource drill, it has to be adjacent to a mineral node
                if(gm.curTowerSelect == 3)
                {
                    valid = pathfinder.ExploreNeighborsResource(coord);

                    if(valid != null)
                    {
                        TryCreateTower(true);
                    }
                }

                else
                {
                    TryCreateTower(false);
                }

            }
        }
    }

    private void TryCreateTower(bool isResource)
    {
        Tower newTower = towerPrefabs[gm.curTowerSelect - 1];

        //If it's a resource tower, have it pass the relevant node (so it can rotate to face it)
        if(isResource)
        {
            newTower.GetComponent<ResourceTower>().resource = valid;
        }

        bool success = towerPrefabs[gm.curTowerSelect - 1].CreateTower(newTower, transform.position);

        if (success)
        {
            gridManager.BlockNode(coord);
            Debug.Log("Blocking Tile at " + coord);
            pathfinder.NotifyReceivers();
        }

        valid = null;
    }
}
