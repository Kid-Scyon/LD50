using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTower : MonoBehaviour
{
    [SerializeField] ResourceShip shipPrefab;
    [SerializeField] int spawnTimer = 10;
    GridManager gridManager;
    ResourceShip ownedShip;
    public Node resource;

    // Start is called before the first frame update
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        transform.LookAt(2 * transform.position - gridManager.PosFromCoord(resource.coord));
        ownedShip = Instantiate(shipPrefab, transform.position, Quaternion.identity);
        ownedShip.SetOwner(this);
    }
    
    public void NewShip()
    {
        StartCoroutine(SpawnShip());
    }

    IEnumerator SpawnShip()
    {
        yield return new WaitForSeconds(spawnTimer);
        ownedShip = Instantiate(shipPrefab, transform.position,Quaternion.identity);
        ownedShip.SetOwner(this);
    }
}
