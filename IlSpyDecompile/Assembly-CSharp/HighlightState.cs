using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.Serialization;

public class HighlightState : MonoBehaviour
{
	private readonly string HIGHLIGHT_SHADER_NAME = "Custom/Selection/Highlight";

	private const string FSM_BIRTH_STATE = "Birth";

	private const string FSM_IDLE_STATE = "Idle";

	private const string FSM_DEATH_STATE = "Death";

	private const string FSM_BIRTHTRANSITION_STATE = "BirthTransition";

	private const string FSM_IDLETRANSITION_STATE = "IdleTransition";

	private const string FSM_DEATHTRANSITION_STATE = "DeathTransition";

	public GameObject m_RenderPlane;

	public HighlightStateType m_highlightType;

	public Texture2D m_StaticSilouetteTexture;

	public Texture2D m_StaticSilouetteTextureUnique;

	[FormerlySerializedAs("m_MultiClassStaticSilouetteTexture")]
	public Texture2D m_TriClassBannerStaticSilouetteTexture;

	[FormerlySerializedAs("m_MultiClassStaticSilouetteTextureUnique")]
	public Texture2D m_TriClassBannerStaticSilouetteTextureUnique;

	public Vector3 m_HistoryTranslation = new Vector3(0f, -0.1f, 0f);

	public int m_RenderQueue;

	public int m_RenderQueueOffset = 3000;

	public List<HighlightRenderState> m_HighlightStates;

	public ActorStateType m_debugState;

	protected ActorStateType m_PreviousState;

	protected ActorStateType m_CurrentState;

	protected PlayMakerFSM m_FSM;

	private string m_sendEvent;

	private bool m_isDirty;

	private bool m_forceRerender;

	private string m_BirthTransition = "None";

	private string m_SecondBirthTransition = "None";

	private string m_IdleTransition = "None";

	private string m_DeathTransition = "None";

	private bool m_Hide;

	private bool m_VisibilityState;

	private float m_seed;

	private Material m_Material;

	public ActorStateType CurrentState => m_CurrentState;

	private void Awake()
	{
		if (m_RenderPlane == null)
		{
			if (!Application.isEditor)
			{
				Debug.LogError("m_RenderPlane is null!");
			}
			base.enabled = false;
		}
		else
		{
			m_RenderPlane.GetComponent<Renderer>().enabled = false;
			m_VisibilityState = false;
			m_FSM = m_RenderPlane.GetComponent<PlayMakerFSM>();
		}
		if (m_FSM != null)
		{
			m_FSM.enabled = true;
		}
		if (m_highlightType == HighlightStateType.NONE)
		{
			Transform parent = base.transform.parent;
			if (parent != null)
			{
				if ((bool)parent.GetComponent<ActorStateMgr>())
				{
					m_highlightType = HighlightStateType.CARD;
				}
				else
				{
					m_highlightType = HighlightStateType.HIGHLIGHT;
				}
			}
		}
		if (m_highlightType == HighlightStateType.NONE)
		{
			Debug.LogError("m_highlightType is not set!");
			base.enabled = false;
		}
		Setup();
	}

	private void Update()
	{
		if (m_debugState != 0)
		{
			ChangeState(m_debugState);
			ForceUpdate();
		}
		if (m_Hide)
		{
			if (!(m_RenderPlane == null))
			{
				m_RenderPlane.GetComponent<Renderer>().enabled = false;
			}
		}
		else if (m_isDirty && !(m_RenderPlane == null) && m_RenderPlane.GetComponent<Renderer>().enabled)
		{
			UpdateSilouette();
			m_isDirty = false;
		}
	}

	private void OnApplicationFocus(bool state)
	{
		m_isDirty = true;
		m_forceRerender = true;
	}

	protected void OnDestroy()
	{
		if ((bool)m_Material)
		{
			Object.Destroy(m_Material);
		}
	}

	private void LateUpdate()
	{
	}

	private void Setup()
	{
		m_seed = Random.value;
		m_CurrentState = ActorStateType.CARD_IDLE;
		Renderer component = m_RenderPlane.GetComponent<Renderer>();
		component.enabled = false;
		m_VisibilityState = false;
		if (m_Material == null)
		{
			Shader shader = ShaderUtils.FindShader(HIGHLIGHT_SHADER_NAME);
			if (!shader)
			{
				Debug.LogError("Failed to load Highlight Shader: " + HIGHLIGHT_SHADER_NAME);
				base.enabled = false;
			}
			m_Material = new Material(shader);
		}
		component.SetSharedMaterial(m_Material);
	}

