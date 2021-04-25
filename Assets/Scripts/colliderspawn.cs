using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderspawn : MonoBehaviour
{
    public MyGameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<MyGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            gm.SpawnEnemy();
            
        }
    }
}
