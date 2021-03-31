using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    // ミッション詳細を保持するList
    private List<string> missionList = new List<string>();
    // ミッション詳細のUIを配列で参照
    private Text[] missionDetail;
    // 現在のミッション数
    public int missionNum;

    // Start is called before the first frame update
    void Start()
    {
        //ミッション詳細をセット
        missionList.Add("ジャンプを1回行う");
        missionList.Add("攻撃する");
        missionList.Add("ウルフを3匹倒す");
        missionList.Add("物見やぐらに登る"); ///物見やぐらに登った時のユニティちゃんの座標の位置を見つけて、ユニティちゃんの現在の位置を取得して、その位置の範囲に入ったらミッションクリア
        missionList.Add("周辺を少し探索する");///ユニティちゃんの現在位置を常に取得。x,y,z軸が動いたらその分設定した変数を増やしていって、その変数が一定以上になったら、ミッションクリア
       
        //ミッションUIへ詳細をセット
        int i = 0;
        foreach (Transform child in transform)
        {
            child.GetComponent<Text>().text = missionList[i];
            i++;
        }
    }
    //ミッションクリア時
    private void MissionClear()
    {

        //現在のミッションを消去
        missionList.RemoveAt(0);

        missionNum++;

        //UI表示の更新
        int i = 0;
        foreach (Transform child in transform)
        {

            if (i < missionList.Count)
            {
                child.GetComponent<Text>().text = missionList[i];
            }
            else
            {
                child.GetComponent<Text>().text = "";
            }
            i++;
        }
    }
    // ミッションクリアチェック
    // 現在チャレンジ中のミッションとミッション条件満たした内容が一致するかどうかの判別
    public void MissionCheck(int mCount)
    {
        if (missionNum == mCount)
        {
            MissionClear();
        }
    }
}
