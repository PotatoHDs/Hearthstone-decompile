using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Hearthstone.Core;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Debug")]
	[Tooltip("Waits for services to be dynamically loaded and ready")]
	public class ServiceLocatorAction : FsmStateAction
	{
		public bool m_sound;

		public override void Reset()
		{
			m_sound = false;
		}

		public override void OnEnter()
		{
			List<Type> list = new List<Type>();
			if (m_sound)
			{
				list.Add(typeof(SoundManager));
			}
			if (list.Count == 0)
			{
				Finish();
				return;
			}
			IJobDependency[] serviceDependencies = null;
			HearthstoneServices.InitializeDynamicServicesIfEditor(out serviceDependencies, list.ToArray());
			Processor.QueueJob("Playmaker.ServiceLocator", Job_Initialize(), serviceDependencies);
		}

		private IEnumerator<IAsyncJobResult> Job_Initialize()
		{
			Finish();
			yield break;
		}
	}
}
