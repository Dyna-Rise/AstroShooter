using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float deleteTime = 1.0f; //消滅までの時間

    // Start is called before the first frame update
    void Start()
    {
        //誕生と同時に時間差で消滅
        Destroy(gameObject, deleteTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //相手がWallタグじゃなかったら
        if (collision.gameObject.tag != "Wall")
        //if (!collision.gameObject.CompareTag("Wall"))
        {
            //相手を親オブジェクトにしてついていく
            transform.SetParent(collision.transform);
            //当たり判定を無効
            GetComponent<BoxCollider2D>().enabled = false;
            //物理処理を無効
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //GetComponent<Rigidbody2D>().simulated = false;
        }

        Destroy(gameObject, 0.1f); //時間差で消滅
    }
}
