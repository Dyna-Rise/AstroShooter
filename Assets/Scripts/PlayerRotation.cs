using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    //エディター上で他コンポーネントをアタッチ
    public PlayerController playerCnt;

    // Update is called once per frame
    void Update()
    {
        //PlayerControllerが持っているangleZの数値だけRotationBodyを回転
        transform.rotation = Quaternion.Euler(0,0,playerCnt.angleZ);
    }
}
