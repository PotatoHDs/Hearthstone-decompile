using System;
using System.Diagnostics;
using Hearthstone.UI.Internal;
using Hearthstone.UI.Logging;
using Hearthstone.UI.Scripting;
using UnityEngine;

namespace Hearthstone.UI
{
	public abstract class StateActionImplementation
	{
		private struct RunOnInstanceOrTargetGameObjectArgs
		{
			public NestedReference NestedReference;

			public Action<WidgetInstance> WidgetInstanceCallback;

			public Action<GameObject> GameObjectCallback;

			public bool EnableInstance;

			public WidgetInstance WidgetInstance;
		}

		public StateAction StateAction { private get; set; }

		protected IDataModelProvider DataContext => StateAction.DataContext;

		protected AnimationClip AnimationClip => StateAction.AnimationClip;

		protected double SecondsSinceRun => StateAction.SecondsSinceRun;

		protected string GetString(int index)
		{
			return StateAction.GetString(index);
		}

		protected bool GetBool(int index)
		{
			return StateAction.GetBool(index);
		}

		protected Override GetOverride(int index)
		{
			return StateAction.GetOverride(index);
		}

		protected ScriptString GetScript(int index)
		{
			return StateAction.GetScript(index);
		}

		protected int GetIntValueAtIndex(int index)
		{
			StateAction.TryGetIntValueAtIndex(index, out var value);
			return value;
		}

		protected float GetFloatValueAtIndex(int index)
		{
			StateAction.TryGetFloatValueAtIndex(index, out var value);
			return value;
		}

		protected WeakAssetReference GetAssetAtIndex(int index)
		{
			StateAction.TryGetAssetAtIndex(index, out var asset);
			return asset;
		}

		protected string GetStateName()
		{
			VisualController visualController = StateAction.DataContext as VisualController;
			if (visualController != null && !string.IsNullOrEmpty(visualController.RequestedState))
			{
				return visualController.RequestedState;
			}
			return "UNKNOWN";
		}

		protected void Complete(bool success)
		{
			StateAction.CompleteAsyncOperation((!success) ? AsyncOperationResult.Failure : AsyncOperationResult.Success);
		}

		protected void Complete(AsyncOperationResult result)
		{
			StateAction.CompleteAsyncOperation(result);
		}

		[Conditional("UNITY_EDITOR")]
		protected void Log(string message, string type, LogLevel level = LogLevel.Info)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, StateAction, level, type);
		}

		protected void RunOnInstanceOrTargetGameObject(NestedReference nestedReference, bool enableInstance, Action<WidgetInstance> instanceCallback, Action<GameObject> gameObjectCallback)
		{
			nestedReference.RegisterReadyListener(HandleNestedReferenceReady, new RunOnInstanceOrTargetGameObjectArgs
			{
				NestedReference = nestedReference,
				WidgetInstanceCallback = instanceCallback,
				GameObjectCallback = gameObjectCallback,
				EnableInstance = enableInstance
			});
		}

		private void HandleNestedReferenceReady(object payload)
		{
			RunOnInstanceOrTargetGameObjectArgs runOnInstanceOrTargetGameObjectArgs = (RunOnInstanceOrTargetGameObjectArgs)payload;
			runOnInstanceOrTargetGameObjectArgs.NestedReference.RemoveReadyOrInactiveListener();
			if (!runOnInstanceOrTargetGameObjectArgs.NestedReference.Resolve(out GameObject gameObject))
			{
				Complete(success: false);
				return;
			}
			WidgetInstance component = gameObject.GetComponent<WidgetInstance>();
			if (component != null)
			{
				if (runOnInstanceOrTargetGameObjectArgs.EnableInstance)
				{
					component.enabled = true;
					component.gameObject.SetActive(value: true);
				}
				if (component.IsReady)
				{
					if (component.Widget == null)
					{
						Complete(success: false);
					}
					else
					{
						runOnInstanceOrTargetGameObjectArgs.WidgetInstanceCallback(component);
					}
				}
				else
				{
					runOnInstanceOrTargetGameObjectArgs.WidgetInstance = component;
					component.RegisterReadyListener(HandleWidgetInstanceReady, runOnInstanceOrTargetGameObjectArgs);
				}
			}
			else
			{
				runOnInstanceOrTargetGameObjectArgs.GameObjectCallback(gameObject);
			}
		}

		private void HandleWidgetInstanceReady(object payload)
		{
			RunOnInstanceOrTargetGameObjectArgs runOnInstanceOrTargetGameObjectArgs = (RunOnInstanceOrTargetGameObjectArgs)payload;
			runOnInstanceOrTargetGameObjectArgs.WidgetInstanceCallback(runOnInstanceOrTargetGameObjectArgs.WidgetInstance);
		}

		public abstract void Run(bool loadSynchronously = false);

		public virtual void Update()
		{
		}
	}
}
