using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体を管理する
/// </summary>
public class GameDirector : MonoBehaviour
{
    GameObject gameOverText;
    GameObject levelText;
    public Fade fade;
    public int x = 0;
    MissionController missionController;
    void Start()
    {
        gameOverText = GameObject.Find("GameOverText");
        levelText = GameObject.Find("LevelText");
        gameOverText.SetActive(false);
        //シーン遷移のフェードアウト
        fade.FadeOut(1f);
        missionController = GameObject.Find("Mission").GetComponent<MissionController>();
        // セーブデータよりレベル値を取得する（保存されていない場合はデフォルト値である1）
        MyPlayer.Instance.level = EncryptedPlayerPrefs.LoadInt(MyPlayer.LevelTag, 1);
    }

    //ゲームオーバー処理
    public void GameOver()
    {
        gameOverText.SetActive(true);
        //コルーチンで遅延処理
        StartCoroutine("ReloadScene");
    }

    //コルーチン
    public IEnumerator ReloadScene()
    {
        //2秒後にリプレイ
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameScene");
    }
    public void WolfKill()
    {
        x++;
        if (x == 3)
        {
            missionController.MissionCheck(2);
            
        }
    }
}
