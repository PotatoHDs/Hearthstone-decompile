using System;
using UnityEngine;

// Token: 0x02000A93 RID: 2707
public class SnapActorToGameObject : MonoBehaviour
{
	// Token: 0x060090AE RID: 37038 RVA: 0x002EEC14 File Offset: 0x002ECE14
	private void Start()
	{
		Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(base.gameObject);
		if (actor == null)
		{
			Spell spell = SceneUtils.FindComponentInParents<Spell>(base.gameObject);
			if (spell != null)
			{
				actor = spell.GetSourceCard().GetActor();
			}
		}
		if (actor == null)
		{
			Debug.LogError(string.Format("SnapActorToGameObject on {0} failed to find Actor object!", base.gameObject.name));
			base.enabled = false;
			return;
		}
		this.m_actorTransform = actor.transform;
	}

	// Token: 0x060090AF RID: 37039 RVA: 0x002EEC90 File Offset: 0x002ECE90
	private void OnEnable()
	{
		if (this.m_actorTransform == null)
		{
			return;
		}
		this.m_OrgPosition = this.m_actorTransform.localPosition;
		this.m_OrgRotation = this.m_actorTransform.localRotation;
		this.m_OrgScale = this.m_actorTransform.localScale;
	}

	// Token: 0x060090B0 RID: 37040 RVA: 0x002EECE0 File Offset: 0x002ECEE0
	private void OnDisable()
	{
		if (this.m_actorTransform == null || !this.m_ResetTransformOnDisable)
		{
			return;
		}
		this.m_actorTransform.localPosition = this.m_OrgPosition;
		this.m_actorTransform.localRotation = this.m_OrgRotation;
		this.m_actorTransform.localScale = this.m_OrgScale;
	}

	// Token: 0x060090B1 RID: 37041 RVA: 0x002EED38 File Offset: 0x002ECF38
	private void LateUpdate()
	{
		if (this.m_actorTransform == null)
		{
			return;
		}
		if (this.m_SnapPostion)
		{
			this.m_actorTransform.position = base.transform.position;
		}
		if (this.m_SnapRotation)
		{
			this.m_actorTransform.rotation = base.transform.rotation;
		}
		if (this.m_SnapScale)
		{
			TransformUtil.SetWorldScale(this.m_actorTransform, base.transform.lossyScale);
		}
	}

	// Token: 0x04007974 RID: 31092
	public bool m_SnapPostion = true;

	// Token: 0x04007975 RID: 31093
	public bool m_SnapRotation = true;

	// Token: 0x04007976 RID: 31094
	public bool m_SnapScale = true;

	// Token: 0x04007977 RID: 31095
	public bool m_ResetTransformOnDisable;

	// Token: 0x04007978 RID: 31096
	private Transform m_actorTransform;

	// Token: 0x04007979 RID: 31097
	private Vector3 m_OrgPosition;

	// Token: 0x0400797A RID: 31098
	private Quaternion m_OrgRotation;

	// Token: 0x0400797B RID: 31099
	private Vector3 m_OrgScale;
}
