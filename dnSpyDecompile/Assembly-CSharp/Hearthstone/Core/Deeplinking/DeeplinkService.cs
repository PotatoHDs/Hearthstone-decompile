using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;

namespace Hearthstone.Core.Deeplinking
{
	// Token: 0x02001089 RID: 4233
	public class DeeplinkService : IService
	{
		// Token: 0x0600B6DA RID: 46810 RVA: 0x002334B2 File Offset: 0x002316B2
		public Type[] GetDependencies()
		{
			return new Type[]
			{
				typeof(MobileCallbackManager)
			};
		}

		// Token: 0x0600B6DB RID: 46811 RVA: 0x003806D9 File Offset: 0x0037E8D9
		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			this.RefreshCachedDeeplink();
			HearthstoneApplication.Get().Unpaused += this.OnApplicationUnpaused;
			Log.DeepLink.PrintDebug("Deeplink service initialized", Array.Empty<object>());
			yield break;
		}

		// Token: 0x0600B6DC RID: 46812 RVA: 0x003806E8 File Offset: 0x0037E8E8
		public void Shutdown()
		{
			HearthstoneApplication.Get().Unpaused -= this.OnApplicationUnpaused;
		}

		// Token: 0x0600B6DD RID: 46813 RVA: 0x00380700 File Offset: 0x0037E900
		private void OnApplicationUnpaused()
		{
			Log.DeepLink.PrintDebug("Application unpaused, refreshing deeplink", Array.Empty<object>());
			this.RefreshCachedDeeplink();
		}

		// Token: 0x0600B6DE RID: 46814 RVA: 0x0038071C File Offset: 0x0037E91C
		public string[] GetDeeplink()
		{
			return this.m_cachedDeeplink;
		}

		// Token: 0x0600B6DF RID: 46815 RVA: 0x00380724 File Offset: 0x0037E924
		public Map<string, string> GetDeepLinkArgs()
		{
			return DeeplinkUtils.GetDeepLinkArgs(this.m_cachedDeeplink);
		}

		// Token: 0x0600B6E0 RID: 46816 RVA: 0x00380734 File Offset: 0x0037E934
		private void RefreshCachedDeeplink()
		{
			string[] array = MobileCallbackManager.ConsumeDeepLink(false);
			string text = this.ConvertDeeplinkToString(array);
			Log.DeepLink.PrintInfo("Retreived deeplink: {0}", new object[]
			{
				text
			});
			this.m_cachedDeeplink = array;
		}

		// Token: 0x0600B6E1 RID: 46817 RVA: 0x00380770 File Offset: 0x0037E970
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

		// Token: 0x040097D0 RID: 38864
		private string[] m_cachedDeeplink;
	}
}
