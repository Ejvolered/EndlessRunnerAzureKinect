using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs; //P�ytki wybierane z prefab�w
    public float zSpawn = 0;
    public float tileLength = 30; //D�ugo�� jednej p�ytki
    public int tileNumber = 5; //Ilo�� widocznych p�ytek
    private List<GameObject> activeTiles = new List<GameObject>(); //Lista aktywnych p�ytek

    public Transform playerTransform;
    void Start()
    {
        for(int i=0; i < tileNumber; i++)
        {
            if (i == 0)
                SpawnTile(0);
            else
                SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }


    void Update()
    {
        if (playerTransform.position.z - 35 > zSpawn - (tileNumber * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile(); 
        }
    }
    public void SpawnTile(int tileIndex) //Spawnowanie kolejnych p�ytek
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }
    private void DeleteTile() //Usuwanie p�ytek za plecami gracza
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
