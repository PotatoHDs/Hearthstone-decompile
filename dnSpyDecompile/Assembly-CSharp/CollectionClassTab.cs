using System;
using UnityEngine;

// Token: 0x02000100 RID: 256
public class CollectionClassTab : BookTab
{
	// Token: 0x06000EB1 RID: 3761 RVA: 0x0005299F File Offset: 0x00050B9F
	public void Init(TAG_CLASS classTag)
	{
		this.m_classTag = classTag;
		base.Init();
	}

	// Token: 0x06000EB2 RID: 3762 RVA: 0x000529AE File Offset: 0x00050BAE
	public TAG_CLASS GetClass()
	{
		return this.m_classTag;
	}

	// Token: 0x06000EB3 RID: 3763 RVA: 0x000529B8 File Offset: 0x00050BB8
	protected override Vector2 GetTextureOffset()
	{
		if (CollectionPageManager.s_classTextureOffsets.ContainsKey(this.m_classTag))
		{
			return CollectionPageManager.s_classTextureOffsets[this.m_classTag];
		}
		Debug.LogWarning(string.Format("CollectionClassTab.GetTextureOffset(): No class texture offsets exist for class {0}", this.m_classTag));
		return Vector2.zero;
	}

	// Token: 0x04000A25 RID: 2597
	private TAG_CLASS m_classTag;
}
