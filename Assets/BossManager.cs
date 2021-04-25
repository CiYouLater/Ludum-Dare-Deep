using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public GameObject target;
    public GameObject firePoint;
    public GameObject eye;
    public GameObject eye2;
    public LineRenderer lr;
    public LineRenderer lr2;
    int maxLength = 800;
    float distmod = .66f;
    public GameObject LaserHit;
    bool shootingLaser = false;
    bool chargingLaser = false;
    float timer = 0f;
    float lastShot = 0f;
    float lastCharged = 0f;
    public AudioSource audios;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip deathclip;
    public AudioClip[] clips = new AudioClip[4];
    public GameObject bossHealth;
    public GameObject EnemyPrefab;
    public List<GameObject> EnemyList;
    private int Health = 100;
    public FloorSpawner callback;
    bool died = false;

    // Start is called before the first frame update
    void Start()
    {
        clips[0] = clip1;
        clips[1] = clip2;
        clips[2] = clip3;
        clips[3] = clip4;
        bossHealth = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
        bossHealth.SetActive(true);
        bossHealth.transform.GetChild(0).GetComponent<Image>().fillAmount = Health / 100f;
        audios = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectsWithTag("Player")[0];

    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0 && !died)
        {
            audios.PlayOneShot(deathclip);
            bossHealth.SetActive(false);
            callback.bossDefeated = true;
            Destroy(gameObject,3f);
            died = true;
        }


        Vector3 _direction = ((target.transform.position+new Vector3(0,2,0)) - transform.position).normalized;

        Quaternion _lookRotation = Quaternion.LookRotation(_direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 5);

        timer += Time.deltaTime;

        if(timer-lastShot >= 3f && chargingLaser == false)
        {
            Debug.Log("Charging" + timer);
            shootingLaser = false;
            chargingLaser = true;
            lastCharged = timer;
            SpawnMinions();

        }
        if(timer-lastCharged >= 15f && chargingLaser == true)
        {
            int ran = UnityEngine.Random.Range(0, clips.Length);
            audios.PlayOneShot(clips[ran]);
            Debug.Log("Shooting" + timer);
            shootingLaser = true;
            lastShot = timer;
            chargingLaser = false;
        }
      



        //Vector3 eyedirection = ((target.transform.position + new Vector3(0, 0, 0)) - eye.transform.position).normalized;
        //Quaternion eyelookRotation = Quaternion.LookRotation(eyedirection);
        //eye.transform.rotation = Quaternion.Slerp(transform.rotation, eyelookRotation, Time.deltaTime * 5);

        //Vector3 eye2direction = ((target.transform.position + new Vector3(0, 0, 0)) - eye2.transform.position).normalized;
        //Quaternion eye2lookRotation = Quaternion.LookRotation(eye2direction);
        //eye2.transform.rotation = Quaternion.Slerp(transform.rotation, eye2lookRotation, Time.deltaTime * 5);
        if (shootingLaser)
        {
            eye.SetActive(true);
            eye2.SetActive(true);
            LaserHit.SetActive(true);

            firePoint = target;

            lr.SetPosition(0, firePoint.transform.position);

            RaycastHit hit;
            if (Physics.Raycast(eye.transform.position, eye.transform.TransformDirection(Vector3.forward), out hit, maxLength))
            {
                //Debug.DrawRay(eye.transform.position, eye.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.collider)
                {
                    Vector3 yuh = eye.transform.InverseTransformPoint(hit.point);
                    lr.SetPosition(1, new Vector3(yuh.x, yuh.y, yuh.z * distmod));

                    LaserHit.transform.position = hit.point;
                    LaserHit.SetActive(true);
                    if (hit.collider.gameObject.tag.Equals("Player"))
                    {
                        hit.collider.gameObject.GetComponent<MyCharacterController>().HitHP(1);
                    }
                }
            }

            lr2.SetPosition(0, firePoint.transform.position);

            RaycastHit hit2;
            if (Physics.Raycast(eye2.transform.position, eye2.transform.TransformDirection(Vector3.forward), out hit2, maxLength))
            {
                //Debug.DrawRay(eye2.transform.position, eye2.transform.TransformDirection(Vector3.forward) * hit2.distance, Color.yellow);
                if (hit.collider)
                {
                    Vector3 yuh2 = eye2.transform.InverseTransformPoint(hit.point);
                    lr2.SetPosition(1, new Vector3(yuh2.x, yuh2.y, yuh2.z * distmod));

                }
            }
        }
        else
        {
            eye.SetActive(false);
            eye2.SetActive(false);
            LaserHit.SetActive(false);

        }
    }
    public void TakeDamage(int amt)
    {
        Health -= amt;
        bossHealth.transform.GetChild(0).GetComponent<Image>().fillAmount = Health / 100f;
    }
    void SpawnMinions()
    {
        for (int i = 0; i <= 1; i++)
        {
            
            GameObject spawned = Instantiate(EnemyPrefab, transform.position, new Quaternion(0, 0, 0, 0));
            spawned.GetComponent<MyCharacterController>().EnemyList = EnemyList;
            spawned.GetComponent<MyCharacterController>().isBossAI = true;
            spawned.GetComponent<MyCharacterController>().bossman = this;
            EnemyList.Add(spawned);
            //hasSpawnedEnemies = true;
            //
            //spawned.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
    
}
