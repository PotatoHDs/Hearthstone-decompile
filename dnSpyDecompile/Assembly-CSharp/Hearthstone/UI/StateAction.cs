using System;
using System.Collections.Generic;
using System.Diagnostics;
using Hearthstone.UI.Logging;
using Hearthstone.UI.Scripting;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200101D RID: 4125
	[Serializable]
	public class StateAction
	{
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x0600B31E RID: 45854 RVA: 0x00372DDD File Offset: 0x00370FDD
		public IDataModelProvider DataContext
		{
			get
			{
				return this.m_dataContext;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600B31F RID: 45855 RVA: 0x00372DE5 File Offset: 0x00370FE5
		public AnimationClip AnimationClip
		{
			get
			{
				return this.m_animationClip;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600B320 RID: 45856 RVA: 0x00372DED File Offset: 0x00370FED
		public double SecondsSinceRun
		{
			get
			{
				return (double)Time.time - this.m_startTime;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600B321 RID: 45857 RVA: 0x00372DFC File Offset: 0x00370FFC
		private StateActionImplementation Implementation
		{
			get
			{
				if (this.m_implementationType != this.Type || this.m_implementation == null)
				{
					this.m_implementation = StateAction.s_implementationFactories[this.Type]();
					this.m_implementation.StateAction = this;
					this.m_implementationType = this.Type;
				}
				return this.m_implementation;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x0600B322 RID: 45858 RVA: 0x00372E58 File Offset: 0x00371058
		public int[] Conditions
		{
			get
			{
				return this.m_conditions;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x0600B323 RID: 45859 RVA: 0x00372E60 File Offset: 0x00371060
		public StateAction.ActionType Type
		{
			get
			{
				return this.m_actionType;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x0600B324 RID: 45860 RVA: 0x00372E68 File Offset: 0x00371068
		public GameObject TargetGameObject
		{
			get
			{
				GameObject result;
				this.m_override.Resolve(out result);
				return result;
			}
		}

		// Token: 0x0600B325 RID: 45861 RVA: 0x00372E84 File Offset: 0x00371084
		public string GetString(int index)
		{
			return this.m_eventName;
		}

		// Token: 0x0600B326 RID: 45862 RVA: 0x00372E8C File Offset: 0x0037108C
		public bool GetBool(int index)
		{
			return this.m_useDynamicValue;
		}

		// Token: 0x0600B327 RID: 45863 RVA: 0x00372E94 File Offset: 0x00371094
		public Override GetOverride(int index)
		{
			return this.m_override;
		}

		// Token: 0x0600B328 RID: 45864 RVA: 0x00372E9C File Offset: 0x0037109C
		public ScriptString GetScript(int index)
		{
			return this.m_valueScript;
		}

		// Token: 0x0600B329 RID: 45865 RVA: 0x00372EA4 File Offset: 0x003710A4
		public bool TryGetIntValueAtIndex(int index, out int value)
		{
			if (index >= this.m_intValues.Length)
			{
				value = 0;
				return false;
			}
			value = this.m_intValues[index];
			return true;
		}

		// Token: 0x0600B32A RID: 45866 RVA: 0x00372EC1 File Offset: 0x003710C1
		public bool TryGetFloatValueAtIndex(int index, out float value)
		{
			if (index >= this.m_floatValues.Length)
			{
				value = 0f;
				return false;
			}
			value = this.m_floatValues[index];
			return true;
		}

		// Token: 0x0600B32B RID: 45867 RVA: 0x00372EE4 File Offset: 0x003710E4
		public bool TryGetAssetAtIndex(int index, out WeakAssetReference asset)
		{
			if (index >= this.m_assets.Length)
			{
				asset = new WeakAssetReference
				{
					AssetString = ""
				};
				return false;
			}
			asset = this.m_assets[index];
			return !string.IsNullOrEmpty(asset.AssetString);
		}

		// Token: 0x0600B32C RID: 45868 RVA: 0x00372F3B File Offset: 0x0037113B
		public bool HasValues()
		{
			return this.m_hasValues;
		}

		// Token: 0x0600B32D RID: 45869 RVA: 0x00372F43 File Offset: 0x00371143
		private void Run(StateAction.RunCallbackDelegate callback, IDataModelProvider dataContext, bool loadSynchronously)
		{
			this.Abort();
			this.m_startTime = (double)Time.time;
			this.m_isRunning = true;
			this.m_dataContext = dataContext;
			this.m_callback = callback;
			this.Implementation.Run(loadSynchronously);
		}

		// Token: 0x0600B32E RID: 45870 RVA: 0x00372F78 File Offset: 0x00371178
		public bool TryRun(StateAction.RunCallbackDelegate callback, IDataModelProvider dataContext, bool loadSynchronously)
		{
			bool flag = true;
			if (!string.IsNullOrEmpty(this.m_conditionScript.Script))
			{
				if (this.m_conditionContext == null)
				{
					this.m_conditionContext = new ScriptContext();
				}
				flag = object.Equals(this.m_conditionContext.Evaluate(this.m_conditionScript.Script, dataContext).Value, true);
			}
			if (flag)
			{
				this.Run(callback, dataContext, loadSynchronously);
			}
			return flag;
		}

		// Token: 0x0600B32F RID: 45871 RVA: 0x00372FE3 File Offset: 0x003711E3
		public void Update()
		{
			if (!this.m_isRunning)
			{
				return;
			}
			this.Implementation.Update();
		}

		// Token: 0x0600B330 RID: 45872 RVA: 0x00372FF9 File Offset: 0x003711F9
		public void Abort()
		{
			this.CompleteAsyncOperation(AsyncOperationResult.Aborted);
			this.m_override.Abort();
		}

		// Token: 0x0600B331 RID: 45873 RVA: 0x00373010 File Offset: 0x00371210
		public void CompleteAsyncOperation(AsyncOperationResult result)
		{
			this.m_isRunning = (result == AsyncOperationResult.Wait);
			if (this.m_callback != null)
			{
				if (result == AsyncOperationResult.Wait)
				{
					StateAction.RunCallbackDelegate callback = this.m_callback;
					if (callback == null)
					{
						return;
					}
					callback(result);
					return;
				}
				else
				{
					StateAction.RunCallbackDelegate callback2 = this.m_callback;
					this.m_callback = null;
					if (callback2 == null)
					{
						return;
					}
					callback2(result);
				}
			}
		}

		// Token: 0x0600B332 RID: 45874 RVA: 0x0036C1AE File Offset: 0x0036A3AE
		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type, LogLevel level = LogLevel.Info)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, level, type);
		}

		// Token: 0x04009659 RID: 38489
		private static Dictionary<StateAction.ActionType, Func<StateActionImplementation>> s_implementationFactories = new Dictionary<StateAction.ActionType, Func<StateActionImplementation>>
		{
			{
				StateAction.ActionType.ShowGameObject,
				() => new ShowGameObjectStateAction()
			},
			{
				StateAction.ActionType.HideGameObject,
				() => new HideGameObjectStateAction()
			},
			{
				StateAction.ActionType.Override,
				() => new OverrideStateAction()
			},
			{
				StateAction.ActionType.PlayAnimation,
				() => new PlayAnimationStateAction()
			},
			{
				StateAction.ActionType.TriggerParticleSystem,
				() => new TriggerParticleSystemStateAction()
			},
			{
				StateAction.ActionType.SendPlayMakerEvent,
				() => new SendPlayMakerEventStateAction()
			},
			{
				StateAction.ActionType.SendEvent,
				() => new SendEventStateAction()
			},
			{
				StateAction.ActionType.SendEventUpward,
				() => new SendEventUpwardStateAction()
			},
			{
				StateAction.ActionType.WaitSeconds,
				() => new WaitSecondsStateAction()
			},
			{
				StateAction.ActionType.SendToForeground,
				() => new SendToForegroundStateAction()
			},
			{
				StateAction.ActionType.DismissFromForeground,
				() => new DismissFromForegroundStateAction()
			},
			{
				StateAction.ActionType.BindDataModel,
				() => new BindDataModelStateAction()
			},
			{
				StateAction.ActionType.PlaySoundClip,
				() => new PlaySoundClipStateAction()
			}
		};

		// Token: 0x0400965A RID: 38490
		[SerializeField]
		private int[] m_conditions;

		// Token: 0x0400965B RID: 38491
		[SerializeField]
		private StateAction.ActionType m_actionType;

		// Token: 0x0400965C RID: 38492
		[SerializeField]
		private Override m_override = new Override();

		// Token: 0x0400965D RID: 38493
		[SerializeField]
		private ScriptString m_conditionScript;

		// Token: 0x0400965E RID: 38494
		[SerializeField]
		private bool m_useDynamicValue;

		// Token: 0x0400965F RID: 38495
		[SerializeField]
		private ScriptString m_valueScript;

		// Token: 0x04009660 RID: 38496
		[SerializeField]
		private AnimationClip m_animationClip;

		// Token: 0x04009661 RID: 38497
		[SerializeField]
		private string m_eventName;

		// Token: 0x04009662 RID: 38498
		[SerializeField]
		private int[] m_intValues;

		// Token: 0x04009663 RID: 38499
		[SerializeField]
		private float[] m_floatValues;

		// Token: 0x04009664 RID: 38500
		[SerializeField]
		private WeakAssetReference[] m_assets;

		// Token: 0x04009665 RID: 38501
		[SerializeField]
		private bool m_hasValues;

		// Token: 0x04009666 RID: 38502
		private StateAction.RunCallbackDelegate m_callback;

		// Token: 0x04009667 RID: 38503
		private IDataModelProvider m_dataContext;

		// Token: 0x04009668 RID: 38504
		private bool m_isRunning;

		// Token: 0x04009669 RID: 38505
		private double m_startTime;

		// Token: 0x0400966A RID: 38506
		private ScriptContext m_conditionContext;

		// Token: 0x0400966B RID: 38507
		private StateActionImplementation m_implementation;

		// Token: 0x0400966C RID: 38508
		private StateAction.ActionType m_implementationType;

		// Token: 0x0200283F RID: 10303
		// (Invoke) Token: 0x06013B60 RID: 80736
		public delegate void RunCallbackDelegate(AsyncOperationResult result);

		// Token: 0x02002840 RID: 10304
		public enum ActionType
		{
			// Token: 0x0400F8EE RID: 63726
			ShowGameObject,
			// Token: 0x0400F8EF RID: 63727
			HideGameObject,
			// Token: 0x0400F8F0 RID: 63728
			Override,
			// Token: 0x0400F8F1 RID: 63729
			PlayAnimation,
			// Token: 0x0400F8F2 RID: 63730
			TriggerParticleSystem,
			// Token: 0x0400F8F3 RID: 63731
			SendPlayMakerEvent,
			// Token: 0x0400F8F4 RID: 63732
			SendEvent,
			// Token: 0x0400F8F5 RID: 63733
			SendEventUpward,
			// Token: 0x0400F8F6 RID: 63734
			WaitSeconds,
			// Token: 0x0400F8F7 RID: 63735
			SendToForeground,
			// Token: 0x0400F8F8 RID: 63736
			DismissFromForeground,
			// Token: 0x0400F8F9 RID: 63737
			BindDataModel,
			// Token: 0x0400F8FA RID: 63738
			PlaySoundClip
		}
	}
}
