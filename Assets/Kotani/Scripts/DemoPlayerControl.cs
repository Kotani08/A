using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        JumpReset();
    }
    void Update()
    {
        Playerwalk();
    }

    #region 移動関連
    public void Playerwalk()
    {
      #region 移動関連
      float x = Input.GetAxisRaw("Horizontal");
      float z = Input.GetAxisRaw ("Vertical");
      movingDirecion = new Vector2(x,z);
	  movingDirecion.Normalize();
	  movingVelocity = movingDirecion * speed;
      if (Input.GetKeyDown(KeyCode.Space)) {Jump();}
      if (Input.GetKeyDown(KeyCode.K)){Suicide();}
      //new input systemで使ってたやつ
      /*if (Gamepad.current != null)
      {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame){Jump();}
      }*/
       
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

    #region 死ぬときの処理
    public void Suicide()
    {
        //動かないようにStaticに変更
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        //DemoPlayerControlを消して処理を軽減
        Destroy(this.GetComponent<DemoPlayerControl>());
        //tagを地面と同じにする
        this.tag = "Ground";

        //これでシングルトンにしたBloodManagerにて血が出ている
        BloodManager.Instance.RunBlood();


        DeathFlag = true;

    }
    #endregion

    #region ぶつかった時の処理
    private void OnTriggerEnter2D(Collider2D other)
    {
        //障害物とぶつかった時用
        if(other.tag == "Goal"){Suicide();}
        else{Debug.Log("Goal");}
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
