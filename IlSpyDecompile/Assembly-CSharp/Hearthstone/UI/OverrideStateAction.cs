using Hearthstone.UI.Scripting;

namespace Hearthstone.UI
{
	public class OverrideStateAction : StateActionImplementation
	{
		private ScriptContext m_valueContext;

		private bool m_loadSynchronously;

		public override void Run(bool loadSynchronously = false)
		{
			m_loadSynchronously = loadSynchronously;
			GetOverride(0).RegisterReadyListener(HandleReady);
		}

		private void HandleReady(object unused)
		{
			GetOverride(0).RemoveReadyOrInactiveListener(HandleReady);
			bool @bool = GetBool(0);
			ScriptString script = GetScript(0);
			Override @override = GetOverride(0);
			if (@bool && !string.IsNullOrEmpty(script.Script))
			{
				if (m_valueContext == null)
				{
					m_valueContext = new ScriptContext();
				}
				@override.ApplyWithValue(value: m_valueContext.Evaluate(script.Script, base.DataContext).Value, callback: HandleOverrideComplete, userData: null, implicitConversion: true);
			}
			else
			{
				@override.Apply(HandleOverrideComplete, null, m_loadSynchronously);
			}
		}

		private void HandleOverrideComplete(AsyncOperationResult result, object asyncOperationId)
		{
			if (result != AsyncOperationResult.Aborted)
			{
				Complete(result == AsyncOperationResult.Success);
			}
		}
	}
}
