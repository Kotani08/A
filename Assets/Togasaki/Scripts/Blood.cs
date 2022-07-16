using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Blood : MonoBehaviour
{
    /// <summary>
    /// オブジェクトプール
    /// </summary>
    public ObjectPool<GameObject> objectPool;

    /// <summary>
    /// 戻される座標
    /// </summary>
    public Transform poolPos;

    /// <summary>
    /// 力の最少と最大
    /// </summary>
    public Vector2 power;

    [SerializeField,Header("自分のRigidBody2d")]
    private Rigidbody2D rb;

    [SerializeField, Header("自分のsprite")]
    private SpriteRenderer selfSprite;

    [SerializeField, Header("最大の赤色")]
    private float maxRed;

    [SerializeField, Header("最小の赤色")]
    private float minRed;

    [SerializeField, Header("動かない血")]
    private GameObject bloodOrnament;

    /// <summary>
    /// 表示されたら
    /// </summary>
    private void OnEnable()
    {
        BloodMovement();
    }

    /// <summary>
    /// 画面外に出たら
    /// </summary>
    void OnBecameInvisible()
    {
        objectPool.Release(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        BloodManager.Instance.GetFakeBlood();
    }

    /// <summary>
    /// 血の挙動
    /// プールから取り出されるなどした出現と同時に呼び出される
    /// </summary>
    public void BloodMovement()
    {
        selfSprite.color = new Color(Random.Range(minRed, maxRed), 0, 0, 1);
        Vector2 rndVec = new Vector2(Random.Range(-power.x, power.x), Random.Range(2000, power.y));
        rb.AddForce(rndVec);
    }

}
