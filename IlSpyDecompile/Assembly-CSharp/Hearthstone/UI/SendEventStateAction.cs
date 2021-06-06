using Hearthstone.UI.Scripting;

namespace Hearthstone.UI
{
	public class SendEventStateAction : StateActionImplementation
	{
		public override void Run(bool loadSynchronously = false)
		{
			GetOverride(0).RegisterReadyListener(HandleReady);
		}

		private void HandleReady(object unused)
		{
			GetOverride(0).RemoveReadyOrInactiveListener(HandleReady);
			Override @override = GetOverride(0);
			string @string = GetString(0);
			bool flag = false;
			if (@override.Resolve(out var gameObject))
			{
				string stateName = GetStateName();
				ScriptString script = GetScript(0);
				ScriptContext scriptContext = null;
				ScriptContext.EvaluationResults? evaluationResults = null;
				if (!string.IsNullOrEmpty(script.Script))
				{
					scriptContext = new ScriptContext();
					evaluationResults = scriptContext.Evaluate(script.Script, base.DataContext);
				}
				flag = EventFunctions.TriggerEvent(gameObject.transform, @string, new Widget.TriggerEventParameters
				{
					SourceName = stateName,
					Payload = (evaluationResults.HasValue ? evaluationResults.Value.Value : null),
					IgnorePlaymaker = true,
					NoDownwardPropagation = true
				});
			}
			Complete(success: true);
		}
	}
}
