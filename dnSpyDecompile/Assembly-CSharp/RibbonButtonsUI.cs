using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

// Token: 0x0200013A RID: 314
public class RibbonButtonsUI : MonoBehaviour
{
	// Token: 0x06001478 RID: 5240 RVA: 0x0007581C File Offset: 0x00073A1C
	public void Awake()
	{
		this.m_rootObject.SetActive(false);
		float aspectRatioDependentValue = TransformUtil.GetAspectRatioDependentValue(this.m_minAspectRatioAdjustment, this.m_wideAspectRatioAdjustment, this.m_extraWideAspectRatioAdjustment);
		TransformUtil.SetLocalPosX(this.m_LeftBones, this.m_LeftBones.localPosition.x + aspectRatioDependentValue);
		TransformUtil.SetLocalPosX(this.m_RightBones, this.m_RightBones.localPosition.x - aspectRatioDependentValue);
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>() == null)
		{
			Network.Get().RegisterNetHandler(InitialClientState.PacketID.ID, new Network.NetHandler(this.OnInitialClientState), null);
			return;
		}
		this.SetupJournalButton();
	}

	// Token: 0x06001479 RID: 5241 RVA: 0x000758BC File Offset: 0x00073ABC
	private void Start()
	{
		float aspectRatioDependentValue = TransformUtil.GetAspectRatioDependentValue(this.m_minAspectRatioZPos, this.m_wideAspectRatioZPos, this.m_extraWideAspectRatioZPos);
		TransformUtil.SetLocalPosZ(base.transform, aspectRatioDependentValue);
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x000758ED File Offset: 0x00073AED
	private void OnDestroy()
	{
		Network network = Network.Get();
		if (network == null)
		{
			return;
		}
		network.RemoveNetHandler(InitialClientState.PacketID.ID, new Network.NetHandler(this.OnInitialClientState));
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x00075915 File Offset: 0x00073B15
	public void Toggle(bool show)
	{
		this.m_shown = show;
		if (show)
		{
			base.StartCoroutine(this.ShowRibbons());
			return;
		}
		base.StartCoroutine(this.HideRibbons());
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x0007593C File Offset: 0x00073B3C
	private IEnumerator ShowRibbons()
	{
		this.m_rootObject.SetActive(false);
		float startDelay = 1f;
		foreach (RibbonButtonsUI.RibbonButtonObject ribbonButtonObject in this.m_Ribbons)
		{
			if (ribbonButtonObject.m_AnimateInDelay < startDelay)
			{
				startDelay = ribbonButtonObject.m_AnimateInDelay;
			}
		}
		yield return new WaitForSeconds(startDelay);
		this.m_rootObject.SetActive(true);
		using (List<RibbonButtonsUI.RibbonButtonObject>.Enumerator enumerator = this.m_Ribbons.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				RibbonButtonsUI.RibbonButtonObject ribbonButtonObject2 = enumerator.Current;
				ribbonButtonObject2.m_Ribbon.transform.position = ribbonButtonObject2.m_HiddenBone.position;
				iTween.Stop(ribbonButtonObject2.m_Ribbon.gameObject);
				Hashtable args = iTween.Hash(new object[]
				{
					"position",
					ribbonButtonObject2.m_ShownBone.position,
					"delay",
					ribbonButtonObject2.m_AnimateInDelay - startDelay,
					"time",
					this.m_EaseInTime,
					"easeType",
					iTween.EaseType.easeOutBack
				});
				iTween.MoveTo(ribbonButtonObject2.m_Ribbon.gameObject, args);
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x0007594B File Offset: 0x00073B4B
	private IEnumerator HideRibbons()
	{
		foreach (RibbonButtonsUI.RibbonButtonObject ribbonButtonObject in this.m_Ribbons)
		{
			ribbonButtonObject.m_Ribbon.transform.position = ribbonButtonObject.m_ShownBone.position;
			iTween.Stop(ribbonButtonObject.m_Ribbon.gameObject);
			Hashtable args = iTween.Hash(new object[]
			{
				"position",
				ribbonButtonObject.m_HiddenBone.position,
				"delay",
				0f,
				"time",
				this.m_EaseOutTime,
				"easeType",
				iTween.EaseType.easeInOutBack
			});
			iTween.MoveTo(ribbonButtonObject.m_Ribbon.gameObject, args);
		}
		yield return new WaitForSeconds(this.m_EaseOutTime);
		if (!this.m_shown)
		{
			this.m_rootObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x0600147E RID: 5246 RVA: 0x0007595C File Offset: 0x00073B5C
	public void SetPackCount(int packs)
	{
		if (packs <= 0)
		{
			this.m_packCount.Text = "";
			this.m_packCountFrame.SetActive(false);
			return;
		}
		this.m_packCount.Text = GameStrings.Format("GLUE_PACK_OPENING_BOOSTER_COUNT", new object[]
		{
			packs
		});
		this.m_packCountFrame.SetActive(true);
	}

	// Token: 0x0600147F RID: 5247 RVA: 0x000759BA File Offset: 0x00073BBA
	private void OnInitialClientState()
	{
		Network.Get().RemoveNetHandler(InitialClientState.PacketID.ID, new Network.NetHandler(this.OnInitialClientState));
		this.SetupJournalButton();
	}

	// Token: 0x06001480 RID: 5248 RVA: 0x000759E4 File Offset: 0x00073BE4
	private void SetupJournalButton()
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject != null)
		{
			if (netObject.ProgressionEnabled)
			{
				this.m_journalButtonWidget.Show();
				this.m_legacyQuestButtonGameObject.SetActive(false);
				return;
			}
			this.m_journalButtonWidget.gameObject.SetActive(false);
			this.m_legacyQuestButtonGameObject.SetActive(true);
		}
	}

	// Token: 0x04000DA8 RID: 3496
	public List<RibbonButtonsUI.RibbonButtonObject> m_Ribbons;

	// Token: 0x04000DA9 RID: 3497
	public Transform m_LeftBones;

	// Token: 0x04000DAA RID: 3498
	public Transform m_RightBones;

	// Token: 0x04000DAB RID: 3499
	public float m_EaseInTime = 1f;

	// Token: 0x04000DAC RID: 3500
	public float m_EaseOutTime = 0.4f;

	// Token: 0x04000DAD RID: 3501
	public GameObject m_rootObject;

	// Token: 0x04000DAE RID: 3502
	public PegUIElement m_collectionManagerRibbon;

	// Token: 0x04000DAF RID: 3503
	public PegUIElement m_questLogRibbon;

	// Token: 0x04000DB0 RID: 3504
	public PegUIElement m_packOpeningRibbon;

	// Token: 0x04000DB1 RID: 3505
	public PegUIElement m_storeRibbon;

	// Token: 0x04000DB2 RID: 3506
	public UberText m_packCount;

	// Token: 0x04000DB3 RID: 3507
	public GameObject m_packCountFrame;

	// Token: 0x04000DB4 RID: 3508
	public float m_minAspectRatioAdjustment = 0.24f;

	// Token: 0x04000DB5 RID: 3509
	public float m_wideAspectRatioAdjustment;

	// Token: 0x04000DB6 RID: 3510
	public float m_extraWideAspectRatioAdjustment = 0.24f;

	// Token: 0x04000DB7 RID: 3511
	public float m_minAspectRatioZPos;

	// Token: 0x04000DB8 RID: 3512
	public float m_wideAspectRatioZPos;

	// Token: 0x04000DB9 RID: 3513
	public float m_extraWideAspectRatioZPos = 0.35f;

	// Token: 0x04000DBA RID: 3514
	public Widget m_journalButtonWidget;

	// Token: 0x04000DBB RID: 3515
	public GameObject m_legacyQuestButtonGameObject;

	// Token: 0x04000DBC RID: 3516
	private bool m_shown = true;

	// Token: 0x020014DD RID: 5341
	[Serializable]
	public class RibbonButtonObject
	{
		// Token: 0x0400AB21 RID: 43809
		public PegUIElement m_Ribbon;

		// Token: 0x0400AB22 RID: 43810
		public Transform m_HiddenBone;

		// Token: 0x0400AB23 RID: 43811
		public Transform m_ShownBone;

		// Token: 0x0400AB24 RID: 43812
		public bool m_LeftSide;

		// Token: 0x0400AB25 RID: 43813
		public float m_AnimateInDelay;
	}
}
