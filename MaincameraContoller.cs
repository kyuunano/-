using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaincameraContoller : MonoBehaviour
{
    public float speed = 0.5f;  // 回転スピード
    public float posY = 200f;   // カメラの高さ座標
    public GameObject centerObj;// 中心となる対象オブジェクト
    float radius;               // 回転する半径（中心オブジェクトとカメラの距離）

    // Start is called before the first frame update
    void Start()
    {
        // 半径を対象物とカメラとの距離から算出
        Vector2 dir = centerObj.transform.position - transform.position;
        radius = dir.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(0, posY, 0);

        // Sin、Cosを使って円状になるように座標を計算する
        // 円の直径分を掛け合わせることで中心オブジェクトの周りを半径分で周回する
        // 中心オブジェクトのx,z座標を加算するとこで円の中心座標を変更する
        pos.x = 2 * radius * Mathf.Sin(Time.time * speed) + centerObj.transform.position.x;
        pos.z = 2 * radius * Mathf.Cos(Time.time * speed) + centerObj.transform.position.z;

        // カメラに計算された座標をセット
        transform.position = pos;

        // カメラを常に中心オブジェクトの方を向かせる
        transform.LookAt(centerObj.transform);
    }
}
