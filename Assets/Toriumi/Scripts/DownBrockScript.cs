using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBrockScript : MonoBehaviour
{
    [SerializeField]
    float speed = 50f;      // ブロックが落ちる速度

    [SerializeField]
    float basepos = -10f;     // ブロックが落ちる条件の位置(設置位置によって変えた方がよいが『-位置』でほとんど事足りると思われる)

    [SerializeField]
    float downpos = -100f;  // ブロックが落ちる終点位置

    private bool Hit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Hit)
        {
            Transform down = this.transform;
            Vector2 pos = down.position;
            if(pos.y >= basepos)
            {
                // ブロックが落ちる(今の位置、落ちる終点位置、落ちる速度)
                pos.y = Mathf.MoveTowards(pos.y, downpos, Time.deltaTime * speed);
                down.position = pos;
            }
        }
    }

    // トリガー判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // トリガーに引っかかったら
        if(collision.gameObject.tag == "Player")
        {
            Hit = true;
        }
    }

    // おまけ
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        this.gameObject.SetActive(false);
    //    }
    //
    //}
}

/* スクリプト内容 */
// プレイヤータグが付いているオブジェクトがトリガー範囲内に入ったら
// ブロックが設定したスピードで落ちてくる
// SlideBrockHitのトリガー判定の範囲次第でとんでもないところから飛ばすことが可能
// おまけは当たった瞬間にオブジェクトを消すもの
