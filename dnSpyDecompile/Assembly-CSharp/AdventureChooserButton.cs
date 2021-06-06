using System;
using UnityEngine;

// Token: 0x0200002B RID: 43
[CustomEditClass]
public class AdventureChooserButton : ChooserButton
{
	// Token: 0x06000168 RID: 360 RVA: 0x000087B5 File Offset: 0x000069B5
	public void SetAdventure(AdventureDbId id)
	{
		this.m_AdventureId = id;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000087BE File Offset: 0x000069BE
	public AdventureDbId GetAdventure()
	{
		return this.m_AdventureId;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x000087C8 File Offset: 0x000069C8
	public AdventureChooserSubButton CreateSubButton(AdventureDbId adventureDbId, AdventureModeDbId adventureModeDbId, AdventureSubDef subDef, string subButtonPrefab, bool useAsLastSelected)
	{
		ChooserSubButton chooserSubButton = base.CreateSubButton(subButtonPrefab, useAsLastSelected);
		AdventureChooserSubButton adventureChooserSubButton = (chooserSubButton != null) ? ((AdventureChooserSubButton)chooserSubButton) : null;
		if (adventureChooserSubButton == null)
		{
			Debug.LogError("newAdvSubButton cannot be null. Unable to create newAdvSubButton.", this);
			return null;
		}
		string buttonText = subDef.GetShortName();
		if (!AdventureConfig.CanPlayMode(this.m_AdventureId, adventureModeDbId, false) && !string.IsNullOrEmpty(subDef.GetLockedShortName()))
		{
			buttonText = subDef.GetLockedShortName();
		}
		adventureChooserSubButton.gameObject.name = string.Format("{0}_{1}", adventureChooserSubButton.gameObject.name, adventureModeDbId);
		adventureChooserSubButton.SetAdventure(adventureDbId, adventureModeDbId);
		adventureChooserSubButton.SetButtonText(buttonText);
		adventureChooserSubButton.SetPortraitTexture(subDef.m_Texture);
		adventureChooserSubButton.SetPortraitTiling(subDef.m_TextureTiling);
		adventureChooserSubButton.SetPortraitOffset(subDef.m_TextureOffset);
		return adventureChooserSubButton;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00008890 File Offset: 0x00006A90
	public AdventureChooserSubButton CreateComingSoonSubButton(AdventureModeDbId adventureModeDbId, string comingSoonSubButtonPrefab)
	{
		ChooserSubButton chooserSubButton = base.CreateSubButton(comingSoonSubButtonPrefab, true);
		AdventureChooserSubButton adventureChooserSubButton = (chooserSubButton != null) ? ((AdventureChooserSubButton)chooserSubButton) : null;
		if (adventureChooserSubButton == null)
		{
			Debug.LogError("comingSoonSubButton cannot be null. Unable to create comingSoonSubButton.", this);
			return null;
		}
		adventureChooserSubButton.SetEnabled(false, false);
		string buttonText = GameStrings.Get("GLOBAL_DATETIME_COMING_SOON");
		base.SubButtonHeight = adventureChooserSubButton.m_ComingSoonBannerHeightOverride;
		adventureChooserSubButton.SetAdventure(this.m_AdventureId, adventureModeDbId);
		adventureChooserSubButton.SetButtonText(buttonText);
		return adventureChooserSubButton;
	}

	// Token: 0x040000F6 RID: 246
	private AdventureDbId m_AdventureId;
}
