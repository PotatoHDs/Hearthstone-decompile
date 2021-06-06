using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class BoxLightMgr : MonoBehaviour
{
	// Token: 0x06000C5C RID: 3164 RVA: 0x0004898F File Offset: 0x00046B8F
	private void Start()
	{
		this.UpdateState();
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x00048997 File Offset: 0x00046B97
	public BoxLightStateType GetActiveState()
	{
		return this.m_activeStateType;
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x0004899F File Offset: 0x00046B9F
	public void ChangeState(BoxLightStateType stateType)
	{
		if (stateType == BoxLightStateType.INVALID)
		{
			return;
		}
		if (this.m_activeStateType == stateType)
		{
			return;
		}
		this.ChangeStateImpl(stateType);
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x000489B6 File Offset: 0x00046BB6
	public void SetState(BoxLightStateType stateType)
	{
		if (this.m_activeStateType == stateType)
		{
			return;
		}
		this.m_activeStateType = stateType;
		this.UpdateState();
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x000489D0 File Offset: 0x00046BD0
	public void UpdateState()
	{
		BoxLightState boxLightState = this.FindState(this.m_activeStateType);
		if (boxLightState == null)
		{
			return;
		}
		boxLightState.m_Spell.ActivateState(SpellStateType.ACTION);
		iTween.Stop(base.gameObject);
		RenderSettings.ambientLight = boxLightState.m_AmbientColor;
		if (boxLightState.m_LightInfos == null)
		{
			return;
		}
		foreach (BoxLightInfo boxLightInfo in boxLightState.m_LightInfos)
		{
			iTween.Stop(boxLightInfo.m_Light.gameObject);
			boxLightInfo.m_Light.color = boxLightInfo.m_Color;
			boxLightInfo.m_Light.intensity = boxLightInfo.m_Intensity;
			LightType type = boxLightInfo.m_Light.type;
			if (type == LightType.Point || type == LightType.Spot)
			{
				boxLightInfo.m_Light.range = boxLightInfo.m_Range;
				if (type == LightType.Spot)
				{
					boxLightInfo.m_Light.spotAngle = boxLightInfo.m_SpotAngle;
				}
			}
		}
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x00048AC4 File Offset: 0x00046CC4
	private BoxLightState FindState(BoxLightStateType stateType)
	{
		foreach (BoxLightState boxLightState in this.m_States)
		{
			if (boxLightState.m_Type == stateType)
			{
				return boxLightState;
			}
		}
		return null;
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x00048B20 File Offset: 0x00046D20
	private void ChangeStateImpl(BoxLightStateType stateType)
	{
		this.m_activeStateType = stateType;
		BoxLightState boxLightState = this.FindState(stateType);
		if (boxLightState == null)
		{
			return;
		}
		iTween.Stop(base.gameObject);
		boxLightState.m_Spell.ActivateState(SpellStateType.BIRTH);
		this.ChangeAmbient(boxLightState);
		if (boxLightState.m_LightInfos != null)
		{
			foreach (BoxLightInfo lightInfo in boxLightState.m_LightInfos)
			{
				this.ChangeLight(boxLightState, lightInfo);
			}
		}
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x00048BB0 File Offset: 0x00046DB0
	private void ChangeAmbient(BoxLightState state)
	{
		Color prevAmbientColor = RenderSettings.ambientLight;
		Action<object> action = delegate(object amount)
		{
			RenderSettings.ambientLight = Color.Lerp(prevAmbientColor, state.m_AmbientColor, (float)amount);
		};
		Hashtable args = iTween.Hash(new object[]
		{
			"from",
			0f,
			"to",
			1f,
			"delay",
			state.m_DelaySec,
			"time",
			state.m_TransitionSec,
			"easetype",
			state.m_TransitionEaseType,
			"onupdate",
			action
		});
		iTween.ValueTo(base.gameObject, args);
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x00048C88 File Offset: 0x00046E88
	private void ChangeLight(BoxLightState state, BoxLightInfo lightInfo)
	{
		iTween.Stop(lightInfo.m_Light.gameObject);
		Hashtable args = iTween.Hash(new object[]
		{
			"color",
			lightInfo.m_Color,
			"delay",
			state.m_DelaySec,
			"time",
			state.m_TransitionSec,
			"easetype",
			state.m_TransitionEaseType
		});
		iTween.ColorTo(lightInfo.m_Light.gameObject, args);
		float intensity = lightInfo.m_Light.intensity;
		Action<object> action = delegate(object amount)
		{
			lightInfo.m_Light.intensity = (float)amount;
		};
		Hashtable args2 = iTween.Hash(new object[]
		{
			"from",
			intensity,
			"to",
			lightInfo.m_Intensity,
			"delay",
			state.m_DelaySec,
			"time",
			state.m_TransitionSec,
			"easetype",
			state.m_TransitionEaseType,
			"onupdate",
			action
		});
		iTween.ValueTo(lightInfo.m_Light.gameObject, args2);
		LightType type = lightInfo.m_Light.type;
		if (type == LightType.Point || type == LightType.Spot)
		{
			float range = lightInfo.m_Light.range;
			Action<object> action2 = delegate(object amount)
			{
				lightInfo.m_Light.range = (float)amount;
			};
			Hashtable args3 = iTween.Hash(new object[]
			{
				"from",
				range,
				"to",
				lightInfo.m_Range,
				"delay",
				state.m_DelaySec,
				"time",
				state.m_TransitionSec,
				"easetype",
				state.m_TransitionEaseType,
				"onupdate",
				action2
			});
			iTween.ValueTo(lightInfo.m_Light.gameObject, args3);
			if (type == LightType.Spot)
			{
				float spotAngle = lightInfo.m_Light.spotAngle;
				Action<object> action3 = delegate(object amount)
				{
					lightInfo.m_Light.spotAngle = (float)amount;
				};
				Hashtable args4 = iTween.Hash(new object[]
				{
					"from",
					spotAngle,
					"to",
					lightInfo.m_SpotAngle,
					"delay",
					state.m_DelaySec,
					"time",
					state.m_TransitionSec,
					"easetype",
					state.m_TransitionEaseType,
					"onupdate",
					action3
				});
				iTween.ValueTo(lightInfo.m_Light.gameObject, args4);
			}
		}
	}

	// Token: 0x04000895 RID: 2197
	public List<BoxLightState> m_States;

	// Token: 0x04000896 RID: 2198
	private BoxLightStateType m_activeStateType = BoxLightStateType.DEFAULT;
}
