using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class ShopController : MonoBehaviour
{
    //ショップアイテム残数
    int item1Count = 2;
    int item2Count = 2;
    int item3Count = 1;
    int item4Count = 1;

    //アイテムボタン
    Button item1;
    Button item2;
    Button item3;
    Button item4;
    //タイムライン
    PlayableDirector shopOpenTimeline;
    // Start is called before the first frame update
    void Start()
    {
        item1 = transform.Find("Window/Item1").GetComponent<Button>();
        item2 = transform.Find("Window/Item2").GetComponent<Button>();
        item3 = transform.Find("Window/Item3").GetComponent<Button>();
        item4 = transform.Find("Window/Item4").GetComponent<Button>();
        //初期は非表示
        gameObject.SetActive(false);
        //タイムライン
        //shopOpenTimeline = GameObject.Find("ShopOpenTimeLine").GetComponent<PlayableDirector>();
    }
    //ショップウインドウを表示
    public void Open()
    {
        gameObject.SetActive(true);
        //タイムライン再生
        //shopOpenTimeline.Play();
        //ゲームを一時停止
        Time.timeScale = 0f;
    }
    //ショップウインドウを閉じる
    public void Close()
    {
        //ゲーム再開
        Time.timeScale = 1f;
        //出口にキャラを配置
        Transform player = GameObject.FindWithTag("Player").transform;
        player.position = GameObject.Find("Exit").transform.position;
        gameObject.SetActive(false);
        
    }
    //アイテム１クリック時
    public void OnPressItem1()
    {
        if (item1Count < 1) return;
        //HP回復
        GameObject.FindWithTag("Player").GetComponent<PlayerContolloer>().LifeUp(50);
        //残数減らす
        item1Count--;
        //残数0でアイコン非表示
        if (item1Count < 1)
        {
            item1.interactable = false;
        }
    }
    //アイテム3クリック時
    public void OnPressItem3()
    {
        if (item3Count < 1) return;
        //パワーブースト
        GameObject.FindWithTag("Player").GetComponent<PlayerContolloer>().PowerBoost();
        //残数減らす
        item3Count--;
        //残数0でアイコン非表示
        if (item3Count < 1)
        {
            item3.interactable = false;
        }
    }

    //アイテム4クリック時
    public void OnPressItem4()
    {
        if (item4Count < 1) return;
        //スピードブースト
        GameObject.FindWithTag("Player").GetComponent<PlayerContolloer>().SpeedBoost();
        //残数減らす
        item4Count--;
        //残数0でアイコン非表示
        if (item4Count < 1)
        {
            item4.interactable = false;
        }
    }
    //private void Update()
    //{
    //    gameObject.SetActive(false);
    //}
}
