using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFloor : MonoBehaviour
{
    public GameObject optionsToActive;
    public GameObject[] optionsToDeactive; 

    // Start is called before the first frame update
    void Start()
    {
        optionsToActive.SetActive(true);
        foreach (GameObject option in optionsToDeactive)
        {
            option.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
