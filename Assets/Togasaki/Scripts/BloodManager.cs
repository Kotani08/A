using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BloodManager : SingletonMonoBehaviour<BloodManager>
{
    #region プール1
    /// <summary>
    /// ObjectPool
    /// </summary>
    public ObjectPool<GameObject> bloodObjectPool;

    [SerializeField, Header("初期のオブジェクト生成量")]
    private int defaultCapacity = 20;

    [SerializeField, Header("プールの最大サイズ")]
    private int poolMaxSize = 100;

    [SerializeField, Header("プールするオブジェクト")]
    private GameObject bulletObj;

    [SerializeField, Header("オブジェクトを出す位置")]
    private Transform playerTransform;

    [SerializeField, Header("返却された際の位置")]
    private Transform poolPosition;

    [SerializeField, Header("加える最大の力")]
    private Vector2 pw;

    #endregion


    /// <summary>
    /// 一回で出す血の量
    /// </summary>
    public int bloodAmount = 30;

    private void Start()
    {
        // オブジェクトプールを作成
        bloodObjectPool = new ObjectPool<GameObject>(
        OnCreatePoolObject,
        OnTakeFromPool,
        OnReturnedToPool,
        OnDestroyPoolObject,
        true,
        defaultCapacity,
        poolMaxSize);

    }

    /// <summary>
    /// 生成処理
    /// </summary>
    /// <returns>生成したオブジェクト</returns>
    private GameObject OnCreatePoolObject()
    {
        // 生成処理
        GameObject bullet = Instantiate(bulletObj, poolPosition.position, Quaternion.identity);
        //PooledObjectスクリプトをget
        Blood pooled = bullet.GetComponent<Blood>();

        //プールしたオブジェクトの変数に情報を代入
        pooled.objectPool = bloodObjectPool;
        pooled.poolPos = poolPosition;
        pooled.power = pw;

        pooled.BloodMovement();

        return bullet;
    }

    /// <summary>
    /// プールから取り出す処理
    /// </summary>
    private void OnTakeFromPool(GameObject obj)
    {
        //プールから取り出されたら血をプレイヤーの位置へ
        obj.transform.position = DemoPlayerManager.Instance.playerPos.position;
        obj.SetActive(true);
        obj.GetComponent<Blood>().BloodMovement();
    }

    /// <summary>
    /// プールに戻す処理
    /// </summary>
    private void OnReturnedToPool(GameObject obj)
    {
        if(obj != null)
        {
            obj.transform.position = poolPosition.transform.position;
            obj.SetActive(false);
        }
    }

    /// <summary>
    /// プールの許容量を超えた場合の破棄処理
    /// </summary>
    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    /// <summary>
    /// 動く血を出す
    /// </summary>
    public void RunBlood()
    {
        for (int i = 0; i < bloodAmount; i++)
        {
            bloodObjectPool.Get();
        }

    }
}
