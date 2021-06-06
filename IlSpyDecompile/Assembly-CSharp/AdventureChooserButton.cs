using UnityEngine;

[CustomEditClass]
public class AdventureChooserButton : ChooserButton
{
	private AdventureDbId m_AdventureId;

	public void SetAdventure(AdventureDbId id)
	{
		m_AdventureId = id;
	}

	public AdventureDbId GetAdventure()
	{
		return m_AdventureId;
	}

	public AdventureChooserSubButton CreateSubButton(AdventureDbId adventureDbId, AdventureModeDbId adventureModeDbId, AdventureSubDef subDef, string subButtonPrefab, bool useAsLastSelected)
	{
		ChooserSubButton chooserSubButton = CreateSubButton(subButtonPrefab, useAsLastSelected);
		AdventureChooserSubButton adventureChooserSubButton = ((chooserSubButton != null) ? ((AdventureChooserSubButton)chooserSubButton) : null);
		if (adventureChooserSubButton == null)
		{
			Debug.LogError("newAdvSubButton cannot be null. Unable to create newAdvSubButton.", this);
			return null;
		}
		string buttonText = subDef.GetShortName();
		if (!AdventureConfig.CanPlayMode(m_AdventureId, adventureModeDbId, checkEventTimings: false) && !string.IsNullOrEmpty(subDef.GetLockedShortName()))
		{
			buttonText = subDef.GetLockedShortName();
		}
		adventureChooserSubButton.gameObject.name = $"{adventureChooserSubButton.gameObject.name}_{adventureModeDbId}";
		adventureChooserSubButton.SetAdventure(adventureDbId, adventureModeDbId);
		adventureChooserSubButton.SetButtonText(buttonText);
		adventureChooserSubButton.SetPortraitTexture(subDef.m_Texture);
		adventureChooserSubButton.SetPortraitTiling(subDef.m_TextureTiling);
		adventureChooserSubButton.SetPortraitOffset(subDef.m_TextureOffset);
		return adventureChooserSubButton;
	}

	public AdventureChooserSubButton CreateComingSoonSubButton(AdventureModeDbId adventureModeDbId, string comingSoonSubButtonPrefab)
	{
		ChooserSubButton chooserSubButton = CreateSubButton(comingSoonSubButtonPrefab, useAsLastSelected: true);
		AdventureChooserSubButton adventureChooserSubButton = ((chooserSubButton != null) ? ((AdventureChooserSubButton)chooserSubButton) : null);
		if (adventureChooserSubButton == null)
		{
			Debug.LogError("comingSoonSubButton cannot be null. Unable to create comingSoonSubButton.", this);
			return null;
		}
		adventureChooserSubButton.SetEnabled(enabled: false);
		string buttonText = GameStrings.Get("GLOBAL_DATETIME_COMING_SOON");
		base.SubButtonHeight = adventureChooserSubButton.m_ComingSoonBannerHeightOverride;
		adventureChooserSubButton.SetAdventure(m_AdventureId, adventureModeDbId);
		adventureChooserSubButton.SetButtonText(buttonText);
		return adventureChooserSubButton;
	}
}
