using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isGoldDoor; //金の扉にするかどうか
    public Sprite goldDoorImage; //金の扉の絵

    public bool isEkey; //ボタンを押して反応するイベント

    // Start is called before the first frame update
    void Start()
    {
        if (isGoldDoor) //もしも金の扉フラグがあれば
        {
            //見た目を変える
            GetComponent<SpriteRenderer>().sprite = goldDoorImage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //キーイベントフラグがなく、ぶつかった相手がPlayer
        if(!isEkey && collision.gameObject.CompareTag("Player"))
        {
            //金の扉でなく、銀の鍵を所持していたら
            if(!isGoldDoor && GameController.hasSilverKey > 0)
            {
                GameController.hasSilverKey--;
                Destroy(gameObject);
            }
            
            //金の扉で、金の鍵を所持していたら
            if (isGoldDoor && GameController.hasGoldKey > 0)
            {
                GameController.hasGoldKey--;
                Destroy(gameObject);
            }
        }
    }
}
