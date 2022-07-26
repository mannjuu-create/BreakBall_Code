using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// �����̎q�I�u�W�F�N�g��S�ăQ�b�g����X�N���v�g.
/// </summary>

public static class GetAllChildren
{
	public static List<GameObject> GetAll(this GameObject obj)
	{
		List<GameObject> allChildren = new List<GameObject>();
		GetChildren(obj, ref allChildren);
		return allChildren;
	}

	//�q�v�f���擾���ă��X�g�ɒǉ�
	public static void GetChildren(GameObject obj, ref List<GameObject> allChildren)
	{
		Transform children = obj.GetComponentInChildren<Transform>();
		//�q�v�f�����Ȃ���ΏI��
		if (children.childCount == 0)
		{
			return;
		}
		foreach (Transform ob in children)
		{
			allChildren.Add(ob.gameObject);
			GetChildren(ob.gameObject, ref allChildren);
		}
	}
}