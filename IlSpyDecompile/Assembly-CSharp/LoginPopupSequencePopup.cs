using System.Collections;
using UnityEngine;

public class LoginPopupSequencePopup : BasicPopup
{
	public class Info
	{
		public string m_prefabAssetReference;

		public string m_headerText;

		public string m_bodyText;

		public string m_buttonText;

		public CollectibleCard m_card;

		public AssetReference m_backgroundMaterialReference;

		public HideCallback m_callbackOnHide;
	}

	private struct CardActorLoadedData
	{
		public DefLoader.DisposableFullDef m_fullDef;

		public TAG_PREMIUM m_premium;
	}

	public Transform m_cardBone;

	public Renderer m_background;

	private Info m_info;

	private Material m_backgroundMaterial;

	private bool m_backgroundReady = true;

	private Actor m_cardActor;

	private bool m_cardReady = true;

	public void SetInfo(Info info)
	{
		m_info = info;
		if (m_info.m_callbackOnHide != null)
		{
			AddHideListener(m_info.m_callbackOnHide);
		}
	}

	public void LoadAssetsAndShowWhenReady()
	{
		if (!string.IsNullOrEmpty(m_info.m_backgroundMaterialReference))
		{
			m_backgroundReady = false;
			AssetLoader.Get().LoadMaterial(m_info.m_backgroundMaterialReference, OnBackgroundMaterialLoaded);
		}
		if (m_info.m_card != null)
		{
			m_cardReady = false;
			DefLoader.DisposableFullDef fullDef = DefLoader.Get().GetFullDef(m_info.m_card.CardId);
			AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(fullDef.EntityDef, m_info.m_card.PremiumType), OnCardActorLoaded, new CardActorLoadedData
			{
				m_fullDef = fullDef,
				m_premium = m_info.m_card.PremiumType
			}, AssetLoadingOptions.IgnorePrefabPosition);
		}
		StartCoroutine(ShowWhenReady());
	}

	public override void Show()
	{
		base.Show();
		SetUpPopup(m_info);
		DialogBase.DoBlur();
	}

	public override void Hide()
	{
		base.Hide();
		DialogBase.EndBlur();
	}

	protected override void Awake()
	{
		base.Awake();
		m_cancelButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			Hide();
		});
	}

	private void OnBackgroundMaterialLoaded(AssetReference assetRef, Object obj, object callbackData)
	{
		m_backgroundMaterial = (Material)obj;
		m_backgroundReady = true;
	}

	private void OnCardActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		CardActorLoadedData cardActorLoadedData = (CardActorLoadedData)callbackData;
		using (cardActorLoadedData.m_fullDef)
		{
			m_cardReady = true;
			m_cardActor = go.GetComponent<Actor>();
			m_cardActor.SetCardDef(cardActorLoadedData.m_fullDef.DisposableCardDef);
			m_cardActor.SetEntityDef(cardActorLoadedData.m_fullDef.EntityDef);
			m_cardActor.ContactShadow(visible: true);
			m_cardActor.SetPremium(cardActorLoadedData.m_premium);
			m_cardActor.UpdateAllComponents();
			GameUtils.SetParent(m_cardActor, m_cardBone, withRotation: true);
			SceneUtils.SetLayer(m_cardActor, m_cardBone.gameObject.layer);
		}
	}

	private IEnumerator ShowWhenReady()
	{
		while (!m_backgroundReady || !m_cardReady)
		{
			yield return new WaitForEndOfFrame();
		}
		Show();
	}

	private void SetUpPopup(Info info)
	{
		if (m_headerText != null)
		{
			m_headerText.Text = info.m_headerText;
		}
		if (m_bodyText != null)
		{
			m_bodyText.Text = info.m_bodyText;
		}
		if (m_cancelButton != null)
		{
			m_cancelButton.SetText(info.m_buttonText);
		}
		if (m_backgroundMaterial != null)
		{
			m_background.SetMaterial(m_backgroundMaterial);
		}
	}
}
