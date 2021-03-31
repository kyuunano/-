using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldViewController : MonoBehaviour
{
    EnemyController enemyController;
    // Start is called before the first frame update
    void Start()
    {
        //親オブジェクトからEnemyControllerスクリプト取得
        enemyController = transform.parent.GetComponent<EnemyController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーが視野内に入った
        if (other.gameObject.tag == "Player")
        {
            enemyController.fieldViewFlg = true;
            Sound.PlaySe("dog2");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //プレイヤーが視野から外れた
        if (other.gameObject.tag == "Player")
        {
            enemyController.fieldViewFlg = false;
        }
    }
}
