using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum ItemType //列挙型 ※自作するデータ型
{
    light,
    bullet,
    goldKey,
    silverKey,
    life
}

public class ItemData : MonoBehaviour
{
    public ItemType type; //アイテムの種類
    public int stockCount; //入手数

    //セーブデータの時の識別番号
    public int arrangeId;

    Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }    
    }
}
