using System;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class UIBObjectSpacing : MonoBehaviour
{
	[Serializable]
	public class SpacedObject
	{
		public GameObject m_Object;

		public Vector3 m_Offset;

		public bool m_CountIfNull;
	}

	private class AnimationPosition
	{
		public Vector3 m_targetPos;

		public GameObject m_object;
	}

	public List<SpacedObject> m_Objects = new List<SpacedObject>();

	[SerializeField]
	private Vector3 m_LocalOffset;

	[SerializeField]
	private Vector3 m_LocalSpacing;

	[SerializeField]
	private Vector3 m_Alignment = new Vector3(0.5f, 0.5f, 0.5f);

	public bool m_reverse;

	public Vector3 LocalOffset
	{
		get
		{
			return m_LocalOffset;
		}
		set
		{
			m_LocalOffset = value;
			UpdatePositions();
		}
	}

	public Vector3 LocalSpacing
	{
		get
		{
			return m_LocalSpacing;
		}
		set
		{
			m_LocalSpacing = value;
			UpdatePositions();
		}
	}

	[CustomEditField(Range = "0 - 1")]
	public Vector3 Alignment
	{
		get
		{
			return m_Alignment;
		}
		set
		{
			m_Alignment = value;
			m_Alignment.x = Mathf.Clamp01(m_Alignment.x);
			m_Alignment.y = Mathf.Clamp01(m_Alignment.y);
			m_Alignment.z = Mathf.Clamp01(m_Alignment.z);
			UpdatePositions();
		}
	}

	public void AddSpace(int index)
	{
		m_Objects.Insert(index, new SpacedObject
		{
			m_CountIfNull = true
		});
	}

	public void AddSpace(int index, Vector3 offset)
	{
		m_Objects.Insert(index, new SpacedObject
		{
			m_Offset = offset,
			m_CountIfNull = true
		});
	}

	public void AddObject(GameObject obj, bool countIfNull = true)
	{
		AddObject(obj, Vector3.zero, countIfNull);
	}

	public void AddObject(Component comp, bool countIfNull = true)
	{
		AddObject(comp, Vector3.zero, countIfNull);
	}

	public void AddObject(Component comp, Vector3 offset, bool countIfNull = true)
	{
		AddObject(comp.gameObject, offset, countIfNull);
	}

	public void AddObject(GameObject obj, Vector3 offset, bool countIfNull = true)
	{
		m_Objects.Add(new SpacedObject
		{
			m_Object = obj,
			m_Offset = offset,
			m_CountIfNull = countIfNull
		});
	}

	public void ClearObjects()
	{
		m_Objects.Clear();
	}

	public void AnimateUpdatePositions(float animTime, iTween.EaseType tweenType = iTween.EaseType.easeInOutQuad)
	{
		List<AnimationPosition> list = new List<AnimationPosition>();
		List<SpacedObject> list2 = m_Objects.FindAll((SpacedObject o) => o.m_CountIfNull || (o.m_Object != null && o.m_Object.activeInHierarchy));
		if (m_reverse)
		{
			list2.Reverse();
		}
		Vector3 localOffset = m_LocalOffset;
		Vector3 localSpacing = m_LocalSpacing;
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < list2.Count; i++)
		{
			SpacedObject spacedObject = list2[i];
			GameObject @object = spacedObject.m_Object;
			if (@object != null)
			{
				list.Add(new AnimationPosition
				{
					m_targetPos = localOffset + spacedObject.m_Offset,
					m_object = @object
				});
			}
			Vector3 offset = spacedObject.m_Offset;
			if (i < list2.Count - 1)
			{
				offset += localSpacing;
			}
			localOffset += offset;
			zero += offset;
		}
		zero.x *= m_Alignment.x;
		zero.y *= m_Alignment.y;
		zero.z *= m_Alignment.z;
		for (int j = 0; j < list.Count; j++)
		{
			AnimationPosition animationPosition = list[j];
			iTween.MoveTo(animationPosition.m_object, iTween.Hash("position", animationPosition.m_targetPos - zero, "islocal", true, "easetype", tweenType, "time", animTime));
		}
	}

	public void UpdatePositions()
	{
		List<SpacedObject> list = m_Objects.FindAll((SpacedObject o) => o.m_CountIfNull || (o.m_Object != null && o.m_Object.activeInHierarchy));
		if (m_reverse)
		{
			list.Reverse();
		}
		Vector3 localOffset = m_LocalOffset;
		Vector3 localSpacing = m_LocalSpacing;
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < list.Count; i++)
		{
			SpacedObject spacedObject = list[i];
			GameObject @object = spacedObject.m_Object;
			if (@object != null)
			{
				@object.transform.localPosition = localOffset + spacedObject.m_Offset;
			}
			Vector3 offset = spacedObject.m_Offset;
			if (i < list.Count - 1)
			{
				offset += localSpacing;
			}
			localOffset += offset;
			zero += offset;
		}
		zero.x *= m_Alignment.x;
		zero.y *= m_Alignment.y;
		zero.z *= m_Alignment.z;
		for (int j = 0; j < list.Count; j++)
		{
			GameObject object2 = list[j].m_Object;
			if (object2 != null)
			{
				object2.transform.localPosition -= zero;
			}
		}
	}
}
