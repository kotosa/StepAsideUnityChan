using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private float forwardForce = 800.0f;
    private float turnForce = 500.0f;
    private float movableRange = 3.4f;  // 左右に移動できる範囲
    private float upForce = 500.0f; // ジャンプするための力
    private float coefficient = 0.95f;  // 動きを減速させる係数
    private bool isEnd = false; // ゲーム終了判定
    private GameObject stateText;   // ゲーム終了時に表示するテキスト
    private GameObject scoreText;   // スコアを表示するテキスト
    private int score = 0;
    private bool isLButtonDown = false; // 左ボタン押下の判定
    private bool isRButtonDown = false; // 右ボタン押下の判定

	// Use this for initialization
	void Start () {
        this.myAnimator = GetComponent<Animator>();
        this.myAnimator.SetFloat("Speed", 1);
        this.myRigidbody = GetComponent<Rigidbody>();
        this.stateText = GameObject.Find("GameResultText");
        this.scoreText = GameObject.Find("ScoreText");
	}
	
	// Update is called once per frame
	void Update () {

        // ゲーム終了ならUnityちゃんの動きを減衰する
        if(this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

        // スコアを表示する
        this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";

        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

        if((Input.GetKey(KeyCode.LeftArrow) || (this.isLButtonDown)) && (-this.movableRange < this.transform.position.x))
        {
            // 左に移動できる場合は左に移動
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
//            this.isLButtonDown = false;
        }
        else if(((Input.GetKey(KeyCode.RightArrow) || (this.isRButtonDown)) && (this.transform.position.x < this.movableRange)))
        {
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
//            this.isRButtonDown = false;
        }

        if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        if(Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 障害物に追突した場合
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        // ゴール地点に到着した場合
        if(other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "GAME CLEAR";
        }

        // コインを獲得した場合
        if(other.gameObject.tag == "CoinTag")
        {
            GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);

            // スコアを加算する
            this.score += 10;

            //// スコアを表示する
            //this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";

        }
    }

    // ジャンプボタンを押した場合の処理
    public void GetMyJumpButtonDown()
    {
        if(this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
    }

    // 左ボタンを押し続けた場合の処理
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    // 右ボタン
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }

}
