using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DemoPlayerControl : MonoBehaviour
{
    //Playerをaddforthで動かす
    #region 移動関連
    private Vector3 movingDirecion;
    private Vector3 movingVelocity;
    [SerializeField]
    private float speed = 1000f;
    [SerializeField]
    private float JumpPower = 1000f;
    [SerializeField]
    private int jumpMax=2;
    private int jumpCount=0;
    #endregion

    // PlayerのRigidbody取得
    [SerializeField]
    private Rigidbody2D player;

    private bool DeathFlag=false;

    private Animator anim = null;

    //Manager呼び出し
    [SerializeField]
    SoundManager soundManager;
    //ダメージ音
    [SerializeField]
    AudioClip DamageSE;
    //クリア時の音
    [SerializeField]
    AudioClip ClearSE;

    private bool stopMove=false;

    [SerializeField]
    FadeManager fadeManager;


    void Start()
    {
        JumpReset();
        anim = GetComponent<Animator>();
        GameObject set = GameObject.Find("GameManager");
        soundManager = set.GetComponent<SoundManager>();
        fadeManager = set.GetComponent<FadeManager>();

    }
    void Update()
    {
      if(stopMove == false){Playerwalk();}
    }

    #region 移動関連
    public void Playerwalk()
    {
      #region 移動関連
      float x = Input.GetAxisRaw("Horizontal");
      float z = Input.GetAxisRaw ("Vertical");

      #region 後追加の走る処理
    if (x > 0) 
    {
      transform.localScale = new Vector3(1, 1, 1);
        anim.SetBool("Run", true);
    }
    else if (x < 0) 
    {
      transform.localScale = new Vector3(-1, 1, 1);
        anim.SetBool("Run", true);
    }
    else
    {
        anim.SetBool("Run", false);
    }
    #endregion

      movingDirecion = new Vector2(x,z);
	  movingDirecion.Normalize();
	  movingVelocity = movingDirecion * speed;
      if (Input.GetKeyDown(KeyCode.Space)) {Jump();}
      if (Input.GetKeyDown(KeyCode.K)){Suicide();}
      //new input systemで使ってたやつ
      if (Gamepad.current != null)
      {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame){Jump();}
        if(Gamepad.current.rightShoulder.wasPressedThisFrame){Suicide();}
      }
       
      player.velocity = new Vector2(movingVelocity.x, player.velocity.y);
      #endregion
    }
    #endregion

    #region ジャンプ
    private void Jump()
    {
      if(jumpCount >=0){
        jumpCount-=1;
      player.AddForce(transform.up * JumpPower, ForceMode2D.Impulse);
      }
    }
    private void JumpReset()
    {
        jumpCount=jumpMax-1;
    }
    #endregion

    #region 死亡時の処理
    public void Suicide()
    {
      #region アニメーション,SEなど演出の処理
      soundManager.PlaySe(DamageSE);
      transform.localScale = new Vector3(1, 1, 1);
      anim.SetBool("Run", false);
      Destroy(anim);
      this.transform.Rotate(new Vector3(0f,0f,90f));
      this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y-10,this.transform.position.z);
      #endregion

      //足場にしたいのでStaticに変更
      this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
      //DemoPlayerControlを消して動かないようにする
      Destroy(this.GetComponent<DemoPlayerControl>());
      //プレイヤー用にtagを地面と同じにする
      this.tag = "Ground";
      DeathFlag = true;
      //これでシングルトンにしたBloodManagerで血が出る
      BloodManager.Instance.RunBlood();
    }
    #endregion

    #region ゴール時の処理
    private void Gool()
    {
      soundManager.PlaySe(ClearSE);
      //ゴール後、入力を受け付けなくする
      fadeManager.SetfadeStopFlag(false);
      stopMove = true;
    }
    #endregion

    #region ゴールと障害物の処理
    private void OnTriggerEnter2D(Collider2D other)
    {
        //障害物とぶつかった時
        switch(other.tag)
        {
          case "Goal":
          Gool();
          break;
          case "Out":
          Suicide();
          break;
          default:
          Debug.Log("現在未設定のタグです");
          break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //ジャンプ回数を地面に振れた時に回復させる用
        if(other.gameObject.tag == "Ground")
        {
          JumpReset();
          return;
        }
    }
    #endregion
}
