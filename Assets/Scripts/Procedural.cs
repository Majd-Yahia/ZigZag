using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Procedural : MonoBehaviour
{
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private int RoadsNumber;
    public List<GameObject> Roads;
    private GameObject lastRoad;
    private string roadTag = "LastRoad";

    private void Start()
    {
        Roads = new List<GameObject>();
        lastRoad = GameObject.FindGameObjectWithTag(roadTag);

        GameObject[] startRoads = GameObject.FindGameObjectsWithTag("Road");
        for (int i = 0; i < startRoads.Length; i++)
        {
            Roads.Add(startRoads[i]);
        }
        Roads.Add(lastRoad);
        LoopSpawn(RoadsNumber);
    }

    public void LoopSpawn(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            BeginSpawnProcedure();
            if (i == 0)
            {
                AddCollider();
            }
        }
    }

    private void BeginSpawnProcedure()
    {
        // Get the position of the last block.
        Vector3 position = lastRoad.transform.position;

        // Decide whether to go right or straight.
        bool forward = GetTrueOrFalse();

        // Set the new position for the block.
        Vector3 newPosition = NewPosition(forward, position);

        // Spawn block.
        SpawnNewBlock(newPosition);
    }

    private bool GetTrueOrFalse() { return Random.Range(0, 2) == 1 ? true : false; }

    private Vector3 NewPosition(bool forward, Vector3 position)
    {
        Vector3 newPosition = position;

        if (forward)
        {
            newPosition += new Vector3(0, 0, 1);
        }
        else
        {
            newPosition += new Vector3(-1, 0, 0);
        }

        return newPosition;
    }

    private void SpawnNewBlock(Vector3 position)
    {
        lastRoad = Instantiate(roadPrefab, position, Quaternion.identity);
        lastRoad.transform.parent = this.gameObject.transform;
        lastRoad.name = "Road " + Roads.Count;
        ActivateCrystal();
        Roads.Add(lastRoad);
    }

    private void ActivateCrystal()
    {
        bool activate = GetTrueOrFalse();
        Transform crystal = lastRoad.transform.GetChild(0);
        crystal.gameObject.SetActive(activate);
    }

    private void AddCollider()
    {
        lastRoad.AddComponent<ProceduralCollision>();
        BoxCollider collid = lastRoad.AddComponent<BoxCollider>();
        collid.size = new Vector3(1, 2, 1);
        collid.isTrigger = true;
    }

    public void BeginDestroyProcedure()
    {
        if (Roads.Count > 20)
        {
            GameObject road = Roads[0];
            Roads.Remove(road);
            Destroy(road, 2f);

            BeginDestroyProcedure();
        }
    }

}
