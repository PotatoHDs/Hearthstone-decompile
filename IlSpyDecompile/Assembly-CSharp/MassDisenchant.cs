using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MassDisenchant : MonoBehaviour
{
	public GameObject m_root;

	public GameObject m_disenchantContainer;

	public MassDisenchantFX m_FX;

	public MassDisenchantSound m_sound;

	public UberText m_headlineText;

	public UberText m_detailsHeadlineText;

	public UberText m_detailsText;

	public UberText m_totalAmountText;

	public NormalButton m_disenchantButton;

	public UberText m_singleSubHeadlineText;

	public UberText m_doubleSubHeadlineText;

	public GameObject m_singleRoot;

	public GameObject m_doubleRoot;

	public List<DisenchantBar> m_singleDisenchantBars;

	public List<DisenchantBar> m_doubleDisenchantBars;

	public UIBButton m_infoButton;

	public Material m_rarityBarNormalMaterial;

	public Material m_rarityBarGoldMaterial;

	public Mesh m_rarityBarNormalMesh;

	public Mesh m_rarityBarGoldMesh;

	private bool m_useSingle = true;

	private int m_totalAmount;

	private int m_totalCardsToDisenchant;

	private Vector3 m_origTotalScale;

	private Vector3 m_origDustScale;

	private int m_highestGlowBalls;

	private List<GameObject> m_cleanupObjects = new List<GameObject>();

	private long m_preMassDisenchantDustValue;

	private static MassDisenchant s_Instance;

	private void Awake()
	{
		s_Instance = this;
		m_headlineText.Text = GameStrings.Get("GLUE_MASS_DISENCHANT_HEADLINE");
		m_detailsHeadlineText.Text = GameStrings.Get("GLUE_MASS_DISENCHANT_DETAILS_HEADLINE");
		m_disenchantButton.SetText(GameStrings.Get("GLUE_MASS_DISENCHANT_BUTTON_TEXT"));
		if (m_detailsText != null)
		{
			m_detailsText.Text = GameStrings.Get("GLUE_MASS_DISENCHANT_DETAILS");
		}
		if (m_singleSubHeadlineText != null)
		{
			m_singleSubHeadlineText.Text = GameStrings.Get("GLUE_MASS_DISENCHANT_SUB_HEADLINE_TEXT");
		}
		if (m_doubleSubHeadlineText != null)
		{
			m_doubleSubHeadlineText.Text = GameStrings.Get("GLUE_MASS_DISENCHANT_SUB_HEADLINE_TEXT");
		}
		m_disenchantButton.SetUserOverYOffset(-0.04409015f);
		foreach (DisenchantBar singleDisenchantBar in m_singleDisenchantBars)
		{
			singleDisenchantBar.Init();
		}
		foreach (DisenchantBar doubleDisenchantBar in m_doubleDisenchantBars)
		{
			doubleDisenchantBar.Init();
		}
		CollectionManager.Get().RegisterMassDisenchantListener(OnMassDisenchant);
	}

	private void Start()
	{
		m_disenchantButton.AddEventListener(UIEventType.RELEASE, OnDisenchantButtonPressed);
		m_disenchantButton.AddEventListener(UIEventType.ROLLOVER, OnDisenchantButtonOver);
		m_disenchantButton.AddEventListener(UIEventType.ROLLOUT, OnDisenchantButtonOut);
		if (m_infoButton != null)
		{
			m_infoButton.AddEventListener(UIEventType.RELEASE, OnInfoButtonPressed);
		}
	}

	public static MassDisenchant Get()
	{
		return s_Instance;
	}

	public void Show()
	{
		m_root.SetActive(value: true);
	}

	public void Hide()
	{
		m_root.SetActive(value: false);
		BlockCurrencyFrame(block: false);
	}

	public bool IsShown()
	{
		return m_root.activeSelf;
	}

	private void OnDestroy()
	{
		foreach (GameObject cleanupObject in m_cleanupObjects)
		{
			if (cleanupObject != null)
			{
				UnityEngine.Object.Destroy(cleanupObject);
			}
		}
		CollectionManager.Get().RemoveMassDisenchantListener(OnMassDisenchant);
		BlockCurrencyFrame(block: false);
	}

	public int GetTotalAmount()
	{
		return m_totalAmount;
	}

	public void UpdateContents(List<CollectibleCard> disenchantCards)
	{
		List<CollectibleCard> list = disenchantCards.Where((CollectibleCard c) => c.PremiumType == TAG_PREMIUM.GOLDEN).ToList();
		m_useSingle = list.Count == 0;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_useSingle = true;
		}
		List<DisenchantBar> list2 = (m_useSingle ? m_singleDisenchantBars : m_doubleDisenchantBars);
		foreach (DisenchantBar item in list2)
		{
			item.Reset();
		}
		m_totalAmount = 0;
		m_totalCardsToDisenchant = 0;
		foreach (CollectibleCard card in disenchantCards)
		{
			if (!card.IsCraftable)
			{
				continue;
			}
			NetCache.CardValue cardValue = CraftingManager.Get().GetCardValue(card.CardId, card.PremiumType);
			if (cardValue != null)
			{
				EntityDef entityDef = DefLoader.Get().GetEntityDef(card.CardId);
				int num = cardValue.GetSellValue() * card.DisenchantCount;
				DisenchantBar disenchantBar = list2.Find((DisenchantBar obj) => (obj.m_premiumType == card.PremiumType || (bool)UniversalInputManager.UsePhoneUI) && obj.m_rarity == entityDef.GetRarity());
				if (disenchantBar == null)
				{
					Debug.LogWarning(string.Format("MassDisenchant.UpdateContents(): Could not find {0} bar to modify for card {1} (premium {2}, disenchant count {3})", m_useSingle ? "single" : "double", entityDef, card.PremiumType, card.DisenchantCount));
				}
				else
				{
					disenchantBar.AddCards(card.DisenchantCount, num, card.PremiumType);
					m_totalCardsToDisenchant += card.DisenchantCount;
					m_totalAmount += num;
				}
			}
		}
		if (m_totalAmount > 0)
		{
			m_singleRoot.SetActive(m_useSingle);
			if (m_doubleRoot != null)
			{
				m_doubleRoot.SetActive(!m_useSingle);
			}
			m_disenchantButton.SetEnabled(enabled: true);
		}
		foreach (DisenchantBar item2 in list2)
		{
			item2.UpdateVisuals(m_totalCardsToDisenchant);
		}
		m_totalAmountText.Text = GameStrings.Format("GLUE_MASS_DISENCHANT_TOTAL_AMOUNT", m_totalAmount);
	}

	public IEnumerator StartHighlight()
	{
		yield return null;
		m_FX.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
	}

	public void OnMassDisenchant(int amount)
	{
		int num = 10;
		num = GraphicsManager.Get().RenderQualityLevel switch
		{
			GraphicsQuality.Low => 3, 
			GraphicsQuality.Medium => 6, 
			_ => 10, 
		};
		BlockUI();
		StartCoroutine(DoDisenchantAnims(num, amount));
	}

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

	private void BlockUI(bool block = true)
	{
		BlockCurrencyFrame(block);
		m_FX.m_blockInteraction.SetActive(block);
	}

	private void OnDisenchantButtonOver(UIEvent e)
	{
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.MASS_DISENCHANT)
		{
			m_FX.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_MOUSE_OVER);
			SoundManager.Get().LoadAndPlay("Hub_Mouseover.prefab:40130da7b734190479c527d6bca1a4a8");
		}
		else
		{
			m_FX.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
	}

	private void OnDisenchantButtonOut(UIEvent e)
	{
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.MASS_DISENCHANT)
		{
			m_FX.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
		}
		else
		{
			m_FX.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		}
	}

	private void OnDisenchantButtonPressed(UIEvent e)
	{
		Options.Get().SetBool(Option.HAS_DISENCHANTED, val: true);
		m_disenchantButton.SetEnabled(enabled: false);
		m_FX.m_highlight.ChangeState(ActorStateType.HIGHLIGHT_OFF);
		BlockCurrencyFrame(block: true);
		m_preMassDisenchantDustValue = NetCache.Get().GetArcaneDustBalance();
		Network.Get().MassDisenchant();
	}

	private void OnInfoButtonPressed(UIEvent e)
	{
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = GameStrings.Get("GLUE_MASS_DISENCHANT_BUTTON_TEXT");
		popupInfo.m_text = string.Format("{0}\n\n{1}", GameStrings.Get("GLUE_MASS_DISENCHANT_DETAILS_HEADLINE"), GameStrings.Get("GLUE_MASS_DISENCHANT_DETAILS"));
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
		DialogManager.Get().ShowPopup(popupInfo);
	}

	private void Unbloomify(List<GameObject> glows, float newVal)
	{
		foreach (GameObject glow in glows)
		{
			glow.GetComponent<RenderToTexture>().m_BloomIntensity = newVal;
		}
	}

	private void UncolorTotal(float newVal)
	{
		m_totalAmountText.TextColor = Color.Lerp(Color.white, new Color(0.7f, 0.85f, 1f, 1f), newVal);
	}

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

	private IEnumerator DoDisenchantAnims(int maxGlowBalls, int disenchantTotal)
	{
		if (disenchantTotal == 0)
		{
			yield return null;
		}
		m_origTotalScale = m_totalAmountText.transform.localScale;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_origDustScale = ArcaneDustAmount.Get().m_dustJar.transform.localScale;
		}
		else
		{
			m_origDustScale = BnetBar.Get().GetCurrencyFrame().GetCurrencyIconContainer()
				.transform.localScale;
		}
		List<DisenchantBar> disenchantBars = (m_useSingle ? m_singleDisenchantBars : m_doubleDisenchantBars);
		float vigTime = 0.2f;
		FullScreenFXMgr.Get().Vignette(0.8f, vigTime, iTween.EaseType.easeInOutCubic);
		iTween.ValueTo(base.gameObject, iTween.Hash("from", 1f, "to", 0.3f, "time", vigTime, "onupdate", (Action<object>)delegate(object newVal)
		{
			SetGemSaturation(disenchantBars, (float)newVal);
		}));
		if (m_sound.m_intro != string.Empty)
		{
			SoundManager.Get().LoadAndPlay(m_sound.m_intro);
		}
		yield return new WaitForSeconds(vigTime);
		float duration = 0.5f;
		float rate = duration / 20f;
		iTween.ValueTo(base.gameObject, iTween.Hash("from", 0.3f, "to", 1.75f, "time", 1.5f * duration, "easeInType", iTween.EaseType.easeInCubic, "onupdate", (Action<object>)delegate(object newVal)
		{
			SetGemSaturation(disenchantBars, (float)newVal, onlyActive: true);
		}));
		List<GameObject> glows = new List<GameObject>();
		if (m_FX.m_glowTotal != null)
		{
			glows.Add(m_FX.m_glowTotal);
		}
		m_totalAmountText.transform.localScale = m_origTotalScale * 2.54f;
		iTween.ScaleTo(m_totalAmountText.gameObject, iTween.Hash("scale", m_origTotalScale, "time", 3.0));
		if (m_FX.m_glowTotal != null)
		{
			m_FX.m_glowTotal.SetActive(value: true);
		}
		m_highestGlowBalls = 0;
		Color glowColor = new Color(0.7f, 0.85f, 1f, 1f);
		float origYSpeed = 0f;
		float origXSpeed = 0f;
		float origInten = 0f;
		foreach (DisenchantBar item in disenchantBars)
		{
			int numCards = item.GetNumCards();
			if (numCards > m_highestGlowBalls)
			{
				m_highestGlowBalls = numCards;
			}
		}
		m_highestGlowBalls = ((m_highestGlowBalls > maxGlowBalls) ? maxGlowBalls : m_highestGlowBalls);
		foreach (DisenchantBar item2 in disenchantBars)
		{
			int numCards2 = item2.GetNumCards();
			if (numCards2 > 0)
			{
				RarityFX rarityFX = GetRarityFX(item2);
				int num = ((numCards2 > maxGlowBalls) ? maxGlowBalls : numCards2);
				for (int j = 0; j < num; j++)
				{
					StartCoroutine(LaunchGlowball(item2, rarityFX, j, num, m_highestGlowBalls));
				}
			}
		}
		int i = 0;
		while ((float)i < duration / rate)
		{
			float num2 = 0f;
			foreach (DisenchantBar item3 in disenchantBars)
			{
				RaritySound raritySound = GetRaritySound(item3);
				int numCards3 = item3.GetNumCards();
				if (i == 0 && numCards3 != 0)
				{
					if (raritySound.m_drainSound != string.Empty)
					{
						SoundManager.Get().LoadAndPlay(raritySound.m_drainSound);
					}
					if (item3.m_numGoldText != null && item3.m_numGoldText.gameObject.activeSelf)
					{
						item3.m_numGoldText.gameObject.SetActive(value: false);
						TransformUtil.SetLocalPosX(item3.m_numCardsText, 2.902672f);
					}
					Vector3 localScale = item3.m_numCardsText.gameObject.transform.localScale;
					iTween.ScaleFrom(item3.m_numCardsText.gameObject, iTween.Hash("x", localScale.x * 2.28f, "y", localScale.y * 2.28f, "z", localScale.z * 2.28f, "time", 3.0));
					item3.m_numCardsText.TextColor = glowColor;
					iTween.ColorTo(item3.m_numCardsText.gameObject, iTween.Hash("r", 1f, "g", 1f, "b", 1f, "time", 3.0));
					if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.High && item3.m_glow != null)
					{
						glows.Add(item3.m_glow);
						item3.m_glow.GetComponent<RenderToTexture>().m_BloomIntensity = 0.01f;
						item3.m_glow.SetActive(value: true);
					}
					Material material = item3.m_rarityGem.GetComponent<Renderer>().GetMaterial();
					origYSpeed = material.GetFloat("_YSpeed");
					origXSpeed = material.GetFloat("_XSpeed");
					origInten = item3.m_amountBar.GetComponent<Renderer>().GetMaterial().GetFloat("_Intensity");
					material.SetFloat("_YSpeed", -10f);
					material.SetFloat("_XSpeed", 20f);
				}
			}
			if (i == 0)
			{
				if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.High)
				{
					iTween.ValueTo(base.gameObject, iTween.Hash("from", 1f, "to", 0.1f, "time", duration * 3f, "onupdate", (Action<object>)delegate(object newVal)
					{
						Unbloomify(glows, (float)newVal);
					}));
				}
				iTween.ValueTo(base.gameObject, iTween.Hash("from", 1f, "to", 0.1f, "time", duration * 3f, "onupdate", (Action<object>)delegate(object newVal)
				{
					UncolorTotal((float)newVal);
				}));
				float num3 = m_preMassDisenchantDustValue;
				iTween.ValueTo(base.gameObject, iTween.Hash("from", num3, "to", num3 + (float)disenchantTotal, "time", 3f * duration, "onupdate", (Action<object>)delegate(object newVal)
				{
					SetDustBalance((float)newVal);
				}, "oncomplete", (Action<object>)delegate
				{
					CollectionManager.Get().GetCollectibleDisplay().m_pageManager.UpdateMassDisenchant();
					FullScreenFXMgr.Get().StopVignette(vigTime, iTween.EaseType.easeInOutCubic);
					BlockUI(block: false);
				}));
			}
			foreach (DisenchantBar item4 in disenchantBars)
			{
				if (item4.GetNumCards() != 0)
				{
					item4.m_amountBar.GetComponent<Renderer>().GetMaterial().SetFloat("_Intensity", 2f);
					num2 += DrainBarAndDust(item4, i, duration, rate);
				}
			}
			m_totalAmountText.Text = Convert.ToInt32(num2).ToString();
			yield return new WaitForSeconds(rate / duration);
			int num4 = i + 1;
			i = num4;
		}
		if (m_FX.m_glowTotal != null)
		{
			m_FX.m_glowTotal.SetActive(value: false);
		}
		m_totalAmountText.Text = "0";
		m_totalAmountText.TextColor = Color.white;
		iTween.ValueTo(base.gameObject, iTween.Hash("from", 0.3f, "to", 1f, "time", duration, "delay", vigTime, "onupdate", (Action<object>)delegate(object newVal)
		{
			SetGemSaturation(disenchantBars, (float)newVal, onlyActive: false, onlyInactive: true);
		}));
		iTween.ValueTo(base.gameObject, iTween.Hash("from", 1.75f, "to", 1f, "time", duration, "delay", vigTime, "onupdate", (Action<object>)delegate(object newVal)
		{
			SetGemSaturation(disenchantBars, (float)newVal, onlyActive: true);
		}));
		foreach (DisenchantBar item5 in disenchantBars)
		{
			if (item5.m_glow != null)
			{
				item5.m_glow.SetActive(value: false);
			}
			item5.m_numCardsText.TextColor = Color.white;
			Material material2 = item5.m_rarityGem.GetComponent<Renderer>().GetMaterial();
			material2.SetFloat("_YSpeed", origYSpeed);
			material2.SetFloat("_XSpeed", origXSpeed);
			item5.m_amountBar.GetComponent<Renderer>().GetMaterial().SetFloat("_Intensity", origInten);
		}
	}

	private void SetDustBalance(float bal)
	{
		int num = (int)bal;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			ArcaneDustAmount.Get().m_dustCount.Text = num.ToString();
		}
		else if (Shop.Get() != null)
		{
			Shop.Get().DisplayCurrencyBalance(CurrencyType.DUST, num);
		}
	}

	private float DrainBarAndDust(DisenchantBar bar, int drainRun, float duration, float rate)
	{
		float num = bar.GetNumCards();
		num -= (float)(drainRun + 1) * num / (duration / rate);
		if (num < 0f)
		{
			num = 0f;
		}
		float num2 = bar.GetAmountDust();
		num2 -= (float)(drainRun + 1) * num2 / (duration / rate);
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		bar.m_numCardsText.Text = Convert.ToInt32(num).ToString();
		bar.m_amountText.Text = Convert.ToInt32(num2).ToString();
		float percent = 0f;
		if (m_totalCardsToDisenchant > 0)
		{
			percent = num / (float)m_totalCardsToDisenchant;
		}
		bar.SetPercent(percent);
		return num2;
	}

	private Vector3 GetRanBoxPt(GameObject box)
	{
		Vector3 localScale = box.transform.localScale;
		Vector3 position = box.transform.position;
		Vector3 vector = new Vector3(UnityEngine.Random.Range((0f - localScale.x) / 2f, localScale.x / 2f), UnityEngine.Random.Range((0f - localScale.y) / 2f, localScale.y / 2f), UnityEngine.Random.Range((0f - localScale.z) / 2f, localScale.z / 2f));
		return position + vector;
	}

	private IEnumerator LaunchGlowball(DisenchantBar bar, RarityFX rareFX, int glowBallNum, int totalGlowBalls, int m_highestGlowBalls)
	{
		float num = 0.02f;
		float num2 = glowBallNum;
		float num3 = (1f - num * (float)m_highestGlowBalls) / (float)totalGlowBalls;
		float min = num2 * num3 + num2 * num;
		float max = (num2 + 1f) * num3 + (num2 + 1f) * num;
		GameObject glowBall = UnityEngine.Object.Instantiate(m_FX.m_glowBall);
		m_cleanupObjects.Add(glowBall);
		glowBall.GetComponent<Renderer>().SetSharedMaterial(rareFX.glowBallMat);
		glowBall.GetComponent<TrailRenderer>().SetMaterial(rareFX.glowTrailMat);
		glowBall.transform.position = bar.m_rarityGem.transform.position;
		glowBall.transform.position = new Vector3(glowBall.transform.position.x, glowBall.transform.position.y + 0.5f, glowBall.transform.position.z);
		List<Vector3> curvePoints = new List<Vector3> { glowBall.transform.position };
		if ((double)UnityEngine.Random.Range(0f, 1f) < 0.5)
		{
			curvePoints.Add(GetRanBoxPt(m_FX.m_gemBoxLeft1));
			curvePoints.Add(GetRanBoxPt(m_FX.m_gemBoxLeft2));
		}
		else
		{
			curvePoints.Add(GetRanBoxPt(m_FX.m_gemBoxRight1));
			curvePoints.Add(GetRanBoxPt(m_FX.m_gemBoxRight2));
		}
		GameObject dustJar;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			dustJar = ArcaneDustAmount.Get().m_dustJar;
			curvePoints.Add(dustJar.transform.position);
		}
		else
		{
			dustJar = BnetBar.Get().GetCurrencyFrame().GetCurrencyIconContainer();
			curvePoints.Add(Camera.main.ViewportToWorldPoint(BaseUI.Get().m_BnetCamera.WorldToViewportPoint(dustJar.transform.position)));
		}
		yield return new WaitForSeconds(UnityEngine.Random.Range(min, max));
		RaritySound rareSound = GetRaritySound(bar);
		if (rareSound.m_missileSound != string.Empty)
		{
			SoundManager.Get().LoadAndPlay(rareSound.m_missileSound);
		}
		if (glowBallNum == 0)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(rareFX.burstFX);
			m_cleanupObjects.Add(gameObject);
			gameObject.transform.position = bar.m_rarityGem.transform.position;
			gameObject.GetComponent<ParticleSystem>().Play();
			UnityEngine.Object.Destroy(gameObject, 3f);
		}
		float num4 = 0.4f;
		glowBall.SetActive(value: true);
		iTween.MoveTo(glowBall, iTween.Hash("path", curvePoints.ToArray(), "time", num4, "easetype", iTween.EaseType.linear));
		UnityEngine.Object.Destroy(glowBall, num4);
		yield return new WaitForSeconds(num4);
		if (rareSound.m_jarSound != string.Empty)
		{
			SoundManager.Get().LoadAndPlay(rareSound.m_jarSound);
		}
		GameObject gameObject2 = ((!UniversalInputManager.UsePhoneUI) ? BnetBar.Get().GetCurrencyFrame().m_dustFX : ArcaneDustAmount.Get().m_dustFX);
		if (UnityEngine.Random.Range(0f, 1f) > 0.7f)
		{
			GameObject gameObject3 = UnityEngine.Object.Instantiate(gameObject2);
			m_cleanupObjects.Add(gameObject3);
			gameObject3.transform.parent = gameObject2.transform.parent;
			gameObject3.transform.localPosition = gameObject2.transform.localPosition;
			gameObject3.transform.localScale = gameObject2.transform.localScale;
			gameObject3.transform.localRotation = gameObject2.transform.localRotation;
			gameObject3.SetActive(value: true);
			gameObject3.GetComponent<ParticleSystem>().Play();
			UnityEngine.Object.Destroy(gameObject3, 4.9f);
		}
		GameObject gameObject4 = UnityEngine.Object.Instantiate(rareFX.explodeFX);
		m_cleanupObjects.Add(gameObject4);
		gameObject4.transform.parent = rareFX.explodeFX.transform.parent;
		gameObject4.transform.localPosition = rareFX.explodeFX.transform.localPosition;
		gameObject4.transform.localScale = rareFX.explodeFX.transform.localScale;
		gameObject4.transform.localRotation = rareFX.explodeFX.transform.localRotation;
		gameObject4.SetActive(value: true);
		gameObject4.GetComponent<ParticleSystem>().Play();
		UnityEngine.Object.Destroy(gameObject4, 3f);
		Vector3 vector = Vector3.Min(dustJar.transform.localScale * 1.2f, m_origDustScale * 3f);
		iTween.ScaleTo(dustJar, iTween.Hash("scale", vector, "time", 0.15f));
		iTween.ScaleTo(dustJar, iTween.Hash("scale", m_origDustScale, "delay", 0.1, "time", 1f));
		yield return null;
	}

	private RarityFX GetRarityFX(DisenchantBar bar)
	{
		RarityFX result = default(RarityFX);
		switch (bar.m_rarity)
		{
		case TAG_RARITY.RARE:
			result.burstFX = m_FX.m_burstFX_Rare;
			result.explodeFX = (UniversalInputManager.UsePhoneUI ? ArcaneDustAmount.Get().m_explodeFX_Rare : BnetBar.Get().GetCurrencyFrame().m_explodeFX_Rare);
			result.glowBallMat = m_FX.m_glowBallMat_Rare;
			result.glowTrailMat = m_FX.m_glowTrailMat_Rare;
			break;
		case TAG_RARITY.EPIC:
			result.burstFX = m_FX.m_burstFX_Epic;
			result.explodeFX = (UniversalInputManager.UsePhoneUI ? ArcaneDustAmount.Get().m_explodeFX_Epic : BnetBar.Get().GetCurrencyFrame().m_explodeFX_Epic);
			result.glowBallMat = m_FX.m_glowBallMat_Epic;
			result.glowTrailMat = m_FX.m_glowTrailMat_Epic;
			break;
		case TAG_RARITY.LEGENDARY:
			result.burstFX = m_FX.m_burstFX_Legendary;
			result.explodeFX = (UniversalInputManager.UsePhoneUI ? ArcaneDustAmount.Get().m_explodeFX_Legendary : BnetBar.Get().GetCurrencyFrame().m_explodeFX_Legendary);
			result.glowBallMat = m_FX.m_glowBallMat_Legendary;
			result.glowTrailMat = m_FX.m_glowTrailMat_Legendary;
			break;
		default:
			result.burstFX = m_FX.m_burstFX_Common;
			result.explodeFX = (UniversalInputManager.UsePhoneUI ? ArcaneDustAmount.Get().m_explodeFX_Legendary : BnetBar.Get().GetCurrencyFrame().m_explodeFX_Legendary);
			result.glowBallMat = m_FX.m_glowBallMat_Common;
			result.glowTrailMat = m_FX.m_glowTrailMat_Common;
			break;
		}
		return result;
	}

	private RaritySound GetRaritySound(DisenchantBar bar)
	{
		RaritySound raritySound = new RaritySound();
		switch (bar.m_rarity)
		{
		case TAG_RARITY.RARE:
			raritySound.m_drainSound = m_sound.m_rare.m_drainSound;
			raritySound.m_jarSound = m_sound.m_rare.m_jarSound;
			raritySound.m_missileSound = m_sound.m_rare.m_missileSound;
			break;
		case TAG_RARITY.EPIC:
			raritySound.m_drainSound = m_sound.m_epic.m_drainSound;
			raritySound.m_jarSound = m_sound.m_epic.m_jarSound;
			raritySound.m_missileSound = m_sound.m_epic.m_missileSound;
			break;
		case TAG_RARITY.LEGENDARY:
			raritySound.m_drainSound = m_sound.m_legendary.m_drainSound;
			raritySound.m_jarSound = m_sound.m_legendary.m_jarSound;
			raritySound.m_missileSound = m_sound.m_legendary.m_missileSound;
			break;
		default:
			raritySound.m_drainSound = m_sound.m_common.m_drainSound;
			raritySound.m_jarSound = m_sound.m_common.m_jarSound;
			raritySound.m_missileSound = m_sound.m_common.m_missileSound;
			break;
		}
		return raritySound;
	}
}
