using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;

namespace Hearthstone.Core.Deeplinking
{
	public class DeeplinkService : IService
	{
		private string[] m_cachedDeeplink;

		public Type[] GetDependencies()
		{
			return new Type[1] { typeof(MobileCallbackManager) };
		}

		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			RefreshCachedDeeplink();
			HearthstoneApplication.Get().Unpaused += OnApplicationUnpaused;
			Log.DeepLink.PrintDebug("Deeplink service initialized");
			yield break;
		}

		public void Shutdown()
		{
			HearthstoneApplication.Get().Unpaused -= OnApplicationUnpaused;
		}

		private void OnApplicationUnpaused()
		{
			Log.DeepLink.PrintDebug("Application unpaused, refreshing deeplink");
			RefreshCachedDeeplink();
		}

		public string[] GetDeeplink()
		{
			return m_cachedDeeplink;
		}

		public Map<string, string> GetDeepLinkArgs()
		{
			return DeeplinkUtils.GetDeepLinkArgs(m_cachedDeeplink);
		}

		private void RefreshCachedDeeplink()
		{
			string[] array = MobileCallbackManager.ConsumeDeepLink(retain: false);
			string text = ConvertDeeplinkToString(array);
			Log.DeepLink.PrintInfo("Retreived deeplink: {0}", text);
			m_cachedDeeplink = array;
		}

		private string ConvertDeeplinkToString(string[] deeplink)
		{
			if (deeplink == null)
			{
				return "<Null>";
			}
			if (deeplink.Length == 0)
			{
				return "<Empty>";
			}
			return string.Join(" ", deeplink);
		}
	}
}
