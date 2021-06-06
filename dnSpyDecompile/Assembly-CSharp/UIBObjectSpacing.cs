using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AF4 RID: 2804
[CustomEditClass]
public class UIBObjectSpacing : MonoBehaviour
{
	// Token: 0x17000880 RID: 2176
	// (get) Token: 0x06009523 RID: 38179 RVA: 0x00304D98 File Offset: 0x00302F98
	// (set) Token: 0x06009524 RID: 38180 RVA: 0x00304DA0 File Offset: 0x00302FA0
	public Vector3 LocalOffset
	{
		get
		{
			return this.m_LocalOffset;
		}
		set
		{
			this.m_LocalOffset = value;
			this.UpdatePositions();
		}
	}

	// Token: 0x17000881 RID: 2177
	// (get) Token: 0x06009525 RID: 38181 RVA: 0x00304DAF File Offset: 0x00302FAF
	// (set) Token: 0x06009526 RID: 38182 RVA: 0x00304DB7 File Offset: 0x00302FB7
	public Vector3 LocalSpacing
	{
		get
		{
			return this.m_LocalSpacing;
		}
		set
		{
			this.m_LocalSpacing = value;
			this.UpdatePositions();
		}
	}

	// Token: 0x17000882 RID: 2178
	// (get) Token: 0x06009527 RID: 38183 RVA: 0x00304DC6 File Offset: 0x00302FC6
	// (set) Token: 0x06009528 RID: 38184 RVA: 0x00304DD0 File Offset: 0x00302FD0
	[CustomEditField(Range = "0 - 1")]
	public Vector3 Alignment
	{
		get
		{
			return this.m_Alignment;
		}
		set
		{
			this.m_Alignment = value;
			this.m_Alignment.x = Mathf.Clamp01(this.m_Alignment.x);
			this.m_Alignment.y = Mathf.Clamp01(this.m_Alignment.y);
			this.m_Alignment.z = Mathf.Clamp01(this.m_Alignment.z);
			this.UpdatePositions();
		}
	}

	// Token: 0x06009529 RID: 38185 RVA: 0x00304E3B File Offset: 0x0030303B
	public void AddSpace(int index)
	{
		this.m_Objects.Insert(index, new UIBObjectSpacing.SpacedObject
		{
			m_CountIfNull = true
		});
	}

	// Token: 0x0600952A RID: 38186 RVA: 0x00304E55 File Offset: 0x00303055
	public void AddSpace(int index, Vector3 offset)
	{
		this.m_Objects.Insert(index, new UIBObjectSpacing.SpacedObject
		{
			m_Offset = offset,
			m_CountIfNull = true
		});
	}

	// Token: 0x0600952B RID: 38187 RVA: 0x00304E76 File Offset: 0x00303076
	public void AddObject(GameObject obj, bool countIfNull = true)
	{
		this.AddObject(obj, Vector3.zero, countIfNull);
	}

	// Token: 0x0600952C RID: 38188 RVA: 0x00304E85 File Offset: 0x00303085
	public void AddObject(Component comp, bool countIfNull = true)
	{
		this.AddObject(comp, Vector3.zero, countIfNull);
	}

	// Token: 0x0600952D RID: 38189 RVA: 0x00304E94 File Offset: 0x00303094
	public void AddObject(Component comp, Vector3 offset, bool countIfNull = true)
	{
		this.AddObject(comp.gameObject, offset, countIfNull);
	}

	// Token: 0x0600952E RID: 38190 RVA: 0x00304EA4 File Offset: 0x003030A4
	public void AddObject(GameObject obj, Vector3 offset, bool countIfNull = true)
	{
		this.m_Objects.Add(new UIBObjectSpacing.SpacedObject
		{
			m_Object = obj,
			m_Offset = offset,
			m_CountIfNull = countIfNull
		});
	}

	// Token: 0x0600952F RID: 38191 RVA: 0x00304ECB File Offset: 0x003030CB
	public void ClearObjects()
	{
		this.m_Objects.Clear();
	}

