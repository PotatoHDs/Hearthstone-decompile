using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B21 RID: 2849
[CustomEditClass]
public class MultiPagePopup : DialogBase
{
	// Token: 0x0600974E RID: 38734 RVA: 0x0030865C File Offset: 0x0030685C
	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().SetSystemDialogActive(false);
		}
	}

	// Token: 0x0600974F RID: 38735 RVA: 0x0030E6FD File Offset: 0x0030C8FD
	public void SetInfo(MultiPagePopup.Info info)
	{
		this.m_info = info;
		if (this.m_info.m_callbackOnHide != null)
		{
			base.AddHideListener(this.m_info.m_callbackOnHide);
		}
	}

	// Token: 0x06009750 RID: 38736 RVA: 0x0030E724 File Offset: 0x0030C924
	public override void Show()
	{
		if (this.m_info.m_blurWhenShown)
		{
			DialogBase.DoBlur();
		}
		UniversalInputManager.Get().SetSystemDialogActive(true);
		int num = 0;
		foreach (MultiPagePopup.PageInfo pageInfo in this.m_info.m_pages)
		{
			string pageAssetRef = this.GetPageAssetRef(pageInfo);
			this.m_pageObjects[num] = null;
			AssetLoader.Get().InstantiatePrefab(pageAssetRef, new PrefabCallback<GameObject>(this.OnPageLoaded), num, AssetLoadingOptions.IgnorePrefabPosition);
			num++;
		}
		base.StartCoroutine(this.ShowWhenReady());
	}

	// Token: 0x06009751 RID: 38737 RVA: 0x0030E7E0 File Offset: 0x0030C9E0
	public override void Hide()
	{
		base.Hide();
		if (this.m_info.m_blurWhenShown)
		{
			DialogBase.EndBlur();
		}
	}

	// Token: 0x06009752 RID: 38738 RVA: 0x0030E7FC File Offset: 0x0030C9FC
	private string GetPageAssetRef(MultiPagePopup.PageInfo pageInfo)
	{
		if (!string.IsNullOrEmpty(pageInfo.m_customPrefabAssetRef))
		{
			return pageInfo.m_customPrefabAssetRef;
		}
		string result;
		this.m_pagePrefabRefs.TryGetValue(pageInfo.m_pageType, out result);
		return result;
	}

	// Token: 0x06009753 RID: 38739 RVA: 0x0030E834 File Offset: 0x0030CA34
	private void OnPageLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		int key = (int)callbackData;
		this.m_pageObjects[key] = go;
		GameUtils.SetParent(go, base.gameObject, false);
		SceneUtils.SetLayer(go, base.gameObject.layer, null);
		go.SetActive(false);
		this.m_numPagesLoaded++;
	}

	// Token: 0x06009754 RID: 38740 RVA: 0x0030E891 File Offset: 0x0030CA91
	private IEnumerator ShowWhenReady()
	{
		while (this.m_numPagesLoaded < this.m_pageObjects.Count)
		{
			yield return null;
		}
		base.Show();
		if (!string.IsNullOrEmpty(this.m_showAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_showAnimationSound);
		}
		Vector3 localScale = base.transform.localScale;
		base.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			localScale,
			"time",
			0.3f,
			"easetype",
			iTween.EaseType.easeOutBack
		});
		iTween.ScaleTo(base.gameObject, args);
		UniversalInputManager.Get().SetSystemDialogActive(true);
		if (!this.ShowPage(this.m_currentPageIdx))
		{
			this.Hide();
		}
		yield break;
	}

	// Token: 0x06009755 RID: 38741 RVA: 0x0030E8A0 File Offset: 0x0030CAA0
	protected override void DoHideAnimation()
	{
		if (!string.IsNullOrEmpty(this.m_hideAnimationSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_hideAnimationSound);
		}
		base.DoHideAnimation();
	}

	// Token: 0x06009756 RID: 38742 RVA: 0x0030E8CC File Offset: 0x0030CACC
	private void PressNext()
	{
		GameObject gameObject = null;
		if (this.m_pageObjects.TryGetValue(this.m_currentPageIdx, out gameObject))
		{
			gameObject.gameObject.SetActive(false);
		}
		this.m_currentPageIdx++;
		if (!this.ShowPage(this.m_currentPageIdx))
		{
			this.Hide();
		}
	}

	// Token: 0x06009757 RID: 38743 RVA: 0x0030E920 File Offset: 0x0030CB20
	private bool ShowPage(int pageIdx)
	{
		if (pageIdx >= this.m_info.m_pages.Count)
		{
			return false;
		}
		MultiPagePopup.PageInfo pageInfo = this.m_info.m_pages[pageIdx];
		if (pageInfo == null)
		{
			return false;
		}
		GameObject gameObject = null;
		if (!this.m_pageObjects.TryGetValue(pageIdx, out gameObject))
		{
			return false;
		}
		MultiPagePopupPage component = gameObject.GetComponent<MultiPagePopupPage>();
		if (component == null)
		{
			return false;
		}
		gameObject.SetActive(true);
		component.m_button.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.PressNext();
		});
		if (pageIdx == this.m_info.m_pages.Count - 1)
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
		CardListPanel componentInChildren = gameObject.GetComponentInChildren<CardListPanel>();
		if (componentInChildren != null)
		{
			componentInChildren.Show(pageInfo.m_cardsToShow);
		}
		if (pageInfo.m_dustAmount > 0)
		{
			DustJarPanel componentInChildren2 = gameObject.GetComponentInChildren<DustJarPanel>();
			if (componentInChildren2 != null)
			{
				componentInChildren2.Show(pageInfo.m_dustAmount);
			}
		}
		return true;
	}

	// Token: 0x04007E9D RID: 32413
	private readonly Map<MultiPagePopup.PageType, string> m_pagePrefabRefs = new Map<MultiPagePopup.PageType, string>
	{
		{
			MultiPagePopup.PageType.CARD_LIST,
			"CardListPage.prefab:e48c89787318c4d49bd21abc51901bf8"
		},
		{
			MultiPagePopup.PageType.DUST_JAR,
			"DustJarPage.prefab:9d96713c54a11764691eb73236976680"
		}
	};

	// Token: 0x04007E9E RID: 32414
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_showAnimationSound = "Expand_Up.prefab:775d97ea42498c044897f396362b9db3";

	// Token: 0x04007E9F RID: 32415
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_hideAnimationSound = "Shrink_Down_Quicker.prefab:2fe963b171811ca4b8d544fa53e3330c";

	// Token: 0x04007EA0 RID: 32416
	private MultiPagePopup.Info m_info = new MultiPagePopup.Info();

	// Token: 0x04007EA1 RID: 32417
	private int m_currentPageIdx;

	// Token: 0x04007EA2 RID: 32418
	private Map<int, GameObject> m_pageObjects = new Map<int, GameObject>();

	// Token: 0x04007EA3 RID: 32419
	private int m_numPagesLoaded;

	// Token: 0x02002768 RID: 10088
	public enum PageType
	{
		// Token: 0x0400F3F6 RID: 62454
		CARD_LIST,
		// Token: 0x0400F3F7 RID: 62455
		DUST_JAR
	}

	// Token: 0x02002769 RID: 10089
	public class PageInfo
	{
		// Token: 0x0400F3F8 RID: 62456
		public MultiPagePopup.PageType m_pageType;

		// Token: 0x0400F3F9 RID: 62457
		public string m_customPrefabAssetRef;

		// Token: 0x0400F3FA RID: 62458
		public string m_headerText;

		// Token: 0x0400F3FB RID: 62459
		public string m_bodyText;

		// Token: 0x0400F3FC RID: 62460
		public string m_footerText;

		// Token: 0x0400F3FD RID: 62461
		public List<CollectibleCard> m_cardsToShow;

		// Token: 0x0400F3FE RID: 62462
		public int m_dustAmount;
	}

	// Token: 0x0200276A RID: 10090
	public class Info
	{
		// Token: 0x0400F3FF RID: 62463
		public DialogBase.HideCallback m_callbackOnHide;

		// Token: 0x0400F400 RID: 62464
		public bool m_blurWhenShown;

		// Token: 0x0400F401 RID: 62465
		public List<MultiPagePopup.PageInfo> m_pages = new List<MultiPagePopup.PageInfo>();
	}
}
