using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float deleteTime = 3.0f;    //削除する時間指定

    public float pushPower = 5f;       // 受ける反発力

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, deleteTime);    //削除設定
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // 弾の方向から反発ベクトルを計算
            //Collision2Dクラスのcontacts[0].point
            //→接触点
            //Vector2 hitPoint = collision.contacts[0].point;

            Vector2 direction = (transform.position - collision.transform.position).normalized;

            GetComponent<Rigidbody2D>().AddForce(direction * pushPower, ForceMode2D.Impulse);
        }
        else
        {
            Destroy(gameObject);   //何かに接触したら消す
        }
    }
}
