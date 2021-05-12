using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseManager : MonoBehaviour
{
    public GameObject UserCanvas;
    public GameObject SaveCanvas;

    public Text usernameInputText;
    public Text userInfo;
    public Text startButtonText;
    public InputField usernameInput;
    public Button startButton;

    public Text saveLoadMessage;

    public static int DISPLAY_TIME = 4;

    private float userSessionStartTime;

    
    public User currentUser;
    public DataService ds;
    // Start is called before the first frame update
    void Start()
    {
        StartDataService();
        SaveCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey(KeyCode.Escape))
        {
            UserCanvas.SetActive(true);
            SaveCanvas.SetActive(false);
            
            usernameInputText.text = null;
            userInfo.text = null;
            startButton.interactable = false;
        }

    }

    //Creates database connection
    private void StartDataService()
    {
        ds = new DataService("UserDatabase.db");
        //Might be better to create some type of check here instead of calling CreateDB which
        //creates a new table, if it exists
        ds.CreateDB();
    }

    //searches for a user in the database, set start button to "New User" if user does not exist.
    public void SearchUser()
    {
        usernameInput.DeactivateInputField();
        string un = usernameInputText.text;
        User user = ds.GetByUsername(un);
        if (user != null)
        {
            currentUser = user;

            userInfo.text = user.ToString();
            startButtonText.text = "Load Save";
        }
        else
        {
            userInfo.text = string.Format("Username '{0}' not found", un);
            startButtonText.text = "New User";
        }
        startButton.interactable = true;
    }

    //loads the save of the searched user or creates a new user with fresh save depending on search results
    public void startGame()
    {
        if (startButtonText.text == "New User")
        {
            SaveManager.LoadFreshScene();
            string save = SaveManager.SaveScene();

            currentUser = new User { Username = usernameInputText.text, TimePlayed = 0f, SaveJSON = save };

            ds.AddUser(currentUser);
            StartCoroutine(ShowMessage("New User \n" + currentUser.ToString(), DISPLAY_TIME));
        }
        else
        {
            string lastsave = SaveManager.LoadScene(currentUser.SaveJSON);
            string message = string.Format("Loaded User\n{0}\nLast Save: {1}", currentUser.ToString(), lastsave);
            StartCoroutine(ShowMessage(message, DISPLAY_TIME));
        }
        
        UserCanvas.SetActive(false);
        SaveCanvas.SetActive(true);
        userSessionStartTime = Time.time;
    }

    //saves the state of the current user's scenes
    public void saveCurrent()
    {
        currentUser.SaveJSON = SaveManager.SaveScene();
        currentUser.TimePlayed += Time.time - userSessionStartTime;
        ds.ReplaceUser(currentUser);
        userSessionStartTime = Time.time;
        StartCoroutine(ShowMessage("Saved", DISPLAY_TIME));
    }

    //loads the current user's last save file
    public void loadCurrent()
    {
        string lastsave = SaveManager.LoadScene(currentUser.SaveJSON);
        string message = string.Format("Loaded\n{0}", lastsave);
        StartCoroutine(ShowMessage(message, DISPLAY_TIME));
    }

    //displays a message for 'delay' seconds
    IEnumerator ShowMessage(string message, float delay)
    {
        saveLoadMessage.text = message;
        saveLoadMessage.enabled = true;
        yield return new WaitForSeconds(delay);
        saveLoadMessage.enabled = false;
    }

    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }



}
