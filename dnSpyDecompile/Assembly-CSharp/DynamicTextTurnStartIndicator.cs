using System;
using System.Collections.Generic;

// Token: 0x02000B10 RID: 2832
public class DynamicTextTurnStartIndicator : TurnStartIndicator
{
	// Token: 0x0600969F RID: 38559 RVA: 0x0030BE38 File Offset: 0x0030A038
	public override void Show()
	{
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		if (friendlySidePlayer == null)
		{
			Log.Gameplay.PrintError("DynamicTextTurnStartIndicator.Show(): playerEntity is somehow null, text will not be displayed! Contact a gameplay engineer.", new object[]
			{
				this
			});
			return;
		}
		foreach (DynamicTextTurnStartIndicator.StringMapping stringMapping in this.m_stringMappings)
		{
			if (stringMapping.m_DynamicText == null)
			{
				Log.Gameplay.PrintError("DynamicTextTurnStartIndicator.Show(): m_DynamicText on {0} is null, please assign an UberText!", new object[]
				{
					this
				});
				return;
			}
			if (stringMapping.m_TagToPullStringIDFrom == 0)
			{
				Log.Gameplay.PrintError("DynamicTextTurnStartIndicator.Show(): m_DynamicText on {0} is null, please assign an UberText!", new object[]
				{
					this
				});
				return;
			}
			stringMapping.m_DynamicText.Text = GameDbf.GetIndex().GetClientString(friendlySidePlayer.GetTag(stringMapping.m_TagToPullStringIDFrom));
		}
		base.Show();
	}

	// Token: 0x04007E2C RID: 32300
	public List<DynamicTextTurnStartIndicator.StringMapping> m_stringMappings;

	// Token: 0x0200275F RID: 10079
	[Serializable]
	public class StringMapping
	{
		// Token: 0x0400F3ED RID: 62445
		public UberText m_DynamicText;

		// Token: 0x0400F3EE RID: 62446
		public int m_TagToPullStringIDFrom;
	}
}
