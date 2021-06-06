using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EA RID: 234
public class CollectionUtils
{
	// Token: 0x02001408 RID: 5128
	public enum ViewMode
	{
		// Token: 0x0400A8AA RID: 43178
		CARDS,
		// Token: 0x0400A8AB RID: 43179
		HERO_SKINS,
		// Token: 0x0400A8AC RID: 43180
		CARD_BACKS,
		// Token: 0x0400A8AD RID: 43181
		DECK_TEMPLATE,
		// Token: 0x0400A8AE RID: 43182
		MASS_DISENCHANT,
		// Token: 0x0400A8AF RID: 43183
		COINS,
		// Token: 0x0400A8B0 RID: 43184
		COUNT
	}

	// Token: 0x02001409 RID: 5129
	public class ViewModeData
	{
		// Token: 0x0400A8B1 RID: 43185
		public TAG_CLASS? m_setPageByClass;

		// Token: 0x0400A8B2 RID: 43186
		public string m_setPageByCard;

		// Token: 0x0400A8B3 RID: 43187
		public TAG_PREMIUM m_setPageByPremium;

		// Token: 0x0400A8B4 RID: 43188
		public BookPageManager.DelOnPageTransitionComplete m_pageTransitionCompleteCallback;

		// Token: 0x0400A8B5 RID: 43189
		public object m_pageTransitionCompleteData;

		// Token: 0x0200297B RID: 10619
		// (Invoke) Token: 0x06013EF5 RID: 81653
		public delegate bool WaitToTurnPageDelegate();
	}

	// Token: 0x0200140A RID: 5130
	[Serializable]
	public class CollectionPageLayoutSettings
	{
		// Token: 0x0600D976 RID: 55670 RVA: 0x003F028C File Offset: 0x003EE48C
		public CollectionUtils.CollectionPageLayoutSettings.Variables GetVariables(CollectionUtils.ViewMode mode)
		{
			CollectionUtils.CollectionPageLayoutSettings.Variables variables = this.m_layoutVariables.Find((CollectionUtils.CollectionPageLayoutSettings.Variables v) => mode == v.m_ViewMode);
			if (variables == null)
			{
				return new CollectionUtils.CollectionPageLayoutSettings.Variables();
			}
			return variables;
		}

		// Token: 0x0400A8B6 RID: 43190
		[CustomEditField(ListTable = true)]
		public List<CollectionUtils.CollectionPageLayoutSettings.Variables> m_layoutVariables = new List<CollectionUtils.CollectionPageLayoutSettings.Variables>();

		// Token: 0x0200297C RID: 10620
		[Serializable]
		public class Variables
		{
			// Token: 0x0400FCE0 RID: 64736
			public CollectionUtils.ViewMode m_ViewMode;

			// Token: 0x0400FCE1 RID: 64737
			public int m_ColumnCount = 4;

			// Token: 0x0400FCE2 RID: 64738
			public int m_RowCount = 2;

			// Token: 0x0400FCE3 RID: 64739
			public float m_Scale;

			// Token: 0x0400FCE4 RID: 64740
			public float m_ColumnSpacing;

			// Token: 0x0400FCE5 RID: 64741
			public float m_RowSpacing;

			// Token: 0x0400FCE6 RID: 64742
			public Vector3 m_Offset;
		}
	}
}
