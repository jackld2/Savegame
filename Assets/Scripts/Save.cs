using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Save
    
{

    public List<Savable> savables = new List<Savable>();
    public string timestamp = "";

}

[System.Serializable]
public class Savable // Must be tagged as "Savable" or whatever you decide to set SAVABLE_TAG to in SaveManager
{
    public string name = "";
    public Vector3 pos = new Vector3();
    public Quaternion rot = new Quaternion();
    public Vector3 velocity = new Vector3();
    public Vector3 angularVelocity = new Vector3();
}

//Consider multiple Savable types. Saving may not be as cut and dry as saving physical object properties. Save what you need!


