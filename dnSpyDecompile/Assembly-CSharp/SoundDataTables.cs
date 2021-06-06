using System;
using Assets;

// Token: 0x0200095B RID: 2395
public class SoundDataTables
{
	// Token: 0x04006EA9 RID: 28329
	public static readonly Map<Global.SoundCategory, Option> s_categoryEnabledOptionMap = new Map<Global.SoundCategory, Option>
	{
		{
			Global.SoundCategory.MUSIC,
			Option.MUSIC
		},
		{
			Global.SoundCategory.SPECIAL_MUSIC,
			Option.MUSIC
		},
		{
			Global.SoundCategory.HERO_MUSIC,
			Option.MUSIC
		}
	};

	// Token: 0x04006EAA RID: 28330
	public static readonly Map<Global.SoundCategory, Option> s_categoryVolumeOptionMap = new Map<Global.SoundCategory, Option>
	{
		{
			Global.SoundCategory.MUSIC,
			Option.MUSIC_VOLUME
		},
		{
			Global.SoundCategory.SPECIAL_MUSIC,
			Option.MUSIC_VOLUME
		},
		{
			Global.SoundCategory.HERO_MUSIC,
			Option.MUSIC_VOLUME
		}
	};

	// Token: 0x04006EAB RID: 28331
	public static readonly Map<Option, float> s_optionVolumeMaxMap = new Map<Option, float>
	{
		{
			Option.MUSIC_VOLUME,
			0.5f
		}
	};
}
