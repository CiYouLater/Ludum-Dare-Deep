using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button playButton;
    public TMPro.TMP_Text topText;
    public TMPro.TMP_Text bottomText;
    public TMPro.TMP_Text explain;
    public GameObject Elite;
    public Button Elitebutton;
    public GameObject Gibby;
    public Button Gibbybutton;
    public GameObject DJ;
    public Button DJbutton;
    public GameObject Woops;
    public Button Woopsbutton;
    public GameObject Vince;
    public Button Vincebutton;

    bool moveFacesForward = false;
    float timer;
    public DataManager dm;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        moveFacesForward = false;
        dm = GameObject.Find("DataManager").GetComponent<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (moveFacesForward && timer <= 4f)
        {
            Elite.transform.position = new Vector3(Elite.transform.position.x, Elite.transform.position.y, Elite.transform.position.z - 20f*Time.deltaTime);
            Gibby.transform.position = new Vector3(Gibby.transform.position.x, Gibby.transform.position.y, Gibby.transform.position.z - 20f * Time.deltaTime);
            DJ.transform.position = new Vector3(DJ.transform.position.x, DJ.transform.position.y, DJ.transform.position.z - 20f * Time.deltaTime);
            Woops.transform.position = new Vector3(Woops.transform.position.x, Woops.transform.position.y, Woops.transform.position.z - 20f * Time.deltaTime);
            Vince.transform.position = new Vector3(Vince.transform.position.x, Vince.transform.position.y, Vince.transform.position.z - 20f * Time.deltaTime);

        }
        else
        {
            moveFacesForward = false;
        }
    }
    public void PlayPress()
    {
        playButton.gameObject.SetActive(false);
        explain.gameObject.SetActive(false);
        topText.text = "Choose your character";
        Elitebutton.gameObject.SetActive(true);
        Gibbybutton.gameObject.SetActive(true);
        DJbutton.gameObject.SetActive(true);
        Woopsbutton.gameObject.SetActive(true);
        Vincebutton.gameObject.SetActive(true);
        bottomText.gameObject.SetActive(true);
        timer = 0f;
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
    public void VincePress()
    {
        dm.chosen = DataManager.Character.Vince;
        SceneManager.LoadScene(1);
    }
}
