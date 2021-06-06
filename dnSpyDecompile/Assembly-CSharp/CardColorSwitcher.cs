using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200086C RID: 2156
[CustomEditClass]
public class CardColorSwitcher : MonoBehaviour
{
	// Token: 0x060075B8 RID: 30136 RVA: 0x0025C969 File Offset: 0x0025AB69
	private void Awake()
	{
		CardColorSwitcher.s_instance = this;
		base.gameObject.AddComponent<HSDontDestroyOnLoad>();
	}

	// Token: 0x060075B9 RID: 30137 RVA: 0x0025C97D File Offset: 0x0025AB7D
	private void OnDestroy()
	{
		CardColorSwitcher.s_instance = null;
	}

	// Token: 0x060075BA RID: 30138 RVA: 0x0025C985 File Offset: 0x0025AB85
	public static CardColorSwitcher Get()
	{
		return CardColorSwitcher.s_instance;
	}

	// Token: 0x060075BB RID: 30139 RVA: 0x0025C98C File Offset: 0x0025AB8C
	public AssetReference GetTexture(TAG_CARDTYPE cardType, CardColorSwitcher.CardColorType colorType)
	{
		List<string> list;
		switch (cardType)
		{
		case TAG_CARDTYPE.HERO:
			list = this.heroCardTextures;
			goto IL_62;
		case TAG_CARDTYPE.MINION:
			list = this.minionCardTextures;
			goto IL_62;
		case TAG_CARDTYPE.SPELL:
			list = this.spellCardTextures;
			goto IL_62;
		case TAG_CARDTYPE.WEAPON:
			list = this.weaponCardTextures;
			goto IL_62;
		}
		Debug.LogErrorFormat("Wrong cardType {0}", new object[]
		{
			cardType
		});
		list = this.minionCardTextures;
		IL_62:
		if (list.Count <= (int)colorType)
		{
			return null;
		}
		return list[(int)colorType];
	}

	// Token: 0x04005CB3 RID: 23731
	private static CardColorSwitcher s_instance;

	// Token: 0x04005CB4 RID: 23732
	[CustomEditField(Sections = "Spells", T = EditType.TEXTURE)]
	public List<string> spellCardTextures;

	// Token: 0x04005CB5 RID: 23733
	[CustomEditField(Sections = "Minions", T = EditType.TEXTURE)]
	public List<string> minionCardTextures;

	// Token: 0x04005CB6 RID: 23734
	[CustomEditField(Sections = "Heroes", T = EditType.TEXTURE)]
	public List<string> heroCardTextures;

	// Token: 0x04005CB7 RID: 23735
	[CustomEditField(Sections = "Weapons", T = EditType.TEXTURE)]
	public List<string> weaponCardTextures;

	// Token: 0x0200248E RID: 9358
	public enum CardColorType
	{
		// Token: 0x0400EAC4 RID: 60100
		TYPE_GENERIC,
		// Token: 0x0400EAC5 RID: 60101
		TYPE_WARLOCK,
		// Token: 0x0400EAC6 RID: 60102
		TYPE_ROGUE,
		// Token: 0x0400EAC7 RID: 60103
		TYPE_DRUID,
		// Token: 0x0400EAC8 RID: 60104
		TYPE_SHAMAN,
		// Token: 0x0400EAC9 RID: 60105
		TYPE_HUNTER,
		// Token: 0x0400EACA RID: 60106
		TYPE_MAGE,
		// Token: 0x0400EACB RID: 60107
		TYPE_PALADIN,
		// Token: 0x0400EACC RID: 60108
		TYPE_PRIEST,
		// Token: 0x0400EACD RID: 60109
		TYPE_WARRIOR,
		// Token: 0x0400EACE RID: 60110
		TYPE_DEATHKNIGHT,
		// Token: 0x0400EACF RID: 60111
		TYPE_DEMONHUNTER,
		// Token: 0x0400EAD0 RID: 60112
		TYPE_PALADIN_PRIEST,
		// Token: 0x0400EAD1 RID: 60113
		TYPE_WARLOCK_PRIEST,
		// Token: 0x0400EAD2 RID: 60114
		TYPE_WARLOCK_DEMONHUNTER,
		// Token: 0x0400EAD3 RID: 60115
		TYPE_HUNTER_DEMONHUNTER,
		// Token: 0x0400EAD4 RID: 60116
		TYPE_DRUID_HUNTER,
		// Token: 0x0400EAD5 RID: 60117
		TYPE_DRUID_SHAMAN,
		// Token: 0x0400EAD6 RID: 60118
		TYPE_SHAMAN_MAGE,
		// Token: 0x0400EAD7 RID: 60119
		TYPE_MAGE_ROGUE,
		// Token: 0x0400EAD8 RID: 60120
		TYPE_WARRIOR_ROGUE,
		// Token: 0x0400EAD9 RID: 60121
		TYPE_WARRIOR_PALADIN
	}
}
