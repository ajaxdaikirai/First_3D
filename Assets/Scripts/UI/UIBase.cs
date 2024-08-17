
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIBase : MonoBehaviour
{
    //���ε��� UI������Ʈ�� �����ϴ� ��ųʸ�
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    private void Start()
    {
        Init();
    }

    //UI������Ʈ�� ���ε���(��ųʸ��� �����Ͽ� UI������Ʈ�� Get�޼���� �������� �����ϰ� ��)
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for(int i = 0; i < names.Length; i++)
        {
            if(typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            }

            if(objects[i] == null)
            {
                if ($"{names[i]}" == "UIHpBar" || $"{names[i]}" == "Panel_GameOver")
                    continue;
                Debug.Log($"Failed to bind {names[i]}");
            }
        }

    }

    //��ųʸ�[index]�� ����� UI������Ʈ�� ����
    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        //value�� ���� ���false, ���� ��� value�� out���� ��ȯ
        if (_objects.TryGetValue(typeof(T), out objects) == false) return null;

        return objects[index] as T;

    }

    protected GameObject GetGameObject(int index) { return Get<GameObject>(index); }
    protected Text GetText(int index) { return Get<Text>(index); }
    protected Button GetButton(int index) { return Get<Button>(index); }
    protected Image GetImage(int index) { return Get<Image>(index); }

    public void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        //�ش� UI�� EventHandler ������Ʈ �߰�
        UIEventHandler evt = Util.GetOrAddComponent<UIEventHandler>(go);

        //�߰��� EventHandler�� �׼��� �߰�
        //�̺�Ʈ�� �ι� �߰��Ǿ� �ι� ����Ǵ� ���� �����ϱ�����
        //�̺�Ʈ�� �ѹ� ���� �߰�����
        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                //�巡�� ó��
                evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; });
                break;
        }
    }

}
