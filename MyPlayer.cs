using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : Singleton<MyPlayer>
{
    //セーブ・ロード用のタグ名
    public const string LevelTag = "MyLevel";
    public const string ExpTag = "MyExp";

    //パラメーター
    public int level;
    public int hp;
    public float atk;
    public float spd;
    public int nextExp;
    //オリジナル追加（ビーム）
    public float beamRange;
    public string beamColor;
    //ショップ機能
    public int powerBoost;
    public int speedBoost;
    // 初期化(コンストラクタ)
    public MyPlayer()
    {
        level = 1;
        hp = 10;
        atk = 1f;
        spd = 1f;
        nextExp = 5;
        powerBoost = 1;
        speedBoost = 1;
    }
}
