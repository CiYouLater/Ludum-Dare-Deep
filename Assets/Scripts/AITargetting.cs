using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITargetting : MonoBehaviour
{
    //NavMeshAgent nav;
    public GameObject target;
    public Rigidbody rb;
    public Collider fistSphere;

    // Start is called before the first frame update
    void Start()
    {
        //nav = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectsWithTag("Player")[0];
        rb = GetComponent<Rigidbody>();
    }
    public void StopTargetting()
    {
        //target = null;
        //nav.isStopped=true;
    }
    // Update is called once per frame
    void Update()
    {
        //nav.SetDestination(target.transform.position);

        if (GetComponent<MyCharacterController>().death == false)
        {

            Vector3 _direction = (target.transform.position - transform.position).normalized;

            Quaternion _lookRotation = Quaternion.LookRotation(_direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 5);

            if (Vector3.Distance(transform.position, target.transform.position) >= 1.5)
            {
                transform.position += transform.forward * Time.deltaTime * 5f;
                //Debug.Log(transform.forward * 0.0001f);
            }
        }



        if(Vector3.Distance(transform.position,target.transform.position) <= 1.5f)
        {
            if (GetComponent<MyCharacterController>().death == false)
            {
                if (target.GetComponent<MyCharacterController>().death == false)
                {
                    if (!GetComponent<MyCharacterController>().ani.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
                    {
                        GetComponent<MyCharacterController>().ani.SetTrigger("Punch");

                    }
                }
            }
        }
    }
    
}
