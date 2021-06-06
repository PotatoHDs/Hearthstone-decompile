using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectionUtils
{
	public enum ViewMode
	{
		CARDS,
		HERO_SKINS,
		CARD_BACKS,
		DECK_TEMPLATE,
		MASS_DISENCHANT,
		COINS,
		COUNT
	}

	public class ViewModeData
	{
		public delegate bool WaitToTurnPageDelegate();

		public TAG_CLASS? m_setPageByClass;

		public string m_setPageByCard;

		public TAG_PREMIUM m_setPageByPremium;

		public BookPageManager.DelOnPageTransitionComplete m_pageTransitionCompleteCallback;

		public object m_pageTransitionCompleteData;
	}

	[Serializable]
	public class CollectionPageLayoutSettings
	{
		[Serializable]
		public class Variables
		{
			public ViewMode m_ViewMode;

			public int m_ColumnCount = 4;

			public int m_RowCount = 2;

			public float m_Scale;

			public float m_ColumnSpacing;

			public float m_RowSpacing;

			public Vector3 m_Offset;
		}

		[CustomEditField(ListTable = true)]
		public List<Variables> m_layoutVariables = new List<Variables>();

		public Variables GetVariables(ViewMode mode)
		{
			Variables variables = m_layoutVariables.Find((Variables v) => mode == v.m_ViewMode);
			if (variables == null)
			{
				return new Variables();
			}
			return variables;
		}
	}
}
