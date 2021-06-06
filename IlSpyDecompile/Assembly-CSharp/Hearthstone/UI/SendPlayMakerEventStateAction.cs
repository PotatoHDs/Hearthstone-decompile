using UnityEngine;

namespace Hearthstone.UI
{
	public class SendPlayMakerEventStateAction : StateActionImplementation
	{
		public override void Run(bool loadSynchronously = false)
		{
			GetOverride(0).RegisterReadyListener(HandleReady);
		}

		private void HandleReady(object unused)
		{
			GetOverride(0).RemoveReadyOrInactiveListener(HandleReady);
			string @string = GetString(0);
			bool flag = false;
			if (GetOverride(0).Resolve(out var gameObject))
			{
				Component[] components = gameObject.GetComponents<Component>();
				foreach (Component obj in components)
				{
					PlayMakerFSM playMakerFSM = obj as PlayMakerFSM;
					if (Application.IsPlaying(obj) && playMakerFSM != null)
					{
						flag = true;
						playMakerFSM.SendEvent(@string);
					}
				}
			}
			if (!flag)
			{
				_ = Application.isPlaying;
			}
			Complete(success: true);
		}
	}
}
