using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldManager : MonoBehaviour
{
    public int Row;
    public int Column;

    public bool isSensorized;

    private bool activaterayCast;
    private bool activaterayCastIntensity;
    private float strenght;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activaterayCast)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -Vector3.forward, out hit))
            {
                Debug.DrawRay(transform.position, transform.forward*10000, Color.green);
                hit.transform.SendMessage("TractionIdentified");
            }
            if (Physics.Raycast(transform.position, Vector3.forward, out hit))
            {
                Debug.DrawRay(transform.position, -transform.forward * 10000, Color.red);
                hit.transform.SendMessage("TractionIdentified");
            }
        }
        if (activaterayCastIntensity)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -Vector3.forward, out hit))
            {
                Debug.DrawRay(transform.position, transform.forward * 10000, Color.green);
                hit.transform.SendMessage("TractionIdentified", strenght);
            }
            if (Physics.Raycast(transform.position, Vector3.forward, out hit))
            {
                Debug.DrawRay(transform.position, -transform.forward * 10000, Color.red);
                hit.transform.SendMessage("TractionIdentified", strenght);
            }
        }
    }

    public void Setup(int col, int row, bool isSensorized) {
        Column = col;
        Row = row;
        this.isSensorized = isSensorized;
        if (isSensorized) {
            MagicRoomManager.instance.MagicRoomClimbingWallManager.TractionEvent += manageTraction;
            TrackerClimbingWall.GraspedHoldEvent += manageGrasp;
            TrackerClimbingWall.ReleasedHoldEvent += releaseGrasp;
        }
    }

    private void manageGrasp(int col, int row)
    {
        if (col == Column && Row == row)
        {
            Debug.Log(gameObject.name + " activated");
            activaterayCast = true;
        }
    }

    private void releaseGrasp(int col, int row)
    {
        if (col == Column && Row == row)
        {
            activaterayCast = false;
        }
    }

    private void manageTraction(int col, int row, Vector3 traction)
    {
        if (col == Column && Row == row)
        {
            activaterayCastIntensity = true;
            strenght = traction.magnitude;
        }
    }
}
