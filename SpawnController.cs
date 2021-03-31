using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnController : MonoBehaviour
{
    //モンスター名
    public string enemyName = "wolf";
    //生成間隔
    public float spanTime = 5f;

    float delta;
    GameObject enemyPrefab;
    //HPゲージ取得
    Slider hpBar;
    float damageTime;


    void Start()
    {
        //モンスター名から生成する敵プレファブを取得
        switch (enemyName)
        {
            case "wolf": enemyPrefab = Resources.Load("Prefabs/WolfPrefab") as GameObject; break;
        }
        //HPゲージ取得
        hpBar = transform.Find("HPBar/Slider").GetComponent<Slider>();
    }


    void Update()
    {
        if (damageTime > 0)
        {
            damageTime -= Time.deltaTime;
        }
        delta += Time.deltaTime;

        //生成間隔おきに敵を生成
        if (delta > spanTime)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            delta = 0;
        }       
    }

    public void Damage()
    {
        damageTime = 0.5f;
        //HP減らす
        hpBar.value -= 0.1f;
        if (hpBar.value <= 0)
        {
            //死亡時エフェクト生成
            GameObject prefab = Resources.Load("Prefabs/Eff_Burst_Fix") as GameObject;
            Instantiate(prefab, this.transform.position, Quaternion.identity);

            Destroy(gameObject);
            Sound.PlaySe("bomb");
        }
    }
}
