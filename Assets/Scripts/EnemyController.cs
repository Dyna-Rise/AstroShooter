using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int hp = 10;
    public float speed = 1.0f; //追跡スピード
    public float searchDistance = 4.0f; //探索距離

    //移動に必要
    float axisH,axisV; //横軸、縦軸の値
    Rigidbody2D rbody;

    bool isActive; //追跡モードのフラグ

    //セーブデータの管理用
    public int arrangeId; //識別ID

    GameObject player; //プレイヤー情報

    // Start is called before the first frame update
    void Start()
    {
        //Playerオブジェクトの取得
        player = GameObject.FindGameObjectWithTag("Player");
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーがいない時は何もしない
        if (player == null) return;

        //追跡モードだった場合
        if (isActive)
        {
            //プレイヤーの方角をX成分、Y成分で取得
            float dirX = player.transform.position.x - transform.position.x;
            float dirY = player.transform.position.y - transform.position.y;

            //プレイヤーとの差分をもとに角度（ラジアン）を算出
            float angle = Mathf.Atan2(dirY,dirX);

            //長辺を1として数値をおさえた際のXとYを求め直す
            axisH = Mathf.Cos(angle); //X方向
            axisV = Mathf.Sin(angle); //Y方向

            SearchPlayer();
        }
        else
        {
            //索敵
            SearchPlayer();
        }
    }

    void FixedUpdate()
    {
        if(isActive && hp > 0)
        //Updateで求めたX成分、Y成分の値を使って実際に動かす
        rbody.velocity = new Vector2(axisH, axisV) * speed;
    }

    //プレイヤーを索敵（距離をはかる）
    void SearchPlayer()
    {
        //エネミーとプレイヤー　2者間の距離
        float distance = Vector2.Distance(transform.position,player.transform.position);

        //基準の距離より近ければ
        if(distance <= searchDistance)
        {
            isActive = true; //追跡モード
        }
        else
        {
            isActive = false;　//追跡モードOFF
            //Debug.Log("追跡OFF");
            rbody.velocity = Vector2.zero; //移動をストップ
            //Vectro2.zero → new Vector2(0,0)
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //弾丸をくらったらダメージ
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hp--;

            if(hp <= 0) //死亡
            {
                //死亡演出
                GetComponent<CapsuleCollider2D>().enabled = false;
                rbody.velocity = Vector2.zero;
                GetComponent<Animator>().SetTrigger("death");          
            }
        }
    }

    public void EnemyDestroy()
    {
        Destroy(gameObject);
    }
}
