using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    
    public Animator ani;
    Rigidbody rb;
    Camera maincam;
    bool isWalking = false;
    float Health = 100f;
    public bool death=false;
    public Image hpbar;
    public List<GameObject> EnemyList;
    public GameObject GibbyFace;
    public GameObject EliteFace;
    public GameObject DJFace;
    public GameObject WoopsFace;

    public AudioClip Ouch1;
    public AudioClip Ouch2;
    public AudioClip Ouch3;
    public AudioClip Ouch4;
    public AudioClip AIOuch;
    public AudioClip HeartPickup;

    public AudioClip[] OuchArray = new AudioClip[4];

    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        OuchArray[0] = Ouch1;
        OuchArray[1] = Ouch2;
        OuchArray[2] = Ouch3;
        OuchArray[3] = Ouch4;



        if (tag.Equals("Player"))
        {
            try
            {
                DataManager.Character chosen = GameObject.Find("DataManager").GetComponent<DataManager>().chosen;
                if (chosen.Equals(DataManager.Character.Gibby))
                {
                    GibbyFace.SetActive(true);
                }
                else if (chosen.Equals(DataManager.Character.Elite))
                {
                    EliteFace.SetActive(true);
                }
                else if (chosen.Equals(DataManager.Character.DJ))
                {
                    DJFace.SetActive(true);
                }
                else if (chosen.Equals(DataManager.Character.Woops))
                {
                    WoopsFace.SetActive(true);
                }
            }
            catch
            {
                GibbyFace.SetActive(true);
            }
        }
        //Vince


        audioSource = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        maincam = Camera.main;
        //EnemyList = GameObject.Find("FloorSpawner").GetComponent<FloorSpawner>().EnemyList;
    }

    // Update is called once per frame
    void Update()
    {
        if (!tag.Equals("Enemy") && !death)
        {
            //Input
            if (Input.GetKey(KeyCode.W))
            {
                if (!isWalking)
                {
                    ani.SetBool("Walking", true);
                    isWalking = true;
                }
                rb.AddForce(transform.forward * (800 * Time.deltaTime));

            }
            else
            {
                ani.SetBool("Walking", false);
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (!isWalking)
                {
                    ani.SetBool("Walking", true);
                    isWalking = true;
                }
                rb.AddForce(transform.forward * (-800 * Time.deltaTime));
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                isWalking = false;

            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                isWalking = false;

            }
            if (Input.GetAxis("Horizontal") != 0)
            {
                float currentRot = rb.rotation.eulerAngles.y;
                rb.rotation = Quaternion.Euler(0, currentRot + ((Input.GetAxis("Horizontal")*300f) * Time.deltaTime), 0);
            }
            
            if (Input.GetButtonDown("Fire1"))
            {
                if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Swing"))
                {
                    ani.SetTrigger("Swing");

                }
            }

            hpbar.fillAmount = Health/100f;
        }

        if(Health <= 0)
        {
            if (!death)
            {
                death = true;
                ani.SetTrigger("Death");
                GetComponent<AITargetting>().StopTargetting();
                rb.freezeRotation = true;
                EnemyList.Remove(gameObject);
                GameObject.Destroy(gameObject, 5f);
            }
            else
            {
                if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Death"))
                {
                    ani.SetTrigger("Death");
                }
            }
        }

       

    }
    public void HitHP(int amount)
    {
        Health -= amount;
        if (tag.Equals("Player"))
        {
            PlayRandomHurt();
        }
        else
        {
            PlayAIHurt();

        }
    }
    public void Heal()
    {
        Health = 100;
        audioSource.PlayOneShot(HeartPickup);
    }
  
    public void PlayRandomHurt()
    {
        //audioSource.clip = OuchArray[UnityEngine.Random.Range(0, OuchArray.Length)];
        int ran = UnityEngine.Random.Range(0, OuchArray.Length);
        //Debug.Log(ran);
        audioSource.PlayOneShot(OuchArray[ran]);
    }
    public void PlayAIHurt()
    {
        audioSource.PlayOneShot(AIOuch);

    }
}
