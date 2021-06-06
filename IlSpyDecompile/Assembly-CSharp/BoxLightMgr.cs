using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLightMgr : MonoBehaviour
{
	public List<BoxLightState> m_States;

	private BoxLightStateType m_activeStateType = BoxLightStateType.DEFAULT;

	private void Start()
	{
		UpdateState();
	}

	public BoxLightStateType GetActiveState()
	{
		return m_activeStateType;
	}

	public void ChangeState(BoxLightStateType stateType)
	{
		if (stateType != 0 && m_activeStateType != stateType)
		{
			ChangeStateImpl(stateType);
		}
	}

	public void SetState(BoxLightStateType stateType)
	{
		if (m_activeStateType != stateType)
		{
			m_activeStateType = stateType;
			UpdateState();
		}
	}

	public void UpdateState()
	{
		BoxLightState boxLightState = FindState(m_activeStateType);
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
		foreach (BoxLightInfo lightInfo in boxLightState.m_LightInfos)
		{
			iTween.Stop(lightInfo.m_Light.gameObject);
			lightInfo.m_Light.color = lightInfo.m_Color;
			lightInfo.m_Light.intensity = lightInfo.m_Intensity;
			LightType type = lightInfo.m_Light.type;
			if (type == LightType.Point || type == LightType.Spot)
			{
				lightInfo.m_Light.range = lightInfo.m_Range;
				if (type == LightType.Spot)
				{
					lightInfo.m_Light.spotAngle = lightInfo.m_SpotAngle;
				}
			}
		}
	}

	private BoxLightState FindState(BoxLightStateType stateType)
	{
		foreach (BoxLightState state in m_States)
		{
			if (state.m_Type == stateType)
			{
				return state;
			}
		}
		return null;
	}

	private void ChangeStateImpl(BoxLightStateType stateType)
	{
		m_activeStateType = stateType;
		BoxLightState boxLightState = FindState(stateType);
		if (boxLightState == null)
		{
			return;
		}
		iTween.Stop(base.gameObject);
		boxLightState.m_Spell.ActivateState(SpellStateType.BIRTH);
		ChangeAmbient(boxLightState);
		if (boxLightState.m_LightInfos == null)
		{
			return;
		}
		foreach (BoxLightInfo lightInfo in boxLightState.m_LightInfos)
		{
			ChangeLight(boxLightState, lightInfo);
		}
	}

	private void ChangeAmbient(BoxLightState state)
	{
		Color prevAmbientColor = RenderSettings.ambientLight;
		Action<object> action = delegate(object amount)
		{
			RenderSettings.ambientLight = Color.Lerp(prevAmbientColor, state.m_AmbientColor, (float)amount);
		};
		Hashtable args = iTween.Hash("from", 0f, "to", 1f, "delay", state.m_DelaySec, "time", state.m_TransitionSec, "easetype", state.m_TransitionEaseType, "onupdate", action);
		iTween.ValueTo(base.gameObject, args);
	}

	private void ChangeLight(BoxLightState state, BoxLightInfo lightInfo)
	{
		iTween.Stop(lightInfo.m_Light.gameObject);
		Hashtable args = iTween.Hash("color", lightInfo.m_Color, "delay", state.m_DelaySec, "time", state.m_TransitionSec, "easetype", state.m_TransitionEaseType);
		iTween.ColorTo(lightInfo.m_Light.gameObject, args);
		float intensity = lightInfo.m_Light.intensity;
		Action<object> action = delegate(object amount)
		{
			lightInfo.m_Light.intensity = (float)amount;
		};
		Hashtable args2 = iTween.Hash("from", intensity, "to", lightInfo.m_Intensity, "delay", state.m_DelaySec, "time", state.m_TransitionSec, "easetype", state.m_TransitionEaseType, "onupdate", action);
		iTween.ValueTo(lightInfo.m_Light.gameObject, args2);
		LightType type = lightInfo.m_Light.type;
		if (type != LightType.Point && type != 0)
		{
			return;
		}
		float range = lightInfo.m_Light.range;
		Action<object> action2 = delegate(object amount)
		{
			lightInfo.m_Light.range = (float)amount;
		};
		Hashtable args3 = iTween.Hash("from", range, "to", lightInfo.m_Range, "delay", state.m_DelaySec, "time", state.m_TransitionSec, "easetype", state.m_TransitionEaseType, "onupdate", action2);
		iTween.ValueTo(lightInfo.m_Light.gameObject, args3);
		if (type == LightType.Spot)
		{
			float spotAngle = lightInfo.m_Light.spotAngle;
			Action<object> action3 = delegate(object amount)
			{
				lightInfo.m_Light.spotAngle = (float)amount;
			};
			Hashtable args4 = iTween.Hash("from", spotAngle, "to", lightInfo.m_SpotAngle, "delay", state.m_DelaySec, "time", state.m_TransitionSec, "easetype", state.m_TransitionEaseType, "onupdate", action3);
			iTween.ValueTo(lightInfo.m_Light.gameObject, args4);
		}
	}
}
