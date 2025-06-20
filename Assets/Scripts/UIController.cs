using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI bulletText; //弾丸数パネル
    public TextMeshProUGUI goldKeyText; //金の鍵パネル
    public TextMeshProUGUI silverKeyText; //銀の鍵パネル
    public GameObject lightPanel; //ライトパネル
    public GameObject[] lifes; //Life1～Life5

    public GameObject EkeyPanel; //調べるパネル

    //UIController.cs 上における アイテムの残数や有無
    int hasBullet;
    int hasKeyG;
    int hasKeyS;
    int hasLife;
    bool hasLight;

    bool investigate; //調べるフラグ

    public GameObject gameOverPanel; //ゲームオーバー時に出すパネル


    // Start is called before the first frame update
    void Start()
    {
        UIDisplay(); //弾丸と鍵、ライトの初期表示をさせる
        hasLife = PlayerController.hp; //一度HPの数をあわせる
        LifeDisplay(); //Lifeの表示
        gameOverPanel.SetActive(false); //ゲームオーバーパネルを隠す
        EkeyPanel.SetActive(false);//調べるパネルは隠す
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.gameState == "gameover")
        {
            gameOverPanel.SetActive(true); //ゲームオーバーと表示
        }

        //もしもUIControllerが把握していた数と実際のstaticに変化が生じたら
        //UIを更新
        if(hasBullet != GameController.hasBullet)
        {
            hasBullet = GameController.hasBullet;
            bulletText.text = hasBullet.ToString();
        }
        if (hasKeyG != GameController.hasGoldKey)
        {
            hasKeyG = GameController.hasGoldKey;
            goldKeyText.text = hasKeyG.ToString();
        }
        if (hasKeyS != GameController.hasSilverKey)
        {
            hasKeyS = GameController.hasSilverKey;
            silverKeyText.text = hasKeyS.ToString();
        }
        if (!hasLight) //一度trueになればライトUIの切替は考えなくて良い
        {
            if (LightController.getLight) //staticがtrueになるタイミングを見る
            {
                hasLight = true;
                lightPanel.SetActive(true);
            }
        }
        if(hasLife != PlayerController.hp)
        {
            hasLife = PlayerController.hp;
            LifeDisplay(); //表示のリセットと再表示
        }

        if (investigate != GameController.investigate) //調べるパネルON
        {
            investigate = GameController.investigate;
            EkeyPanel.SetActive(investigate);
        }
    }

    void UIDisplay()
    {
        //static変数とプログラムが把握している数を一致させて、text欄に反映
        hasBullet = GameController.hasBullet; 
        bulletText.text = hasBullet.ToString(); 
        hasKeyG = GameController.hasGoldKey; 
        goldKeyText.text = hasKeyG.ToString(); 
        hasKeyS = GameController.hasSilverKey; 
        silverKeyText.text = hasKeyS.ToString();
        hasLight = LightController.getLight;
        lightPanel.SetActive(hasLight);
    }

    //Lifeを順番に全非表示
    void LifeReset()
    {
        for(int i = 0; i < lifes.Length; i++)
        {
            lifes[i].SetActive(false);
        }
    }

    //hasLifeで把握しているhp数だけハートを表示する
    void LifeDisplay()
    {
        LifeReset(); //Lifeは一度全消し

        for (int i = 0; i < hasLife; i++)
        {
            lifes[i].SetActive(true);
        }
    }

    //タイトルに移動
    public void toTitle()
    {
        SceneManager.LoadScene("Title");
    }

    //セーブした地点からやり直す
    public void Retry()
    {
        //BGMをストップ
        SoundController.soundController.PlayBgm(BGMType.None);

        if(PlayerPrefs.GetInt("Life") <= 0)
        PlayerPrefs.SetInt("Life", 5);

        SaveSystem.LoadGame();
    }
}
