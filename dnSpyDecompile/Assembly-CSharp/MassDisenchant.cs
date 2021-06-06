using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000134 RID: 308
public class MassDisenchant : MonoBehaviour
{
	// Token: 0x06001436 RID: 5174 RVA: 0x00073C98 File Offset: 0x00071E98
	private void Awake()
	{
		MassDisenchant.s_Instance = this;
		this.m_headlineText.Text = GameStrings.Get("GLUE_MASS_DISENCHANT_HEADLINE");
		this.m_detailsHeadlineText.Text = GameStrings.Get("GLUE_MASS_DISENCHANT_DETAILS_HEADLINE");
		this.m_disenchantButton.SetText(GameStrings.Get("GLUE_MASS_DISENCHANT_BUTTON_TEXT"));
		if (this.m_detailsText != null)
		{
			this.m_detailsText.Text = GameStrings.Get("GLUE_MASS_DISENCHANT_DETAILS");
		}
		if (this.m_singleSubHeadlineText != null)
		{
			this.m_singleSubHeadlineText.Text = GameStrings.Get("GLUE_MASS_DISENCHANT_SUB_HEADLINE_TEXT");
		}
		if (this.m_doubleSubHeadlineText != null)
		{
			this.m_doubleSubHeadlineText.Text = GameStrings.Get("GLUE_MASS_DISENCHANT_SUB_HEADLINE_TEXT");
		}
		this.m_disenchantButton.SetUserOverYOffset(-0.04409015f);
		foreach (DisenchantBar disenchantBar in this.m_singleDisenchantBars)
		{
			disenchantBar.Init();
		}
		foreach (DisenchantBar disenchantBar2 in this.m_doubleDisenchantBars)
		{
			disenchantBar2.Init();
		}
		CollectionManager.Get().RegisterMassDisenchantListener(new CollectionManager.OnMassDisenchant(this.OnMassDisenchant));
	}

