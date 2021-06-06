using System;
using Hearthstone.UI.Scripting;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001024 RID: 4132
	public class SendEventStateAction : StateActionImplementation
	{
		// Token: 0x0600B34A RID: 45898 RVA: 0x00373685 File Offset: 0x00371885
		public override void Run(bool loadSynchronously = false)
		{
			base.GetOverride(0).RegisterReadyListener(new Action<object>(this.HandleReady), null);
		}

		// Token: 0x0600B34B RID: 45899 RVA: 0x003736A0 File Offset: 0x003718A0
		private void HandleReady(object unused)
		{
			base.GetOverride(0).RemoveReadyOrInactiveListener(new Action<object>(this.HandleReady));
			Override @override = base.GetOverride(0);
			string @string = base.GetString(0);
			GameObject gameObject;
			if (@override.Resolve(out gameObject))
			{
				string stateName = base.GetStateName();
				ScriptString script = base.GetScript(0);
				ScriptContext.EvaluationResults? evaluationResults = null;
				if (!string.IsNullOrEmpty(script.Script))
				{
					ScriptContext scriptContext = new ScriptContext();
					evaluationResults = new ScriptContext.EvaluationResults?(scriptContext.Evaluate(script.Script, base.DataContext));
				}
				bool flag = EventFunctions.TriggerEvent(gameObject.transform, @string, new Widget.TriggerEventParameters
				{
					SourceName = stateName,
					Payload = ((evaluationResults != null) ? evaluationResults.Value.Value : null),
					IgnorePlaymaker = true,
					NoDownwardPropagation = true
				});
			}
			base.Complete(true);
		}
	}
}
