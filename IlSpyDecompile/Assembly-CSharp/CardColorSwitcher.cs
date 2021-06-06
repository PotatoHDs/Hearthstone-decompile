using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class CardColorSwitcher : MonoBehaviour
{
	public enum CardColorType
	{
		TYPE_GENERIC,
		TYPE_WARLOCK,
		TYPE_ROGUE,
		TYPE_DRUID,
		TYPE_SHAMAN,
		TYPE_HUNTER,
		TYPE_MAGE,
		TYPE_PALADIN,
		TYPE_PRIEST,
		TYPE_WARRIOR,
		TYPE_DEATHKNIGHT,
		TYPE_DEMONHUNTER,
		TYPE_PALADIN_PRIEST,
		TYPE_WARLOCK_PRIEST,
		TYPE_WARLOCK_DEMONHUNTER,
		TYPE_HUNTER_DEMONHUNTER,
		TYPE_DRUID_HUNTER,
		TYPE_DRUID_SHAMAN,
		TYPE_SHAMAN_MAGE,
		TYPE_MAGE_ROGUE,
		TYPE_WARRIOR_ROGUE,
		TYPE_WARRIOR_PALADIN
	}

	private static CardColorSwitcher s_instance;

	[CustomEditField(Sections = "Spells", T = EditType.TEXTURE)]
	public List<string> spellCardTextures;

	[CustomEditField(Sections = "Minions", T = EditType.TEXTURE)]
	public List<string> minionCardTextures;

	[CustomEditField(Sections = "Heroes", T = EditType.TEXTURE)]
	public List<string> heroCardTextures;

	[CustomEditField(Sections = "Weapons", T = EditType.TEXTURE)]
	public List<string> weaponCardTextures;

	private void Awake()
	{
		s_instance = this;
		base.gameObject.AddComponent<HSDontDestroyOnLoad>();
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public static CardColorSwitcher Get()
	{
		return s_instance;
	}

	public AssetReference GetTexture(TAG_CARDTYPE cardType, CardColorType colorType)
	{
		List<string> list;
		switch (cardType)
		{
		case TAG_CARDTYPE.MINION:
			list = minionCardTextures;
			break;
		case TAG_CARDTYPE.SPELL:
			list = spellCardTextures;
			break;
		case TAG_CARDTYPE.HERO:
			list = heroCardTextures;
			break;
		case TAG_CARDTYPE.WEAPON:
			list = weaponCardTextures;
			break;
		default:
			Debug.LogErrorFormat("Wrong cardType {0}", cardType);
			list = minionCardTextures;
			break;
		}
		if (list.Count <= (int)colorType)
		{
			return null;
		}
		return list[(int)colorType];
	}
}
