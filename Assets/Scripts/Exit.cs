using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//出口として機能した時のプレイヤーの位置を列挙型で自作
public enum ExitDirection
{
    right,
    left,
    up,
    down
}

public class Exit : MonoBehaviour
{
    public string sceneName; //切替先のシーン名
    public int doorNumber; //切替先の出入り口との連動番号

    //自作した列挙型でプレイヤーをどの位置におく出口なのか決めておく変数　※初期値は下
    public ExitDirection direction = ExitDirection.down;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //RoomControllerのシーン切替メソッド発動
            RoomController.ChangeScene(sceneName, doorNumber);
        }
    }
}
