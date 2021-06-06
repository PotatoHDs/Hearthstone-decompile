using System;
using UnityEngine;

// Token: 0x02000343 RID: 835
public class ResizableTooltipPanel : TooltipPanel
{
	// Token: 0x0600308C RID: 12428 RVA: 0x000F9E94 File Offset: 0x000F8094
	public override void Initialize(string keywordName, string keywordText)
	{
		if (this.m_background.GetComponent<NewThreeSliceElement>() == null)
		{
			Error.AddDevFatal("Prefab expecting m_background to have a NewThreeSliceElement!", Array.Empty<object>());
		}
		base.Initialize(keywordName, keywordText);
		this.m_bodyTextHeight = this.m_body.GetTextBounds().size.y;
		if (keywordText == "")
		{
			this.m_bodyTextHeight = 0f;
		}
		if (this.m_initialBackgroundHeight == 0f || this.m_initialBackgroundScale == Vector3.zero)
		{
			this.m_initialBackgroundHeight = this.m_background.GetComponent<NewThreeSliceElement>().m_middle.GetComponent<Renderer>().bounds.size.z;
			this.m_initialBackgroundScale = this.m_background.GetComponent<NewThreeSliceElement>().m_middle.transform.localScale;
		}
		float backgroundSize;
		if (keywordName.Length == 0)
		{
			backgroundSize = (this.m_bodyTextHeight + this.m_bodyPadding) * this.m_heightPadding;
		}
		else
		{
			backgroundSize = (this.m_name.Height + this.m_bodyTextHeight) * this.m_heightPadding;
		}
		this.SetBackgroundSize(backgroundSize);
	}

	// Token: 0x0600308D RID: 12429 RVA: 0x000F9FB0 File Offset: 0x000F81B0
	protected void SetBackgroundSize(float totalHeight)
	{
		this.m_background.GetComponent<NewThreeSliceElement>().SetSize(new Vector3(this.m_initialBackgroundScale.x, this.m_initialBackgroundScale.y * totalHeight / this.m_initialBackgroundHeight, this.m_initialBackgroundScale.z));
	}

	// Token: 0x0600308E RID: 12430 RVA: 0x000F9FFC File Offset: 0x000F81FC
	public override float GetHeight()
	{
		return this.m_background.GetComponent<NewThreeSliceElement>().m_leftOrTop.GetComponent<Renderer>().bounds.size.z + this.m_background.GetComponent<NewThreeSliceElement>().m_middle.GetComponent<Renderer>().bounds.size.z + this.m_background.GetComponent<NewThreeSliceElement>().m_rightOrBottom.GetComponent<Renderer>().bounds.size.z;
	}

	// Token: 0x0600308F RID: 12431 RVA: 0x000FA080 File Offset: 0x000F8280
	public override float GetWidth()
	{
		return this.m_background.GetComponent<NewThreeSliceElement>().m_leftOrTop.GetComponent<Renderer>().bounds.size.x;
	}

	// Token: 0x04001AF7 RID: 6903
	protected float m_heightPadding = 1f;

	// Token: 0x04001AF8 RID: 6904
	protected float m_bodyTextHeight;

	// Token: 0x04001AF9 RID: 6905
	protected float m_bodyPadding = 0.1f;
}
