using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ビームエフェクトのパーティクル衝突判定用
/// </summary>
public class BeamParticleController : MonoBehaviour
{
    public int toubatu=0;
    private void OnParticleCollision(GameObject other)
    {
        //ウルフと接触
        if (other.tag == "Wolf")
        {
            other.GetComponent<EnemyController>().Damage();
            //ダメージエフェクト生成
            GameObject prefab = Resources.Load("Prefabs/Eff_Hit_Fix") as GameObject;
            GameObject obj = Instantiate(prefab,other.transform.position,Quaternion.identity);
        }
        if (other.tag == "Spown")
        {
            other.GetComponent<SpawnController>().Damage();
            //ダメージエフェクト生成
            GameObject prefab = Resources.Load("Prefabs/DamageEff") as GameObject;
            GameObject obj = Instantiate(prefab, other.transform.position, Quaternion.identity);
        }
    }
}

