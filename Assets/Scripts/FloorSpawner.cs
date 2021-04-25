using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FloorSpawner : MonoBehaviour
{
    public MyGameManager gm;
    public GameObject Enemyprefab;
    public GameObject spawningArea;
    public GameObject spawningArea2;
    public GameObject Gates;
    public List<GameObject> EnemyList = new List<GameObject>();
    public int floorNumber = 1;
    float startingY;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<MyGameManager>();
        startingY = Gates.transform.position.y;
        for(int i = 0; i<=floorNumber; i++)
        {
            float xbound = Random.Range(spawningArea2.transform.position.x, spawningArea.transform.position.x);
            float zbound = Random.Range(spawningArea2.transform.position.z, spawningArea.transform.position.z);
            GameObject spawned = Instantiate(Enemyprefab, new Vector3(xbound, spawningArea.transform.position.y, zbound), new Quaternion(0, 0, 0, 0));
            spawned.GetComponent<CharacterController>().EnemyList = EnemyList;
            EnemyList.Add(spawned);
            hasSpawnedEnemies = true;
            //
            //spawned.GetComponent<NavMeshAgent>().enabled = true;
        }
        
    }
    bool spawnedNextFloor = false;
    bool hasSpawnedEnemies = false;
    void Update()
    {
        if (EnemyList.Count <= 0)
        {
            if(Gates.transform.position.y <= (startingY + 7f))
            {
                Gates.transform.position += new Vector3(0f, 0.5f * Time.deltaTime, 0f);
                if (!spawnedNextFloor && hasSpawnedEnemies)
                {
                    gm.ProceedNextFloor();
                    spawnedNextFloor = true;
                }
            }
        }
    }
  
}
