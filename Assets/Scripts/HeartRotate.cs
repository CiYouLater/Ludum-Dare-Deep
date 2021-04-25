using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRotate : MonoBehaviour
{
    float x = 0f;
    bool hasHealed=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x += 30f * Time.deltaTime;
        transform.rotation = Quaternion.Euler(180,x,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (!hasHealed)
            {
                other.GetComponent<CharacterController>().Heal();
                hasHealed = true;
                Destroy(gameObject);
            }
        }
    }
}
