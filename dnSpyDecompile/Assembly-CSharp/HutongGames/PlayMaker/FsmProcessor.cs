using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace HutongGames.PlayMaker
{
	// Token: 0x02000B98 RID: 2968
	[Preserve]
	public class FsmProcessor
	{
		// Token: 0x06009B8A RID: 39818 RVA: 0x0031FB94 File Offset: 0x0031DD94
		public static void OnPreprocess(PlayMakerFSM fsm)
		{
			if (fsm.Fsm.HandleLegacyNetworking && !FsmProcessor.AddEventHandlerComponent(fsm, ReflectionUtils.GetGlobalType("PlayMakerLegacyNetworking")))
			{
				Debug.LogError("Could not add PlayMakerLegacyNetworking proxy!");
			}
			if (fsm.Fsm.HandleUiEvents != UiEvents.None)
			{
				FsmProcessor.HandleUiEvent<PlayMakerUiClickEvent>(fsm, UiEvents.Click);
				FsmProcessor.HandleUiEvent<PlayMakerUiDragEvents>(fsm, UiEvents.DragEvents);
				FsmProcessor.HandleUiEvent<PlayMakerUiDropEvent>(fsm, UiEvents.Drop);
				FsmProcessor.HandleUiEvent<PlayMakerUiPointerEvents>(fsm, UiEvents.PointerEvents);
				FsmProcessor.HandleUiEvent<PlayMakerUiBoolValueChangedEvent>(fsm, UiEvents.BoolValueChanged);
				FsmProcessor.HandleUiEvent<PlayMakerUiFloatValueChangedEvent>(fsm, UiEvents.FloatValueChanged);
				FsmProcessor.HandleUiEvent<PlayMakerUiIntValueChangedEvent>(fsm, UiEvents.IntValueChanged);
				FsmProcessor.HandleUiEvent<PlayMakerUiVector2ValueChangedEvent>(fsm, UiEvents.Vector2ValueChanged);
				FsmProcessor.HandleUiEvent<PlayMakerUiEndEditEvent>(fsm, UiEvents.EndEdit);
			}
		}

		// Token: 0x06009B8B RID: 39819 RVA: 0x0031FC30 File Offset: 0x0031DE30
		private static void HandleUiEvent<T>(PlayMakerFSM fsm, UiEvents uiEvent) where T : PlayMakerUiEventBase
		{
			if ((fsm.Fsm.HandleUiEvents & uiEvent) != UiEvents.None)
			{
				FsmProcessor.AddUiEventHandler<T>(fsm);
			}
		}

		// Token: 0x06009B8C RID: 39820 RVA: 0x0031FC48 File Offset: 0x0031DE48
		private static void AddUiEventHandler<T>(PlayMakerFSM fsm) where T : PlayMakerUiEventBase
		{
			T t = fsm.GetComponent<T>();
			if (t == null)
			{
				t = fsm.gameObject.AddComponent<T>();
				if (!PlayMakerPrefs.ShowEventHandlerComponents)
				{
					t.hideFlags = HideFlags.HideInInspector;
				}
			}
			t.AddTargetFsm(fsm);
		}

		// Token: 0x06009B8D RID: 39821 RVA: 0x0031FC98 File Offset: 0x0031DE98
		private static bool AddEventHandlerComponent(PlayMakerFSM fsm, Type type)
		{
			if (type == null)
			{
				return false;
			}
			PlayMakerProxyBase eventHandlerComponent = FsmProcessor.GetEventHandlerComponent(fsm.gameObject, type);
			if (eventHandlerComponent == null)
			{
				return false;
			}
			eventHandlerComponent.AddTarget(fsm);
			if (!PlayMakerGlobals.IsEditor && PlayMakerPrefs.LogPerformanceWarnings)
			{
				Debug.Log("AddEventHandlerComponent: " + type.FullName);
			}
			return true;
		}

		// Token: 0x06009B8E RID: 39822 RVA: 0x0031FCF4 File Offset: 0x0031DEF4
		public static PlayMakerProxyBase GetEventHandlerComponent(GameObject go, Type type)
		{
			if (go == null)
			{
				return null;
			}
			Component component = go.GetComponent(type);
			if (component == null)
			{
				component = go.AddComponent(type);
				if (!PlayMakerPrefs.ShowEventHandlerComponents)
				{
					component.hideFlags = HideFlags.HideInInspector;
				}
			}
			return component as PlayMakerProxyBase;
		}
	}
}
