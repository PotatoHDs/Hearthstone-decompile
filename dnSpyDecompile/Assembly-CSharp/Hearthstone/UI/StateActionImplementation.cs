using System;
using System.Diagnostics;
using Hearthstone.UI.Internal;
using Hearthstone.UI.Logging;
using Hearthstone.UI.Scripting;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200101C RID: 4124
	public abstract class StateActionImplementation
	{
		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x0600B309 RID: 45833 RVA: 0x00372B6B File Offset: 0x00370D6B
		// (set) Token: 0x0600B308 RID: 45832 RVA: 0x00372B62 File Offset: 0x00370D62
		public StateAction StateAction { private get; set; }

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x0600B30A RID: 45834 RVA: 0x00372B73 File Offset: 0x00370D73
		protected IDataModelProvider DataContext
		{
			get
			{
				return this.StateAction.DataContext;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x0600B30B RID: 45835 RVA: 0x00372B80 File Offset: 0x00370D80
		protected AnimationClip AnimationClip
		{
			get
			{
				return this.StateAction.AnimationClip;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x0600B30C RID: 45836 RVA: 0x00372B8D File Offset: 0x00370D8D
		protected double SecondsSinceRun
		{
			get
			{
				return this.StateAction.SecondsSinceRun;
			}
		}

		// Token: 0x0600B30D RID: 45837 RVA: 0x00372B9A File Offset: 0x00370D9A
		protected string GetString(int index)
		{
			return this.StateAction.GetString(index);
		}

		// Token: 0x0600B30E RID: 45838 RVA: 0x00372BA8 File Offset: 0x00370DA8
		protected bool GetBool(int index)
		{
			return this.StateAction.GetBool(index);
		}

		// Token: 0x0600B30F RID: 45839 RVA: 0x00372BB6 File Offset: 0x00370DB6
		protected Override GetOverride(int index)
		{
			return this.StateAction.GetOverride(index);
		}

		// Token: 0x0600B310 RID: 45840 RVA: 0x00372BC4 File Offset: 0x00370DC4
		protected ScriptString GetScript(int index)
		{
			return this.StateAction.GetScript(index);
		}

		// Token: 0x0600B311 RID: 45841 RVA: 0x00372BD4 File Offset: 0x00370DD4
		protected int GetIntValueAtIndex(int index)
		{
			int result;
			this.StateAction.TryGetIntValueAtIndex(index, out result);
			return result;
		}

		// Token: 0x0600B312 RID: 45842 RVA: 0x00372BF4 File Offset: 0x00370DF4
		protected float GetFloatValueAtIndex(int index)
		{
			float result;
			this.StateAction.TryGetFloatValueAtIndex(index, out result);
			return result;
		}

		// Token: 0x0600B313 RID: 45843 RVA: 0x00372C14 File Offset: 0x00370E14
		protected WeakAssetReference GetAssetAtIndex(int index)
		{
			WeakAssetReference result;
			this.StateAction.TryGetAssetAtIndex(index, out result);
			return result;
		}

		// Token: 0x0600B314 RID: 45844 RVA: 0x00372C34 File Offset: 0x00370E34
		protected string GetStateName()
		{
			VisualController visualController = this.StateAction.DataContext as VisualController;
			if (visualController != null && !string.IsNullOrEmpty(visualController.RequestedState))
			{
				return visualController.RequestedState;
			}
			return "UNKNOWN";
		}

		// Token: 0x0600B315 RID: 45845 RVA: 0x00372C74 File Offset: 0x00370E74
		protected void Complete(bool success)
		{
			this.StateAction.CompleteAsyncOperation(success ? AsyncOperationResult.Success : AsyncOperationResult.Failure);
		}

		// Token: 0x0600B316 RID: 45846 RVA: 0x00372C88 File Offset: 0x00370E88
		protected void Complete(AsyncOperationResult result)
		{
			this.StateAction.CompleteAsyncOperation(result);
		}

		// Token: 0x0600B317 RID: 45847 RVA: 0x00372C96 File Offset: 0x00370E96
		[Conditional("UNITY_EDITOR")]
		protected void Log(string message, string type, LogLevel level = LogLevel.Info)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this.StateAction, level, type);
		}

		// Token: 0x0600B318 RID: 45848 RVA: 0x00372CAC File Offset: 0x00370EAC
		protected void RunOnInstanceOrTargetGameObject(NestedReference nestedReference, bool enableInstance, Action<WidgetInstance> instanceCallback, Action<GameObject> gameObjectCallback)
		{
			nestedReference.RegisterReadyListener(new Action<object>(this.HandleNestedReferenceReady), new StateActionImplementation.RunOnInstanceOrTargetGameObjectArgs
			{
				NestedReference = nestedReference,
				WidgetInstanceCallback = instanceCallback,
				GameObjectCallback = gameObjectCallback,
				EnableInstance = enableInstance
			});
		}

		// Token: 0x0600B319 RID: 45849 RVA: 0x00372CFC File Offset: 0x00370EFC
		private void HandleNestedReferenceReady(object payload)
		{
			StateActionImplementation.RunOnInstanceOrTargetGameObjectArgs runOnInstanceOrTargetGameObjectArgs = (StateActionImplementation.RunOnInstanceOrTargetGameObjectArgs)payload;
			runOnInstanceOrTargetGameObjectArgs.NestedReference.RemoveReadyOrInactiveListener();
			GameObject gameObject;
			if (!runOnInstanceOrTargetGameObjectArgs.NestedReference.Resolve(out gameObject))
			{
				this.Complete(false);
				return;
			}
			WidgetInstance component = gameObject.GetComponent<WidgetInstance>();
			if (!(component != null))
			{
				runOnInstanceOrTargetGameObjectArgs.GameObjectCallback(gameObject);
				return;
			}
			if (runOnInstanceOrTargetGameObjectArgs.EnableInstance)
			{
				component.enabled = true;
				component.gameObject.SetActive(true);
			}
			if (!component.IsReady)
			{
				runOnInstanceOrTargetGameObjectArgs.WidgetInstance = component;
				component.RegisterReadyListener(new Action<object>(this.HandleWidgetInstanceReady), runOnInstanceOrTargetGameObjectArgs, true);
				return;
			}
			if (component.Widget == null)
			{
				this.Complete(false);
				return;
			}
			runOnInstanceOrTargetGameObjectArgs.WidgetInstanceCallback(component);
		}

		// Token: 0x0600B31A RID: 45850 RVA: 0x00372DB8 File Offset: 0x00370FB8
		private void HandleWidgetInstanceReady(object payload)
		{
			StateActionImplementation.RunOnInstanceOrTargetGameObjectArgs runOnInstanceOrTargetGameObjectArgs = (StateActionImplementation.RunOnInstanceOrTargetGameObjectArgs)payload;
			runOnInstanceOrTargetGameObjectArgs.WidgetInstanceCallback(runOnInstanceOrTargetGameObjectArgs.WidgetInstance);
		}

		// Token: 0x0600B31B RID: 45851
		public abstract void Run(bool loadSynchronously = false);

		// Token: 0x0600B31C RID: 45852 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public virtual void Update()
		{
		}

		// Token: 0x0200283E RID: 10302
		private struct RunOnInstanceOrTargetGameObjectArgs
		{
			// Token: 0x0400F8E8 RID: 63720
			public NestedReference NestedReference;

			// Token: 0x0400F8E9 RID: 63721
			public Action<WidgetInstance> WidgetInstanceCallback;

			// Token: 0x0400F8EA RID: 63722
			public Action<GameObject> GameObjectCallback;

			// Token: 0x0400F8EB RID: 63723
			public bool EnableInstance;

			// Token: 0x0400F8EC RID: 63724
			public WidgetInstance WidgetInstance;
		}
	}
}
