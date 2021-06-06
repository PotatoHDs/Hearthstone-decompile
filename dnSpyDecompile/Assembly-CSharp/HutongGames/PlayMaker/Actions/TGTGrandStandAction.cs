using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F95 RID: 3989
	[ActionCategory("Pegasus")]
	[Tooltip("Used to control TGT Grand Stands")]
	public class TGTGrandStandAction : FsmStateAction
	{
		// Token: 0x0600ADE5 RID: 44517 RVA: 0x00362A2C File Offset: 0x00360C2C
		public void PlayEmote(TGTGrandStandAction.EMOTE emote)
		{
			TGTGrandStand tgtgrandStand = TGTGrandStand.Get();
			if (tgtgrandStand == null)
			{
				return;
			}
			if (emote == TGTGrandStandAction.EMOTE.Cheer)
			{
				tgtgrandStand.PlayCheerAnimation();
				return;
			}
			if (emote != TGTGrandStandAction.EMOTE.OhNo)
			{
				return;
			}
			tgtgrandStand.PlayOhNoAnimation();
		}

		// Token: 0x0600ADE6 RID: 44518 RVA: 0x00362A5E File Offset: 0x00360C5E
		public override void Reset()
		{
			this.m_emote = TGTGrandStandAction.EMOTE.Cheer;
		}

		// Token: 0x0600ADE7 RID: 44519 RVA: 0x00362A67 File Offset: 0x00360C67
		public override void OnEnter()
		{
			this.PlayEmote(this.m_emote);
		}

		// Token: 0x040094B9 RID: 38073
		[RequiredField]
		public TGTGrandStandAction.EMOTE m_emote;

		// Token: 0x040094BA RID: 38074
		protected Actor m_actor;

		// Token: 0x020027C8 RID: 10184
		public enum EMOTE
		{
			// Token: 0x0400F5A5 RID: 62885
			Cheer,
			// Token: 0x0400F5A6 RID: 62886
			OhNo
		}
	}
}
