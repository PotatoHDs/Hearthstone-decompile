using System;
using System.Collections.Generic;
using System.Linq;

namespace Hearthstone.Core.Streaming
{
	public static class DownloadTags
	{
		public enum Quality
		{
			Unknown,
			Manifest,
			Initial,
			Fonts,
			SoundSpell,
			CardDef,
			CardAsset,
			CardTexture,
			SoundMission,
			PlaySounds,
			SoundLegend,
			PortPremium,
			MusicExpansion,
			SoundOtherMinion,
			PortHigh,
			HeroMusic
		}

		public enum Content
		{
			Unknown,
			Base,
			Drga,
			Ulda,
			Dala,
			Trla,
			Bota,
			Gila,
			Loota,
			Icca,
			Naxa,
			Brma,
			Loea,
			Kara,
			Tb,
			Bgs,
			Prog
		}

		public enum Locale
		{
			EnUS,
			EnGB,
			RuRU,
			EsES,
			DeDE,
			PlPL,
			PtBR,
			EsMX,
			FrFR,
			ItIT,
			KoKR,
			JaJP,
			ThTH,
			ZhTW,
			ZhCN
		}

		public enum TagGroup
		{
			Quality,
			Content,
			Locale
		}

		public static Dictionary<Quality, string> QualityTags = new Dictionary<Quality, string>
		{
			{
				Quality.Unknown,
				string.Empty
			},
			{
				Quality.Manifest,
				"manifest"
			},
			{
				Quality.Initial,
				"initial"
			},
			{
				Quality.Fonts,
				"fonts"
			},
			{
				Quality.SoundSpell,
				"soundspell"
			},
			{
				Quality.CardDef,
				"carddef"
			},
			{
				Quality.CardAsset,
				"cardasset"
			},
			{
				Quality.CardTexture,
				"cardtexture"
			},
			{
				Quality.SoundMission,
				"soundmission"
			},
			{
				Quality.PlaySounds,
				"playsounds"
			},
			{
				Quality.SoundLegend,
				"soundlegend"
			},
			{
				Quality.PortPremium,
				"portpremium"
			},
			{
				Quality.MusicExpansion,
				"musicexpansion"
			},
			{
				Quality.SoundOtherMinion,
				"soundotherminion"
			},
			{
				Quality.PortHigh,
				"porthigh"
			},
			{
				Quality.HeroMusic,
				"heromusic"
			}
		};

		public static Dictionary<Content, string> ContentTags = new Dictionary<Content, string>
		{
			{
				Content.Unknown,
				string.Empty
			},
			{
				Content.Base,
				"base"
			},
			{
				Content.Drga,
				"drga"
			},
			{
				Content.Ulda,
				"ulda"
			},
			{
				Content.Dala,
				"dala"
			},
			{
				Content.Trla,
				"trla"
			},
			{
				Content.Bota,
				"bota"
			},
			{
				Content.Gila,
				"gila"
			},
			{
				Content.Loota,
				"loota"
			},
			{
				Content.Icca,
				"icca"
			},
			{
				Content.Naxa,
				"naxa"
			},
			{
				Content.Brma,
				"brma"
			},
			{
				Content.Loea,
				"loea"
			},
			{
				Content.Kara,
				"kara"
			},
			{
				Content.Tb,
				"tb"
			},
			{
				Content.Bgs,
				"bgs"
			},
			{
				Content.Prog,
				"prog"
			}
		};

		public static TEnum GetLastEnum<TEnum>() where TEnum : IComparable, IConvertible, IFormattable
		{
			return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Last();
		}

		public static Quality GetQualityTag(string tagStr)
		{
			foreach (Quality value in Enum.GetValues(typeof(Quality)))
			{
				if (tagStr == QualityTags[value])
				{
					return value;
				}
			}
			return Quality.Unknown;
		}

		public static Content GetContentTag(string tagStr)
		{
			foreach (Content value in Enum.GetValues(typeof(Content)))
			{
				if (tagStr == ContentTags[value])
				{
					return value;
				}
			}
			return Content.Unknown;
		}

		public static string GetTagString(Quality tag)
		{
			return QualityTags[tag];
		}

		public static string GetTagString(Content tag)
		{
			return ContentTags[tag];
		}

		public static string[] GetTagStrings(Content[] tags)
		{
			List<string> list = new List<string>();
			foreach (Content tag in tags)
			{
				list.Add(GetTagString(tag));
			}
			return list.ToArray();
		}

		public static string GetTagString(Locale tag)
		{
			return tag switch
			{
				Locale.EnUS => "enUS", 
				Locale.EnGB => "enGB", 
				Locale.RuRU => "ruRU", 
				Locale.EsES => "esES", 
				Locale.DeDE => "deDE", 
				Locale.PlPL => "plPL", 
				Locale.PtBR => "ptBR", 
				Locale.EsMX => "esMX", 
				Locale.FrFR => "frFR", 
				Locale.ItIT => "itIT", 
				Locale.KoKR => "koKR", 
				Locale.JaJP => "jaJP", 
				Locale.ThTH => "thTH", 
				Locale.ZhTW => "zhTW", 
				Locale.ZhCN => "zhCN", 
				_ => string.Empty, 
			};
		}

		public static string GetTagGroupString(TagGroup tagGroup)
		{
			return tagGroup switch
			{
				TagGroup.Quality => "quality", 
				TagGroup.Content => "content", 
				TagGroup.Locale => "locale", 
				_ => string.Empty, 
			};
		}
	}
}
