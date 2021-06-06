using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.Serialization;

// Token: 0x02000A40 RID: 2624
public class HighlightState : MonoBehaviour
{
	// Token: 0x06008CF8 RID: 36088 RVA: 0x002D2E70 File Offset: 0x002D1070
	private void Awake()
	{
		if (this.m_RenderPlane == null)
		{
			if (!Application.isEditor)
			{
				Debug.LogError("m_RenderPlane is null!");
			}
			base.enabled = false;
		}
		else
		{
			this.m_RenderPlane.GetComponent<Renderer>().enabled = false;
			this.m_VisibilityState = false;
			this.m_FSM = this.m_RenderPlane.GetComponent<PlayMakerFSM>();
		}
		if (this.m_FSM != null)
		{
			this.m_FSM.enabled = true;
		}
		if (this.m_highlightType == HighlightStateType.NONE)
		{
			Transform parent = base.transform.parent;
			if (parent != null)
			{
				if (parent.GetComponent<ActorStateMgr>())
				{
					this.m_highlightType = HighlightStateType.CARD;
				}
				else
				{
					this.m_highlightType = HighlightStateType.HIGHLIGHT;
				}
			}
		}
		if (this.m_highlightType == HighlightStateType.NONE)
		{
			Debug.LogError("m_highlightType is not set!");
			base.enabled = false;
		}
		this.Setup();
	}

