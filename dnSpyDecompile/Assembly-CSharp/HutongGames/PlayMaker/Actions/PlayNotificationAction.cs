using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F64 RID: 3940
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Plays a notification.")]
	public class PlayNotificationAction : FsmStateAction
	{
		// Token: 0x0600AD1A RID: 44314 RVA: 0x0035FE7B File Offset: 0x0035E07B
		public override void Reset()
		{
			this.m_NotificationPrefab = string.Empty;
			this.m_NotificationVO = string.Empty;
		}

		// Token: 0x0600AD1B RID: 44315 RVA: 0x0035FEA0 File Offset: 0x0035E0A0
		public override void OnEnter()
		{
			Vector3 position = NotificationManager.DEFAULT_CHARACTER_POS;
			if (!this.m_NotificationPosition.IsNone)
			{
				position = this.m_NotificationPosition.Value;
			}
			string legacyAssetName = new AssetReference(this.m_NotificationVO.Value).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(this.m_NotificationPrefab.Value, position, GameStrings.Get(legacyAssetName), this.m_NotificationVO.Value, true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
		}

		// Token: 0x040093FC RID: 37884
		[Tooltip("Notification quote prefab to use.")]
		public FsmString m_NotificationPrefab;

		// Token: 0x040093FD RID: 37885
		[Tooltip("The VO line to play (+loc string).")]
		public FsmString m_NotificationVO;

		// Token: 0x040093FE RID: 37886
		[Tooltip("Notification popup position")]
		public FsmVector3 m_NotificationPosition;
	}
}
