using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson; //追加

public class PlayerContolloer : MonoBehaviour
{
    //ビームエフェクト
    ParticleSystem beamEff;
    Slider hp;              //HP
    float damageSpan;       //ダメージを受ける間隔

    int maxHp;
    int currentHp;
    Image hpBody;

    //HPバーのUnityちゃんイメージ
    Image unityChanImage;
    Image kaihukuicon;
    Image poticon;
    Image kougekiicon;
    Image spedicon;
    Texture2D unityChanImage1;
    Texture2D unityChanImage2;
    Texture2D spedup;
    Texture2D pot;
    Texture2D hpheal;
    Texture2D kougeki;

    //ミッション
    MissionController missionController;

    int exp = 0;
    //ShopUIのShopControllerスクリプトをアウトレット接続する
    public ShopController shopController;
    private void Start()
    {
        //ビームエフェクト取得
        beamEff = transform.Find("Beam_Eff/glitter1_add").GetComponent<ParticleSystem>();
        //HP
        //hp = GameObject.Find("PlayerHP").GetComponent<Slider>();
        //円型HP
        hpBody = GameObject.Find("PlayerHP2").transform.Find("Body").GetComponent<Image>();

        //HPバーのUnityちゃんイメージ
        unityChanImage = GameObject.Find("PlayerHP2").transform.Find("UnityChanImage").GetComponent<Image>();
        kaihukuicon = GameObject.Find("ShopUI").transform.Find("Window").transform.Find("Item1").transform.Find("kaihukuicon").GetComponent<Image>();
        poticon = GameObject.Find("ShopUI").transform.Find("Window").transform.Find("Item2").transform.Find("poticon").GetComponent<Image>();
        kougekiicon = GameObject.Find("ShopUI").transform.Find("Window").transform.Find("Item3").transform.Find("kougekiicon").GetComponent<Image>();
        spedicon = GameObject.Find("ShopUI").transform.Find("Window").transform.Find("Item4").transform.Find("spedicon").GetComponent<Image>();
        unityChanImage1 = Resources.Load("Sprites/UnityChanImage1") as Texture2D;
        unityChanImage2 = Resources.Load("Sprites/UnityChanImage2") as Texture2D;
        spedup = Resources.Load("Sprites/スピード２") as Texture2D;
        pot = Resources.Load("Sprites/ポッド") as Texture2D;
        hpheal = Resources.Load("Sprites/体力回復") as Texture2D;
        kougeki = Resources.Load("Sprites/攻撃") as Texture2D;
        unityChanImage.sprite = Sprite.Create(unityChanImage1, new Rect(0, 0, unityChanImage1.width, unityChanImage1.height), Vector2.zero);
        kaihukuicon.sprite = Sprite.Create(hpheal, new Rect(0, 0, hpheal.width, hpheal.height), Vector2.zero);
        poticon.sprite = Sprite.Create(pot, new Rect(0, 0, pot.width, pot.height), Vector2.zero);
        kougekiicon.sprite = Sprite.Create(kougeki, new Rect(0, 0, kougeki.width, kougeki.height), Vector2.zero);
        spedicon.sprite = Sprite.Create(spedup, new Rect(0, 0, spedup.width, spedup.height), Vector2.zero);
        //ミッションコントローラー取得
        missionController = GameObject.Find("Mission").GetComponent<MissionController>();
        //前回の経験値をロード
        exp = EncryptedPlayerPrefs.LoadInt(MyPlayer.ExpTag, 0);
        //パラメーター初期化
        SetParameter();
    }

