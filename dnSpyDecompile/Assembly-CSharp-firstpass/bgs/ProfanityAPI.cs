using System;
using System.Text.RegularExpressions;
using bnet.protocol.profanity.v1;

namespace bgs
{
	// Token: 0x0200020B RID: 523
	public class ProfanityAPI : BattleNetAPI
	{
		// Token: 0x0600208A RID: 8330 RVA: 0x000757B0 File Offset: 0x000739B0
		public ProfanityAPI(BattleNetCSharp battlenet) : base(battlenet, "Profanity")
		{
			this.m_rand = new Random();
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000757CC File Offset: 0x000739CC
		public string FilterProfanity(string unfiltered)
		{
			if (this.m_wordFilters == null)
			{
				return unfiltered;
			}
			string text = unfiltered;
			foreach (WordFilter wordFilter in this.m_wordFilters.FiltersList)
			{
				if (!(wordFilter.Type != "bad"))
				{
					MatchCollection matchCollection = new Regex(wordFilter.Regex, RegexOptions.IgnoreCase).Matches(text);
					if (matchCollection.Count != 0)
					{
						foreach (object obj in matchCollection)
						{
							Match match = (Match)obj;
							if (match.Success)
							{
								char[] array = text.ToCharArray();
								if (match.Index <= array.Length)
								{
									int num = match.Length;
									if (match.Index + match.Length > array.Length)
									{
										num = array.Length - match.Index;
									}
									for (int i = 0; i < num; i++)
									{
										array[match.Index + i] = this.GetReplacementChar();
									}
									text = new string(array);
								}
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x0007593C File Offset: 0x00073B3C
		public override void Initialize()
		{
			this.m_wordFilters = null;
			ResourcesAPI resources = this.m_battleNet.Resources;
			if (resources == null)
			{
				base.ApiLog.LogWarning("ResourcesAPI is not initialized! Unable to proceed.");
				return;
			}
			this.m_localeName = BattleNet.Client().GetLocaleName();
			if (string.IsNullOrEmpty(this.m_localeName))
			{
				base.ApiLog.LogWarning("Unable to get Locale from Localization class");
				this.m_localeName = "enUS";
			}
			base.ApiLog.LogDebug("Locale is {0}", new object[]
			{
				this.m_localeName
			});
			resources.LookupResource(new FourCC("BN"), new FourCC("apft"), new FourCC(this.m_localeName), new ResourcesAPI.ResourceLookupCallback(this.ResouceLookupCallback), null);
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000759FC File Offset: 0x00073BFC
		private void ResouceLookupCallback(ContentHandle contentHandle, object userContext)
		{
			if (contentHandle == null)
			{
				base.ApiLog.LogWarning("BN resource look up failed unable to proceed");
				return;
			}
			base.ApiLog.LogDebug("Lookup done Region={0} Usage={1} SHA256={2}", new object[]
			{
				contentHandle.Region,
				contentHandle.Usage,
				contentHandle.Sha256Digest
			});
			this.m_battleNet.LocalStorage.GetFile(contentHandle, new LocalStorageAPI.DownloadCompletedCallback(this.DownloadCompletedCallback), null);
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x00075A70 File Offset: 0x00073C70
		private void DownloadCompletedCallback(byte[] data, object userContext)
		{
			if (data == null)
			{
				base.ApiLog.LogWarning("Downloading of profanity data from depot failed!");
				return;
			}
			base.ApiLog.LogDebug("Downloading of profanity data completed");
			try
			{
				WordFilters wordFilters = WordFilters.ParseFrom(data);
				this.m_wordFilters = wordFilters;
			}
			catch (Exception ex)
			{
				base.ApiLog.LogWarning("Failed to parse received data into protocol buffer. Ex  = {0}", new object[]
				{
					ex.ToString()
				});
			}
			if (this.m_wordFilters == null || !this.m_wordFilters.IsInitialized)
			{
				base.ApiLog.LogWarning("WordFilters failed to initialize");
				return;
			}
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00075B0C File Offset: 0x00073D0C
		private char GetReplacementChar()
		{
			int num = this.m_rand.Next(ProfanityAPI.REPLACEMENT_CHARS.Length);
			return ProfanityAPI.REPLACEMENT_CHARS[num];
		}

		// Token: 0x04000BB0 RID: 2992
		private Random m_rand;

		// Token: 0x04000BB1 RID: 2993
		private static readonly char[] REPLACEMENT_CHARS = new char[]
		{
			'!',
			'@',
			'#',
			'$',
			'%',
			'^',
			'&',
			'*'
		};

		// Token: 0x04000BB2 RID: 2994
		private string m_localeName;

		// Token: 0x04000BB3 RID: 2995
		private WordFilters m_wordFilters;
	}
}
