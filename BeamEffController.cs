using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamEffController : MonoBehaviour
{
    ParticleSystem beamRange;   // ビーム射程を調整するParticleSystem
    ParticleSystem beamColor;   // ビーム色を調整するParticleSystem

    void Awake()
    {
        //参照
        beamRange = transform.Find("glitter1_add/parline1_alpha").GetComponent<ParticleSystem>();
        beamColor = transform.Find("glitter1_add/parline1_add").GetComponent<ParticleSystem>();
    }
    // レベルアップ時のビームを更新
    public void BeamLevelUp()
    {

        var main = beamRange.main;
        main.startLifetime = MyPlayer.Instance.beamRange;

        var main2 = beamColor.main;
        switch (MyPlayer.Instance.beamColor)
        {
            case "white":
                main2.startColor = Color.white;
                break;
            case "blue":
                main2.startColor = Color.blue;
                break;
            case "green":
                main2.startColor = Color.green;
                break;
            case "yellow":
                main2.startColor = Color.yellow;
                break;
            case "red":
                main2.startColor = Color.red;
                break;
        }
    }
}
