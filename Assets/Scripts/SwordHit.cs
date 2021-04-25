using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
          
            if(other.gameObject.GetComponent<MyCharacterController>().death == false)
            {
                //Debug.Log("hit");
                other.gameObject.GetComponent<Animator>().SetTrigger("Hit");
                other.gameObject.GetComponent<MyCharacterController>().HitHP(40);
            }
            
        }
    }
}
