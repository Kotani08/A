using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BloodManager : SingletonMonoBehaviour<BloodManager>
{
    #region �v�[��1
    /// <summary>
    /// ObjectPool
    /// </summary>
    public ObjectPool<GameObject> bloodObjectPool;

    [SerializeField, Header("�����̃I�u�W�F�N�g������")]
    private int defaultCapacity = 20;

    [SerializeField, Header("�v�[���̍ő�T�C�Y")]
    private int poolMaxSize = 100;

    [SerializeField, Header("�v�[������I�u�W�F�N�g")]
    private GameObject bulletObj;

    [SerializeField, Header("�I�u�W�F�N�g���o���ʒu")]
    private Transform playerTransform;

    [SerializeField, Header("�ԋp���ꂽ�ۂ̈ʒu")]
    private Transform poolPosition;

    [SerializeField, Header("������ő�̗�")]
    private Vector2 pw;

    #endregion

    #region �v�[��2
    /// <summary>
    /// ObjectPool
    /// </summary>
    public ObjectPool<GameObject> bloodObjectPool2;

    [SerializeField, Header("�����̃I�u�W�F�N�g������")]
    private int defaultCapacity2 = 20;

    [SerializeField, Header("�v�[���̍ő�T�C�Y")]
    private int poolMaxSize2 = 100;

    [SerializeField, Header("�v�[������I�u�W�F�N�g")]
    private GameObject bulletObj2;

    #endregion


    /// <summary>
    /// ���ŏo�����̗�
    /// </summary>
    public int bloodAmount = 30;

    private void Start()
    {
        // �I�u�W�F�N�g�v�[�����쐬
        bloodObjectPool = new ObjectPool<GameObject>(
        OnCreatePoolObject,
        OnTakeFromPool,
        OnReturnedToPool,
        OnDestroyPoolObject,
        true,
        defaultCapacity,
        poolMaxSize);

        bloodObjectPool2 = new ObjectPool<GameObject>(
        OnCreatePoolObject2,
        OnTakeFromPool2,
        OnReturnedToPool2,
        OnDestroyPoolObject2,
        true,
        defaultCapacity2,
        poolMaxSize2);

    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <returns>���������I�u�W�F�N�g</returns>
    private GameObject OnCreatePoolObject()
    {
        // ��������
        GameObject bullet = Instantiate(bulletObj, poolPosition.position, Quaternion.identity);
        //PooledObject�X�N���v�g��get
        Blood pooled = bullet.GetComponent<Blood>();

        //�v�[�������I�u�W�F�N�g�̕ϐ��ɏ�����
        pooled.objectPool = bloodObjectPool;
        pooled.poolPos = poolPosition;
        pooled.power = pw;

        pooled.BloodMovement();

        return bullet;
    }

    /// <summary>
    /// �v�[��������o������
    /// </summary>
    private void OnTakeFromPool(GameObject obj)
    {
        //�v�[��������o���ꂽ�猌���v���C���[�̈ʒu��
        obj.transform.position = DemoPlayerManager.Instance.playerPos.position;
        obj.SetActive(true);
        obj.GetComponent<Blood>().BloodMovement();
    }

    /// <summary>
    /// �v�[���ɖ߂�����
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
    /// �v�[���̋��e�ʂ𒴂����ꍇ�̔j������
    /// </summary>
    private void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }


    ////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// ��������2
    /// </summary>
    /// <returns>���������I�u�W�F�N�g</returns>
    private GameObject OnCreatePoolObject2()
    {
        // ��������
        GameObject bullet = Instantiate(bulletObj, poolPosition.position, Quaternion.identity);
        //PooledObject�X�N���v�g��get
        FakeBlood pooled = bullet.GetComponent<FakeBlood>();

        //�v�[�������I�u�W�F�N�g�̕ϐ��ɏ�����
        pooled.objectPool = bloodObjectPool2;

        return bullet;
    }

    /// <summary>
    /// �v�[��������o������
    /// </summary>
    private void OnTakeFromPool2(GameObject obj)
    {
        //�v�[��������o���ꂽ�猌���v���C���[�̈ʒu��
        obj.transform.position = obj.transform.position;
        obj.SetActive(true);
    }

    /// <summary>
    /// �v�[���ɖ߂�����
    /// </summary>
    private void OnReturnedToPool2(GameObject obj)
    {
        if (obj != null)
        {
            obj.transform.position = poolPosition.transform.position;
            obj.SetActive(false);
        }
    }

    /// <summary>
    /// �v�[���̋��e�ʂ𒴂����ꍇ�̔j������
    /// </summary>
    private void OnDestroyPoolObject2(GameObject obj)
    {
        Destroy(obj);
    }



    /// <summary>
    /// ���������o��
    /// </summary>
    public void RunBlood()
    {
        for (int i = 0; i < bloodAmount; i++)
        {
            bloodObjectPool.Get();
        }
    }

    public void GetFakeBlood()
    {
        Debug.Log("EE");
        bloodObjectPool2.Get();
    }
}
