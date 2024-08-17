using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : ManagerBase
{

    //������Ʈ�� �޸𸮿� �ε�
    public T Load<T>(string path) where T : Object
    {
        //Ǯ���� ������Ʈ�� ���� ��� ������
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if(index >= 0)
            {
                name = name.Substring(index + 1);
            }

            //Ǯ���� ������Ʈ �������� ó��
            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
            {
                return go as T;
            }

        }

        //Ǯ���Ǿ� ���� ������ �� �ε�
        return Resources.Load<T>(path);
    }

    //������Ʈ ����
    //Ǯ���� ������Ʈ�� ������ �������� �ʰ� ���
    public GameObject Instantiate(string path, Transform parent = null)
    {
        //�޸𸮿� ����
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
                if ($"{path}" == "UI/Scene/UISceneMain")
                return null;
            Debug.Log($"Failed to load Prefab : {path}");
            return null;
        }

        //Ǯ���� ������Ʈ���� Ȯ��
        if(original.GetComponent<Poolable>() != null)
        {
            return Managers.Pool.Pop(original, parent).gameObject;
        }

        //������Ʈ ����
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        
        return go;
    }

    //������Ʈ ����
    public void Destroy(GameObject go)
    {

        if (go == null) return;

        //���� Ǯ���� �ʿ��� ������Ʈ��� Ǯ�� �Ŵ������� ��Ź
        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }


        Object.Destroy(go);


    }

    public override void Init()
    {
        throw new System.NotImplementedException(); 
    }

    public void Clear()
    {
    }
}
