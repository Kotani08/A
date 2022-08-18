using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Blood : MonoBehaviour
{
    /// <summary>
    /// �I�u�W�F�N�g�v�[��
    /// </summary>
    public ObjectPool<GameObject> objectPool;

    /// <summary>
    /// �߂������W
    /// </summary>
    public Transform poolPos;

    /// <summary>
    /// �͂̍ŏ��ƍő�
    /// </summary>
    public Vector2 power;

    [SerializeField,Header("������RigidBody2d")]
    private Rigidbody2D rb;

    [SerializeField, Header("�ő�̐ԐF")]
    private float maxRed;

    [SerializeField, Header("�ŏ��̐ԐF")]
    private float minRed;

    /// <summary>
    /// �\�����ꂽ��
    /// </summary>
    private void OnEnable()
    {
        BloodMovement();
    }

    /// <summary>
    /// ��ʊO�ɏo����
    /// </summary>
    void OnBecameInvisible()
    {
        objectPool.Release(gameObject);
    }

    /// <summary>
    /// ���̋���
    /// �v�[��������o�����Ȃǂ����o���Ɠ����ɌĂяo�����
    /// </summary>
    public void BloodMovement()
    {
        //selfSprite.color = new Color(Random.Range(minRed, maxRed), 0, 0, 1);

        int rn = Random.Range(0, 2);
        gameObject.transform.localScale = new Vector3(Random.Range(1, 3), Random.Range(1, 3),Random.Range(1, 3));
        Vector2 rndVec;

        if (rn == 0)
        {
            rndVec = new Vector2(Random.Range(-power.y, power.y), Random.Range(-power.x, power.x) );
        }
        else
        {
            rndVec = new Vector2(Random.Range(-power.x, power.x), Random.Range(2000, power.y));
        }

        rb.AddForce(rndVec);
    }

}
