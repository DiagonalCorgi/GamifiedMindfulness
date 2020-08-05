using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTiling : MonoBehaviour
{
    public GameObject environmentTile;

    public float tileSize;

    public GameObject endTile;
    public GameObject lastTile;
    public GameObject currentTile;
    public GameObject nextTile;

    public Vector3 lastTilePosition;


    // Start is called before the first frame update
    void Start()
    {
        lastTile = Instantiate(environmentTile, new Vector3(0, 0, 0), Quaternion.identity);
        tileSize = lastTile.GetComponentInChildren<MeshFilter>().mesh.bounds.size.z * lastTile.GetComponentInChildren<MeshFilter>().transform.localScale.z;
        lastTilePosition = lastTile.transform.position;
        endTile = Instantiate(environmentTile, new Vector3(lastTilePosition.x, lastTilePosition.y, lastTilePosition.z - tileSize), Quaternion.identity);
        currentTile = Instantiate(environmentTile, new Vector3(lastTilePosition.x, lastTilePosition.y, lastTilePosition.z + tileSize), Quaternion.identity);
        nextTile = Instantiate(environmentTile, new Vector3(lastTilePosition.x, lastTilePosition.y, lastTilePosition.z + (2 * tileSize)), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, lastTile.transform.position) > Vector3.Distance(gameObject.transform.position, currentTile.transform.position))
        {
            Destroy(endTile);
            endTile = lastTile;
            lastTile = currentTile;
            lastTilePosition = lastTile.transform.position;
            currentTile = nextTile;
            nextTile = Instantiate(environmentTile, new Vector3(lastTilePosition.x, lastTilePosition.y, lastTilePosition.z + (2 * tileSize)), Quaternion.identity);
        }
    }
}
