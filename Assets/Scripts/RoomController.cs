using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomController : MonoBehaviour
{
    //ゲーム中共通して管理するドア番号
    public static int doorNumber;


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] exits = GameObject.FindGameObjectsWithTag("Exit");
        for(int i = 0; i <exits.Length; i++)
        {
            if(doorNumber == exits[i].GetComponent<Exit>().doorNumber)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3(exits[i].transform.position.x, exits[i].transform.position.y);

                break; //そのループを抜ける

            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //静的メソッド　（どこのシーンに、何番のドア？）
    public static void ChangeScene(string scenename,int doornum)
    {
        //staticであるRoomControllerのdoorNumberに引数に指定したdoornumを代入
        doorNumber = doornum; //次のシーンにドア番号が引き継がれる
        SceneManager.LoadScene(scenename);
    }
}
