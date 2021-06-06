using System;
using System.Collections.Generic;
using System.Diagnostics;
using Hearthstone.UI.Logging;
using Hearthstone.UI.Scripting;
using UnityEngine;

namespace Hearthstone.UI
{
	[Serializable]
	public class StateAction
	{
		public delegate void RunCallbackDelegate(AsyncOperationResult result);

		public enum ActionType
		{
			ShowGameObject,
			HideGameObject,
			Override,
			PlayAnimation,
			TriggerParticleSystem,
			SendPlayMakerEvent,
			SendEvent,
			SendEventUpward,
			WaitSeconds,
			SendToForeground,
			DismissFromForeground,
			BindDataModel,
			PlaySoundClip
		}

		private static Dictionary<ActionType, Func<StateActionImplementation>> s_implementationFactories = new Dictionary<ActionType, Func<StateActionImplementation>>
		{
			{
				ActionType.ShowGameObject,
				() => new ShowGameObjectStateAction()
			},
			{
				ActionType.HideGameObject,
				() => new HideGameObjectStateAction()
			},
			{
				ActionType.Override,
				() => new OverrideStateAction()
			},
			{
				ActionType.PlayAnimation,
				() => new PlayAnimationStateAction()
			},
			{
				ActionType.TriggerParticleSystem,
				() => new TriggerParticleSystemStateAction()
			},
			{
				ActionType.SendPlayMakerEvent,
				() => new SendPlayMakerEventStateAction()
			},
			{
				ActionType.SendEvent,
				() => new SendEventStateAction()
			},
			{
				ActionType.SendEventUpward,
				() => new SendEventUpwardStateAction()
			},
			{
				ActionType.WaitSeconds,
				() => new WaitSecondsStateAction()
			},
			{
				ActionType.SendToForeground,
				() => new SendToForegroundStateAction()
			},
			{
				ActionType.DismissFromForeground,
				() => new DismissFromForegroundStateAction()
			},
			{
				ActionType.BindDataModel,
				() => new BindDataModelStateAction()
			},
			{
				ActionType.PlaySoundClip,
				() => new PlaySoundClipStateAction()
			}
		};

		[SerializeField]
		private int[] m_conditions;

		[SerializeField]
		private ActionType m_actionType;

		[SerializeField]
		private Override m_override = new Override();

		[SerializeField]
		private ScriptString m_conditionScript;

		[SerializeField]
		private bool m_useDynamicValue;

		[SerializeField]
		private ScriptString m_valueScript;

		[SerializeField]
		private AnimationClip m_animationClip;

		[SerializeField]
		private string m_eventName;

		[SerializeField]
		private int[] m_intValues;

		[SerializeField]
		private float[] m_floatValues;

		[SerializeField]
		private WeakAssetReference[] m_assets;

		[SerializeField]
		private bool m_hasValues;

		private RunCallbackDelegate m_callback;

		private IDataModelProvider m_dataContext;

		private bool m_isRunning;

		private double m_startTime;

		private ScriptContext m_conditionContext;

		private StateActionImplementation m_implementation;

		private ActionType m_implementationType;

		public IDataModelProvider DataContext => m_dataContext;

		public AnimationClip AnimationClip => m_animationClip;

		public double SecondsSinceRun => (double)Time.time - m_startTime;

		private StateActionImplementation Implementation
		{
			get
			{
				if (m_implementationType != Type || m_implementation == null)
				{
					m_implementation = s_implementationFactories[Type]();
					m_implementation.StateAction = this;
					m_implementationType = Type;
				}
				return m_implementation;
			}
		}

		public int[] Conditions => m_conditions;

		public ActionType Type => m_actionType;

		public GameObject TargetGameObject
		{
			get
			{
				m_override.Resolve(out var gameObject);
				return gameObject;
			}
		}

		public string GetString(int index)
		{
			return m_eventName;
		}

		public bool GetBool(int index)
		{
			return m_useDynamicValue;
		}

		public Override GetOverride(int index)
		{
			return m_override;
		}

		public ScriptString GetScript(int index)
		{
			return m_valueScript;
		}

		public bool TryGetIntValueAtIndex(int index, out int value)
		{
			if (index >= m_intValues.Length)
			{
				value = 0;
				return false;
			}
			value = m_intValues[index];
			return true;
		}

		public bool TryGetFloatValueAtIndex(int index, out float value)
		{
			if (index >= m_floatValues.Length)
			{
				value = 0f;
				return false;
			}
			value = m_floatValues[index];
			return true;
		}

		public bool TryGetAssetAtIndex(int index, out WeakAssetReference asset)
		{
			if (index >= m_assets.Length)
			{
				asset = new WeakAssetReference
				{
					AssetString = ""
				};
				return false;
			}
			asset = m_assets[index];
			if (string.IsNullOrEmpty(asset.AssetString))
			{
				return false;
			}
			return true;
		}

		public bool HasValues()
		{
			return m_hasValues;
		}

		private void Run(RunCallbackDelegate callback, IDataModelProvider dataContext, bool loadSynchronously)
		{
			Abort();
			m_startTime = Time.time;
			m_isRunning = true;
			m_dataContext = dataContext;
			m_callback = callback;
			Implementation.Run(loadSynchronously);
		}

		public bool TryRun(RunCallbackDelegate callback, IDataModelProvider dataContext, bool loadSynchronously)
		{
			bool flag = true;
			if (!string.IsNullOrEmpty(m_conditionScript.Script))
			{
				if (m_conditionContext == null)
				{
					m_conditionContext = new ScriptContext();
				}
				flag = object.Equals(m_conditionContext.Evaluate(m_conditionScript.Script, dataContext).Value, true);
			}
			if (flag)
			{
				Run(callback, dataContext, loadSynchronously);
			}
			return flag;
		}

		public void Update()
		{
			if (m_isRunning)
			{
				Implementation.Update();
			}
		}

		public void Abort()
		{
			CompleteAsyncOperation(AsyncOperationResult.Aborted);
			m_override.Abort();
		}

		public void CompleteAsyncOperation(AsyncOperationResult result)
		{
			m_isRunning = result == AsyncOperationResult.Wait;
			if (m_callback != null)
			{
				if (result == AsyncOperationResult.Wait)
				{
					m_callback?.Invoke(result);
					return;
				}
				RunCallbackDelegate callback = m_callback;
				m_callback = null;
				callback?.Invoke(result);
			}
		}

		[Conditional("UNITY_EDITOR")]
		private void Log(string message, string type, LogLevel level = LogLevel.Info)
		{
			Hearthstone.UI.Logging.Log.Get().AddMessage(message, this, level, type);
		}
	}
}
