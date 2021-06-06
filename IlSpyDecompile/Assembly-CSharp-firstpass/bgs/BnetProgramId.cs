using System;

namespace bgs
{
	[Serializable]
	public class BnetProgramId : FourCC
	{
		public static readonly BnetProgramId HEARTHSTONE = new BnetProgramId("WTCG");

		public static readonly BnetProgramId WOW = new BnetProgramId("WoW");

		public static readonly BnetProgramId DIABLO3 = new BnetProgramId("D3");

		public static readonly BnetProgramId STARCRAFT = new BnetProgramId("S1");

		public static readonly BnetProgramId STARCRAFT2 = new BnetProgramId("S2");

		public static readonly BnetProgramId BNET = new BnetProgramId("BN");

		public static readonly BnetProgramId PHOENIX_MOBILE = new BnetProgramId("BSAp");

		public static readonly BnetProgramId PHOENIX = new BnetProgramId("App");

		public static readonly BnetProgramId PHOENIX_OLD = new BnetProgramId("CLNT");

		public static readonly BnetProgramId HEROES = new BnetProgramId("Hero");

		public static readonly BnetProgramId OVERWATCH = new BnetProgramId("Pro");

		public static readonly BnetProgramId DESTINY2 = new BnetProgramId("DST2");

		public static readonly BnetProgramId BLACKOPS4 = new BnetProgramId("VIPR");

		public static readonly BnetProgramId WARCRAFT3 = new BnetProgramId("W3");

		public static readonly BnetProgramId MODERNWARFARE = new BnetProgramId("ODIN");

		public static readonly BnetProgramId MODERNWARFARE2_REMASTER = new BnetProgramId("LAZR");

		public static readonly BnetProgramId BLACKOPSCOLDWAR = new BnetProgramId("ZEUS");

		public static readonly BnetProgramId CRASHBANDICOOT4 = new BnetProgramId("WLBY");

		private static readonly Map<BnetProgramId, string> s_textureNameMap = new Map<BnetProgramId, string>
		{
			{ HEARTHSTONE, "HS.tif:f7eebe7fed3c76b4da1dd53875182b34" },
			{ WOW, "WOW.tif:c1d7415957aa3497e8ac1e3cc25442b3" },
			{ DIABLO3, "D3.tif:97e4dfeddc92e4eaf965bf7ad67f85a7" },
			{ STARCRAFT, "S1.tif:4fed402b1d52dcc4189ac7dce9d17900" },
			{ STARCRAFT2, "SC2.tif:9b4c5e61999e44d5385b6ec0f732ec49" },
			{ PHOENIX, "BN.tif:5097f44734476465a9e079abd8b8d576" },
			{ PHOENIX_OLD, "BN.tif:5097f44734476465a9e079abd8b8d576" },
			{ PHOENIX_MOBILE, "BN.tif:5097f44734476465a9e079abd8b8d576" },
			{ HEROES, "Heroes.tif:9ffc5e07959da3e4e850acb6b3054cad" },
			{ OVERWATCH, "Overwatch.tif:a950d4020e3431649992a6da7c716e09" },
			{ DESTINY2, "Destiny2.tif:e04fa7ffd41e1ca4bb3610f823ca9e7d" },
			{ BLACKOPS4, "VIPR.tif:96eba72539c21b4408d61ae35f571e0b" },
			{ WARCRAFT3, "W3.tif:4e24f53718a4ff344ba934da823e471d" },
			{ MODERNWARFARE, "ModernWarfare.tif:c7ea70acfe6235d4baf5c475d6d30b5e" },
			{ MODERNWARFARE2_REMASTER, "MW2.tif:83b5e9a927db745499fb61379e3c7cd3" },
			{ BLACKOPSCOLDWAR, "BO.tif:322f8c355eeced846a4d6d1c0940e73e" },
			{ CRASHBANDICOOT4, "WLBY.tif:a2e632bf5fdd0e947a2bfdaa80520e9c" }
		};

		private static readonly Map<BnetProgramId, string> s_nameStringTagMap = new Map<BnetProgramId, string>
		{
			{ HEARTHSTONE, "GLOBAL_PROGRAMNAME_HEARTHSTONE" },
			{ WOW, "GLOBAL_PROGRAMNAME_WOW" },
			{ DIABLO3, "GLOBAL_PROGRAMNAME_DIABLO3" },
			{ STARCRAFT, "GLOBAL_PROGRAMNAME_STARCRAFT" },
			{ STARCRAFT2, "GLOBAL_PROGRAMNAME_STARCRAFT2" },
			{ PHOENIX, "GLOBAL_PROGRAMNAME_PHOENIX" },
			{ PHOENIX_OLD, "GLOBAL_PROGRAMNAME_PHOENIX" },
			{ PHOENIX_MOBILE, "GLOBAL_PROGRAMNAME_PHOENIX" },
			{ HEROES, "GLOBAL_PROGRAMNAME_HEROES" },
			{ OVERWATCH, "GLOBAL_PROGRAMNAME_OVERWATCH" },
			{ DESTINY2, "GLOBAL_PROGRAMNAME_DESTINY2" },
			{ BLACKOPS4, "GLOBAL_PROGRAMNAME_BLACKOPS4" },
			{ WARCRAFT3, "GLOBAL_PROGRAMNAME_WARCRAFT3" },
			{ MODERNWARFARE, "GLOBAL_PROGRAMNAME_MODERNWARFARE" },
			{ MODERNWARFARE2_REMASTER, "GLOBAL_PROGRAMNAME_MODERNWARFARE2_REMASTER" },
			{ BLACKOPSCOLDWAR, "GLOBAL_PROGRAMNAME_BLACKOPSCOLDWAR" },
			{ CRASHBANDICOOT4, "GLOBAL_PROGRAMNAME_CRASHBANDICOOT4" }
		};

		public new BnetProgramId Clone()
		{
			return (BnetProgramId)MemberwiseClone();
		}

		public static string GetTextureName(BnetProgramId programId)
		{
			if (programId == null)
			{
				return null;
			}
			string value = null;
			s_textureNameMap.TryGetValue(programId, out value);
			return value;
		}

		public static string GetNameTag(BnetProgramId programId)
		{
			if (programId == null)
			{
				return null;
			}
			string value = null;
			s_nameStringTagMap.TryGetValue(programId, out value);
			return value;
		}

		public BnetProgramId()
		{
		}

		public BnetProgramId(uint val)
			: base(val)
		{
		}

		public BnetProgramId(string stringVal)
			: base(stringVal)
		{
		}

		public bool IsGame()
		{
			if (this != PHOENIX && this != PHOENIX_OLD)
			{
				return this != PHOENIX_MOBILE;
			}
			return false;
		}

		public bool IsPhoenix()
		{
			if (!(this == PHOENIX) && !(this == PHOENIX_OLD))
			{
				return this == PHOENIX_MOBILE;
			}
			return true;
		}
	}
}
