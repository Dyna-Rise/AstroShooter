using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody; //Rigidbody2D
    Animator anime; //Animator

    float axisH, axisV; //横軸、縦軸

    public float speed = 3.0f; //スピード
    public float angleZ = -90.0f; //角度
    int direction = 0; //アニメの方向番号

    public static int hp = 5; //プレイヤーの体力
    bool inDamage; //ダメージ中フラグ
    bool isMobileInput; //スマホ操作中かどうか

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();

        //下向き
        anime.SetInteger("direction",0);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.gameState != "playing" || inDamage) return;

        //モバイルからの入力がない場合のみ
        if (!isMobileInput)
        {
            //水平方向と垂直方向のキー入力を検知
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }

        VectorAnime(axisH, axisV); //方向アニメを決めるメソッド
    }

    private void FixedUpdate()
    {
        if (GameController.gameState != "playing") return;

        if (inDamage)
        {
            //点滅処理
            float value = Mathf.Sin(Time.time * 50); //valueに正負の波をつくる ※Time.timeはゲームの経過時間
            if (value > 0) GetComponent<SpriteRenderer>().enabled = true; //絵を表示
            else GetComponent<SpriteRenderer>().enabled = false; //絵を非表示

            return;
        }

        rbody.velocity = new Vector2(axisH,axisV).normalized * speed;
    }

    void VectorAnime(float h,float v)
    {
        angleZ = GetAngle();

        //なるべきアニメ番号を一時記録用
        int dir = direction;

        //if (angleZ > -135 && angleZ < -45) dir = 0; //下
        //else if (angleZ >= -45 && angleZ <= 45) dir = 3; //右
        //else if (angleZ > 45 && angleZ < 135) dir = 1; //上
        //else dir = 2; //左

        //左右キーが押されたら
        if (Mathf.Abs(h) >= Mathf.Abs(v))
        {
            if (h > 0) dir = 3;       // 右
            else if (h < 0) dir = 2;  // 左
        }
        else //左右キーが押されなかったら
        {
            if (v > 0) dir = 1;       // 上
            else if (v < 0) dir = 0;  // 下
        }

        //前フレームのdirectionといまあるべきアニメ番号がことなっていなければそのまま
        if (dir != direction)
        {
            direction = dir;
            anime.SetInteger("direction", direction);
        }
    }

    public float GetAngle()
    {
        Vector2 fromPos = transform.position; //現在地
        Vector2 toPos = new Vector2(fromPos.x + axisH, fromPos.y + axisV);

        float angle; //角度情報の受け皿

        if(axisH != 0 || axisV != 0)
        {
            //キーが押された方向と現在地の差分
            float dirX = toPos.x - fromPos.x;
            float dirY = toPos.y - fromPos.y;

            //アークタンジェントに
            //第一：高さ、第二：底辺
            //を与えると角度が出る(ラジアン値）
            float rad = Mathf.Atan2(dirY,dirX);
            //ラジアン値をオイラー値に変換
            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            //前フレームの角度情報そのまま
            angle = angleZ;
        }

        return angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Enemyタグのオブジェクトとぶつかったら
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //ダメージメソッドの発動
            GetDamage(collision.gameObject);
        }
    }

    //ダメージメソッド
    void GetDamage(GameObject enemy)
    {
        hp--; //体力を減らす

        if(hp > 0)
        {
            //動きが止まる
            rbody.velocity = Vector2.zero;

            //ノックバック（方角計算、AddForceで飛ぶ)
            //プレイヤー位置 - 敵の位置 の差分を正規化で整えて * 4.0で方向と力を調整
            Vector3 v = (transform.position - enemy.transform.position).normalized * 4.0f;
            //事前にセッティングした方向と力を頼りに後ろに飛ぶ
            rbody.AddForce(v, ForceMode2D.Impulse);

            //ダメージフラグをONにする(硬直する ※ FixedUpdate に影響が行く）
            inDamage = true;

            //時間差でダメージフラグをOFFに解除
            Invoke("DamageEnd", 0.25f);
        }
        else
        {
            GameOver();
            //ゲームオーバー
            Debug.Log("ゲームオーバー");
        }
    }

    //ダメージフラグを解除するメソッド
    void DamageEnd()
    {
        inDamage = false; //フラグ解除
        //プレイヤーの姿(SpriteRecdererコンポーネント)を明確に有効状態にしておく
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void GameOver()
    {
        GameController.gameState = "gameover";
        //ゲームオーバー演出
        GetComponent<CircleCollider2D>().enabled = false;　//コライダーなし
        rbody.velocity = Vector2.zero; //動きを止める
        rbody.gravityScale = 1; //重力発生
        rbody.AddForce(new Vector2(0, 5), ForceMode2D.Impulse); //上に跳ね上げる
        anime.SetTrigger("death"); //死亡アニメの開始
    }

    //プレイヤーを消滅　※死亡アニメの終わりに起動予定
    public void PlayerDestroy()
    {
        Destroy(gameObject);
    }


}