	// Token: 0x06001437 RID: 5175 RVA: 0x00073DFC File Offset: 0x00071FFC
	private void Start()
	{
		this.m_disenchantButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDisenchantButtonPressed));
		this.m_disenchantButton.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(this.OnDisenchantButtonOver));
		this.m_disenchantButton.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(this.OnDisenchantButtonOut));
		if (this.m_infoButton != null)
		{
			this.m_infoButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInfoButtonPressed));
		}
	}

	// Token: 0x06001438 RID: 5176 RVA: 0x00073E7B File Offset: 0x0007207B
	public static MassDisenchant Get()
	{
		return MassDisenchant.s_Instance;
	}

	// Token: 0x06001439 RID: 5177 RVA: 0x00073E82 File Offset: 0x00072082
	public void Show()
	{
		this.m_root.SetActive(true);
	}

	// Token: 0x0600143A RID: 5178 RVA: 0x00073E90 File Offset: 0x00072090
	public void Hide()
	{
		this.m_root.SetActive(false);
		this.BlockCurrencyFrame(false);
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x00073EA5 File Offset: 0x000720A5
	public bool IsShown()
	{
		return this.m_root.activeSelf;
	}

	// Token: 0x0600143C RID: 5180 RVA: 0x00073EB4 File Offset: 0x000720B4
	private void OnDestroy()
	{
		foreach (GameObject gameObject in this.m_cleanupObjects)
		{
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		CollectionManager.Get().RemoveMassDisenchantListener(new CollectionManager.OnMassDisenchant(this.OnMassDisenchant));
		this.BlockCurrencyFrame(false);
	}

	// Token: 0x0600143D RID: 5181 RVA: 0x00073F2C File Offset: 0x0007212C
	public int GetTotalAmount()
	{
		return this.m_totalAmount;
	}

	// Token: 0x0600143E RID: 5182 RVA: 0x00073F34 File Offset: 0x00072134
	public void UpdateContents(List<CollectibleCard> disenchantCards)
	{
		List<CollectibleCard> list = (from c in disenchantCards
		where c.PremiumType == TAG_PREMIUM.GOLDEN
		select c).ToList<CollectibleCard>();
		this.m_useSingle = (list.Count == 0);
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_useSingle = true;
		}
		List<DisenchantBar> list2 = this.m_useSingle ? this.m_singleDisenchantBars : this.m_doubleDisenchantBars;
		foreach (DisenchantBar disenchantBar in list2)
		{
			disenchantBar.Reset();
		}
		this.m_totalAmount = 0;
		this.m_totalCardsToDisenchant = 0;
		using (List<CollectibleCard>.Enumerator enumerator2 = disenchantCards.GetEnumerator())
		{
			while (enumerator2.MoveNext())
			{
				MassDisenchant.<>c__DisplayClass37_0 CS$<>8__locals1 = new MassDisenchant.<>c__DisplayClass37_0();
				CS$<>8__locals1.card = enumerator2.Current;
				if (CS$<>8__locals1.card.IsCraftable)
				{
					NetCache.CardValue cardValue = CraftingManager.Get().GetCardValue(CS$<>8__locals1.card.CardId, CS$<>8__locals1.card.PremiumType);
					if (cardValue != null)
					{
						EntityDef entityDef = DefLoader.Get().GetEntityDef(CS$<>8__locals1.card.CardId);
						int num = cardValue.GetSellValue() * CS$<>8__locals1.card.DisenchantCount;
						DisenchantBar disenchantBar2 = list2.Find((DisenchantBar obj) => (obj.m_premiumType == CS$<>8__locals1.card.PremiumType || UniversalInputManager.UsePhoneUI) && obj.m_rarity == entityDef.GetRarity());
						if (disenchantBar2 == null)
						{
							Debug.LogWarning(string.Format("MassDisenchant.UpdateContents(): Could not find {0} bar to modify for card {1} (premium {2}, disenchant count {3})", new object[]
							{
								this.m_useSingle ? "single" : "double",
								entityDef,
								CS$<>8__locals1.card.PremiumType,
								CS$<>8__locals1.card.DisenchantCount
							}));
						}
						else
						{
							disenchantBar2.AddCards(CS$<>8__locals1.card.DisenchantCount, num, CS$<>8__locals1.card.PremiumType);
							this.m_totalCardsToDisenchant += CS$<>8__locals1.card.DisenchantCount;
							this.m_totalAmount += num;
						}
					}
				}
			}
		}
		if (this.m_totalAmount > 0)
		{
			this.m_singleRoot.SetActive(this.m_useSingle);
			if (this.m_doubleRoot != null)
			{
				this.m_doubleRoot.SetActive(!this.m_useSingle);
			}
			this.m_disenchantButton.SetEnabled(true, false);
		}
		foreach (DisenchantBar disenchantBar3 in list2)
		{
			disenchantBar3.UpdateVisuals(this.m_totalCardsToDisenchant);
		}
		this.m_totalAmountText.Text = GameStrings.Format("GLUE_MASS_DISENCHANT_TOTAL_AMOUNT", new object[]
		{
			this.m_totalAmount
		});
	}

	// Token: 0x0600143F RID: 5183 RVA: 0x00074284 File Offset: 0x00072484
	public IEnumerator StartHighlight()
	{
		yield return null;
		this.m_FX.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		yield break;
	}

	// Token: 0x06001440 RID: 5184 RVA: 0x00074294 File Offset: 0x00072494
	public void OnMassDisenchant(int amount)
	{
		GraphicsQuality renderQualityLevel = GraphicsManager.Get().RenderQualityLevel;
		int maxGlowBalls;
		if (renderQualityLevel != GraphicsQuality.Low)
		{
			if (renderQualityLevel != GraphicsQuality.Medium)
			{
				maxGlowBalls = 10;
			}
			else
			{
				maxGlowBalls = 6;
			}
		}
		else
		{
			maxGlowBalls = 3;
		}
		this.BlockUI(true);
		base.StartCoroutine(this.DoDisenchantAnims(maxGlowBalls, amount));
	}

	// Token: 0x06001441 RID: 5185 RVA: 0x000742DC File Offset: 0x000724DC
	private void BlockCurrencyFrame(bool block)
	{
		BnetBar bnetBar = BnetBar.Get();
		if (bnetBar == null)
		{
			return;
		}
		foreach (CurrencyFrame currencyFrame in bnetBar.CurrencyFrames)
		{
			currencyFrame.SetBlockedStatus(block);
		}
	}

	// Token: 0x06001442 RID: 5186 RVA: 0x00074338 File Offset: 0x00072538
	private void BlockUI(bool block = true)
	{
		this.BlockCurrencyFrame(block);
		this.m_FX.m_blockInteraction.SetActive(block);
	}

	// Token: 0x06001443 RID: 5187 RVA: 0x00074354 File Offset: 0x00072554
	private void OnDisenchantButtonOver(UIEvent e)
	{
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.MASS_DISENCHANT)
		{
			this.m_FX.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
			SoundManager.Get().LoadAndPlay("Hub_Mouseover.prefab:40130da7b734190479c527d6bca1a4a8");
			return;
		}
		this.m_FX.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
	}

	// Token: 0x06001444 RID: 5188 RVA: 0x000743AE File Offset: 0x000725AE
	private void OnDisenchantButtonOut(UIEvent e)
	{
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.MASS_DISENCHANT)
		{
			this.m_FX.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
			return;
		}
		this.m_FX.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
	}

	// Token: 0x06001445 RID: 5189 RVA: 0x000743EC File Offset: 0x000725EC
	private void OnDisenchantButtonPressed(UIEvent e)
	{
		Options.Get().SetBool(Option.HAS_DISENCHANTED, true);
		this.m_disenchantButton.SetEnabled(false, false);
		this.m_FX.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		this.BlockCurrencyFrame(true);
		this.m_preMassDisenchantDustValue = NetCache.Get().GetArcaneDustBalance();
		Network.Get().MassDisenchant();
	}

	// Token: 0x06001446 RID: 5190 RVA: 0x0007444C File Offset: 0x0007264C
	private void OnInfoButtonPressed(UIEvent e)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_MASS_DISENCHANT_BUTTON_TEXT");
		popupInfo.m_text = string.Format("{0}\n\n{1}", GameStrings.Get("GLUE_MASS_DISENCHANT_DETAILS_HEADLINE"), GameStrings.Get("GLUE_MASS_DISENCHANT_DETAILS"));
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	// Token: 0x06001447 RID: 5191 RVA: 0x000744AC File Offset: 0x000726AC
	private void Unbloomify(List<GameObject> glows, float newVal)
	{
		foreach (GameObject gameObject in glows)
		{
			gameObject.GetComponent<RenderToTexture>().m_BloomIntensity = newVal;
		}
	}

	// Token: 0x06001448 RID: 5192 RVA: 0x00074500 File Offset: 0x00072700
	private void UncolorTotal(float newVal)
	{
		this.m_totalAmountText.TextColor = Color.Lerp(Color.white, new Color(0.7f, 0.85f, 1f, 1f), newVal);
	}

	// Token: 0x06001449 RID: 5193 RVA: 0x00074534 File Offset: 0x00072734
	private void SetGemSaturation(List<DisenchantBar> disenchantBars, float saturation, bool onlyActive = false, bool onlyInactive = false)
	{
		foreach (DisenchantBar disenchantBar in disenchantBars)
		{
			int numCards = disenchantBar.GetNumCards();
			if ((onlyActive && numCards != 0) || (onlyInactive && numCards == 0) || (!onlyInactive && !onlyActive))
			{
				disenchantBar.m_rarityGem.GetComponent<Renderer>().GetMaterial().SetColor("_Fade", new Color(saturation, saturation, saturation, 1f));
			}
		}
	}

	// Token: 0x0600144A RID: 5194 RVA: 0x000745C0 File Offset: 0x000727C0
	private IEnumerator DoDisenchantAnims(int maxGlowBalls, int disenchantTotal)
	{
		if (disenchantTotal == 0)
		{
			yield return null;
		}
		this.m_origTotalScale = this.m_totalAmountText.transform.localScale;
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_origDustScale = ArcaneDustAmount.Get().m_dustJar.transform.localScale;
		}
		else
		{
			this.m_origDustScale = BnetBar.Get().GetCurrencyFrame(0).GetCurrencyIconContainer().transform.localScale;
		}
		List<DisenchantBar> disenchantBars = this.m_useSingle ? this.m_singleDisenchantBars : this.m_doubleDisenchantBars;
		float vigTime = 0.2f;
		FullScreenFXMgr.Get().Vignette(0.8f, vigTime, iTween.EaseType.easeInOutCubic, null, null);
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"from",
			1f,
			"to",
			0.3f,
			"time",
			vigTime,
			"onupdate",
			new Action<object>(delegate(object newVal)
			{
				this.SetGemSaturation(disenchantBars, (float)newVal, false, false);
			})
		}));
		if (this.m_sound.m_intro != string.Empty)
		{
			SoundManager.Get().LoadAndPlay(this.m_sound.m_intro);
		}
		yield return new WaitForSeconds(vigTime);
		float duration = 0.5f;
		float rate = duration / 20f;
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"from",
			0.3f,
			"to",
			1.75f,
			"time",
			1.5f * duration,
			"easeInType",
			iTween.EaseType.easeInCubic,
			"onupdate",
			new Action<object>(delegate(object newVal)
			{
				this.SetGemSaturation(disenchantBars, (float)newVal, true, false);
			})
		}));
		List<GameObject> glows = new List<GameObject>();
		if (this.m_FX.m_glowTotal != null)
		{
			glows.Add(this.m_FX.m_glowTotal);
		}
		this.m_totalAmountText.transform.localScale = this.m_origTotalScale * 2.54f;
		iTween.ScaleTo(this.m_totalAmountText.gameObject, iTween.Hash(new object[]
		{
			"scale",
			this.m_origTotalScale,
			"time",
			3.0
		}));
		if (this.m_FX.m_glowTotal != null)
		{
			this.m_FX.m_glowTotal.SetActive(true);
		}
		this.m_highestGlowBalls = 0;
		Color glowColor = new Color(0.7f, 0.85f, 1f, 1f);
		float origYSpeed = 0f;
		float origXSpeed = 0f;
		float origInten = 0f;
		foreach (DisenchantBar disenchantBar in disenchantBars)
		{
			int numCards = disenchantBar.GetNumCards();
			if (numCards > this.m_highestGlowBalls)
			{
				this.m_highestGlowBalls = numCards;
			}
		}
		this.m_highestGlowBalls = ((this.m_highestGlowBalls > maxGlowBalls) ? maxGlowBalls : this.m_highestGlowBalls);
		foreach (DisenchantBar disenchantBar2 in disenchantBars)
		{
			int numCards2 = disenchantBar2.GetNumCards();
			if (numCards2 > 0)
			{
				RarityFX rarityFX = this.GetRarityFX(disenchantBar2);
				int num = (numCards2 > maxGlowBalls) ? maxGlowBalls : numCards2;
				for (int j = 0; j < num; j++)
				{
					base.StartCoroutine(this.LaunchGlowball(disenchantBar2, rarityFX, j, num, this.m_highestGlowBalls));
				}
			}
		}
		int i = 0;
		Action<object> <>9__4;
		Action<object> <>9__5;
		Action<object> <>9__6;
		Action<object> <>9__7;
		while ((float)i < duration / rate)
		{
			float num2 = 0f;
			foreach (DisenchantBar disenchantBar3 in disenchantBars)
			{
				RaritySound raritySound = this.GetRaritySound(disenchantBar3);
				int numCards3 = disenchantBar3.GetNumCards();
				if (i == 0 && numCards3 != 0)
				{
					if (raritySound.m_drainSound != string.Empty)
					{
						SoundManager.Get().LoadAndPlay(raritySound.m_drainSound);
					}
					if (disenchantBar3.m_numGoldText != null && disenchantBar3.m_numGoldText.gameObject.activeSelf)
					{
						disenchantBar3.m_numGoldText.gameObject.SetActive(false);
						TransformUtil.SetLocalPosX(disenchantBar3.m_numCardsText, 2.902672f);
					}
					Vector3 localScale = disenchantBar3.m_numCardsText.gameObject.transform.localScale;
					iTween.ScaleFrom(disenchantBar3.m_numCardsText.gameObject, iTween.Hash(new object[]
					{
						"x",
						localScale.x * 2.28f,
						"y",
						localScale.y * 2.28f,
						"z",
						localScale.z * 2.28f,
						"time",
						3.0
					}));
					disenchantBar3.m_numCardsText.TextColor = glowColor;
					iTween.ColorTo(disenchantBar3.m_numCardsText.gameObject, iTween.Hash(new object[]
					{
						"r",
						1f,
						"g",
						1f,
						"b",
						1f,
						"time",
						3.0
					}));
					if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.High && disenchantBar3.m_glow != null)
					{
						glows.Add(disenchantBar3.m_glow);
						disenchantBar3.m_glow.GetComponent<RenderToTexture>().m_BloomIntensity = 0.01f;
						disenchantBar3.m_glow.SetActive(true);
					}
					Material material = disenchantBar3.m_rarityGem.GetComponent<Renderer>().GetMaterial();
					origYSpeed = material.GetFloat("_YSpeed");
					origXSpeed = material.GetFloat("_XSpeed");
					origInten = disenchantBar3.m_amountBar.GetComponent<Renderer>().GetMaterial().GetFloat("_Intensity");
					material.SetFloat("_YSpeed", -10f);
					material.SetFloat("_XSpeed", 20f);
				}
			}
			if (i == 0)
			{
				if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.High)
				{
					GameObject gameObject = base.gameObject;
					object[] array = new object[8];
					array[0] = "from";
					array[1] = 1f;
					array[2] = "to";
					array[3] = 0.1f;
					array[4] = "time";
					array[5] = duration * 3f;
					array[6] = "onupdate";
					int num3 = 7;
					Action<object> action;
					if ((action = <>9__4) == null)
					{
						action = (<>9__4 = delegate(object newVal)
						{
							this.Unbloomify(glows, (float)newVal);
						});
					}
					array[num3] = action;
					iTween.ValueTo(gameObject, iTween.Hash(array));
				}
				GameObject gameObject2 = base.gameObject;
				object[] array2 = new object[8];
				array2[0] = "from";
				array2[1] = 1f;
				array2[2] = "to";
				array2[3] = 0.1f;
				array2[4] = "time";
				array2[5] = duration * 3f;
				array2[6] = "onupdate";
				int num4 = 7;
				Action<object> action2;
				if ((action2 = <>9__5) == null)
				{
					action2 = (<>9__5 = delegate(object newVal)
					{
						this.UncolorTotal((float)newVal);
					});
				}
				array2[num4] = action2;
				iTween.ValueTo(gameObject2, iTween.Hash(array2));
				float num5 = (float)this.m_preMassDisenchantDustValue;
				GameObject gameObject3 = base.gameObject;
				object[] array3 = new object[10];
				array3[0] = "from";
				array3[1] = num5;
				array3[2] = "to";
				array3[3] = num5 + (float)disenchantTotal;
				array3[4] = "time";
				array3[5] = 3f * duration;
				array3[6] = "onupdate";
				int num6 = 7;
				Action<object> action3;
				if ((action3 = <>9__6) == null)
				{
					action3 = (<>9__6 = delegate(object newVal)
					{
						this.SetDustBalance((float)newVal);
					});
				}
				array3[num6] = action3;
				array3[8] = "oncomplete";
				int num7 = 9;
				Action<object> action4;
				if ((action4 = <>9__7) == null)
				{
					action4 = (<>9__7 = delegate(object newVal)
					{
						CollectionManager.Get().GetCollectibleDisplay().m_pageManager.UpdateMassDisenchant();
						FullScreenFXMgr.Get().StopVignette(vigTime, iTween.EaseType.easeInOutCubic, null, null);
						this.BlockUI(false);
					});
				}
				array3[num7] = action4;
				iTween.ValueTo(gameObject3, iTween.Hash(array3));
			}
			foreach (DisenchantBar disenchantBar4 in disenchantBars)
			{
				if (disenchantBar4.GetNumCards() != 0)
				{
					disenchantBar4.m_amountBar.GetComponent<Renderer>().GetMaterial().SetFloat("_Intensity", 2f);
					num2 += this.DrainBarAndDust(disenchantBar4, i, duration, rate);
				}
			}
			this.m_totalAmountText.Text = Convert.ToInt32(num2).ToString();
			yield return new WaitForSeconds(rate / duration);
			int num8 = i + 1;
			i = num8;
		}
		if (this.m_FX.m_glowTotal != null)
		{
			this.m_FX.m_glowTotal.SetActive(false);
		}
		this.m_totalAmountText.Text = "0";
		this.m_totalAmountText.TextColor = Color.white;
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"from",
			0.3f,
			"to",
			1f,
			"time",
			duration,
			"delay",
			vigTime,
			"onupdate",
			new Action<object>(delegate(object newVal)
			{
				this.SetGemSaturation(disenchantBars, (float)newVal, false, true);
			})
		}));
		iTween.ValueTo(base.gameObject, iTween.Hash(new object[]
		{
			"from",
			1.75f,
			"to",
			1f,
			"time",
			duration,
			"delay",
			vigTime,
			"onupdate",
			new Action<object>(delegate(object newVal)
			{
				this.SetGemSaturation(disenchantBars, (float)newVal, true, false);
			})
		}));
		using (List<DisenchantBar>.Enumerator enumerator = disenchantBars.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				DisenchantBar disenchantBar5 = enumerator.Current;
				if (disenchantBar5.m_glow != null)
				{
					disenchantBar5.m_glow.SetActive(false);
				}
				disenchantBar5.m_numCardsText.TextColor = Color.white;
				Material material2 = disenchantBar5.m_rarityGem.GetComponent<Renderer>().GetMaterial();
				material2.SetFloat("_YSpeed", origYSpeed);
				material2.SetFloat("_XSpeed", origXSpeed);
				disenchantBar5.m_amountBar.GetComponent<Renderer>().GetMaterial().SetFloat("_Intensity", origInten);
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x0600144B RID: 5195 RVA: 0x000745E0 File Offset: 0x000727E0
	private void SetDustBalance(float bal)
	{
		int num = (int)bal;
		if (UniversalInputManager.UsePhoneUI)
		{
			ArcaneDustAmount.Get().m_dustCount.Text = num.ToString();
			return;
		}
		if (Shop.Get() != null)
		{
			Shop.Get().DisplayCurrencyBalance(CurrencyType.DUST, (long)num);
		}
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x00074630 File Offset: 0x00072830
	private float DrainBarAndDust(DisenchantBar bar, int drainRun, float duration, float rate)
	{
		float num = (float)bar.GetNumCards();
		num -= (float)(drainRun + 1) * num / (duration / rate);
		if (num < 0f)
		{
			num = 0f;
		}
		float num2 = (float)bar.GetAmountDust();
		num2 -= (float)(drainRun + 1) * num2 / (duration / rate);
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		bar.m_numCardsText.Text = Convert.ToInt32(num).ToString();
		bar.m_amountText.Text = Convert.ToInt32(num2).ToString();
		float percent = 0f;
		if (this.m_totalCardsToDisenchant > 0)
		{
			percent = num / (float)this.m_totalCardsToDisenchant;
		}
		bar.SetPercent(percent);
		return num2;
	}

	// Token: 0x0600144D RID: 5197 RVA: 0x000746D8 File Offset: 0x000728D8
	private Vector3 GetRanBoxPt(GameObject box)
	{
		Vector3 localScale = box.transform.localScale;
		Vector3 position = box.transform.position;
		Vector3 b = new Vector3(UnityEngine.Random.Range(-localScale.x / 2f, localScale.x / 2f), UnityEngine.Random.Range(-localScale.y / 2f, localScale.y / 2f), UnityEngine.Random.Range(-localScale.z / 2f, localScale.z / 2f));
		return position + b;
	}

	// Token: 0x0600144E RID: 5198 RVA: 0x00074763 File Offset: 0x00072963
	private IEnumerator LaunchGlowball(DisenchantBar bar, RarityFX rareFX, int glowBallNum, int totalGlowBalls, int m_highestGlowBalls)
	{
		float num = 1f;
		float num2 = 0.02f;
		float num3 = (float)glowBallNum;
		float num4 = (num - num2 * (float)m_highestGlowBalls) / (float)totalGlowBalls;
		float min = num3 * num4 + num3 * num2;
		float max = (num3 + 1f) * num4 + (num3 + 1f) * num2;
		GameObject glowBall = UnityEngine.Object.Instantiate<GameObject>(this.m_FX.m_glowBall);
		this.m_cleanupObjects.Add(glowBall);
		glowBall.GetComponent<Renderer>().SetSharedMaterial(rareFX.glowBallMat);
		glowBall.GetComponent<TrailRenderer>().SetMaterial(rareFX.glowTrailMat);
		glowBall.transform.position = bar.m_rarityGem.transform.position;
		glowBall.transform.position = new Vector3(glowBall.transform.position.x, glowBall.transform.position.y + 0.5f, glowBall.transform.position.z);
		List<Vector3> curvePoints = new List<Vector3>();
		curvePoints.Add(glowBall.transform.position);
		if ((double)UnityEngine.Random.Range(0f, 1f) < 0.5)
		{
			curvePoints.Add(this.GetRanBoxPt(this.m_FX.m_gemBoxLeft1));
			curvePoints.Add(this.GetRanBoxPt(this.m_FX.m_gemBoxLeft2));
		}
		else
		{
			curvePoints.Add(this.GetRanBoxPt(this.m_FX.m_gemBoxRight1));
			curvePoints.Add(this.GetRanBoxPt(this.m_FX.m_gemBoxRight2));
		}
		GameObject dustJar;
		if (UniversalInputManager.UsePhoneUI)
		{
			dustJar = ArcaneDustAmount.Get().m_dustJar;
			curvePoints.Add(dustJar.transform.position);
		}
		else
		{
			dustJar = BnetBar.Get().GetCurrencyFrame(0).GetCurrencyIconContainer();
			curvePoints.Add(Camera.main.ViewportToWorldPoint(BaseUI.Get().m_BnetCamera.WorldToViewportPoint(dustJar.transform.position)));
		}
		yield return new WaitForSeconds(UnityEngine.Random.Range(min, max));
		RaritySound rareSound = this.GetRaritySound(bar);
		if (rareSound.m_missileSound != string.Empty)
		{
			SoundManager.Get().LoadAndPlay(rareSound.m_missileSound);
		}
		if (glowBallNum == 0)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(rareFX.burstFX);
			this.m_cleanupObjects.Add(gameObject);
			gameObject.transform.position = bar.m_rarityGem.transform.position;
			gameObject.GetComponent<ParticleSystem>().Play();
			UnityEngine.Object.Destroy(gameObject, 3f);
		}
		float num5 = 0.4f;
		glowBall.SetActive(true);
		iTween.MoveTo(glowBall, iTween.Hash(new object[]
		{
			"path",
			curvePoints.ToArray(),
			"time",
			num5,
			"easetype",
			iTween.EaseType.linear
		}));
		UnityEngine.Object.Destroy(glowBall, num5);
		yield return new WaitForSeconds(num5);
		if (rareSound.m_jarSound != string.Empty)
		{
			SoundManager.Get().LoadAndPlay(rareSound.m_jarSound);
		}
		GameObject dustFX;
		if (UniversalInputManager.UsePhoneUI)
		{
			dustFX = ArcaneDustAmount.Get().m_dustFX;
		}
		else
		{
			dustFX = BnetBar.Get().GetCurrencyFrame(0).m_dustFX;
		}
		if (UnityEngine.Random.Range(0f, 1f) > 0.7f)
		{
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(dustFX);
			this.m_cleanupObjects.Add(gameObject2);
			gameObject2.transform.parent = dustFX.transform.parent;
			gameObject2.transform.localPosition = dustFX.transform.localPosition;
			gameObject2.transform.localScale = dustFX.transform.localScale;
			gameObject2.transform.localRotation = dustFX.transform.localRotation;
			gameObject2.SetActive(true);
			gameObject2.GetComponent<ParticleSystem>().Play();
			UnityEngine.Object.Destroy(gameObject2, 4.9f);
		}
		GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(rareFX.explodeFX);
		this.m_cleanupObjects.Add(gameObject3);
		gameObject3.transform.parent = rareFX.explodeFX.transform.parent;
		gameObject3.transform.localPosition = rareFX.explodeFX.transform.localPosition;
		gameObject3.transform.localScale = rareFX.explodeFX.transform.localScale;
		gameObject3.transform.localRotation = rareFX.explodeFX.transform.localRotation;
		gameObject3.SetActive(true);
		gameObject3.GetComponent<ParticleSystem>().Play();
		UnityEngine.Object.Destroy(gameObject3, 3f);
		Vector3 vector = Vector3.Min(dustJar.transform.localScale * 1.2f, this.m_origDustScale * 3f);
		iTween.ScaleTo(dustJar, iTween.Hash(new object[]
		{
			"scale",
			vector,
			"time",
			0.15f
		}));
		iTween.ScaleTo(dustJar, iTween.Hash(new object[]
		{
			"scale",
			this.m_origDustScale,
			"delay",
			0.1,
			"time",
			1f
		}));
		yield return null;
		yield break;
	}

	// Token: 0x0600144F RID: 5199 RVA: 0x00074798 File Offset: 0x00072998
	private RarityFX GetRarityFX(DisenchantBar bar)
	{
		RarityFX result = default(RarityFX);
		switch (bar.m_rarity)
		{
		case TAG_RARITY.RARE:
			result.burstFX = this.m_FX.m_burstFX_Rare;
			result.explodeFX = (UniversalInputManager.UsePhoneUI ? ArcaneDustAmount.Get().m_explodeFX_Rare : BnetBar.Get().GetCurrencyFrame(0).m_explodeFX_Rare);
			result.glowBallMat = this.m_FX.m_glowBallMat_Rare;
			result.glowTrailMat = this.m_FX.m_glowTrailMat_Rare;
			break;
		case TAG_RARITY.EPIC:
			result.burstFX = this.m_FX.m_burstFX_Epic;
			result.explodeFX = (UniversalInputManager.UsePhoneUI ? ArcaneDustAmount.Get().m_explodeFX_Epic : BnetBar.Get().GetCurrencyFrame(0).m_explodeFX_Epic);
			result.glowBallMat = this.m_FX.m_glowBallMat_Epic;
			result.glowTrailMat = this.m_FX.m_glowTrailMat_Epic;
			break;
		case TAG_RARITY.LEGENDARY:
			result.burstFX = this.m_FX.m_burstFX_Legendary;
			result.explodeFX = (UniversalInputManager.UsePhoneUI ? ArcaneDustAmount.Get().m_explodeFX_Legendary : BnetBar.Get().GetCurrencyFrame(0).m_explodeFX_Legendary);
			result.glowBallMat = this.m_FX.m_glowBallMat_Legendary;
			result.glowTrailMat = this.m_FX.m_glowTrailMat_Legendary;
			break;
		default:
			result.burstFX = this.m_FX.m_burstFX_Common;
			result.explodeFX = (UniversalInputManager.UsePhoneUI ? ArcaneDustAmount.Get().m_explodeFX_Legendary : BnetBar.Get().GetCurrencyFrame(0).m_explodeFX_Legendary);
			result.glowBallMat = this.m_FX.m_glowBallMat_Common;
			result.glowTrailMat = this.m_FX.m_glowTrailMat_Common;
			break;
		}
		return result;
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x00074970 File Offset: 0x00072B70
	private RaritySound GetRaritySound(DisenchantBar bar)
	{
		RaritySound raritySound = new RaritySound();
		switch (bar.m_rarity)
		{
		case TAG_RARITY.RARE:
			raritySound.m_drainSound = this.m_sound.m_rare.m_drainSound;
			raritySound.m_jarSound = this.m_sound.m_rare.m_jarSound;
			raritySound.m_missileSound = this.m_sound.m_rare.m_missileSound;
			break;
		case TAG_RARITY.EPIC:
			raritySound.m_drainSound = this.m_sound.m_epic.m_drainSound;
			raritySound.m_jarSound = this.m_sound.m_epic.m_jarSound;
			raritySound.m_missileSound = this.m_sound.m_epic.m_missileSound;
			break;
		case TAG_RARITY.LEGENDARY:
			raritySound.m_drainSound = this.m_sound.m_legendary.m_drainSound;
			raritySound.m_jarSound = this.m_sound.m_legendary.m_jarSound;
			raritySound.m_missileSound = this.m_sound.m_legendary.m_missileSound;
			break;
		default:
			raritySound.m_drainSound = this.m_sound.m_common.m_drainSound;
			raritySound.m_jarSound = this.m_sound.m_common.m_jarSound;
			raritySound.m_missileSound = this.m_sound.m_common.m_missileSound;
			break;
		}
		return raritySound;
	}

	// Token: 0x04000D6C RID: 3436
	public GameObject m_root;

	// Token: 0x04000D6D RID: 3437
	public GameObject m_disenchantContainer;

	// Token: 0x04000D6E RID: 3438
	public MassDisenchantFX m_FX;

	// Token: 0x04000D6F RID: 3439
	public MassDisenchantSound m_sound;

	// Token: 0x04000D70 RID: 3440
	public UberText m_headlineText;

	// Token: 0x04000D71 RID: 3441
	public UberText m_detailsHeadlineText;

	// Token: 0x04000D72 RID: 3442
	public UberText m_detailsText;

	// Token: 0x04000D73 RID: 3443
	public UberText m_totalAmountText;

	// Token: 0x04000D74 RID: 3444
	public NormalButton m_disenchantButton;

	// Token: 0x04000D75 RID: 3445
	public UberText m_singleSubHeadlineText;

	// Token: 0x04000D76 RID: 3446
	public UberText m_doubleSubHeadlineText;

	// Token: 0x04000D77 RID: 3447
	public GameObject m_singleRoot;

	// Token: 0x04000D78 RID: 3448
	public GameObject m_doubleRoot;

	// Token: 0x04000D79 RID: 3449
	public List<DisenchantBar> m_singleDisenchantBars;

	// Token: 0x04000D7A RID: 3450
	public List<DisenchantBar> m_doubleDisenchantBars;

	// Token: 0x04000D7B RID: 3451
	public UIBButton m_infoButton;

	// Token: 0x04000D7C RID: 3452
	public Material m_rarityBarNormalMaterial;

	// Token: 0x04000D7D RID: 3453
	public Material m_rarityBarGoldMaterial;

	// Token: 0x04000D7E RID: 3454
	public Mesh m_rarityBarNormalMesh;

	// Token: 0x04000D7F RID: 3455
	public Mesh m_rarityBarGoldMesh;

	// Token: 0x04000D80 RID: 3456
	private bool m_useSingle = true;

	// Token: 0x04000D81 RID: 3457
	private int m_totalAmount;

	// Token: 0x04000D82 RID: 3458
	private int m_totalCardsToDisenchant;

	// Token: 0x04000D83 RID: 3459
	private Vector3 m_origTotalScale;

	// Token: 0x04000D84 RID: 3460
	private Vector3 m_origDustScale;

	// Token: 0x04000D85 RID: 3461
	private int m_highestGlowBalls;

	// Token: 0x04000D86 RID: 3462
	private List<GameObject> m_cleanupObjects = new List<GameObject>();

	// Token: 0x04000D87 RID: 3463
	private long m_preMassDisenchantDustValue;

	// Token: 0x04000D88 RID: 3464
	private static MassDisenchant s_Instance;
}
