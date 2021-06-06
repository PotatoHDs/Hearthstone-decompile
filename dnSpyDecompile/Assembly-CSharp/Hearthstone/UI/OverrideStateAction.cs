using System;
using Hearthstone.UI.Scripting;

namespace Hearthstone.UI
{
	// Token: 0x02001021 RID: 4129
	public class OverrideStateAction : StateActionImplementation
	{
		// Token: 0x0600B33F RID: 45887 RVA: 0x0037339F File Offset: 0x0037159F
		public override void Run(bool loadSynchronously = false)
		{
			this.m_loadSynchronously = loadSynchronously;
			base.GetOverride(0).RegisterReadyListener(new Action<object>(this.HandleReady), null);
		}

		// Token: 0x0600B340 RID: 45888 RVA: 0x003733C4 File Offset: 0x003715C4
		private void HandleReady(object unused)
		{
			base.GetOverride(0).RemoveReadyOrInactiveListener(new Action<object>(this.HandleReady));
			bool @bool = base.GetBool(0);
			ScriptString script = base.GetScript(0);
			Override @override = base.GetOverride(0);
			if (@bool && !string.IsNullOrEmpty(script.Script))
			{
				if (this.m_valueContext == null)
				{
					this.m_valueContext = new ScriptContext();
				}
				ScriptContext.EvaluationResults evaluationResults = this.m_valueContext.Evaluate(script.Script, base.DataContext);
				@override.ApplyWithValue(new Override.ApplyCallbackDelegate(this.HandleOverrideComplete), evaluationResults.Value, null, true);
				return;
			}
			@override.Apply(new Override.ApplyCallbackDelegate(this.HandleOverrideComplete), null, this.m_loadSynchronously);
		}

		// Token: 0x0600B341 RID: 45889 RVA: 0x0037346E File Offset: 0x0037166E
		private void HandleOverrideComplete(AsyncOperationResult result, object asyncOperationId)
		{
			if (result != AsyncOperationResult.Aborted)
			{
				base.Complete(result == AsyncOperationResult.Success);
			}
		}

		// Token: 0x0400966E RID: 38510
		private ScriptContext m_valueContext;

		// Token: 0x0400966F RID: 38511
		private bool m_loadSynchronously;
	}
}
