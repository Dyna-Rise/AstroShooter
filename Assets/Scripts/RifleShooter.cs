using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleShooter : MonoBehaviour
{
    public GameObject riflePrefab; //生成されるべきライフル
    GameObject rifleObj; //生成されたライフルオブジェの情報

    // Start is called before the first frame update
    void Start()
    {
        Vector3　playerPos = transform.position; //Playerの位置
        //ライフルを生成しつつrifleOjbに情報を格納
        rifleObj = Instantiate(riflePrefab, playerPos, Quaternion.identity);
        rifleObj.transform.SetParent(transform); //生成したライフルはPlayerの子オブジェクトになる
    }

    // Update is called once per frame
    void Update()
    {
        //ライフルの回転とZ軸の優先順位
        float rifleZ = -1; //基本は手前に写るように値をセッティング
        PlayerController playerCnt = GetComponent<PlayerController>();

        if (playerCnt.angleZ > 45 && playerCnt.angleZ < 135)
        {
            rifleZ = 1; //Playerが上向きの時はライフルは奥に写るようにセッティング
        }

        //ライフルをangleZに合わせて常に回転 ※元の絵の問題より90度プラス
        rifleObj.transform.rotation = Quaternion.Euler(0,0,(playerCnt.angleZ + 90));

        //ライフルのZ座標の「手前か奥か」の位置を調整
        rifleObj.transform.position = new Vector3(transform.position.x, transform.position.y, rifleZ);
    }
}