	public void Show()
	{
		m_Hide = false;
		if (!(m_RenderPlane == null) && m_VisibilityState && !m_RenderPlane.GetComponent<Renderer>().enabled)
		{
			m_RenderPlane.GetComponent<Renderer>().enabled = true;
		}
	}

	public void Hide()
	{
		m_Hide = true;
		if (!(m_RenderPlane == null))
		{
			m_RenderPlane.GetComponent<Renderer>().enabled = false;
		}
	}

	public void SetDirty()
	{
		m_isDirty = true;
	}

	public void ForceUpdate()
	{
		m_isDirty = true;
		m_forceRerender = true;
	}

	public void ContinuousUpdate(float updateTime)
	{
		StartCoroutine(ContinuousSilouetteRender(updateTime));
	}

	public bool IsReady()
	{
		return m_Material != null;
	}

	public bool ChangeState(ActorStateType stateType)
	{
		if (stateType == m_CurrentState)
		{
			return true;
		}
		m_PreviousState = m_CurrentState;
		m_CurrentState = stateType;
		switch (stateType)
		{
		case ActorStateType.NONE:
			m_RenderPlane.GetComponent<Renderer>().enabled = false;
			m_VisibilityState = false;
			return true;
		case ActorStateType.CARD_IDLE:
		case ActorStateType.HIGHLIGHT_OFF:
			if (m_FSM == null)
			{
				m_RenderPlane.GetComponent<Renderer>().enabled = false;
				m_VisibilityState = false;
				return true;
			}
			m_DeathTransition = m_PreviousState.ToString();
			SendDataToPlaymaker();
			SendPlaymakerDeathEvent();
			return true;
		default:
			foreach (HighlightRenderState highlightState in m_HighlightStates)
			{
				if (highlightState.m_StateType != stateType)
				{
					continue;
				}
				Renderer component = m_RenderPlane.GetComponent<Renderer>();
				if (highlightState.m_Material != null && m_Material != null)
				{
					m_Material.CopyPropertiesFromMaterial(highlightState.m_Material);
					component.SetSharedMaterial(m_Material);
					component.GetSharedMaterial().SetFloat("_Seed", m_seed);
					bool result = RenderSilouette();
					if (stateType == ActorStateType.CARD_HISTORY)
					{
						base.transform.localPosition = m_HistoryTranslation;
					}
					else
					{
						base.transform.localPosition = highlightState.m_Offset;
					}
					if (m_FSM == null)
					{
						if (!m_Hide)
						{
							component.enabled = true;
						}
						m_VisibilityState = true;
					}
					else
					{
						m_BirthTransition = stateType.ToString();
						m_SecondBirthTransition = m_PreviousState.ToString();
						m_IdleTransition = m_BirthTransition;
						SendDataToPlaymaker();
						SendPlaymakerBirthEvent();
					}
					return result;
				}
				component.enabled = false;
				m_VisibilityState = false;
				return true;
			}
			if (m_highlightType == HighlightStateType.CARD)
			{
				m_CurrentState = ActorStateType.CARD_IDLE;
			}
			else if (m_highlightType == HighlightStateType.HIGHLIGHT)
			{
				m_CurrentState = ActorStateType.HIGHLIGHT_OFF;
			}
			m_DeathTransition = m_PreviousState.ToString();
			SendDataToPlaymaker();
			SendPlaymakerDeathEvent();
			m_RenderPlane.GetComponent<Renderer>().enabled = false;
			m_VisibilityState = false;
			return false;
		}
	}

	protected void UpdateSilouette()
	{
		RenderSilouette();
	}

