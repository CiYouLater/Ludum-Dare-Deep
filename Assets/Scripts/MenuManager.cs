using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button playButton;
    public TMPro.TMP_Text topText;
    public GameObject Elite;
    public Button Elitebutton;
    public GameObject Gibby;
    public Button Gibbybutton;
    public GameObject DJ;
    public Button DJbutton;
    public GameObject Woops;
    public Button Woopsbutton;

    bool moveFacesForward = false;
    public DataManager dm;
    // Start is called before the first frame update
    void Start()
    {
        dm = GameObject.Find("DataManager").GetComponent<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveFacesForward && Elite.transform.position.z >= 16f)
        {
            Elite.transform.position = new Vector3(Elite.transform.position.x, Elite.transform.position.y, Elite.transform.position.z - 20f*Time.deltaTime);
            Gibby.transform.position = new Vector3(Gibby.transform.position.x, Gibby.transform.position.y, Gibby.transform.position.z - 20f * Time.deltaTime);
            DJ.transform.position = new Vector3(DJ.transform.position.x, DJ.transform.position.y, DJ.transform.position.z - 20f * Time.deltaTime);
            Woops.transform.position = new Vector3(Woops.transform.position.x, Woops.transform.position.y, Woops.transform.position.z - 20f * Time.deltaTime);

        }
        else
        {
            moveFacesForward = false;
        }
    }
    public void PlayPress()
    {
        playButton.gameObject.SetActive(false);
        topText.text = "Choose your character";
        Elitebutton.gameObject.SetActive(true);
        Gibbybutton.gameObject.SetActive(true);
        DJbutton.gameObject.SetActive(true);
        Woopsbutton.gameObject.SetActive(true);

        moveFacesForward = true;
    }
    public void ElitePress()
    {
        dm.chosen = DataManager.Character.Elite;
        SceneManager.LoadScene(1);
    }
    public void GibbyPress()
    {
        dm.chosen = DataManager.Character.Gibby;
        SceneManager.LoadScene(1);
    }
    public void DJPress()
    {
        dm.chosen = DataManager.Character.DJ;
        SceneManager.LoadScene(1);
    }
    public void WoopsPress()
    {
        dm.chosen = DataManager.Character.Woops;
        SceneManager.LoadScene(1);
    }
}
