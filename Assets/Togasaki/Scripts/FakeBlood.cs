using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FakeBlood : MonoBehaviour
{
    /// <summary>
    /// オブジェクトプール
    /// </summary>
    public ObjectPool<GameObject> objectPool;

    [SerializeField, Header("待つ時間")]
    private float waitTime = 3f;

    private Coroutine holdCoroutine;

    private void OnEnable()
    {
        holdCoroutine = StartCoroutine("WaitAndRerease");
    }

    /// <summary>
    /// Getされたら一定時間後にプールに戻す
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitAndRerease()
    {
        yield return new WaitForSeconds(waitTime);

        objectPool.Release(gameObject);
        StopCoroutine(holdCoroutine);
    }
}
