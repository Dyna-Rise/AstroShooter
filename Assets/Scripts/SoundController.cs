using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BGMのタイプ
public enum BGMType
{
    None, //なし
    Title,　 //タイトル
    InGame, //ゲーム中
    InBoss //ボス戦
}

//SEのタイプ
public enum SEType
{
    GameClear, //ゲームクリア
    GameOver, //ゲームオーバー
    Shoot //発砲時
}

public class SoundController : MonoBehaviour
{
    //各BGM
    public AudioClip bgmInTitle;
    public AudioClip bgmInGame;
    public AudioClip bgmInInBoss;

    //各SE
    public AudioClip meGameClear;
    public AudioClip meGameOver;
    public AudioClip seShoot;

    public static SoundController soundController; //自分自身(ゲーム起動時から最初のSoundController)

    public static BGMType playingBGM = BGMType.None; //再生中のBGM情報

    void Awake()
    {
        if (soundController == null)
        {
            soundController = this; //static変数にはじめてシーンのSoundControllerが代入させる
            //シーン切替先に自分自身を持っていく
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //BGMを変更する
    public void PlayBgm(BGMType type)
    {
        //メソッドの引数に与えたものが
        //現在の曲情報と異なれば
        if (type != playingBGM)
        {
            playingBGM = type;　//BGM変更
            AudioSource audio = GetComponent<AudioSource>();

            if (type == BGMType.Title)
            {
                audio.clip = bgmInTitle;
            }
            else if (type == BGMType.InGame)
            {
                audio.clip = bgmInGame;
            }
            else if (type == BGMType.InBoss)
            {
                audio.clip = bgmInInBoss;
            }

            //AudioSourceのPlayメソッド
            audio.Play();
        }
    }

    public void StopBgm()
    {
        //曲を止める
        GetComponent<AudioSource>().Stop();
        playingBGM = BGMType.None;
    }

    //BGMを変更する
    public void SEPlay(SEType type)
    {
        //メソッドの引数に与えたものが
        //現在の曲情報と異なれば
        if (type == SEType.GameClear)
        {
            GetComponent<AudioSource>().PlayOneShot(meGameClear);
        }
        else if (type == SEType.GameOver)
        {
            GetComponent<AudioSource>().PlayOneShot(meGameOver);
        }
        else if (type == SEType.Shoot)
        {
            GetComponent<AudioSource>().PlayOneShot(seShoot);
        }
    }
}


