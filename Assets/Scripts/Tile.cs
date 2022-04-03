using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] List<Tower> towerPrefabs;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }

    GameManager gm;
    PlayerMovement player;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        gm = FindObjectOfType<GameManager>();

        //Grab the list of available towers from the Game Manager
        towerPrefabs = gm.towerPrefabs;
    }

    private void OnMouseDown()
    {
        //Check we're in build mode before constructing anything
        if(player.IsBuildMode)
        {
            //If we can place a tower AND a tower is selected, place it
            if(isPlaceable && gm.curTowerSelect != 0)
            {
                bool success = towerPrefabs[gm.curTowerSelect - 1].CreateTower(towerPrefabs[gm.curTowerSelect - 1], transform.position);
                isPlaceable = success;
            }
        }
    }
}
