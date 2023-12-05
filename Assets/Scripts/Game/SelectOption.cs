using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageController : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        // MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.green);
        Debug.Log("yey");
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
