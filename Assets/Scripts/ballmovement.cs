using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballmovement : MonoBehaviour
{
    public GameObject body;
    public Vector3 force;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            body.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.2f, 1.0f), ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            body.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.2f, -1.0f), ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.GetComponent<Rigidbody>().AddForce(new Vector3(-1.0f, 0.2f, 0.0f), ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            body.GetComponent<Rigidbody>().AddForce(new Vector3(1.0f, 0.2f, 0.0f), ForceMode.Force);
        }

    }
}
