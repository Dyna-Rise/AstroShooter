using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public static bool getLight = true; //ライトを入手しているかどうかフラグ
    public static bool onLight; //ライトスイッチのON/OFF
    public GameObject playerLight; //ライトのオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        playerLight.SetActive(onLight);
    }

    // Update is called once per frame
    void Update()
    {
        if (!getLight) return; //ライトを取得していなければ何もできない

        if(Input.GetKeyDown(KeyCode.L))
        {
            onLight = !onLight; //ライトのスイッチをON/OFF　切り替え
            playerLight.SetActive(onLight); //オブジェクトが連動して表示/非表示
        }
    }
}
