using System;
using UnityEngine;

// Token: 0x0200007E RID: 126
public class ChatLogMessageFrame : MonoBehaviour
{
	// Token: 0x06000726 RID: 1830 RVA: 0x00028EEC File Offset: 0x000270EC
	private void Awake()
	{
		Bounds bounds = this.m_Background.GetComponent<Collider>().bounds;
		Bounds textWorldSpaceBounds = this.m_Text.GetTextWorldSpaceBounds();
		this.m_initialPadding = bounds.size.y - textWorldSpaceBounds.size.y;
		this.m_initialBackgroundHeight = bounds.size.y;
		this.m_initialBackgroundLocalScaleY = this.m_Background.transform.localScale.y;
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x00028F62 File Offset: 0x00027162
	public string GetMessage()
	{
		return this.m_Text.Text;
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x00028F6F File Offset: 0x0002716F
	public void SetMessage(string message)
	{
		this.m_Text.Text = message;
		this.UpdateText();
		this.UpdateBackground();
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x00028F89 File Offset: 0x00027189
	public Color GetColor()
	{
		return this.m_Text.TextColor;
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x00028F96 File Offset: 0x00027196
	public void SetColor(Color color)
	{
		this.m_Text.TextColor = color;
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00028FA4 File Offset: 0x000271A4
	private void UpdateText()
	{
		this.m_Text.UpdateNow(false);
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x00028FB4 File Offset: 0x000271B4
	private void UpdateBackground()
	{
		float num = this.m_Text.GetTextWorldSpaceBounds().size.y + this.m_initialPadding;
		float num2 = this.m_initialBackgroundLocalScaleY;
		if (num > this.m_initialBackgroundHeight)
		{
			num2 *= num / this.m_initialBackgroundHeight;
		}
		TransformUtil.SetLocalScaleY(this.m_Background, num2);
	}

	// Token: 0x040004ED RID: 1261
	public GameObject m_Background;

	// Token: 0x040004EE RID: 1262
	public UberText m_Text;

	// Token: 0x040004EF RID: 1263
	private float m_initialPadding;

	// Token: 0x040004F0 RID: 1264
	private float m_initialBackgroundHeight;

	// Token: 0x040004F1 RID: 1265
	private float m_initialBackgroundLocalScaleY;
}
