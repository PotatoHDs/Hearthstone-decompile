using Assets;

public class SoundDataTables
{
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

	public static readonly Map<Option, float> s_optionVolumeMaxMap = new Map<Option, float> { 
	{
		Option.MUSIC_VOLUME,
		0.5f
	} };
}
