using UnityEngine;

[CustomEditClass]
public class GeneralStoreHeroesContentDisplay : MonoBehaviour
{
	public UberText m_heroName;

	public UberText m_className;

	public GameObject m_renderArtQuad;

	public UIBButton m_previewToggle;

	public Animator m_keyArtAnimation;

	public MeshRenderer m_classIcon;

	public MeshRenderer m_fauxPlateTexture;

	public MeshRenderer m_backgroundFrame;

	public int m_backgroundMaterialIndex;

	private Texture m_defaultBackgroundTexture;

	public GameObject m_heroContainer;

	public GameObject m_heroPowerContainer;

	public GameObject m_cardBackContainer;

	public GameObject m_previewButtonFX;

	public GameObject m_purchasedCheckMark;

	public GeneralStoreHeroesContentLite m_parentLite;

	private GeneralStoreHeroesContent m_parent;

	private CollectionHeroDef m_currentHeroAsset;

	private GameObject m_cardBack;

	private Actor m_heroActor;

	private Actor m_heroPowerActor;

	private bool m_keyArtShowing = true;

	private CardSoundSpell m_previewEmote;

	private CardSoundSpell m_purchaseEmote;

	private MeshRenderer m_keyArt;

	private void Awake()
	{
		if (m_defaultBackgroundTexture == null && m_backgroundFrame != null && m_backgroundMaterialIndex >= 0 && m_backgroundMaterialIndex < m_backgroundFrame.GetMaterials().Count)
		{
			m_defaultBackgroundTexture = m_backgroundFrame.GetMaterial(m_backgroundMaterialIndex).GetTexture("_MainTex");
		}
		m_previewToggle.AddEventListener(UIEventType.RELEASE, delegate
		{
			TogglePreview();
		});
	}

	public void SetKeyArtRenderer(MeshRenderer keyArtRenderer)
	{
		m_keyArt = keyArtRenderer;
	}

	public void PlayPreviewEmote()
	{
		if (!(m_previewEmote == null) && !(Box.Get() == null) && !(Box.Get().GetCamera() == null))
		{
			m_previewEmote.SetPosition(Box.Get().GetCamera().transform.position);
			m_previewEmote.Reactivate();
		}
	}

	public void PlayPurchaseEmote()
	{
		if (!(m_purchaseEmote == null))
		{
			m_purchaseEmote.SetPosition(Box.Get().GetCamera().transform.position);
			m_purchaseEmote.Reactivate();
		}
	}

	public void SetParent(GeneralStoreHeroesContent parent)
	{
		m_parent = parent;
	}