	private bool RenderSilouette()
	{
		m_isDirty = false;
		if (m_StaticSilouetteTexture != null)
		{
			Texture2D mainTexture = m_StaticSilouetteTexture;
			Actor actor = SceneUtils.FindComponentInParents<Actor>(base.gameObject);
			if (actor != null)
			{
				CardSilhouetteOverride cardSilhouetteOverride = actor.CardSilhouetteOverride;
				bool flag = actor.IsElite();
				bool flag2 = actor.IsMultiClass() && (actor.GetCardSet() == TAG_CARD_SET.GANGS || actor.GetCardSet() == TAG_CARD_SET.GANGS_RESERVE);
				switch (cardSilhouetteOverride)
				{
				case CardSilhouetteOverride.SingleClass:
					flag2 = false;
					break;
				case CardSilhouetteOverride.TriClassBanner:
					flag2 = true;
					break;
				}
				if (flag && m_StaticSilouetteTextureUnique != null)
				{
					mainTexture = m_StaticSilouetteTextureUnique;
				}
				if (flag2 && m_TriClassBannerStaticSilouetteTexture != null)
				{
					mainTexture = m_TriClassBannerStaticSilouetteTexture;
				}
				if (flag && flag2 && m_TriClassBannerStaticSilouetteTextureUnique != null)
				{
					mainTexture = m_TriClassBannerStaticSilouetteTextureUnique;
				}
			}
			Material sharedMaterial = m_RenderPlane.GetComponent<Renderer>().GetSharedMaterial();
			sharedMaterial.mainTexture = mainTexture;
			sharedMaterial.renderQueue = m_RenderQueueOffset + m_RenderQueue;
			m_forceRerender = false;
			return true;
		}
		HighlightRender component = m_RenderPlane.GetComponent<HighlightRender>();
		if (component == null)
		{
			Debug.LogError("Unable to find HighlightRender component on m_RenderPlane");
			return false;
		}
		if (component.enabled)
		{
			component.CreateSilhouetteTexture(m_forceRerender);
			Material sharedMaterial2 = m_RenderPlane.GetComponent<Renderer>().GetSharedMaterial();
			sharedMaterial2.mainTexture = component.SilhouetteTexture;
			sharedMaterial2.renderQueue = m_RenderQueueOffset + m_RenderQueue;
		}
		m_forceRerender = false;
		return true;
	}

	private IEnumerator ContinuousSilouetteRender(float renderTime)
	{
		if (m_RenderPlane == null || GraphicsManager.Get() == null)
		{
			yield break;
		}
		Renderer renderer = m_RenderPlane.GetComponent<Renderer>();
		if (renderer == null)
		{
			yield break;
		}
		if (GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
		{
			yield return new WaitForSeconds(renderTime);
			if (renderer.enabled)
			{
				m_isDirty = true;
				m_forceRerender = true;
				RenderSilouette();
			}
			yield break;
		}
		float endTime = Time.realtimeSinceStartup + renderTime;
		while (Time.realtimeSinceStartup < endTime)
		{
			if (renderer.enabled)
			{
				m_isDirty = true;
				m_forceRerender = true;
				RenderSilouette();
			}
			yield return null;
		}
	}

	private void SendDataToPlaymaker()
	{
		if (!(m_FSM == null))
		{
			FsmMaterial fsmMaterial = m_FSM.FsmVariables.GetFsmMaterial("HighlightMaterial");
			if (fsmMaterial != null)
			{
				fsmMaterial.Value = m_RenderPlane.GetComponent<Renderer>().GetSharedMaterial();
			}
			FsmString fsmString = m_FSM.FsmVariables.GetFsmString("CurrentState");
			if (fsmString != null)
			{
				fsmString.Value = m_CurrentState.ToString();
			}
			FsmString fsmString2 = m_FSM.FsmVariables.GetFsmString("PreviousState");
			if (fsmString2 != null)
			{
				fsmString2.Value = m_PreviousState.ToString();
			}
		}
	}

	private void SendPlaymakerDeathEvent()
	{
		if (!(m_FSM == null))
		{
			FsmString fsmString = m_FSM.FsmVariables.GetFsmString("DeathTransition");
			if (fsmString != null)
			{
				fsmString.Value = m_DeathTransition;
			}
			m_FSM.SendEvent("Death");
		}
	}

	private void SendPlaymakerBirthEvent()
	{
		if (!(m_FSM == null))
		{
			FsmString fsmString = m_FSM.FsmVariables.GetFsmString("BirthTransition");
			if (fsmString != null)
			{
				fsmString.Value = m_BirthTransition;
			}
			FsmString fsmString2 = m_FSM.FsmVariables.GetFsmString("SecondBirthTransition");
			if (fsmString2 != null)
			{
				fsmString2.Value = m_SecondBirthTransition;
			}
			FsmString fsmString3 = m_FSM.FsmVariables.GetFsmString("IdleTransition");
			if (fsmString3 != null)
			{
				fsmString3.Value = m_IdleTransition;
			}
			m_FSM.SendEvent("Birth");
		}
	}

	public void OnActionFinished()
	{
	}
}
