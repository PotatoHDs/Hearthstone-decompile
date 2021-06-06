using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000EB1 RID: 3761
	[ActionCategory(ActionCategory.Vector2)]
	[Tooltip("Multiplies a Vector2 variable by Time.deltaTime. Useful for frame rate independent motion.")]
	public class Vector2PerSecond : FsmStateAction
	{
		// Token: 0x0600AA0B RID: 43531 RVA: 0x003542D6 File Offset: 0x003524D6
		public override void Reset()
		{
			this.vector2Variable = null;
			this.everyFrame = true;
		}

		// Token: 0x0600AA0C RID: 43532 RVA: 0x003542E6 File Offset: 0x003524E6
		public override void OnEnter()
		{
			this.vector2Variable.Value = this.vector2Variable.Value * Time.deltaTime;
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600AA0D RID: 43533 RVA: 0x00354316 File Offset: 0x00352516
		public override void OnUpdate()
		{
			this.vector2Variable.Value = this.vector2Variable.Value * Time.deltaTime;
		}

		// Token: 0x040090AA RID: 37034
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Vector2")]
		public FsmVector2 vector2Variable;

		// Token: 0x040090AB RID: 37035
		[Tooltip("Repeat every frame")]
		public bool everyFrame;
	}
}
