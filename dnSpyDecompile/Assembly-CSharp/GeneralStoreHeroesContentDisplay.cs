using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006F9 RID: 1785
[CustomEditClass]
public class GeneralStoreHeroesContentDisplay : MonoBehaviour
{
	// Token: 0x060063A7 RID: 25511 RVA: 0x002073F8 File Offset: 0x002055F8
	private void Awake()
	{
		if (this.m_defaultBackgroundTexture == null && this.m_backgroundFrame != null && this.m_backgroundMaterialIndex >= 0 && this.m_backgroundMaterialIndex < this.m_backgroundFrame.GetMaterials().Count)
		{
			this.m_defaultBackgroundTexture = this.m_backgroundFrame.GetMaterial(this.m_backgroundMaterialIndex).GetTexture("_MainTex");
		}
		this.m_previewToggle.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.TogglePreview();
		});
	}

	// Token: 0x060063A8 RID: 25512 RVA: 0x0020747C File Offset: 0x0020567C
	public void SetKeyArtRenderer(MeshRenderer keyArtRenderer)
	{
		this.m_keyArt = keyArtRenderer;
	}

	// Token: 0x060063A9 RID: 25513 RVA: 0x00207488 File Offset: 0x00205688
	public void PlayPreviewEmote()
	{
		if (this.m_previewEmote == null)
		{
			return;
		}
		if (Box.Get() == null || Box.Get().GetCamera() == null)
		{
			return;
		}
		this.m_previewEmote.SetPosition(Box.Get().GetCamera().transform.position);
		this.m_previewEmote.Reactivate();
	}

	// Token: 0x060063AA RID: 25514 RVA: 0x002074EE File Offset: 0x002056EE
	public void PlayPurchaseEmote()
	{
		if (this.m_purchaseEmote == null)
		{
			return;
		}
		this.m_purchaseEmote.SetPosition(Box.Get().GetCamera().transform.position);
		this.m_purchaseEmote.Reactivate();
	}

	// Token: 0x060063AB RID: 25515 RVA: 0x00207529 File Offset: 0x00205729
	public void SetParent(GeneralStoreHeroesContent parent)
	{
		this.m_parent = parent;
	}

	// Token: 0x060063AC RID: 25516 RVA: 0x00207534 File Offset: 0x00205734
	public void Init()
	{
		if (this.m_heroActor == null)
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Card_Play_Hero.prefab:42cbbd2c4969afb46b3887bb628de19d", AssetLoadingOptions.IgnorePrefabPosition);
			this.m_heroActor = gameObject.GetComponent<Actor>();
			this.m_heroActor.SetUnlit();
			this.m_heroActor.Show();
			this.m_heroActor.GetHealthObject().Hide();
			this.m_heroActor.GetAttackObject().Hide();
			GameUtils.SetParent(this.m_heroActor, this.m_heroContainer, true);
			SceneUtils.SetLayer(this.m_heroActor, this.m_heroContainer.layer);
		}
		if (this.m_heroPowerActor == null)
		{
			GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab("Card_Play_HeroPower.prefab:a3794839abb947146903a26be13e09af", AssetLoadingOptions.IgnorePrefabPosition);
			this.m_heroPowerActor = gameObject2.GetComponent<Actor>();
			this.m_heroPowerActor.SetUnlit();
			this.m_heroPowerActor.Show();
			GameUtils.SetParent(this.m_heroPowerActor, this.m_heroPowerContainer, true);
			SceneUtils.SetLayer(this.m_heroPowerActor, this.m_heroPowerContainer.layer);
		}
	}

	// Token: 0x060063AD RID: 25517 RVA: 0x00207640 File Offset: 0x00205840
	public void ShowPurchasedCheckmark(bool show)
	{
		if (this.m_purchasedCheckMark != null)
		{
			this.m_purchasedCheckMark.SetActive(show);
		}
	}

	// Token: 0x060063AE RID: 25518 RVA: 0x0020765C File Offset: 0x0020585C
	public void UpdateFrame(CardHeroDbfRecord cardHeroDbfRecord, int cardBackIdx, CollectionHeroDef heroDef)
	{
		this.Init();
		if (heroDef.m_fauxPlateTexture != null)
		{
			this.m_fauxPlateTexture.GetMaterial().SetTexture("_MainTex", heroDef.m_fauxPlateTexture);
		}
		this.m_keyArt.SetMaterial(heroDef.m_previewMaterial.GetMaterial());
		string heroUberShaderAnimationPath = heroDef.GetHeroUberShaderAnimationPath();
		if (!string.IsNullOrEmpty(heroUberShaderAnimationPath))
		{
			UberShaderAnimation uberShaderAnimation = AssetLoader.Get().LoadUberAnimation(heroUberShaderAnimationPath, false, false);
			if (uberShaderAnimation == null)
			{
				Error.AddDevFatal("Failed to load animation {0} for {1}", new object[]
				{
					heroUberShaderAnimationPath,
					heroDef
				});
			}
			else
			{
				UberShaderController uberShaderController = this.m_keyArt.GetComponent<UberShaderController>();
				if (uberShaderController == null)
				{
					uberShaderController = this.m_keyArt.gameObject.AddComponent<UberShaderController>();
				}
				uberShaderController.UberShaderAnimation = uberShaderAnimation;
				uberShaderController.m_MaterialIndex = 0;
			}
		}
		DefLoader.LoadDefCallback<DefLoader.DisposableFullDef> <>9__2;
		GameUtils.EmoteSoundLoaded <>9__3;
		GameUtils.EmoteSoundLoaded <>9__4;
		DefLoader.Get().LoadFullDef(GameUtils.TranslateDbIdToCardId(cardHeroDbfRecord.CardId, false), delegate(string heroCardId, DefLoader.DisposableFullDef heroFullDef, object data1)
		{
			try
			{
				this.m_heroActor.SetPremium(TAG_PREMIUM.NORMAL);
				this.m_heroActor.SetFullDef(heroFullDef);
				this.m_heroActor.UpdateAllComponents();
				this.m_heroActor.Hide();
				this.m_heroName.Text = heroFullDef.EntityDef.GetName();
				this.m_className.Text = GameStrings.GetClassName(heroFullDef.EntityDef.GetClass());
				string heroPowerCardIdFromHero = GameUtils.GetHeroPowerCardIdFromHero(heroCardId);
				DefLoader defLoader = DefLoader.Get();
				string cardId = heroPowerCardIdFromHero;
				DefLoader.LoadDefCallback<DefLoader.DisposableFullDef> callback;
				if ((callback = <>9__2) == null)
				{
					callback = (<>9__2 = delegate(string powerCardId, DefLoader.DisposableFullDef powerDef, object data2)
					{
						try
						{
							this.m_heroPowerActor.SetPremium(TAG_PREMIUM.GOLDEN);
							this.m_heroPowerActor.SetFullDef(powerDef);
							this.m_heroPowerActor.UpdateAllComponents();
							this.m_heroPowerActor.Hide();
						}
						finally
						{
							if (powerDef != null)
							{
								((IDisposable)powerDef).Dispose();
							}
						}
					});
				}
				defLoader.LoadFullDef(cardId, callback, null, null);
				Vector2 value;
				if (CollectionPageManager.s_classTextureOffsets.TryGetValue(heroFullDef.EntityDef.GetClass(), out value))
				{
					this.m_classIcon.GetMaterial().SetTextureOffset("_MainTex", value);
				}
				this.ClearEmotes();
				if (heroDef.m_storePreviewEmote != EmoteType.INVALID)
				{
					List<EmoteEntryDef> emoteDefs = heroFullDef.CardDef.m_EmoteDefs;
					EmoteType storePreviewEmote = heroDef.m_storePreviewEmote;
					GameUtils.EmoteSoundLoaded callback2;
					if ((callback2 = <>9__3) == null)
					{
						callback2 = (<>9__3 = delegate(CardSoundSpell spell)
						{
							if (spell == null)
							{
								return;
							}
							this.m_previewEmote = spell;
							GameUtils.SetParent(this.m_previewEmote, this, false);
						});
					}
					GameUtils.LoadCardDefEmoteSound(emoteDefs, storePreviewEmote, callback2);
				}
				if (heroDef.m_storePurchaseEmote != EmoteType.INVALID)
				{
					List<EmoteEntryDef> emoteDefs2 = heroFullDef.CardDef.m_EmoteDefs;
					EmoteType storePurchaseEmote = heroDef.m_storePurchaseEmote;
					GameUtils.EmoteSoundLoaded callback3;
					if ((callback3 = <>9__4) == null)
					{
						callback3 = (<>9__4 = delegate(CardSoundSpell spell)
						{
							if (spell == null)
							{
								return;
							}
							this.m_purchaseEmote = spell;
							GameUtils.SetParent(this.m_purchaseEmote, this, false);
						});
					}
					GameUtils.LoadCardDefEmoteSound(emoteDefs2, storePurchaseEmote, callback3);
				}
			}
			finally
			{
				if (heroFullDef != null)
				{
					((IDisposable)heroFullDef).Dispose();
				}
			}
		}, null, null);
		if (this.m_cardBack != null)
		{
			UnityEngine.Object.Destroy(this.m_cardBack);
			this.m_cardBack = null;
		}
		if (cardBackIdx != 0)
		{
			CardBackManager.Get().LoadCardBackByIndex(cardBackIdx, delegate(CardBackManager.LoadCardBackData cardBackData)
			{
				GameObject gameObject = cardBackData.m_GameObject;
				gameObject.name = "CARD_BACK_" + cardBackIdx;
				this.m_cardBack = gameObject;
				SceneUtils.SetLayer(gameObject, this.m_cardBackContainer.gameObject.layer, null);
				GameUtils.SetParent(gameObject, this.m_cardBackContainer, false);
				this.m_cardBack.transform.localPosition = Vector3.zero;
				this.m_cardBack.transform.localScale = Vector3.one;
				this.m_cardBack.transform.localRotation = Quaternion.identity;
				AnimationUtil.FloatyPosition(this.m_cardBack, 0.05f, 10f);
			}, null);
		}
		if (this.m_backgroundFrame != null && this.m_backgroundMaterialIndex >= 0 && this.m_backgroundMaterialIndex <= this.m_backgroundFrame.GetMaterials().Count)
		{
			Texture texture = this.m_defaultBackgroundTexture;
			if (!string.IsNullOrEmpty(cardHeroDbfRecord.StoreBackgroundTexture))
			{
				Texture texture2 = AssetLoader.Get().LoadTexture(cardHeroDbfRecord.StoreBackgroundTexture, false, false);
				if (texture2 != null)
				{
					texture = texture2;
				}
			}
			if (texture != null)
			{
				this.m_backgroundFrame.GetMaterial(this.m_backgroundMaterialIndex).SetTexture("_MainTex", texture);
			}
		}
	}

	// Token: 0x060063AF RID: 25519 RVA: 0x0020785C File Offset: 0x00205A5C
	public void TogglePreview()
	{
		if (this.m_parentLite != null)
		{
			if (!string.IsNullOrEmpty(this.m_parentLite.m_previewButtonClick))
			{
				SoundManager.Get().LoadAndPlay(this.m_parentLite.m_previewButtonClick);
			}
		}
		else if (!string.IsNullOrEmpty(this.m_parent.m_previewButtonClick))
		{
			SoundManager.Get().LoadAndPlay(this.m_parent.m_previewButtonClick);
		}
		this.PlayKeyArtAnimation(this.m_keyArtShowing);
		this.m_keyArtShowing = !this.m_keyArtShowing;
		if (!this.m_keyArtShowing)
		{
			this.m_heroActor.Show();
			this.m_heroPowerActor.Show();
			this.PlayPreviewEmote();
			return;
		}
		this.m_heroActor.Hide();
		this.m_heroPowerActor.Hide();
	}

	// Token: 0x060063B0 RID: 25520 RVA: 0x00207928 File Offset: 0x00205B28
	public void ResetPreview()
	{
		this.m_keyArtShowing = true;
		this.m_keyArtAnimation.enabled = true;
		this.m_keyArtAnimation.StopPlayback();
		if (this.m_parentLite != null)
		{
			this.m_keyArtAnimation.Play(this.m_parentLite.m_keyArtAppearAnim, -1, 1f);
		}
		else
		{
			this.m_keyArtAnimation.Play(this.m_parent.m_keyArtAppearAnim, -1, 1f);
		}
		this.m_previewButtonFX.SetActive(false);
	}

	// Token: 0x060063B1 RID: 25521 RVA: 0x002079A8 File Offset: 0x00205BA8
	private void PlayKeyArtAnimation(bool showPreview)
	{
		string stateName;
		string text;
		if (showPreview)
		{
			if (this.m_parentLite != null)
			{
				stateName = this.m_parentLite.m_keyArtFadeAnim;
				text = this.m_parentLite.m_keyArtFadeSound;
			}
			else
			{
				stateName = this.m_parent.m_keyArtFadeAnim;
				text = this.m_parent.m_keyArtFadeSound;
			}
		}
		else if (this.m_parentLite != null)
		{
			stateName = this.m_parentLite.m_keyArtAppearAnim;
			text = this.m_parentLite.m_keyArtAppearSound;
		}
		else
		{
			stateName = this.m_parent.m_keyArtAppearAnim;
			text = this.m_parent.m_keyArtAppearSound;
		}
		this.m_previewButtonFX.SetActive(showPreview);
		if (!string.IsNullOrEmpty(text))
		{
			SoundManager.Get().LoadAndPlay(text);
		}
		this.m_keyArtAnimation.enabled = true;
		this.m_keyArtAnimation.StopPlayback();
		this.m_keyArtAnimation.Play(stateName, -1, 0f);
	}

	// Token: 0x060063B2 RID: 25522 RVA: 0x00207A87 File Offset: 0x00205C87
	private void ClearEmotes()
	{
		if (this.m_previewEmote != null)
		{
			UnityEngine.Object.Destroy(this.m_previewEmote.gameObject);
		}
		if (this.m_purchaseEmote != null)
		{
			UnityEngine.Object.Destroy(this.m_purchaseEmote.gameObject);
		}
	}

	// Token: 0x04005296 RID: 21142
	public UberText m_heroName;

	// Token: 0x04005297 RID: 21143
	public UberText m_className;

	// Token: 0x04005298 RID: 21144
	public GameObject m_renderArtQuad;

	// Token: 0x04005299 RID: 21145
	public UIBButton m_previewToggle;

	// Token: 0x0400529A RID: 21146
	public Animator m_keyArtAnimation;

	// Token: 0x0400529B RID: 21147
	public MeshRenderer m_classIcon;

	// Token: 0x0400529C RID: 21148
	public MeshRenderer m_fauxPlateTexture;

	// Token: 0x0400529D RID: 21149
	public MeshRenderer m_backgroundFrame;

	// Token: 0x0400529E RID: 21150
	public int m_backgroundMaterialIndex;

	// Token: 0x0400529F RID: 21151
	private Texture m_defaultBackgroundTexture;

	// Token: 0x040052A0 RID: 21152
	public GameObject m_heroContainer;

	// Token: 0x040052A1 RID: 21153
	public GameObject m_heroPowerContainer;

	// Token: 0x040052A2 RID: 21154
	public GameObject m_cardBackContainer;

	// Token: 0x040052A3 RID: 21155
	public GameObject m_previewButtonFX;

	// Token: 0x040052A4 RID: 21156
	public GameObject m_purchasedCheckMark;

	// Token: 0x040052A5 RID: 21157
	public GeneralStoreHeroesContentLite m_parentLite;

	// Token: 0x040052A6 RID: 21158
	private GeneralStoreHeroesContent m_parent;

	// Token: 0x040052A7 RID: 21159
	private CollectionHeroDef m_currentHeroAsset;

	// Token: 0x040052A8 RID: 21160
	private GameObject m_cardBack;

	// Token: 0x040052A9 RID: 21161
	private Actor m_heroActor;

	// Token: 0x040052AA RID: 21162
	private Actor m_heroPowerActor;

	// Token: 0x040052AB RID: 21163
	private bool m_keyArtShowing = true;

	// Token: 0x040052AC RID: 21164
	private CardSoundSpell m_previewEmote;

	// Token: 0x040052AD RID: 21165
	private CardSoundSpell m_purchaseEmote;

	// Token: 0x040052AE RID: 21166
	private MeshRenderer m_keyArt;
}
