using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickHandler : MonoBehaviour,
    IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Action<Vector3> OnDragHandler = null;
    public Action OnUpHandler = null;

    //������
    RectTransform _handle;
    //���̽�ƽ
    RectTransform _joyStick;
    //���̽�ƽ ������
    float _radius;
    //�ڵ� ó�� ��ġ
    Vector3 _handleFirstPos;

    public RectTransform Handle
    {
        set
        {
            _handle = value;
        }
    }

    private void Start()
    {
        _joyStick = Util.GetOrAddComponent<RectTransform>(gameObject);
        _handleFirstPos = _handle.position;
        _radius = _joyStick.sizeDelta.x * 0.5f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Ŭ���� �κк��� �巡�װ� ����ǵ�����
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //���� ����� ���� ����2�� ����3�� ��ȯ
        Vector3 downSpot = eventData.position;
        //������ ����
        Vector3 dir = (downSpot - _handleFirstPos).normalized;
        //�Ÿ��� ����
        float dis = (downSpot - _handleFirstPos).magnitude;
        //ĳ���� �̵��� ���� ����
        Vector3 charDir = new Vector3(dir.x, 0.0f, dir.y);

        if (OnDragHandler != null)
        {
            OnDragHandler.Invoke(charDir);
        }

        if (dis > _radius)
        {
            _handle.position = _handleFirstPos + dir * _radius;
            
        }
        else
        {
            _handle.position = _handleFirstPos + dir * dis;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _handle.position = _handleFirstPos;
        if(OnUpHandler != null)
        {
            OnUpHandler.Invoke();
        }
    }
}
