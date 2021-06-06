using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearthstone.Core.Streaming
{
	// Token: 0x0200108A RID: 4234
	public static class DownloadTags
	{
		// Token: 0x0600B6E3 RID: 46819 RVA: 0x00380790 File Offset: 0x0037E990
		public static TEnum GetLastEnum<TEnum>() where TEnum : IComparable, IConvertible, IFormattable
		{
			return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Last<TEnum>();
		}

		// Token: 0x0600B6E4 RID: 46820 RVA: 0x003807AC File Offset: 0x0037E9AC
		public static DownloadTags.Quality GetQualityTag(string tagStr)
		{
			foreach (object obj in Enum.GetValues(typeof(DownloadTags.Quality)))
			{
				DownloadTags.Quality quality = (DownloadTags.Quality)obj;
				if (tagStr == DownloadTags.QualityTags[quality])
				{
					return quality;
				}
			}
			return DownloadTags.Quality.Unknown;
		}

		// Token: 0x0600B6E5 RID: 46821 RVA: 0x00380824 File Offset: 0x0037EA24
		public static DownloadTags.Content GetContentTag(string tagStr)
		{
			foreach (object obj in Enum.GetValues(typeof(DownloadTags.Content)))
			{
				DownloadTags.Content content = (DownloadTags.Content)obj;
				if (tagStr == DownloadTags.ContentTags[content])
				{
					return content;
				}
			}
			return DownloadTags.Content.Unknown;
		}

		// Token: 0x0600B6E6 RID: 46822 RVA: 0x0038089C File Offset: 0x0037EA9C
		public static string GetTagString(DownloadTags.Quality tag)
		{
			return DownloadTags.QualityTags[tag];
		}

		// Token: 0x0600B6E7 RID: 46823 RVA: 0x003808A9 File Offset: 0x0037EAA9
		public static string GetTagString(DownloadTags.Content tag)
		{
			return DownloadTags.ContentTags[tag];
		}

		// Token: 0x0600B6E8 RID: 46824 RVA: 0x003808B8 File Offset: 0x0037EAB8
		public static string[] GetTagStrings(DownloadTags.Content[] tags)
		{
			List<string> list = new List<string>();
			foreach (DownloadTags.Content tag in tags)
			{
				list.Add(DownloadTags.GetTagString(tag));
			}
			return list.ToArray();
		}

		// Token: 0x0600B6E9 RID: 46825 RVA: 0x003808F4 File Offset: 0x0037EAF4
		public static string GetTagString(DownloadTags.Locale tag)
		{
			switch (tag)
			{
			case DownloadTags.Locale.EnUS:
				return "enUS";
			case DownloadTags.Locale.EnGB:
				return "enGB";
			case DownloadTags.Locale.RuRU:
				return "ruRU";
			case DownloadTags.Locale.EsES:
				return "esES";
			case DownloadTags.Locale.DeDE:
				return "deDE";
			case DownloadTags.Locale.PlPL:
				return "plPL";
			case DownloadTags.Locale.PtBR:
				return "ptBR";
			case DownloadTags.Locale.EsMX:
				return "esMX";
			case DownloadTags.Locale.FrFR:
				return "frFR";
			case DownloadTags.Locale.ItIT:
				return "itIT";
			case DownloadTags.Locale.KoKR:
				return "koKR";
			case DownloadTags.Locale.JaJP:
				return "jaJP";
			case DownloadTags.Locale.ThTH:
				return "thTH";
			case DownloadTags.Locale.ZhTW:
				return "zhTW";
			case DownloadTags.Locale.ZhCN:
				return "zhCN";
			default:
				return string.Empty;
			}
		}

		// Token: 0x0600B6EA RID: 46826 RVA: 0x003809A4 File Offset: 0x0037EBA4
		public static string GetTagGroupString(DownloadTags.TagGroup tagGroup)
		{
			switch (tagGroup)
			{
			case DownloadTags.TagGroup.Quality:
				return "quality";
			case DownloadTags.TagGroup.Content:
				return "content";
			case DownloadTags.TagGroup.Locale:
				return "locale";
			default:
				return string.Empty;
			}
		}

		// Token: 0x040097D1 RID: 38865
		public static Dictionary<DownloadTags.Quality, string> QualityTags = new Dictionary<DownloadTags.Quality, string>
		{
			{
				DownloadTags.Quality.Unknown,
				string.Empty
			},
			{
				DownloadTags.Quality.Manifest,
				"manifest"
			},
			{
				DownloadTags.Quality.Initial,
				"initial"
			},
			{
				DownloadTags.Quality.Fonts,
				"fonts"
			},
			{
				DownloadTags.Quality.SoundSpell,
				"soundspell"
			},
			{
				DownloadTags.Quality.CardDef,
				"carddef"
			},
			{
				DownloadTags.Quality.CardAsset,
				"cardasset"
			},
			{
				DownloadTags.Quality.CardTexture,
				"cardtexture"
			},
			{
				DownloadTags.Quality.SoundMission,
				"soundmission"
			},
			{
				DownloadTags.Quality.PlaySounds,
				"playsounds"
			},
			{
				DownloadTags.Quality.SoundLegend,
				"soundlegend"
			},
			{
				DownloadTags.Quality.PortPremium,
				"portpremium"
			},
			{
				DownloadTags.Quality.MusicExpansion,
				"musicexpansion"
			},
			{
				DownloadTags.Quality.SoundOtherMinion,
				"soundotherminion"
			},
			{
				DownloadTags.Quality.PortHigh,
				"porthigh"
			},
			{
				DownloadTags.Quality.HeroMusic,
				"heromusic"
			}
		};

		// Token: 0x040097D2 RID: 38866
		public static Dictionary<DownloadTags.Content, string> ContentTags = new Dictionary<DownloadTags.Content, string>
		{
			{
				DownloadTags.Content.Unknown,
				string.Empty
			},
			{
				DownloadTags.Content.Base,
				"base"
			},
			{
				DownloadTags.Content.Drga,
				"drga"
			},
			{
				DownloadTags.Content.Ulda,
				"ulda"
			},
			{
				DownloadTags.Content.Dala,
				"dala"
			},
			{
				DownloadTags.Content.Trla,
				"trla"
			},
			{
				DownloadTags.Content.Bota,
				"bota"
			},
			{
				DownloadTags.Content.Gila,
				"gila"
			},
			{
				DownloadTags.Content.Loota,
				"loota"
			},
			{
				DownloadTags.Content.Icca,
				"icca"
			},
			{
				DownloadTags.Content.Naxa,
				"naxa"
			},
			{
				DownloadTags.Content.Brma,
				"brma"
			},
			{
				DownloadTags.Content.Loea,
				"loea"
			},
			{
				DownloadTags.Content.Kara,
				"kara"
			},
			{
				DownloadTags.Content.Tb,
				"tb"
			},
			{
				DownloadTags.Content.Bgs,
				"bgs"
			},
			{
				DownloadTags.Content.Prog,
				"prog"
			}
		};

		// Token: 0x0200288A RID: 10378
		public enum Quality
		{
			// Token: 0x0400F9EB RID: 63979
			Unknown,
			// Token: 0x0400F9EC RID: 63980
			Manifest,
			// Token: 0x0400F9ED RID: 63981
			Initial,
			// Token: 0x0400F9EE RID: 63982
			Fonts,
			// Token: 0x0400F9EF RID: 63983
			SoundSpell,
			// Token: 0x0400F9F0 RID: 63984
			CardDef,
			// Token: 0x0400F9F1 RID: 63985
			CardAsset,
			// Token: 0x0400F9F2 RID: 63986
			CardTexture,
			// Token: 0x0400F9F3 RID: 63987
			SoundMission,
			// Token: 0x0400F9F4 RID: 63988
			PlaySounds,
			// Token: 0x0400F9F5 RID: 63989
			SoundLegend,
			// Token: 0x0400F9F6 RID: 63990
			PortPremium,
			// Token: 0x0400F9F7 RID: 63991
			MusicExpansion,
			// Token: 0x0400F9F8 RID: 63992
			SoundOtherMinion,
			// Token: 0x0400F9F9 RID: 63993
			PortHigh,
			// Token: 0x0400F9FA RID: 63994
			HeroMusic
		}

		// Token: 0x0200288B RID: 10379
		public enum Content
		{
			// Token: 0x0400F9FC RID: 63996
			Unknown,
			// Token: 0x0400F9FD RID: 63997
			Base,
			// Token: 0x0400F9FE RID: 63998
			Drga,
			// Token: 0x0400F9FF RID: 63999
			Ulda,
			// Token: 0x0400FA00 RID: 64000
			Dala,
			// Token: 0x0400FA01 RID: 64001
			Trla,
			// Token: 0x0400FA02 RID: 64002
			Bota,
			// Token: 0x0400FA03 RID: 64003
			Gila,
			// Token: 0x0400FA04 RID: 64004
			Loota,
			// Token: 0x0400FA05 RID: 64005
			Icca,
			// Token: 0x0400FA06 RID: 64006
			Naxa,
			// Token: 0x0400FA07 RID: 64007
			Brma,
			// Token: 0x0400FA08 RID: 64008
			Loea,
			// Token: 0x0400FA09 RID: 64009
			Kara,
			// Token: 0x0400FA0A RID: 64010
			Tb,
			// Token: 0x0400FA0B RID: 64011
			Bgs,
			// Token: 0x0400FA0C RID: 64012
			Prog
		}

		// Token: 0x0200288C RID: 10380
		public enum Locale
		{
			// Token: 0x0400FA0E RID: 64014
			EnUS,
			// Token: 0x0400FA0F RID: 64015
			EnGB,
			// Token: 0x0400FA10 RID: 64016
			RuRU,
			// Token: 0x0400FA11 RID: 64017
			EsES,
			// Token: 0x0400FA12 RID: 64018
			DeDE,
			// Token: 0x0400FA13 RID: 64019
			PlPL,
			// Token: 0x0400FA14 RID: 64020
			PtBR,
			// Token: 0x0400FA15 RID: 64021
			EsMX,
			// Token: 0x0400FA16 RID: 64022
			FrFR,
			// Token: 0x0400FA17 RID: 64023
			ItIT,
			// Token: 0x0400FA18 RID: 64024
			KoKR,
			// Token: 0x0400FA19 RID: 64025
			JaJP,
			// Token: 0x0400FA1A RID: 64026
			ThTH,
			// Token: 0x0400FA1B RID: 64027
			ZhTW,
			// Token: 0x0400FA1C RID: 64028
			ZhCN
		}

		// Token: 0x0200288D RID: 10381
		public enum TagGroup
		{
			// Token: 0x0400FA1E RID: 64030
			Quality,
			// Token: 0x0400FA1F RID: 64031
			Content,
			// Token: 0x0400FA20 RID: 64032
			Locale
		}
	}
}
