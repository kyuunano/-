using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviour
{
    //　フェードキャンバス取得
    public Fade fade;

    private void Start()
    {
        // セーブデータよりレベル値を取得する（保存されていない場合はデフォルト値である1）
        MyPlayer.Instance.level = EncryptedPlayerPrefs.LoadInt(MyPlayer.LevelTag, 1);
    }
    //　スタートボタン
    public void OnPressStartBtn()
    {
        // トランジションをかけてシーンを偏移する
        fade.FadeIn(1f, () => {
            SceneManager.LoadScene("GameScene");
        });
        
    }
}
