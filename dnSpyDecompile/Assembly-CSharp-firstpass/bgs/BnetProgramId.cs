using System;

namespace bgs
{
	// Token: 0x0200024E RID: 590
	[Serializable]
	public class BnetProgramId : FourCC
	{
		// Token: 0x0600248B RID: 9355 RVA: 0x0008141E File Offset: 0x0007F61E
		public new BnetProgramId Clone()
		{
			return (BnetProgramId)base.MemberwiseClone();
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x0008142C File Offset: 0x0007F62C
		public static string GetTextureName(BnetProgramId programId)
		{
			if (programId == null)
			{
				return null;
			}
			string result = null;
			BnetProgramId.s_textureNameMap.TryGetValue(programId, out result);
			return result;
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x00081458 File Offset: 0x0007F658
		public static string GetNameTag(BnetProgramId programId)
		{
			if (programId == null)
			{
				return null;
			}
			string result = null;
			BnetProgramId.s_nameStringTagMap.TryGetValue(programId, out result);
			return result;
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x00081481 File Offset: 0x0007F681
		public BnetProgramId()
		{
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x00081489 File Offset: 0x0007F689
		public BnetProgramId(uint val) : base(val)
		{
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x00081492 File Offset: 0x0007F692
		public BnetProgramId(string stringVal) : base(stringVal)
		{
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x0008149B File Offset: 0x0007F69B
		public bool IsGame()
		{
			return this != BnetProgramId.PHOENIX && this != BnetProgramId.PHOENIX_OLD && this != BnetProgramId.PHOENIX_MOBILE;
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x000814C4 File Offset: 0x0007F6C4
		public bool IsPhoenix()
		{
			return this == BnetProgramId.PHOENIX || this == BnetProgramId.PHOENIX_OLD || this == BnetProgramId.PHOENIX_MOBILE;
		}

		// Token: 0x04000F2E RID: 3886
		public static readonly BnetProgramId HEARTHSTONE = new BnetProgramId("WTCG");

		// Token: 0x04000F2F RID: 3887
		public static readonly BnetProgramId WOW = new BnetProgramId("WoW");

		// Token: 0x04000F30 RID: 3888
		public static readonly BnetProgramId DIABLO3 = new BnetProgramId("D3");

		// Token: 0x04000F31 RID: 3889
		public static readonly BnetProgramId STARCRAFT = new BnetProgramId("S1");

		// Token: 0x04000F32 RID: 3890
		public static readonly BnetProgramId STARCRAFT2 = new BnetProgramId("S2");

		// Token: 0x04000F33 RID: 3891
		public static readonly BnetProgramId BNET = new BnetProgramId("BN");

		// Token: 0x04000F34 RID: 3892
		public static readonly BnetProgramId PHOENIX_MOBILE = new BnetProgramId("BSAp");

		// Token: 0x04000F35 RID: 3893
		public static readonly BnetProgramId PHOENIX = new BnetProgramId("App");

		// Token: 0x04000F36 RID: 3894
		public static readonly BnetProgramId PHOENIX_OLD = new BnetProgramId("CLNT");

		// Token: 0x04000F37 RID: 3895
		public static readonly BnetProgramId HEROES = new BnetProgramId("Hero");

		// Token: 0x04000F38 RID: 3896
		public static readonly BnetProgramId OVERWATCH = new BnetProgramId("Pro");

		// Token: 0x04000F39 RID: 3897
		public static readonly BnetProgramId DESTINY2 = new BnetProgramId("DST2");

		// Token: 0x04000F3A RID: 3898
		public static readonly BnetProgramId BLACKOPS4 = new BnetProgramId("VIPR");

		// Token: 0x04000F3B RID: 3899
		public static readonly BnetProgramId WARCRAFT3 = new BnetProgramId("W3");

		// Token: 0x04000F3C RID: 3900
		public static readonly BnetProgramId MODERNWARFARE = new BnetProgramId("ODIN");

		// Token: 0x04000F3D RID: 3901
		public static readonly BnetProgramId MODERNWARFARE2_REMASTER = new BnetProgramId("LAZR");

		// Token: 0x04000F3E RID: 3902
		public static readonly BnetProgramId BLACKOPSCOLDWAR = new BnetProgramId("ZEUS");

		// Token: 0x04000F3F RID: 3903
		public static readonly BnetProgramId CRASHBANDICOOT4 = new BnetProgramId("WLBY");

		// Token: 0x04000F40 RID: 3904
		private static readonly Map<BnetProgramId, string> s_textureNameMap = new Map<BnetProgramId, string>
		{
			{
				BnetProgramId.HEARTHSTONE,
				"HS.tif:f7eebe7fed3c76b4da1dd53875182b34"
			},
			{
				BnetProgramId.WOW,
				"WOW.tif:c1d7415957aa3497e8ac1e3cc25442b3"
			},
			{
				BnetProgramId.DIABLO3,
				"D3.tif:97e4dfeddc92e4eaf965bf7ad67f85a7"
			},
			{
				BnetProgramId.STARCRAFT,
				"S1.tif:4fed402b1d52dcc4189ac7dce9d17900"
			},
			{
				BnetProgramId.STARCRAFT2,
				"SC2.tif:9b4c5e61999e44d5385b6ec0f732ec49"
			},
			{
				BnetProgramId.PHOENIX,
				"BN.tif:5097f44734476465a9e079abd8b8d576"
			},
			{
				BnetProgramId.PHOENIX_OLD,
				"BN.tif:5097f44734476465a9e079abd8b8d576"
			},
			{
				BnetProgramId.PHOENIX_MOBILE,
				"BN.tif:5097f44734476465a9e079abd8b8d576"
			},
			{
				BnetProgramId.HEROES,
				"Heroes.tif:9ffc5e07959da3e4e850acb6b3054cad"
			},
			{
				BnetProgramId.OVERWATCH,
				"Overwatch.tif:a950d4020e3431649992a6da7c716e09"
			},
			{
				BnetProgramId.DESTINY2,
				"Destiny2.tif:e04fa7ffd41e1ca4bb3610f823ca9e7d"
			},
			{
				BnetProgramId.BLACKOPS4,
				"VIPR.tif:96eba72539c21b4408d61ae35f571e0b"
			},
			{
				BnetProgramId.WARCRAFT3,
				"W3.tif:4e24f53718a4ff344ba934da823e471d"
			},
			{
				BnetProgramId.MODERNWARFARE,
				"ModernWarfare.tif:c7ea70acfe6235d4baf5c475d6d30b5e"
			},
			{
				BnetProgramId.MODERNWARFARE2_REMASTER,
				"MW2.tif:83b5e9a927db745499fb61379e3c7cd3"
			},
			{
				BnetProgramId.BLACKOPSCOLDWAR,
				"BO.tif:322f8c355eeced846a4d6d1c0940e73e"
			},
			{
				BnetProgramId.CRASHBANDICOOT4,
				"WLBY.tif:a2e632bf5fdd0e947a2bfdaa80520e9c"
			}
		};

		// Token: 0x04000F41 RID: 3905
		private static readonly Map<BnetProgramId, string> s_nameStringTagMap = new Map<BnetProgramId, string>
		{
			{
				BnetProgramId.HEARTHSTONE,
				"GLOBAL_PROGRAMNAME_HEARTHSTONE"
			},
			{
				BnetProgramId.WOW,
				"GLOBAL_PROGRAMNAME_WOW"
			},
			{
				BnetProgramId.DIABLO3,
				"GLOBAL_PROGRAMNAME_DIABLO3"
			},
			{
				BnetProgramId.STARCRAFT,
				"GLOBAL_PROGRAMNAME_STARCRAFT"
			},
			{
				BnetProgramId.STARCRAFT2,
				"GLOBAL_PROGRAMNAME_STARCRAFT2"
			},
			{
				BnetProgramId.PHOENIX,
				"GLOBAL_PROGRAMNAME_PHOENIX"
			},
			{
				BnetProgramId.PHOENIX_OLD,
				"GLOBAL_PROGRAMNAME_PHOENIX"
			},
			{
				BnetProgramId.PHOENIX_MOBILE,
				"GLOBAL_PROGRAMNAME_PHOENIX"
			},
			{
				BnetProgramId.HEROES,
				"GLOBAL_PROGRAMNAME_HEROES"
			},
			{
				BnetProgramId.OVERWATCH,
				"GLOBAL_PROGRAMNAME_OVERWATCH"
			},
			{
				BnetProgramId.DESTINY2,
				"GLOBAL_PROGRAMNAME_DESTINY2"
			},
			{
				BnetProgramId.BLACKOPS4,
				"GLOBAL_PROGRAMNAME_BLACKOPS4"
			},
			{
				BnetProgramId.WARCRAFT3,
				"GLOBAL_PROGRAMNAME_WARCRAFT3"
			},
			{
				BnetProgramId.MODERNWARFARE,
				"GLOBAL_PROGRAMNAME_MODERNWARFARE"
			},
			{
				BnetProgramId.MODERNWARFARE2_REMASTER,
				"GLOBAL_PROGRAMNAME_MODERNWARFARE2_REMASTER"
			},
			{
				BnetProgramId.BLACKOPSCOLDWAR,
				"GLOBAL_PROGRAMNAME_BLACKOPSCOLDWAR"
			},
			{
				BnetProgramId.CRASHBANDICOOT4,
				"GLOBAL_PROGRAMNAME_CRASHBANDICOOT4"
			}
		};
	}
}
