using System;
using System.Text.RegularExpressions;
using bnet.protocol.profanity.v1;

namespace bgs
{
	public class ProfanityAPI : BattleNetAPI
	{
		private Random m_rand;

		private static readonly char[] REPLACEMENT_CHARS = new char[8] { '!', '@', '#', '$', '%', '^', '&', '*' };

		private string m_localeName;

		private WordFilters m_wordFilters;

		public ProfanityAPI(BattleNetCSharp battlenet)
			: base(battlenet, "Profanity")
		{
			m_rand = new Random();
		}

		public string FilterProfanity(string unfiltered)
		{
			if (m_wordFilters == null)
			{
				return unfiltered;
			}
			string text = unfiltered;
			foreach (WordFilter filters in m_wordFilters.FiltersList)
			{
				if (filters.Type != "bad")
				{
					continue;
				}
				MatchCollection matchCollection = new Regex(filters.Regex, RegexOptions.IgnoreCase).Matches(text);
				if (matchCollection.Count == 0)
				{
					continue;
				}
				foreach (Match item in matchCollection)
				{
					if (!item.Success)
					{
						continue;
					}
					char[] array = text.ToCharArray();
					if (item.Index <= array.Length)
					{
						int num = item.Length;
						if (item.Index + item.Length > array.Length)
						{
							num = array.Length - item.Index;
						}
						for (int i = 0; i < num; i++)
						{
							array[item.Index + i] = GetReplacementChar();
						}
						text = new string(array);
					}
				}
			}
			return text;
		}

		public override void Initialize()
		{
			m_wordFilters = null;
			ResourcesAPI resources = m_battleNet.Resources;
			if (resources == null)
			{
				base.ApiLog.LogWarning("ResourcesAPI is not initialized! Unable to proceed.");
				return;
			}
			m_localeName = BattleNet.Client().GetLocaleName();
			if (string.IsNullOrEmpty(m_localeName))
			{
				base.ApiLog.LogWarning("Unable to get Locale from Localization class");
				m_localeName = "enUS";
			}
			base.ApiLog.LogDebug("Locale is {0}", m_localeName);
			resources.LookupResource(new FourCC("BN"), new FourCC("apft"), new FourCC(m_localeName), ResouceLookupCallback, null);
		}

		private void ResouceLookupCallback(ContentHandle contentHandle, object userContext)
		{
			if (contentHandle == null)
			{
				base.ApiLog.LogWarning("BN resource look up failed unable to proceed");
				return;
			}
			base.ApiLog.LogDebug("Lookup done Region={0} Usage={1} SHA256={2}", contentHandle.Region, contentHandle.Usage, contentHandle.Sha256Digest);
			m_battleNet.LocalStorage.GetFile(contentHandle, DownloadCompletedCallback);
		}

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
				WordFilters wordFilters = (m_wordFilters = WordFilters.ParseFrom(data));
			}
			catch (Exception ex)
			{
				base.ApiLog.LogWarning("Failed to parse received data into protocol buffer. Ex  = {0}", ex.ToString());
			}
			if (m_wordFilters == null || !m_wordFilters.IsInitialized)
			{
				base.ApiLog.LogWarning("WordFilters failed to initialize");
			}
		}

		private char GetReplacementChar()
		{
			int num = m_rand.Next(REPLACEMENT_CHARS.Length);
			return REPLACEMENT_CHARS[num];
		}
	}
}
