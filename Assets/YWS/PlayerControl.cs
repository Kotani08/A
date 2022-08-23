using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerControl : MonoBehaviour
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
    private int jumpMax = 2;
    private int jumpCount = 0;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Vector2 groundCheckSize;
    private bool grounded;
    private bool canJump;
    [Header("For WallSliding")]
    [SerializeField] float wallSlideSpeed = 0;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Vector2 wallCheckSize;
    private bool isTouchingWall;
    private bool isWallSliding;
    [Header("For WallJumping")]
    [SerializeField] float WallJumpForce = 18f;
    [SerializeField] float wallJumpDirection = -1f;
    [SerializeField] Vector2 wallJumpAngele;
    #endregion

    // PlayerのRigidbody取得
    [SerializeField]
    private Rigidbody2D player;

    private bool DeathFlag = false;
    private bool JumpFlag = false;

    void Start()
    {
      //RandomRebirth();
      JumpReset();
    }

    void Update()
    {
        Playerwalk();
        CheckWorld();
    }

    private void FixedUpdate()
    {
      WallSlide();
      WallJump();  
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
      if (Input.GetKeyDown(KeyCode.K)) {Suicide();}
      //new input systemで使ってたやつ
      /*if (Gamepad.current != null)
      {
        if (Gamepad.current.buttonSouth.wasPressedThisFrame){Jump();}
      }*/
       
      player.velocity = new Vector2(movingVelocity.x, player.velocity.y);
      //ジャンプ回数を地面に振れた時に回復させる用
      if (grounded)
      {
          JumpReset();
      }
      #endregion
    }
    #endregion

    #region ジャンプ
    private void Jump()
    {
      if(jumpCount > 0)
      {
        jumpCount -= 1;
        player.AddForce(transform.up * JumpPower, ForceMode2D.Impulse);
        JumpFlag = true;
      }
    }

    private void JumpReset()
    {
        jumpCount = jumpMax;
        JumpFlag = false;
    }
    #endregion

    #region 死ぬときの処理
    public void Suicide()
    {
        //動かないようにStaticに変更
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        //PlayerControlを消して処理を軽減
        Destroy(this.GetComponent<PlayerControl>());
        //tagを地面と同じにする
        this.tag = "Ground";
        //接触チェックのためにレイヤーを壁・地面判定できるレイヤーに変更する
        this.gameObject.layer = 6;
        DeathFlag = true;
    }
    #endregion

    #region ぶつかった時の処理
    void CheckWorld()
    {
        //地面に着いてるかチェック
        grounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        //壁に接触してるかチェック
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //障害物とぶつかった時用
        if(other.tag == "Goal") {Debug.Log("Goal");}
        //死ぬのは「デス」のタグが付いてる障害物とぶつかった時だけ
        else if (other.tag == "Death") {Suicide();}
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        //ジャンプ回数を地面に振れた時に回復させる用
        if(other.gameObject.tag == "Ground")
        {
            if (this.transform.position.y > other.gameObject.transform.position.y)
            {
              JumpReset();
              return;
            }
            else if (JumpFlag == true)
            {
              Debug.Log(movingDirecion.x);
            }
        }
    }*/

    #region 壁接触関係
    //壁に貼り付いて滑り落ちるみたいな奴
    void WallSlide()
    {
        if (isTouchingWall && !grounded && player.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            player.velocity = new Vector2(player.velocity.x, wallSlideSpeed);
        }
    }

    //壁キック
    void WallJump()
    {
        if ((isWallSliding || isTouchingWall) && canJump)
        {
            player.AddForce(new Vector2(WallJumpForce * wallJumpDirection * wallJumpAngele.x, WallJumpForce * wallJumpAngele.y), ForceMode2D.Impulse);
            canJump = false;
        }
    }
    #endregion
    #endregion

    //多分使わない復活時にステータス変わる奴
    private void RandomRebirth()
    {
      speed = Random.Range(0,100);
      JumpPower = Random.Range(0,70);
    }

    //これは無視していいやつ（インスペクターでこのスクリプト付けてる奴をクリックした状態でシーン画面上になんか描画される奴）
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);

        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
    }
}