	// Token: 0x06008CF9 RID: 36089 RVA: 0x002D2F44 File Offset: 0x002D1144
	private void Update()
	{
		if (this.m_debugState != ActorStateType.NONE)
		{
			this.ChangeState(this.m_debugState);
			this.ForceUpdate();
		}
		if (!this.m_Hide)
		{
			if (this.m_isDirty)
			{
				if (this.m_RenderPlane == null)
				{
					return;
				}
				if (this.m_RenderPlane.GetComponent<Renderer>().enabled)
				{
					this.UpdateSilouette();
					this.m_isDirty = false;
				}
			}
			return;
		}
		if (this.m_RenderPlane == null)
		{
			return;
		}
		this.m_RenderPlane.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x06008CFA RID: 36090 RVA: 0x002D2FCB File Offset: 0x002D11CB
	private void OnApplicationFocus(bool state)
	{
		this.m_isDirty = true;
		this.m_forceRerender = true;
	}

	// Token: 0x06008CFB RID: 36091 RVA: 0x002D2FDB File Offset: 0x002D11DB
	protected void OnDestroy()
	{
		if (this.m_Material)
		{
			UnityEngine.Object.Destroy(this.m_Material);
		}
	}

	// Token: 0x06008CFC RID: 36092 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void LateUpdate()
	{
	}

	// Token: 0x06008CFD RID: 36093 RVA: 0x002D2FF8 File Offset: 0x002D11F8
	private void Setup()
	{
		this.m_seed = UnityEngine.Random.value;
		this.m_CurrentState = ActorStateType.CARD_IDLE;
		Renderer component = this.m_RenderPlane.GetComponent<Renderer>();
		component.enabled = false;
		this.m_VisibilityState = false;
		if (this.m_Material == null)
		{
			Shader shader = ShaderUtils.FindShader(this.HIGHLIGHT_SHADER_NAME);
			if (!shader)
			{
				Debug.LogError("Failed to load Highlight Shader: " + this.HIGHLIGHT_SHADER_NAME);
				base.enabled = false;
			}
			this.m_Material = new Material(shader);
		}
		component.SetSharedMaterial(this.m_Material);
	}

	// Token: 0x06008CFE RID: 36094 RVA: 0x002D3088 File Offset: 0x002D1288
	public void Show()
	{
		this.m_Hide = false;
		if (this.m_RenderPlane == null)
		{
			return;
		}
		if (this.m_VisibilityState && !this.m_RenderPlane.GetComponent<Renderer>().enabled)
		{
			this.m_RenderPlane.GetComponent<Renderer>().enabled = true;
		}
	}

	// Token: 0x06008CFF RID: 36095 RVA: 0x002D30D6 File Offset: 0x002D12D6
	public void Hide()
	{
		this.m_Hide = true;
		if (this.m_RenderPlane == null)
		{
			return;
		}
		this.m_RenderPlane.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x06008D00 RID: 36096 RVA: 0x002D30FF File Offset: 0x002D12FF
	public void SetDirty()
	{
		this.m_isDirty = true;
	}

	// Token: 0x06008D01 RID: 36097 RVA: 0x002D2FCB File Offset: 0x002D11CB
	public void ForceUpdate()
	{
		this.m_isDirty = true;
		this.m_forceRerender = true;
	}

	// Token: 0x06008D02 RID: 36098 RVA: 0x002D3108 File Offset: 0x002D1308
	public void ContinuousUpdate(float updateTime)
	{
		base.StartCoroutine(this.ContinuousSilouetteRender(updateTime));
	}

	// Token: 0x06008D03 RID: 36099 RVA: 0x002D3118 File Offset: 0x002D1318
	public bool IsReady()
	{
		return this.m_Material != null;
	}

	// Token: 0x06008D04 RID: 36100 RVA: 0x002D3128 File Offset: 0x002D1328
	public bool ChangeState(ActorStateType stateType)
	{
		if (stateType == this.m_CurrentState)
		{
			return true;
		}
		this.m_PreviousState = this.m_CurrentState;
		this.m_CurrentState = stateType;
		if (stateType == ActorStateType.NONE)
		{
			this.m_RenderPlane.GetComponent<Renderer>().enabled = false;
			this.m_VisibilityState = false;
			return true;
		}
		if (stateType != ActorStateType.CARD_IDLE && stateType != ActorStateType.HIGHLIGHT_OFF)
		{
			foreach (HighlightRenderState highlightRenderState in this.m_HighlightStates)
			{
				if (highlightRenderState.m_StateType == stateType)
				{
					Renderer component = this.m_RenderPlane.GetComponent<Renderer>();
					if (highlightRenderState.m_Material != null && this.m_Material != null)
					{
						this.m_Material.CopyPropertiesFromMaterial(highlightRenderState.m_Material);
						component.SetSharedMaterial(this.m_Material);
						component.GetSharedMaterial().SetFloat("_Seed", this.m_seed);
						bool result = this.RenderSilouette();
						if (stateType == ActorStateType.CARD_HISTORY)
						{
							base.transform.localPosition = this.m_HistoryTranslation;
						}
						else
						{
							base.transform.localPosition = highlightRenderState.m_Offset;
						}
						if (this.m_FSM == null)
						{
							if (!this.m_Hide)
							{
								component.enabled = true;
							}
							this.m_VisibilityState = true;
						}
						else
						{
							this.m_BirthTransition = stateType.ToString();
							this.m_SecondBirthTransition = this.m_PreviousState.ToString();
							this.m_IdleTransition = this.m_BirthTransition;
							this.SendDataToPlaymaker();
							this.SendPlaymakerBirthEvent();
						}
						return result;
					}
					component.enabled = false;
					this.m_VisibilityState = false;
					return true;
				}
			}
			if (this.m_highlightType == HighlightStateType.CARD)
			{
				this.m_CurrentState = ActorStateType.CARD_IDLE;
			}
			else if (this.m_highlightType == HighlightStateType.HIGHLIGHT)
			{
				this.m_CurrentState = ActorStateType.HIGHLIGHT_OFF;
			}
			this.m_DeathTransition = this.m_PreviousState.ToString();
			this.SendDataToPlaymaker();
			this.SendPlaymakerDeathEvent();
			this.m_RenderPlane.GetComponent<Renderer>().enabled = false;
			this.m_VisibilityState = false;
			return false;
		}
		if (this.m_FSM == null)
		{
			this.m_RenderPlane.GetComponent<Renderer>().enabled = false;
			this.m_VisibilityState = false;
			return true;
		}
		this.m_DeathTransition = this.m_PreviousState.ToString();
		this.SendDataToPlaymaker();
		this.SendPlaymakerDeathEvent();
		return true;
	}

	// Token: 0x170007F2 RID: 2034
	// (get) Token: 0x06008D05 RID: 36101 RVA: 0x002D3394 File Offset: 0x002D1594
	public ActorStateType CurrentState
	{
		get
		{
			return this.m_CurrentState;
		}
	}

	// Token: 0x06008D06 RID: 36102 RVA: 0x002D339C File Offset: 0x002D159C
	protected void UpdateSilouette()
	{
		this.RenderSilouette();
	}

	// Token: 0x06008D07 RID: 36103 RVA: 0x002D33A8 File Offset: 0x002D15A8
	private bool RenderSilouette()
	{
		this.m_isDirty = false;
		if (this.m_StaticSilouetteTexture != null)
		{
			Texture2D mainTexture = this.m_StaticSilouetteTexture;
			Actor actor = SceneUtils.FindComponentInParents<Actor>(base.gameObject);
			if (actor != null)
			{
				CardSilhouetteOverride cardSilhouetteOverride = actor.CardSilhouetteOverride;
				bool flag = actor.IsElite();
				bool flag2 = actor.IsMultiClass() && (actor.GetCardSet() == TAG_CARD_SET.GANGS || actor.GetCardSet() == TAG_CARD_SET.GANGS_RESERVE);
				if (cardSilhouetteOverride != CardSilhouetteOverride.SingleClass)
				{
					if (cardSilhouetteOverride == CardSilhouetteOverride.TriClassBanner)
					{
						flag2 = true;
					}
				}
				else
				{
					flag2 = false;
				}
				if (flag && this.m_StaticSilouetteTextureUnique != null)
				{
					mainTexture = this.m_StaticSilouetteTextureUnique;
				}
				if (flag2 && this.m_TriClassBannerStaticSilouetteTexture != null)
				{
					mainTexture = this.m_TriClassBannerStaticSilouetteTexture;
				}
				if (flag && flag2 && this.m_TriClassBannerStaticSilouetteTextureUnique != null)
				{
					mainTexture = this.m_TriClassBannerStaticSilouetteTextureUnique;
				}
			}
			Material sharedMaterial = this.m_RenderPlane.GetComponent<Renderer>().GetSharedMaterial();
			sharedMaterial.mainTexture = mainTexture;
			sharedMaterial.renderQueue = this.m_RenderQueueOffset + this.m_RenderQueue;
			this.m_forceRerender = false;
			return true;
		}
		HighlightRender component = this.m_RenderPlane.GetComponent<HighlightRender>();
		if (component == null)
		{
			Debug.LogError("Unable to find HighlightRender component on m_RenderPlane");
			return false;
		}
		if (component.enabled)
		{
			component.CreateSilhouetteTexture(this.m_forceRerender);
			Material sharedMaterial2 = this.m_RenderPlane.GetComponent<Renderer>().GetSharedMaterial();
			sharedMaterial2.mainTexture = component.SilhouetteTexture;
			sharedMaterial2.renderQueue = this.m_RenderQueueOffset + this.m_RenderQueue;
		}
		this.m_forceRerender = false;
		return true;
	}

	// Token: 0x06008D08 RID: 36104 RVA: 0x002D351C File Offset: 0x002D171C
	private IEnumerator ContinuousSilouetteRender(float renderTime)
	{
		if (this.m_RenderPlane == null || GraphicsManager.Get() == null)
		{
			yield break;
		}
		Renderer renderer = this.m_RenderPlane.GetComponent<Renderer>();
		if (renderer == null)
		{
			yield break;
		}
		if (GraphicsManager.Get().RenderQualityLevel != GraphicsQuality.Low)
		{
			float endTime = Time.realtimeSinceStartup + renderTime;
			while (Time.realtimeSinceStartup < endTime)
			{
				if (renderer.enabled)
				{
					this.m_isDirty = true;
					this.m_forceRerender = true;
					this.RenderSilouette();
				}
				yield return null;
			}
			yield break;
		}
		yield return new WaitForSeconds(renderTime);
		if (!renderer.enabled)
		{
			yield break;
		}
		this.m_isDirty = true;
		this.m_forceRerender = true;
		this.RenderSilouette();
		yield break;
	}

	// Token: 0x06008D09 RID: 36105 RVA: 0x002D3534 File Offset: 0x002D1734
	private void SendDataToPlaymaker()
	{
		if (this.m_FSM == null)
		{
			return;
		}
		FsmMaterial fsmMaterial = this.m_FSM.FsmVariables.GetFsmMaterial("HighlightMaterial");
		if (fsmMaterial != null)
		{
			fsmMaterial.Value = this.m_RenderPlane.GetComponent<Renderer>().GetSharedMaterial();
		}
		FsmString fsmString = this.m_FSM.FsmVariables.GetFsmString("CurrentState");
		if (fsmString != null)
		{
			fsmString.Value = this.m_CurrentState.ToString();
		}
		FsmString fsmString2 = this.m_FSM.FsmVariables.GetFsmString("PreviousState");
		if (fsmString2 != null)
		{
			fsmString2.Value = this.m_PreviousState.ToString();
		}
	}

	// Token: 0x06008D0A RID: 36106 RVA: 0x002D35E0 File Offset: 0x002D17E0
	private void SendPlaymakerDeathEvent()
	{
		if (this.m_FSM == null)
		{
			return;
		}
		FsmString fsmString = this.m_FSM.FsmVariables.GetFsmString("DeathTransition");
		if (fsmString != null)
		{
			fsmString.Value = this.m_DeathTransition;
		}
		this.m_FSM.SendEvent("Death");
	}

	// Token: 0x06008D0B RID: 36107 RVA: 0x002D3634 File Offset: 0x002D1834
	private void SendPlaymakerBirthEvent()
	{
		if (this.m_FSM == null)
		{
			return;
		}
		FsmString fsmString = this.m_FSM.FsmVariables.GetFsmString("BirthTransition");
		if (fsmString != null)
		{
			fsmString.Value = this.m_BirthTransition;
		}
		FsmString fsmString2 = this.m_FSM.FsmVariables.GetFsmString("SecondBirthTransition");
		if (fsmString2 != null)
		{
			fsmString2.Value = this.m_SecondBirthTransition;
		}
		FsmString fsmString3 = this.m_FSM.FsmVariables.GetFsmString("IdleTransition");
		if (fsmString3 != null)
		{
			fsmString3.Value = this.m_IdleTransition;
		}
		this.m_FSM.SendEvent("Birth");
	}

	// Token: 0x06008D0C RID: 36108 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void OnActionFinished()
	{
	}

	// Token: 0x040075E5 RID: 30181
	private readonly string HIGHLIGHT_SHADER_NAME = "Custom/Selection/Highlight";

	// Token: 0x040075E6 RID: 30182
	private const string FSM_BIRTH_STATE = "Birth";

	// Token: 0x040075E7 RID: 30183
	private const string FSM_IDLE_STATE = "Idle";

	// Token: 0x040075E8 RID: 30184
	private const string FSM_DEATH_STATE = "Death";

	// Token: 0x040075E9 RID: 30185
	private const string FSM_BIRTHTRANSITION_STATE = "BirthTransition";

	// Token: 0x040075EA RID: 30186
	private const string FSM_IDLETRANSITION_STATE = "IdleTransition";

	// Token: 0x040075EB RID: 30187
	private const string FSM_DEATHTRANSITION_STATE = "DeathTransition";

	// Token: 0x040075EC RID: 30188
	public GameObject m_RenderPlane;

	// Token: 0x040075ED RID: 30189
	public HighlightStateType m_highlightType;

	// Token: 0x040075EE RID: 30190
	public Texture2D m_StaticSilouetteTexture;

	// Token: 0x040075EF RID: 30191
	public Texture2D m_StaticSilouetteTextureUnique;

	// Token: 0x040075F0 RID: 30192
	[FormerlySerializedAs("m_MultiClassStaticSilouetteTexture")]
	public Texture2D m_TriClassBannerStaticSilouetteTexture;

	// Token: 0x040075F1 RID: 30193
	[FormerlySerializedAs("m_MultiClassStaticSilouetteTextureUnique")]
	public Texture2D m_TriClassBannerStaticSilouetteTextureUnique;

	// Token: 0x040075F2 RID: 30194
	public Vector3 m_HistoryTranslation = new Vector3(0f, -0.1f, 0f);

	// Token: 0x040075F3 RID: 30195
	public int m_RenderQueue;

	// Token: 0x040075F4 RID: 30196
	public int m_RenderQueueOffset = 3000;

	// Token: 0x040075F5 RID: 30197
	public List<HighlightRenderState> m_HighlightStates;

	// Token: 0x040075F6 RID: 30198
	public ActorStateType m_debugState;

	// Token: 0x040075F7 RID: 30199
	protected ActorStateType m_PreviousState;

	// Token: 0x040075F8 RID: 30200
	protected ActorStateType m_CurrentState;

	// Token: 0x040075F9 RID: 30201
	protected PlayMakerFSM m_FSM;

	// Token: 0x040075FA RID: 30202
	private string m_sendEvent;

	// Token: 0x040075FB RID: 30203
	private bool m_isDirty;

	// Token: 0x040075FC RID: 30204
	private bool m_forceRerender;

	// Token: 0x040075FD RID: 30205
	private string m_BirthTransition = "None";

	// Token: 0x040075FE RID: 30206
	private string m_SecondBirthTransition = "None";

	// Token: 0x040075FF RID: 30207
	private string m_IdleTransition = "None";

	// Token: 0x04007600 RID: 30208
	private string m_DeathTransition = "None";

	// Token: 0x04007601 RID: 30209
	private bool m_Hide;

	// Token: 0x04007602 RID: 30210
	private bool m_VisibilityState;

	// Token: 0x04007603 RID: 30211
	private float m_seed;

	// Token: 0x04007604 RID: 30212
	private Material m_Material;
}
