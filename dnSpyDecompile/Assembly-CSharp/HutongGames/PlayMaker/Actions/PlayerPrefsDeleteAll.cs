using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000D28 RID: 3368
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Removes all keys and values from the preferences. Use with caution.")]
	public class PlayerPrefsDeleteAll : FsmStateAction
	{
		// Token: 0x0600A2CD RID: 41677 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override void Reset()
		{
		}

		// Token: 0x0600A2CE RID: 41678 RVA: 0x0033D9DB File Offset: 0x0033BBDB
		public override void OnEnter()
		{
			PlayerPrefs.DeleteAll();
			base.Finish();
		}
	}
}
