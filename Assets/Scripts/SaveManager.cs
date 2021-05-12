using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    private static string defaultSave;
    public static string SAVABLE_TAG = "Savable";

    // Start is called before the first frame update
    void Start()
    {
        defaultSave = SaveScene();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private static Save CreateSaveObject()
    {
        Save save = new Save();
        
        GameObject[] objects = GameObject.FindGameObjectsWithTag(SAVABLE_TAG);

        //save transforms
        foreach (GameObject obj in objects)
        {
            Savable savable = new Savable();
            savable.name = obj.name;
            savable.pos = obj.transform.position;
            savable.rot = obj.transform.rotation;
            save.savables.Add(savable);

            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                
                savable.velocity = rb.velocity;
                savable.angularVelocity = rb.angularVelocity;
                //Debug.Log("saved velocity: " + savable.velocity);

            }
        }
        save.timestamp = System.DateTime.Now.ToString();
        return save;
    }


    //Save all savable objects in the scene
    public static string SaveScene()
    {
        Save save = CreateSaveObject();
        string saveJSON = JsonUtility.ToJson(save);
        return saveJSON;
    }

    //load up the JSON, search for objects of the same name, load savable properties into scene
    public static string LoadScene(string saveJSON)
    {
        if (saveJSON != null)
        {
            Debug.Log("Found save file");
            Save save = JsonUtility.FromJson<Save>(saveJSON);

            foreach (var savable in save.savables)
            {
                GameObject sceneObject = GameObject.Find(savable.name);
                Debug.Log("Looking for " + savable.name + "...");
                if (sceneObject != null)
                {
                    sceneObject.transform.position = savable.pos;
                    sceneObject.transform.rotation = savable.rot;
                    Rigidbody rb = sceneObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.velocity = savable.velocity;
                        rb.angularVelocity = savable.angularVelocity;
                        //Debug.Log("Loaded velocity: " + savable.velocity);
                    }
                }
                else
                {
                    Debug.LogError("'" + savable.name + "'" + " is not currently in a scene.");
                }
            }
            Debug.Log("Loaded save");
            return save.timestamp;
        }
        else
        {
            Debug.Log("No save file found");
            return null;
        }

    }

    //loads the default save
    public static string LoadFreshScene()
    {
        return LoadScene(defaultSave);
    }

    
}