	// Token: 0x06009530 RID: 38192 RVA: 0x00304ED8 File Offset: 0x003030D8
	public void AnimateUpdatePositions(float animTime, iTween.EaseType tweenType = iTween.EaseType.easeInOutQuad)
	{
		List<UIBObjectSpacing.AnimationPosition> list = new List<UIBObjectSpacing.AnimationPosition>();
		List<UIBObjectSpacing.SpacedObject> list2 = this.m_Objects.FindAll((UIBObjectSpacing.SpacedObject o) => o.m_CountIfNull || (o.m_Object != null && o.m_Object.activeInHierarchy));
		if (this.m_reverse)
		{
			list2.Reverse();
		}
		Vector3 a = this.m_LocalOffset;
		Vector3 localSpacing = this.m_LocalSpacing;
		Vector3 vector = Vector3.zero;
		for (int i = 0; i < list2.Count; i++)
		{
			UIBObjectSpacing.SpacedObject spacedObject = list2[i];
			GameObject @object = spacedObject.m_Object;
			if (@object != null)
			{
				list.Add(new UIBObjectSpacing.AnimationPosition
				{
					m_targetPos = a + spacedObject.m_Offset,
					m_object = @object
				});
			}
			Vector3 vector2 = spacedObject.m_Offset;
			if (i < list2.Count - 1)
			{
				vector2 += localSpacing;
			}
			a += vector2;
			vector += vector2;
		}
		vector.x *= this.m_Alignment.x;
		vector.y *= this.m_Alignment.y;
		vector.z *= this.m_Alignment.z;
		for (int j = 0; j < list.Count; j++)
		{
			UIBObjectSpacing.AnimationPosition animationPosition = list[j];
			iTween.MoveTo(animationPosition.m_object, iTween.Hash(new object[]
			{
				"position",
				animationPosition.m_targetPos - vector,
				"islocal",
				true,
				"easetype",
				tweenType,
				"time",
				animTime
			}));
		}
	}

	// Token: 0x06009531 RID: 38193 RVA: 0x00305094 File Offset: 0x00303294
	public void UpdatePositions()
	{
		List<UIBObjectSpacing.SpacedObject> list = this.m_Objects.FindAll((UIBObjectSpacing.SpacedObject o) => o.m_CountIfNull || (o.m_Object != null && o.m_Object.activeInHierarchy));
		if (this.m_reverse)
		{
			list.Reverse();
		}
		Vector3 a = this.m_LocalOffset;
		Vector3 localSpacing = this.m_LocalSpacing;
		Vector3 vector = Vector3.zero;
		for (int i = 0; i < list.Count; i++)
		{
			UIBObjectSpacing.SpacedObject spacedObject = list[i];
			GameObject @object = spacedObject.m_Object;
			if (@object != null)
			{
				@object.transform.localPosition = a + spacedObject.m_Offset;
			}
			Vector3 vector2 = spacedObject.m_Offset;
			if (i < list.Count - 1)
			{
				vector2 += localSpacing;
			}
			a += vector2;
			vector += vector2;
		}
		vector.x *= this.m_Alignment.x;
		vector.y *= this.m_Alignment.y;
		vector.z *= this.m_Alignment.z;
		for (int j = 0; j < list.Count; j++)
		{
			GameObject object2 = list[j].m_Object;
			if (object2 != null)
			{
				object2.transform.localPosition -= vector;
			}
		}
	}

	// Token: 0x04007D15 RID: 32021
	public List<UIBObjectSpacing.SpacedObject> m_Objects = new List<UIBObjectSpacing.SpacedObject>();

	// Token: 0x04007D16 RID: 32022
	[SerializeField]
	private Vector3 m_LocalOffset;

	// Token: 0x04007D17 RID: 32023
	[SerializeField]
	private Vector3 m_LocalSpacing;

	// Token: 0x04007D18 RID: 32024
	[SerializeField]
	private Vector3 m_Alignment = new Vector3(0.5f, 0.5f, 0.5f);

	// Token: 0x04007D19 RID: 32025
	public bool m_reverse;

	// Token: 0x0200272A RID: 10026
	[Serializable]
	public class SpacedObject
	{
		// Token: 0x0400F391 RID: 62353
		public GameObject m_Object;

		// Token: 0x0400F392 RID: 62354
		public Vector3 m_Offset;

		// Token: 0x0400F393 RID: 62355
		public bool m_CountIfNull;
	}

	// Token: 0x0200272B RID: 10027
	private class AnimationPosition
	{
		// Token: 0x0400F394 RID: 62356
		public Vector3 m_targetPos;

		// Token: 0x0400F395 RID: 62357
		public GameObject m_object;
	}
}