    private void OnCollisionStay(Collision other)
    {
        //ダメージを連続で受けないように間隔を開ける
        if (damageSpan > 0) return;
        //ウルフと接触
        if (other.gameObject.tag == "Wolf")
        {
            //ダメージ処理
            Damage(10);
            //ダメージ間隔時間を設定
            damageSpan = 1f;
            //死亡処理
            if (currentHp <= 0)
            {
                GameObject.Find("GameDirector").GetComponent<GameDirector>().GameOver();
            }
        }
    }
    // ショップと接触でShopUIを表示させゲームを一時停止
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Shop")
        {
            shopController.Open();
        }
    }

    private void Update()
    {
       
        Vector3 iti = GameObject.Find("Utc_win_humanoid").transform.position;
        float x = iti.x;
        float y = iti.y;
        float z = iti.z;
        // ジャンプボタンが押された時
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Sound.PlaySe("jump");
            // ミッション1を確認
            missionController.MissionCheck(0);
        }
        //攻撃ボタンでビーム発射
        if (Input.GetKeyDown(KeyCode.Q))
        {
            beamEff.Play();
            Sound.PlaySe("laser");
            missionController.MissionCheck(1);
        }

        //攻撃ボタンリリースでビームストップ
        if (Input.GetKeyUp(KeyCode.Q))
        {
            beamEff.Stop();
        }
        if (damageSpan > 0)
        {
            damageSpan -= Time.deltaTime;
        }
        //ダメージを受ける間隔時間を計測
        if (damageSpan > 0)
        {
            damageSpan -= Time.deltaTime;
        }
        else
        {
            unityChanImage.sprite = Sprite.Create(unityChanImage1, new Rect(0, 0, unityChanImage1.width, unityChanImage1.height), Vector2.zero);
        }
    }
    //ダメージ処理メソッド
    private void Damage(int d)
    {
        //hp.value -= d;
        currentHp -= d;
        hpBody.fillAmount = (float)currentHp / (float)maxHp;
        //SE
        //Sound.PlaySe("damage");
        //HPバーのUnityちゃんイメージ
        unityChanImage.sprite = Sprite.Create(unityChanImage2, new Rect(0, 0, unityChanImage2.width, unityChanImage2.height), Vector2.zero);
        iTween.ShakePosition(unityChanImage.gameObject, iTween.Hash("x", 5f, "y", 5f, "z", 0f));
    }
    // プレイヤーのパラメーターセット
    public void SetParameter()
    {
        //CSVデータからレベルごとのパラメーター値を取得
        List<string[]> csvData = new List<string[]>();
        csvData = CSVLoader.ReadFile("Level", ',');

        //Listデータから繰り返し処理で現在の問題Noと一致する情報を取得する
        foreach (string[] data in csvData)
        {
            //data.Lengthをチェックして空白行の場合はスキップさせる
            if (data.Length > 1)
            {
                //現在のレベルと同じデータを取得する
                if (int.Parse(data[0]) == MyPlayer.Instance.level)
                {
                    MyPlayer.Instance.hp = int.Parse(data[1]);
                    MyPlayer.Instance.atk = float.Parse(data[2]);
                    MyPlayer.Instance.spd = float.Parse(data[3]);
                    MyPlayer.Instance.nextExp = int.Parse(data[4]);
                    //オリジナル追加（ビーム）
                    MyPlayer.Instance.beamRange = float.Parse(data[5]);
                    MyPlayer.Instance.beamColor = data[6];
                }
            }

        }
        //HPセット
        currentHp = MyPlayer.Instance.hp;
        maxHp = MyPlayer.Instance.hp;
        //プレイヤーのスピードをセット
        GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier = MyPlayer.Instance.spd;
        //オリジナル追加（ビーム）
        transform.Find("Beam_Eff").GetComponent<BeamEffController>().BeamLevelUp();
        Debug.Log("現在のレベル" + MyPlayer.Instance.level);
    }
    //経験値獲得
    public void GetExp(int exp)
    {
        this.exp += exp;

        //レベルアップチェック
        if (this.exp > MyPlayer.Instance.nextExp)
        {
            LevelUp();
            this.exp = 0;
        }
    }
    //レベルアップ
    private void LevelUp()
    {
        MyPlayer.Instance.level++;

        //レベルの数に応じたパラメーターをCSVから取得（CSV処理は後ほど実装）
        SetParameter();
        GameObject prefab = Resources.Load("Prefabs/Eff_ShootingStar_Fix") as GameObject;
        Instantiate(prefab, this.transform.position, Quaternion.identity);
        //ログ
        Debug.Log("レベルが" + MyPlayer.Instance.level + "に上がった！");
        Debug.Log("さいだいHPが" + MyPlayer.Instance.hp + "に上がった！");
        Debug.Log("こうげきが" + MyPlayer.Instance.atk + "に上がった！");
        Debug.Log("すばやさが" + MyPlayer.Instance.spd + "に上がった");
        Debug.Log("次のレベルまで経験値が" + MyPlayer.Instance.nextExp + "必要です！");
    }
    // アプリケーションが終了する前にプレイヤーデータをセーブする
    void OnApplicationQuit()
    {
        //MyPlayer.Instance.level = 1;
        //exp = 0;
        EncryptedPlayerPrefs.SaveInt(MyPlayer.LevelTag, MyPlayer.Instance.level);
        EncryptedPlayerPrefs.SaveInt(MyPlayer.ExpTag, exp);
        Debug.Log("ゲームを閉じる前のレベル" + MyPlayer.Instance.level);
    }
    // ショップでアイテム購入（HP回復）
    public void LifeUp(int life)
    {
        currentHp += life;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
        hpBody.fillAmount = (float)currentHp / (float)maxHp;
    }
    //攻撃力のブースト（30秒で効果が元に戻る）
    public void PowerBoost()
    {
        MyPlayer.Instance.powerBoost = 10;
        //遅延処理するコルーチン（呼び出し側）
        StartCoroutine(DelayMethod(30.0f, () =>
        {
            MyPlayer.Instance.powerBoost = 1;
        }));
    }

    //移動速度のブースト（30秒で効果が元に戻る）
    public void SpeedBoost()
    {
        MyPlayer.Instance.speedBoost = 3;
        GameObject.FindWithTag("Player").GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier = MyPlayer.Instance.spd * MyPlayer.Instance.speedBoost;
        //遅延処理するコルーチン（呼び出し側）
        StartCoroutine(DelayMethod(30.0f, () =>
        {
            MyPlayer.Instance.speedBoost = 1;
            GameObject.FindWithTag("Player").GetComponent<ThirdPersonCharacter>().m_MoveSpeedMultiplier = MyPlayer.Instance.spd * MyPlayer.Instance.speedBoost;
        }));
    }

    /// <summary>
    /// 渡された処理を指定時間後に実行する
    /// </summary>
    /// <param name="waitTime">遅延時間[ミリ秒]</param>
    /// <param name="action">実行したい処理</param>
    /// <returns></returns>
    private IEnumerator DelayMethod(float waitTime, System.Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}