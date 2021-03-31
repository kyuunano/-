using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //回転を固定
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
