using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FakeBlood : MonoBehaviour
{
    /// <summary>
    /// �I�u�W�F�N�g�v�[��
    /// </summary>
    public ObjectPool<GameObject> objectPool;

    [SerializeField, Header("�҂���")]
    private float waitTime = 3f;

    private Coroutine holdCoroutine;

    private void OnEnable()
    {
        holdCoroutine = StartCoroutine("WaitAndRerease");
    }

    /// <summary>
    /// Get���ꂽ���莞�Ԍ�Ƀv�[���ɖ߂�
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitAndRerease()
    {
        yield return new WaitForSeconds(waitTime);

        objectPool.Release(gameObject);
        StopCoroutine(holdCoroutine);
    }
}
