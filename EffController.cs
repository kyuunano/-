using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ダメージ時に呼び出されるエフェクトを自動で削除するスクリプト
/// </summary>
public class EffController : MonoBehaviour
{

    float durationTime;

    void Start()
    {
        durationTime = GetComponent<ParticleSystem>().main.duration;
        Destroy(gameObject, durationTime);
    }
}
