using System;
using UnityEngine;

// Token: 0x02000328 RID: 808
public class MarkOfEvilCounter : MonoBehaviour
{
	// Token: 0x06002DDC RID: 11740 RVA: 0x000E8EAE File Offset: 0x000E70AE
	private void Awake()
	{
		this.OnMarksChanged(0);
	}

	// Token: 0x06002DDD RID: 11741 RVA: 0x000E8EB8 File Offset: 0x000E70B8
	public void OnMarksChanged(int numMarks)
	{
		if (numMarks <= 0)
		{
			base.gameObject.SetActive(false);
			return;
		}
		if (numMarks > this.m_MarkOfEvilIcons.Length)
		{
			Log.Gameplay.PrintWarning("{0}.OnMarksChanged() : num marks is greater than the number of icons!", Array.Empty<object>());
		}
		for (int i = 0; i < this.m_MarkOfEvilIcons.Length; i++)
		{
			this.m_MarkOfEvilIcons[i].sprite = ((i < numMarks) ? this.m_FullMarkOfEvilSprite : this.m_EmptyMarkOfEvilSprite);
		}
		base.gameObject.SetActive(true);
	}

	// Token: 0x04001949 RID: 6473
	public SpriteRenderer[] m_MarkOfEvilIcons;

	// Token: 0x0400194A RID: 6474
	public Sprite m_FullMarkOfEvilSprite;

	// Token: 0x0400194B RID: 6475
	public Sprite m_EmptyMarkOfEvilSprite;
}
