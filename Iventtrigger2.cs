using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Iventtrigger2 : MonoBehaviour
{
    MissionController missionController;
    private void Start()
    {
        missionController = GameObject.Find("Mission").GetComponent<MissionController>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            missionController.MissionCheck(4);
            //Debug.Log("接触");
            //Debug.Log(gameObject.name);
        }
    }
}
