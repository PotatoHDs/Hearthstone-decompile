using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

public class RibbonButtonsUI : MonoBehaviour
{
	[Serializable]
	public class RibbonButtonObject
	{
		public PegUIElement m_Ribbon;

		public Transform m_HiddenBone;

		public Transform m_ShownBone;

		public bool m_LeftSide;

		public float m_AnimateInDelay;
	}

	public List<RibbonButtonObject> m_Ribbons;

	public Transform m_LeftBones;

	public Transform m_RightBones;

	public float m_EaseInTime = 1f;

	public float m_EaseOutTime = 0.4f;

	public GameObject m_rootObject;

	public PegUIElement m_collectionManagerRibbon;

	public PegUIElement m_questLogRibbon;

	public PegUIElement m_packOpeningRibbon;

	public PegUIElement m_storeRibbon;

	public UberText m_packCount;

	public GameObject m_packCountFrame;

	public float m_minAspectRatioAdjustment = 0.24f;

	public float m_wideAspectRatioAdjustment;

	public float m_extraWideAspectRatioAdjustment = 0.24f;

	public float m_minAspectRatioZPos;

	public float m_wideAspectRatioZPos;

	public float m_extraWideAspectRatioZPos = 0.35f;

	public Widget m_journalButtonWidget;

	public GameObject m_legacyQuestButtonGameObject;

	private bool m_shown = true;

	public void Awake()
	{
		m_rootObject.SetActive(value: false);
		float aspectRatioDependentValue = TransformUtil.GetAspectRatioDependentValue(m_minAspectRatioAdjustment, m_wideAspectRatioAdjustment, m_extraWideAspectRatioAdjustment);
		TransformUtil.SetLocalPosX(m_LeftBones, m_LeftBones.localPosition.x + aspectRatioDependentValue);
		TransformUtil.SetLocalPosX(m_RightBones, m_RightBones.localPosition.x - aspectRatioDependentValue);
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>() == null)
		{
			Network.Get().RegisterNetHandler(InitialClientState.PacketID.ID, OnInitialClientState);
		}
		else
		{
			SetupJournalButton();
		}
	}

	private void Start()
	{
		float aspectRatioDependentValue = TransformUtil.GetAspectRatioDependentValue(m_minAspectRatioZPos, m_wideAspectRatioZPos, m_extraWideAspectRatioZPos);
		TransformUtil.SetLocalPosZ(base.transform, aspectRatioDependentValue);
	}

	private void OnDestroy()
	{
		Network.Get()?.RemoveNetHandler(InitialClientState.PacketID.ID, OnInitialClientState);
	}

	public void Toggle(bool show)
	{
		m_shown = show;
		if (show)
		{
			StartCoroutine(ShowRibbons());
		}
		else
		{
			StartCoroutine(HideRibbons());
		}
	}

	private IEnumerator ShowRibbons()
	{
		m_rootObject.SetActive(value: false);
		float startDelay = 1f;
		foreach (RibbonButtonObject ribbon in m_Ribbons)
		{
			if (ribbon.m_AnimateInDelay < startDelay)
			{
				startDelay = ribbon.m_AnimateInDelay;
			}
		}
		yield return new WaitForSeconds(startDelay);
		m_rootObject.SetActive(value: true);
		foreach (RibbonButtonObject ribbon2 in m_Ribbons)
		{
			ribbon2.m_Ribbon.transform.position = ribbon2.m_HiddenBone.position;
			iTween.Stop(ribbon2.m_Ribbon.gameObject);
			Hashtable args = iTween.Hash("position", ribbon2.m_ShownBone.position, "delay", ribbon2.m_AnimateInDelay - startDelay, "time", m_EaseInTime, "easeType", iTween.EaseType.easeOutBack);
			iTween.MoveTo(ribbon2.m_Ribbon.gameObject, args);
		}
	}

	private IEnumerator HideRibbons()
	{
		foreach (RibbonButtonObject ribbon in m_Ribbons)
		{
			ribbon.m_Ribbon.transform.position = ribbon.m_ShownBone.position;
			iTween.Stop(ribbon.m_Ribbon.gameObject);
			Hashtable args = iTween.Hash("position", ribbon.m_HiddenBone.position, "delay", 0f, "time", m_EaseOutTime, "easeType", iTween.EaseType.easeInOutBack);
			iTween.MoveTo(ribbon.m_Ribbon.gameObject, args);
		}
		yield return new WaitForSeconds(m_EaseOutTime);
		if (!m_shown)
		{
			m_rootObject.SetActive(value: false);
		}
	}

	public void SetPackCount(int packs)
	{
		if (packs <= 0)
		{
			m_packCount.Text = "";
			m_packCountFrame.SetActive(value: false);
		}
		else
		{
			m_packCount.Text = GameStrings.Format("GLUE_PACK_OPENING_BOOSTER_COUNT", packs);
			m_packCountFrame.SetActive(value: true);
		}
	}

	private void OnInitialClientState()
	{
		Network.Get().RemoveNetHandler(InitialClientState.PacketID.ID, OnInitialClientState);
		SetupJournalButton();
	}

	private void SetupJournalButton()
	{
		NetCache.NetCacheFeatures netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>();
		if (netObject != null)
		{
			if (netObject.ProgressionEnabled)
			{
				m_journalButtonWidget.Show();
				m_legacyQuestButtonGameObject.SetActive(value: false);
			}
			else
			{
				m_journalButtonWidget.gameObject.SetActive(value: false);
				m_legacyQuestButtonGameObject.SetActive(value: true);
			}
		}
	}
}
