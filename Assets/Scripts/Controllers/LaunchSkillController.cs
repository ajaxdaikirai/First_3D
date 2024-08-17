using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSkillController : MonoBehaviour
{
    //�߻��� ��ġ
    Vector3 _startPos;

    //����
    Vector3 _dir;

    //��Ÿ�
    float _distance;

    //����ü �ӵ�
    float _speed;

    //���ط�
    int _damage;

    //��� ���̾�
    int[] _layers;


    public void SetSkillStatus(Vector3 startPos, Vector3 dir, float distance, float speed, int damage , int[] layers)
    {
        _startPos = startPos;
         _dir = dir;
        _distance = distance;
        _speed = speed;
        _damage = damage;
        _layers = layers;
    }

    void Start()
    {
    }

    void Update()
    {
        if(_distance < (_startPos - transform.position).magnitude)
        {
            Cleer();
            Managers.Resource.Destroy(gameObject);
        }
    }

    public void StartLaunch()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(_dir);
        gameObject.GetComponent<Rigidbody>().AddForce(_dir * _speed, ForceMode.VelocityChange);
    }

    void OnTriggerEnter(Collider other)
    {
        //��� ���̾ �ƴϸ� ����
        int targetLayer = other.gameObject.layer;
        bool canAttack = false;
        foreach (int layer in _layers)
        {
            if(targetLayer == layer)
            {
                canAttack = true;
                break;
            }
        }
        if (canAttack == false) return;

        if (other.gameObject.GetComponent<Stat>().OnAttacked(_damage))
        {
            Cleer();
 
            Managers.Resource.Destroy(gameObject);
        }
    }

    void Cleer()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
