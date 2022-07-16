using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBrockScript : MonoBehaviour
{
    [SerializeField]
    float speed = 30f;          // ブロックが横切る速度

    [SerializeField]
    float basepos = -10f;       // ブロックが横切る条件の位置(設置位置によって指定した方がよい)

    [SerializeField]
    float slidepos = -100f;     // ブロックが横切る終点位置

    private bool Hit = false;   // 当たり判定
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Hit)
        {
            Transform slide = this.transform;
            Vector2 pos = slide.position;
            if (pos.x >= basepos)
            {
                // ブロックが落ちる(今の位置、横切る終点位置、横切る速度)
                pos.x = Mathf.MoveTowards(pos.x, slidepos, Time.deltaTime * speed);
                slide.position = pos;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // トリガーに引っかかったら
        if (collision.gameObject.tag == "Player")
        {
            Hit = true;
        }
    }
}

/* スクリプト内容 */
// プレイヤータグが付いているオブジェクトがトリガー範囲内に入ったら
// ブロックが設定したスピードで横切る
// SlideBrockHitのトリガー判定の範囲次第でとんでもないところから飛ばすことが可能
// おまけは当たった瞬間にオブジェクトを消すもの