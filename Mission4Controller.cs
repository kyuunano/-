using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission4Controller : MonoBehaviour
{
    MissionController missionController;
    private void Start()
    {
        missionController = GameObject.Find("Mission").GetComponent<MissionController>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            missionController.MissionCheck(4);
            Debug.Log("ミッション4から抜けた");
            Debug.Log(gameObject.name);
        }
    }
}