	public void Init()
	{
		if (m_heroActor == null)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", AssetLoadingOptions.IgnorePrefabPosition);
			m_heroActor = gameObject.GetComponent<Actor>();
			m_heroActor.SetUnlit();
			m_heroActor.Show();
			m_heroActor.GetHealthObject().Hide();
			m_heroActor.GetAttackObject().Hide();
			GameUtils.SetParent(m_heroActor, m_heroContainer, withRotation: true);
			SceneUtils.SetLayer(m_heroActor, m_heroContainer.layer);
		}
		if (m_heroPowerActor == null)
		{
			GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab("Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af", AssetLoadingOptions.IgnorePrefabPosition);
			m_heroPowerActor = gameObject2.GetComponent<Actor>();
			m_heroPowerActor.SetUnlit();
			m_heroPowerActor.Show();
			GameUtils.SetParent(m_heroPowerActor, m_heroPowerContainer, withRotation: true);
			SceneUtils.SetLayer(m_heroPowerActor, m_heroPowerContainer.layer);
		}
	}

	public void ShowPurchasedCheckmark(bool show)
	{
		if (m_purchasedCheckMark != null)
		{
			m_purchasedCheckMark.SetActive(show);
		}
	}

	public void UpdateFrame(CardHeroDbfRecord cardHeroDbfRecord, int cardBackIdx, CollectionHeroDef heroDef)
	{
		Init();
		if (heroDef.m_fauxPlateTexture != null)
		{
			m_fauxPlateTexture.GetMaterial().SetTexture("_MainTex", heroDef.m_fauxPlateTexture);
		}
		m_keyArt.SetMaterial(heroDef.m_previewMaterial.GetMaterial());
		string heroUberShaderAnimationPath = heroDef.GetHeroUberShaderAnimationPath();
		if (!string.IsNullOrEmpty(heroUberShaderAnimationPath))
		{
			UberShaderAnimation uberShaderAnimation = AssetLoader.Get().LoadUberAnimation(heroUberShaderAnimationPath, usePrefabPosition: false);
			if (uberShaderAnimation == null)
			{
				Error.AddDevFatal("Failed to load animation {0} for {1}", heroUberShaderAnimationPath, heroDef);
			}
			else
			{
				UberShaderController uberShaderController = m_keyArt.GetComponent<UberShaderController>();
				if (uberShaderController == null)
				{
					uberShaderController = m_keyArt.gameObject.AddComponent<UberShaderController>();
				}
				uberShaderController.UberShaderAnimation = uberShaderAnimation;
				uberShaderController.m_MaterialIndex = 0;
			}
		}
		DefLoader.Get().LoadFullDef(GameUtils.TranslateDbIdToCardId(cardHeroDbfRecord.CardId), delegate(string heroCardId, DefLoader.DisposableFullDef heroFullDef, object data1)
		{
			using (heroFullDef)
			{
				m_heroActor.SetPremium(TAG_PREMIUM.NORMAL);
				m_heroActor.SetFullDef(heroFullDef);
				m_heroActor.UpdateAllComponents();
				m_heroActor.Hide();
				m_heroName.Text = heroFullDef.EntityDef.GetName();
				m_className.Text = GameStrings.GetClassName(heroFullDef.EntityDef.GetClass());
				string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(heroCardId);
				DefLoader.Get().LoadFullDef(heroPowerCardIdFromHero, delegate(string powerCardId, DefLoader.DisposableFullDef powerDef, object data2)
				{
					using (powerDef)
					{
						m_heroPowerActor.SetPremium(TAG_PREMIUM.GOLDEN);
						m_heroPowerActor.SetFullDef(powerDef);
						m_heroPowerActor.UpdateAllComponents();
						m_heroPowerActor.Hide();
					}
				});
				if (CollectionPageManager.s_classTextureOffsets.TryGetValue(heroFullDef.EntityDef.GetClass(), out var value))
				{
					m_classIcon.GetMaterial().SetTextureOffset("_MainTex", value);
				}
				ClearEmotes();
				if (heroDef.m_storePreviewEmote != 0)
				{
					GameUtils.LoadCardDefEmoteSound(heroFullDef.CardDef.m_EmoteDefs, heroDef.m_storePreviewEmote, delegate(CardSoundSpell spell)
					{
						if (!(spell == null))
						{
							m_previewEmote = spell;
							GameUtils.SetParent(m_previewEmote, this);
						}
					});
				}
				if (heroDef.m_storePurchaseEmote != 0)
				{
					GameUtils.LoadCardDefEmoteSound(heroFullDef.CardDef.m_EmoteDefs, heroDef.m_storePurchaseEmote, delegate(CardSoundSpell spell)
					{
						if (!(spell == null))
						{
							m_purchaseEmote = spell;
							GameUtils.SetParent(m_purchaseEmote, this);
						}
					});
				}
			}
		});
		if (m_cardBack != null)
		{
			Object.Destroy(m_cardBack);
			m_cardBack = null;
		}
		if (cardBackIdx != 0)
		{
			CardBackManager.Get().LoadCardBackByIndex(cardBackIdx, delegate(CardBackManager.LoadCardBackData cardBackData)
			{
				GameObject gameObject = cardBackData.m_GameObject;
				gameObject.name = "CARD_BACK_" + cardBackIdx;
				m_cardBack = gameObject;
				SceneUtils.SetLayer(gameObject, m_cardBackContainer.gameObject.layer);
				GameUtils.SetParent(gameObject, m_cardBackContainer);
				m_cardBack.transform.localPosition = Vector3.zero;
				m_cardBack.transform.localScale = Vector3.one;
				m_cardBack.transform.localRotation = Quaternion.identity;
				AnimationUtil.FloatyPosition(m_cardBack, 0.05f, 10f);
			});
		}
		if (!(m_backgroundFrame != null) || m_backgroundMaterialIndex < 0 || m_backgroundMaterialIndex > m_backgroundFrame.GetMaterials().Count)
		{
			return;
		}
		Texture texture = m_defaultBackgroundTexture;
		if (!string.IsNullOrEmpty(cardHeroDbfRecord.StoreBackgroundTexture))
		{
			Texture texture2 = AssetLoader.Get().LoadTexture(cardHeroDbfRecord.StoreBackgroundTexture);
			if (texture2 != null)
			{
				texture = texture2;
			}
		}
		if (texture != null)
		{
			m_backgroundFrame.GetMaterial(m_backgroundMaterialIndex).SetTexture("_MainTex", texture);
		}
	}

	public void TogglePreview()
	{
		if (m_parentLite != null)
		{
			if (!string.IsNullOrEmpty(m_parentLite.m_previewButtonClick))
			{
				SoundManager.Get().LoadAndPlay(m_parentLite.m_previewButtonClick);
			}
		}
		else if (!string.IsNullOrEmpty(m_parent.m_previewButtonClick))
		{
			SoundManager.Get().LoadAndPlay(m_parent.m_previewButtonClick);
		}
		PlayKeyArtAnimation(m_keyArtShowing);
		m_keyArtShowing = !m_keyArtShowing;
		if (!m_keyArtShowing)
		{
			m_heroActor.Show();
			m_heroPowerActor.Show();
			PlayPreviewEmote();
		}
		else
		{
			m_heroActor.Hide();
			m_heroPowerActor.Hide();
		}
	}

	public void ResetPreview()
	{
		m_keyArtShowing = true;
		m_keyArtAnimation.enabled = true;
		m_keyArtAnimation.StopPlayback();
		if (m_parentLite != null)
		{
			m_keyArtAnimation.Play(m_parentLite.m_keyArtAppearAnim, -1, 1f);
		}
		else
		{
			m_keyArtAnimation.Play(m_parent.m_keyArtAppearAnim, -1, 1f);
		}
		m_previewButtonFX.SetActive(value: false);
	}

	private void PlayKeyArtAnimation(bool showPreview)
	{
		string stateName;
		string text;
		if (showPreview)
		{
			if (m_parentLite != null)
			{
				stateName = m_parentLite.m_keyArtFadeAnim;
				text = m_parentLite.m_keyArtFadeSound;
			}
			else
			{
				stateName = m_parent.m_keyArtFadeAnim;
				text = m_parent.m_keyArtFadeSound;
			}
		}
		else if (m_parentLite != null)
		{
			stateName = m_parentLite.m_keyArtAppearAnim;
			text = m_parentLite.m_keyArtAppearSound;
		}
		else
		{
			stateName = m_parent.m_keyArtAppearAnim;
			text = m_parent.m_keyArtAppearSound;
		}
		m_previewButtonFX.SetActive(showPreview);
		if (!string.IsNullOrEmpty(text))
		{
			SoundManager.Get().LoadAndPlay(text);
		}
		m_keyArtAnimation.enabled = true;
		m_keyArtAnimation.StopPlayback();
		m_keyArtAnimation.Play(stateName, -1, 0f);
	}

	private void ClearEmotes()
	{
		if (m_previewEmote != null)
		{
			Object.Destroy(m_previewEmote.gameObject);
		}
		if (m_purchaseEmote != null)
		{
			Object.Destroy(m_purchaseEmote.gameObject);
		}
	}
}
