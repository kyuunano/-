using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    GameObject target;
    NavMeshAgent agent;
    //ダメージ時に動きを止める時間
    float damageTime;
    //HPゲージ取得
    Slider hpBar;
    //追跡フラグ
    public bool fieldViewFlg;
    //アニメーションリスト
    List<AnimationClip> animations = new List<AnimationClip>();

    //ミッション
    MissionController missionController;
    int Wolfk = 0;
    GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        //HPゲージ取得
        hpBar = transform.Find("HPBar/Slider").GetComponent<Slider>();
        //アニメーション取得
        foreach (AnimationState anim in GetComponent<Animation>())
        {
            animations.Add(anim.clip);
        }
        //ミッションコントローラー取得
        missionController = GameObject.Find("Mission").GetComponent<MissionController>();
        this.director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        if (damageTime > 0)
        {
            agent.speed = 0.5f;
            damageTime -= Time.deltaTime;

        }
        else
        {
            if (fieldViewFlg)
            {
                //走るアニメーション
                GetComponent<Animation>().Play(animations[3].name);

                //追いかけるスピードをランダムにする
                agent.speed = Random.Range(3f, 6f);

                //目的地への移動をAIに設定する
                agent.destination = target.transform.position;
            }
            else
            {
                //待機アニメーション
                GetComponent<Animation>().Play(animations[0].name);
            }
        }
    }
    public void Damage()
    {
        damageTime = 0.5f;
        //HP減らす
        //HP減らす
        hpBar.value -= 0.1f * MyPlayer.Instance.atk * MyPlayer.Instance.powerBoost;
        if (hpBar.value <= 0)
        {
            //死亡時エフェクト生成
            GameObject prefab = Resources.Load("Prefabs/Eff_Vanish_Fix") as GameObject;
            Instantiate(prefab, this.transform.position, Quaternion.identity);
            //敵倒してプレイヤーが経験値獲得
            if (gameObject.tag == "Wolf")
            {
                target.GetComponent<PlayerContolloer>().GetExp(10);
            }
            Destroy(gameObject);
            Sound.PlaySe("dog3");
            //  ミッション3を確認 
            this.director.GetComponent<GameDirector>().WolfKill();
            //if (this.director.GetComponent<GameDirector>().x == 3)
            //{
            //    missionController.MissionCheck(2);
            //}
            //else
            //{
            //    this.director.GetComponent<GameDirector>().WolfKill();
            //    Wolfk = this.director.GetComponent<GameDirector>().x;
            //    Debug.Log(Wolfk);
            //}



        }
    }
}
