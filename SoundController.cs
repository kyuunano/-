using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Soundデータ初期化
/// </summary>
public class SoundController : MonoBehaviour {

	void Start () {

        //SE
        Sound.LoadSe("coin", "coin");
        Sound.LoadSe("jump", "jump");
        Sound.LoadSe("laser", "laser");
        Sound.LoadSe("bomb", "bomb");
        Sound.LoadSe("dog3","dog3");
        Sound.LoadSe("dog2", "dog2");
        Sound.LoadSe("rain", "rain");
        Sound.LoadSe("bgm1", "bgm1");
        //BGM

    }
}
