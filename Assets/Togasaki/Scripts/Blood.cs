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

    [SerializeField, Header("������sprite")]
    private SpriteRenderer selfSprite;

    [SerializeField, Header("�ő�̐ԐF")]
    private float maxRed;

    [SerializeField, Header("�ŏ��̐ԐF")]
    private float minRed;

    [SerializeField, Header("�����Ȃ���")]
    private GameObject bloodOrnament;

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        BloodManager.Instance.GetFakeBlood();
    }

    /// <summary>
    /// ���̋���
    /// �v�[��������o�����Ȃǂ����o���Ɠ����ɌĂяo�����
    /// </summary>
    public void BloodMovement()
    {
        selfSprite.color = new Color(Random.Range(minRed, maxRed), 0, 0, 1);
        Vector2 rndVec = new Vector2(Random.Range(-power.x, power.x), Random.Range(2000, power.y));
        rb.AddForce(rndVec);
    }

}
