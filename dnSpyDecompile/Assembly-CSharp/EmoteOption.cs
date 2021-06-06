using System;
using UnityEngine;

// Token: 0x02000304 RID: 772
[CustomEditClass]
public class EmoteOption : MonoBehaviour
{
	// Token: 0x06002971 RID: 10609 RVA: 0x000D2B14 File Offset: 0x000D0D14
	private void Awake()
	{
		this.UpdateEmoteType();
		if (this.m_Text != null)
		{
			this.m_Text.gameObject.SetActive(false);
		}
		if (this.m_Backplate != null)
		{
			this.m_Backplate.enabled = false;
		}
		if (this.m_VisualEmoteImage != null)
		{
			this.m_VisualEmoteImage.SetActive(false);
		}
		if (!string.IsNullOrEmpty(this.m_SpeechBubbleSpellPath))
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(this.m_SpeechBubbleSpellPath, AssetLoadingOptions.None);
			if (gameObject != null)
			{
				this.m_speechBubbleSpell = gameObject.GetComponent<Spell>();
				if (this.m_speechBubbleSpell == null)
				{
					UnityEngine.Object.Destroy(gameObject);
					Error.AddDevFatalUnlessWorkarounds("EmoteOption.Awake() - \"{0}\" does not have a Spell component.", new object[]
					{
						this.m_SpeechBubbleSpellPath
					});
				}
				SpellUtils.SetupSpell(this.m_speechBubbleSpell, this);
			}
			if (this.m_speechBubbleSpell != null && this.m_Backplate != null && this.m_RenderSpellOnSpeechBubbleLayer)
			{
				Renderer component = this.m_Backplate.GetComponent<Renderer>();
				if (component != null)
				{
					int layer = this.m_Backplate.gameObject.layer;
					SetRenderQue component2 = this.m_Backplate.GetComponent<SetRenderQue>();
					int num = (component2 != null) ? component2.queue : 0;
					int renderQueue = component.GetMaterial().renderQueue + num;
					SceneUtils.SetLayer(this.m_speechBubbleSpell, layer);
					SceneUtils.SetRenderQueue(this.m_speechBubbleSpell.gameObject, renderQueue, true);
				}
			}
		}
		this.m_startingScale = base.transform.localScale;
		base.transform.localScale = Vector3.zero;
	}

	// Token: 0x06002972 RID: 10610 RVA: 0x000D2CAC File Offset: 0x000D0EAC
	private void Update()
	{
		if (this.m_Text == null && this.m_VisualEmoteImage == null)
		{
			return;
		}
		if (EmoteHandler.Get().EmoteSpamBlocked())
		{
			if (!this.m_textIsGrey)
			{
				this.m_textIsGrey = true;
				if (this.m_Text != null)
				{
					this.m_Text.TextColor = new Color(0.5372549f, 0.5372549f, 0.5372549f);
				}
				if (this.m_VisualEmoteImage != null)
				{
					this.m_VisualEmoteImage.GetComponent<Renderer>().GetMaterial().color = new Color(1f, 1f, 1f, 0.5f);
					return;
				}
			}
		}
		else if (this.m_textIsGrey)
		{
			this.m_textIsGrey = false;
			if (this.m_Text != null)
			{
				this.m_Text.TextColor = new Color(0f, 0f, 0f);
			}
			if (this.m_VisualEmoteImage != null)
			{
				this.m_VisualEmoteImage.GetComponent<Renderer>().GetMaterial().color = new Color(1f, 1f, 1f, 1f);
			}
		}
	}

	// Token: 0x06002973 RID: 10611 RVA: 0x000D2DDC File Offset: 0x000D0FDC
	public void DoClick()
	{
		EmoteHandler.Get().ResetTimeSinceLastEmote();
		EmoteType emoteType = this.m_currentEmoteType;
		EmoteType emoteResponseType = EmoteHandler.Get().GetEmoteResponseType(this.m_currentEmoteType);
		Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		if (EmoteHandler.Get().ShouldUseEmoteResponse(this.m_currentEmoteType, Player.Side.FRIENDLY) && heroCard.GetEmoteEntry(emoteResponseType) != null)
		{
			emoteType = emoteResponseType;
		}
		heroCard.PlayEmote(emoteType);
		Network.Get().SendEmote(emoteType);
		EmoteHandler.Get().HideEmotes();
	}

	// Token: 0x06002974 RID: 10612 RVA: 0x000D2E58 File Offset: 0x000D1058
	public void Enable()
	{
		this.m_Backplate.enabled = true;
		if (this.m_Text != null)
		{
			this.m_Text.gameObject.SetActive(true);
		}
		if (this.m_VisualEmoteImage != null)
		{
			this.m_VisualEmoteImage.gameObject.SetActive(true);
		}
		base.GetComponent<Collider>().enabled = true;
		iTween.Stop(base.gameObject);
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			this.m_startingScale,
			"time",
			0.5f,
			"ignoretimescale",
			true,
			"easetype",
			iTween.EaseType.easeOutElastic
		}));
		if (this.m_speechBubbleSpell != null)
		{
			TransformUtil.CopyWorld(this.m_speechBubbleSpell, base.gameObject);
			this.m_speechBubbleSpell.transform.localScale = Vector3.one;
			this.m_speechBubbleSpell.ActivateState(SpellStateType.BIRTH);
		}
	}

	// Token: 0x06002975 RID: 10613 RVA: 0x000D2F6C File Offset: 0x000D116C
	public void Disable()
	{
		base.GetComponent<Collider>().enabled = false;
		iTween.Stop(base.gameObject);
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			Vector3.zero,
			"time",
			0.1f,
			"ignoretimescale",
			true,
			"easetype",
			iTween.EaseType.linear,
			"oncompletetarget",
			base.gameObject,
			"oncomplete",
			"FinishDisable"
		}));
		if (this.m_speechBubbleSpell != null)
		{
			this.m_speechBubbleSpell.ActivateState(SpellStateType.DEATH);
		}
	}

	// Token: 0x06002976 RID: 10614 RVA: 0x000D3034 File Offset: 0x000D1234
	public void HandleMouseOut()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			this.m_startingScale,
			"time",
			0.2f,
			"ignoretimescale",
			true
		}));
	}

	// Token: 0x06002977 RID: 10615 RVA: 0x000D3094 File Offset: 0x000D1294
	public void HandleMouseOver()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash(new object[]
		{
			"scale",
			this.m_startingScale * 1.1f,
			"time",
			0.2f,
			"ignoretimescale",
			true
		}));
	}

	// Token: 0x06002978 RID: 10616 RVA: 0x000D3100 File Offset: 0x000D1300
	public void UpdateEmoteType()
	{
		GameState gameState = GameState.Get();
		Player player = (gameState != null) ? gameState.GetFriendlySidePlayer() : null;
		if (player != null && this.ShouldUseFallbackEmote(player))
		{
			this.m_currentEmoteType = this.m_FallbackEmoteType;
			this.m_currentStringTag = this.m_FallbackStringTag;
		}
		else
		{
			this.m_currentEmoteType = this.m_EmoteType;
			this.m_currentStringTag = this.m_StringTag;
		}
		if (this.m_Text != null)
		{
			this.m_Text.Text = GameStrings.Get(this.m_currentStringTag);
		}
	}

	// Token: 0x06002979 RID: 10617 RVA: 0x000D3184 File Offset: 0x000D1384
	public bool ShouldPlayerUseEmoteOverride(Player player)
	{
		if (player == null)
		{
			return false;
		}
		Card heroCard = player.GetHeroCard();
		return !(heroCard == null) && heroCard.GetEmoteEntry(this.m_EmoteType) != null;
	}

	// Token: 0x0600297A RID: 10618 RVA: 0x000D31BC File Offset: 0x000D13BC
	public bool CanPlayerUseEmoteType(Player player)
	{
		if (player == null)
		{
			return false;
		}
		if (GameState.Get().GetBooleanGameOption(GameEntityOption.USES_PREMIUM_EMOTES))
		{
			return true;
		}
		Card heroCard = player.GetHeroCard();
		return !(heroCard == null) && (heroCard.GetEmoteEntry(this.m_EmoteType) != null || heroCard.GetEmoteEntry(this.m_FallbackEmoteType) != null);
	}

	// Token: 0x0600297B RID: 10619 RVA: 0x000D3211 File Offset: 0x000D1411
	public bool HasEmoteTypeForPlayer(EmoteType emoteType, Player player)
	{
		if (this.ShouldUseFallbackEmote(player))
		{
			return emoteType == this.m_FallbackEmoteType;
		}
		return emoteType == this.m_EmoteType;
	}

	// Token: 0x0600297C RID: 10620 RVA: 0x000D3230 File Offset: 0x000D1430
	private bool ShouldUseFallbackEmote(Player player)
	{
		if (player == null)
		{
			return false;
		}
		Card heroCard = player.GetHeroCard();
		return !(heroCard == null) && heroCard.GetEmoteEntry(this.m_EmoteType) == null && heroCard.GetEmoteEntry(this.m_FallbackEmoteType) != null;
	}

	// Token: 0x0600297D RID: 10621 RVA: 0x000D3275 File Offset: 0x000D1475
	private void FinishDisable()
	{
		if (base.GetComponent<Collider>().enabled)
		{
			return;
		}
		this.m_Backplate.enabled = false;
		if (this.m_Text != null)
		{
			this.m_Text.gameObject.SetActive(false);
		}
	}

	// Token: 0x04001788 RID: 6024
	public EmoteType m_EmoteType;

	// Token: 0x04001789 RID: 6025
	public string m_StringTag;

	// Token: 0x0400178A RID: 6026
	public EmoteType m_FallbackEmoteType;

	// Token: 0x0400178B RID: 6027
	public string m_FallbackStringTag;

	// Token: 0x0400178C RID: 6028
	public MeshRenderer m_Backplate;

	// Token: 0x0400178D RID: 6029
	public UberText m_Text;

	// Token: 0x0400178E RID: 6030
	public GameObject m_VisualEmoteImage;

	// Token: 0x0400178F RID: 6031
	[CustomEditField(T = EditType.SPELL)]
	public string m_SpeechBubbleSpellPath;

	// Token: 0x04001790 RID: 6032
	public bool m_RenderSpellOnSpeechBubbleLayer = true;

	// Token: 0x04001791 RID: 6033
	private EmoteType m_currentEmoteType;

	// Token: 0x04001792 RID: 6034
	private string m_currentStringTag;

	// Token: 0x04001793 RID: 6035
	private Vector3 m_startingScale;

	// Token: 0x04001794 RID: 6036
	private bool m_textIsGrey;

	// Token: 0x04001795 RID: 6037
	private Spell m_speechBubbleSpell;
}
