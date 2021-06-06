using UnityEngine;

[CustomEditClass]
public class EmoteOption : MonoBehaviour
{
	public EmoteType m_EmoteType;

	public string m_StringTag;

	public EmoteType m_FallbackEmoteType;

	public string m_FallbackStringTag;

	public MeshRenderer m_Backplate;

	public UberText m_Text;

	public GameObject m_VisualEmoteImage;

	[CustomEditField(T = EditType.SPELL)]
	public string m_SpeechBubbleSpellPath;

	public bool m_RenderSpellOnSpeechBubbleLayer = true;

	private EmoteType m_currentEmoteType;

	private string m_currentStringTag;

	private Vector3 m_startingScale;

	private bool m_textIsGrey;

	private Spell m_speechBubbleSpell;

	private void Awake()
	{
		UpdateEmoteType();
		if (m_Text != null)
		{
			m_Text.gameObject.SetActive(value: false);
		}
		if (m_Backplate != null)
		{
			m_Backplate.enabled = false;
		}
		if (m_VisualEmoteImage != null)
		{
			m_VisualEmoteImage.SetActive(value: false);
		}
		if (!string.IsNullOrEmpty(m_SpeechBubbleSpellPath))
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab(m_SpeechBubbleSpellPath);
			if (gameObject != null)
			{
				m_speechBubbleSpell = gameObject.GetComponent<Spell>();
				if (m_speechBubbleSpell == null)
				{
					Object.Destroy(gameObject);
					Error.AddDevFatalUnlessWorkarounds("EmoteOption.Awake() - \"{0}\" does not have a Spell component.", m_SpeechBubbleSpellPath);
				}
				SpellUtils.SetupSpell(m_speechBubbleSpell, this);
			}
			if (m_speechBubbleSpell != null && m_Backplate != null && m_RenderSpellOnSpeechBubbleLayer)
			{
				Renderer component = m_Backplate.GetComponent<Renderer>();
				if (component != null)
				{
					int layer = m_Backplate.gameObject.layer;
					SetRenderQue component2 = m_Backplate.GetComponent<SetRenderQue>();
					int num = ((component2 != null) ? component2.queue : 0);
					int renderQueue = component.GetMaterial().renderQueue + num;
					SceneUtils.SetLayer(m_speechBubbleSpell, layer);
					SceneUtils.SetRenderQueue(m_speechBubbleSpell.gameObject, renderQueue, includeInactive: true);
				}
			}
		}
		m_startingScale = base.transform.localScale;
		base.transform.localScale = Vector3.zero;
	}

	private void Update()
	{
		if (m_Text == null && m_VisualEmoteImage == null)
		{
			return;
		}
		if (EmoteHandler.Get().EmoteSpamBlocked())
		{
			if (!m_textIsGrey)
			{
				m_textIsGrey = true;
				if (m_Text != null)
				{
					m_Text.TextColor = new Color(137f / 255f, 137f / 255f, 137f / 255f);
				}
				if (m_VisualEmoteImage != null)
				{
					m_VisualEmoteImage.GetComponent<Renderer>().GetMaterial().color = new Color(1f, 1f, 1f, 0.5f);
				}
			}
		}
		else if (m_textIsGrey)
		{
			m_textIsGrey = false;
			if (m_Text != null)
			{
				m_Text.TextColor = new Color(0f, 0f, 0f);
			}
			if (m_VisualEmoteImage != null)
			{
				m_VisualEmoteImage.GetComponent<Renderer>().GetMaterial().color = new Color(1f, 1f, 1f, 1f);
			}
		}
	}

	public void DoClick()
	{
		EmoteHandler.Get().ResetTimeSinceLastEmote();
		EmoteType emoteType = m_currentEmoteType;
		EmoteType emoteResponseType = EmoteHandler.Get().GetEmoteResponseType(m_currentEmoteType);
		Card heroCard = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		if (EmoteHandler.Get().ShouldUseEmoteResponse(m_currentEmoteType, Player.Side.FRIENDLY) && heroCard.GetEmoteEntry(emoteResponseType) != null)
		{
			emoteType = emoteResponseType;
		}
		heroCard.PlayEmote(emoteType);
		Network.Get().SendEmote(emoteType);
		EmoteHandler.Get().HideEmotes();
	}

	public void Enable()
	{
		m_Backplate.enabled = true;
		if (m_Text != null)
		{
			m_Text.gameObject.SetActive(value: true);
		}
		if (m_VisualEmoteImage != null)
		{
			m_VisualEmoteImage.gameObject.SetActive(value: true);
		}
		GetComponent<Collider>().enabled = true;
		iTween.Stop(base.gameObject);
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", m_startingScale, "time", 0.5f, "ignoretimescale", true, "easetype", iTween.EaseType.easeOutElastic));
		if (m_speechBubbleSpell != null)
		{
			TransformUtil.CopyWorld(m_speechBubbleSpell, base.gameObject);
			m_speechBubbleSpell.transform.localScale = Vector3.one;
			m_speechBubbleSpell.ActivateState(SpellStateType.BIRTH);
		}
	}

	public void Disable()
	{
		GetComponent<Collider>().enabled = false;
		iTween.Stop(base.gameObject);
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", Vector3.zero, "time", 0.1f, "ignoretimescale", true, "easetype", iTween.EaseType.linear, "oncompletetarget", base.gameObject, "oncomplete", "FinishDisable"));
		if (m_speechBubbleSpell != null)
		{
			m_speechBubbleSpell.ActivateState(SpellStateType.DEATH);
		}
	}

	public void HandleMouseOut()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", m_startingScale, "time", 0.2f, "ignoretimescale", true));
	}

	public void HandleMouseOver()
	{
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", m_startingScale * 1.1f, "time", 0.2f, "ignoretimescale", true));
	}

	public void UpdateEmoteType()
	{
		Player player = GameState.Get()?.GetFriendlySidePlayer();
		if (player != null && ShouldUseFallbackEmote(player))
		{
			m_currentEmoteType = m_FallbackEmoteType;
			m_currentStringTag = m_FallbackStringTag;
		}
		else
		{
			m_currentEmoteType = m_EmoteType;
			m_currentStringTag = m_StringTag;
		}
		if (m_Text != null)
		{
			m_Text.Text = GameStrings.Get(m_currentStringTag);
		}
	}

	public bool ShouldPlayerUseEmoteOverride(Player player)
	{
		if (player == null)
		{
			return false;
		}
		Card heroCard = player.GetHeroCard();
		if (heroCard == null)
		{
			return false;
		}
		if (heroCard.GetEmoteEntry(m_EmoteType) != null)
		{
			return true;
		}
		return false;
	}

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
		if (heroCard == null)
		{
			return false;
		}
		if (heroCard.GetEmoteEntry(m_EmoteType) != null)
		{
			return true;
		}
		if (heroCard.GetEmoteEntry(m_FallbackEmoteType) != null)
		{
			return true;
		}
		return false;
	}

	public bool HasEmoteTypeForPlayer(EmoteType emoteType, Player player)
	{
		if (ShouldUseFallbackEmote(player))
		{
			return emoteType == m_FallbackEmoteType;
		}
		return emoteType == m_EmoteType;
	}

	private bool ShouldUseFallbackEmote(Player player)
	{
		if (player == null)
		{
			return false;
		}
		Card heroCard = player.GetHeroCard();
		if (heroCard == null)
		{
			return false;
		}
		if (heroCard.GetEmoteEntry(m_EmoteType) != null)
		{
			return false;
		}
		if (heroCard.GetEmoteEntry(m_FallbackEmoteType) == null)
		{
			return false;
		}
		return true;
	}

	private void FinishDisable()
	{
		if (!GetComponent<Collider>().enabled)
		{
			m_Backplate.enabled = false;
			if (m_Text != null)
			{
				m_Text.gameObject.SetActive(value: false);
			}
		}
	}
}
