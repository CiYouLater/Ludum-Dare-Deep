using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MyGameManager : MonoBehaviour
{
    public int floorProgression = 1;
    public GameObject roomPrefab;
    public GameObject previousRoom;
    public GameObject room;
    public FloorSpawner floorSpawner;
    public TMPro.TMP_Text floorcounter;
    public Button butt;
    public Image greyback;
    public GameObject cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
        previousRoom = GameObject.Find("RoomFirst");
    }

    public void ProceedNextFloor()
    {
        if (floorProgression > 1)
        {
            previousRoom = room;
        }

        if (floorProgression < 2)
        {
            room = GameObject.Instantiate(roomPrefab, previousRoom.transform.position + new Vector3(8.32f, -6.52f, 46.77f), new Quaternion(0, 0, 0, 0));
        }
        else
        {
            room = GameObject.Instantiate(roomPrefab, previousRoom.transform.position + new Vector3(0f, -10.39f, 79.79f), new Quaternion(0, 0, 0, 0));

        }
       

        floorProgression++;
        floorcounter.text = "Floor: "+floorProgression;
        Debug.Log("Spawned floor: "+floorProgression);
    }
    public void SpawnEnemy()
    {
        floorSpawner = room.transform.GetChild(5).GetComponent<FloorSpawner>();
        floorSpawner.floorNumber = floorProgression;
        floorSpawner.enabled = true;
    }
    public bool inMenu = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")&& !inMenu)
        {
            Cursor.lockState = CursorLockMode.Locked;
            inMenu = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !inMenu)
        {
            Cursor.lockState = CursorLockMode.None;
            greyback.gameObject.SetActive(true);
            butt.gameObject.SetActive(true);
            Time.timeScale = 0f;
            cam.GetComponent<ThirdPersonOrbitCamBasic>().enabled = false;
            inMenu = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inMenu)
        {
            Cursor.lockState = CursorLockMode.Locked;
            greyback.gameObject.SetActive(false);
            butt.gameObject.SetActive(false);
            Time.timeScale = 1f;
            cam.GetComponent<ThirdPersonOrbitCamBasic>().enabled = true;
            inMenu = false;
        }


    }
    public void MainMenuPress()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
