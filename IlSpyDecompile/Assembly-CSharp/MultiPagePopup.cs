using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class MultiPagePopup : DialogBase
{
	public enum PageType
	{
		CARD_LIST,
		DUST_JAR
	}

	public class PageInfo
	{
		public PageType m_pageType;

		public string m_customPrefabAssetRef;

		public string m_headerText;

		public string m_bodyText;

		public string m_footerText;

		public List<CollectibleCard> m_cardsToShow;

		public int m_dustAmount;
	}

	public class Info
	{
		public HideCallback m_callbackOnHide;

		public bool m_blurWhenShown;

		public List<PageInfo> m_pages = new List<PageInfo>();
	}

	private readonly Map<PageType, string> m_pagePrefabRefs = new Map<PageType, string>
	{
		{
			PageType.CARD_LIST,
			"CardListPage.prefab:e48c89787318c4d49bd21abc51901bf8"
		},
		{
			PageType.DUST_JAR,
			"DustJarPage.prefab:9d96713c54a11764691eb73236976680"
		}
	};

	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_showAnimationSound = "Expand_Up.prefab:775d97ea42498c044897f396362b9db3";

	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_hideAnimationSound = "Shrink_Down_Quicker.prefab:2fe963b171811ca4b8d544fa53e3330c";

	private Info m_info = new Info();

	private int m_currentPageIdx;

	private Map<int, GameObject> m_pageObjects = new Map<int, GameObject>();

	private int m_numPagesLoaded;

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().SetSystemDialogActive(active: false);
		}
	}

	public void SetInfo(Info info)
	{
		m_info = info;
		if (m_info.m_callbackOnHide != null)
		{
			AddHideListener(m_info.m_callbackOnHide);
		}
	}

	public override void Show()
	{
		if (m_info.m_blurWhenShown)
		{
			DialogBase.DoBlur();
		}
		UniversalInputManager.Get().SetSystemDialogActive(active: true);
		int num = 0;
		foreach (PageInfo page in m_info.m_pages)
		{
			string pageAssetRef = GetPageAssetRef(page);
			m_pageObjects[num] = null;
			AssetLoader.Get().InstantiatePrefab(pageAssetRef, OnPageLoaded, num, AssetLoadingOptions.IgnorePrefabPosition);
			num++;
		}
		StartCoroutine(ShowWhenReady());
	}

	public override void Hide()
	{
		base.Hide();
		if (m_info.m_blurWhenShown)
		{
			DialogBase.EndBlur();
		}
	}

	private string GetPageAssetRef(PageInfo pageInfo)
	{
		if (!string.IsNullOrEmpty(pageInfo.m_customPrefabAssetRef))
		{
			return pageInfo.m_customPrefabAssetRef;
		}
		m_pagePrefabRefs.TryGetValue(pageInfo.m_pageType, out var value);
		return value;
	}

	private void OnPageLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		int key = (int)callbackData;
		m_pageObjects[key] = go;
		GameUtils.SetParent(go, base.gameObject);
		SceneUtils.SetLayer(go, base.gameObject.layer);
		go.SetActive(value: false);
		m_numPagesLoaded++;
	}

	private IEnumerator ShowWhenReady()
	{
		while (m_numPagesLoaded < m_pageObjects.Count)
		{
			yield return null;
		}
		base.Show();
		if (!string.IsNullOrEmpty(m_showAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(m_showAnimationSound);
		}
		Vector3 localScale = base.transform.localScale;
		base.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		Hashtable args = iTween.Hash("scale", localScale, "time", 0.3f, "easetype", iTween.EaseType.easeOutBack);
		iTween.ScaleTo(base.gameObject, args);
		UniversalInputManager.Get().SetSystemDialogActive(active: true);
		if (!ShowPage(m_currentPageIdx))
		{
			Hide();
		}
	}

	protected override void DoHideAnimation()
	{
		if (!string.IsNullOrEmpty(m_hideAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(m_hideAnimationSound);
		}
		base.DoHideAnimation();
	}

	private void PressNext()
	{
		GameObject value = null;
		if (m_pageObjects.TryGetValue(m_currentPageIdx, out value))
		{
			value.gameObject.SetActive(value: false);
		}
		m_currentPageIdx++;
		if (!ShowPage(m_currentPageIdx))
		{
			Hide();
		}
	}

	private bool ShowPage(int pageIdx)
	{
		if (pageIdx >= m_info.m_pages.Count)
		{
			return false;
		}
		PageInfo pageInfo = m_info.m_pages[pageIdx];
		if (pageInfo == null)
		{
			return false;
		}
		GameObject value = null;
		if (!m_pageObjects.TryGetValue(pageIdx, out value))
		{
			return false;
		}
		MultiPagePopupPage component = value.GetComponent<MultiPagePopupPage>();
		if (component == null)
		{
			return false;
		}
		value.SetActive(value: true);
		component.m_button.AddEventListener(UIEventType.RELEASE, delegate
		{
			PressNext();
		});
		if (pageIdx == m_info.m_pages.Count - 1)
		{
			component.m_buttonText.Text = GameStrings.Get("GLOBAL_DONE");
		}
		else
		{
			component.m_buttonText.Text = GameStrings.Get("GLOBAL_BUTTON_NEXT");
		}
		if (component.m_headerText != null && pageInfo.m_headerText != null)
		{
			component.m_headerText.Text = pageInfo.m_headerText;
		}
		if (component.m_bodyText != null && pageInfo.m_bodyText != null)
		{
			component.m_bodyText.Text = pageInfo.m_bodyText;
		}
		if (component.m_footerText != null && pageInfo.m_footerText != null)
		{
			component.m_footerText.Text = pageInfo.m_footerText;
		}
		CardListPanel componentInChildren = value.GetComponentInChildren<CardListPanel>();
		if (componentInChildren != null)
		{
			componentInChildren.Show(pageInfo.m_cardsToShow);
		}
		if (pageInfo.m_dustAmount > 0)
		{
			DustJarPanel componentInChildren2 = value.GetComponentInChildren<DustJarPanel>();
			if (componentInChildren2 != null)
			{
				componentInChildren2.Show(pageInfo.m_dustAmount);
			}
		}
		return true;
	}
}
