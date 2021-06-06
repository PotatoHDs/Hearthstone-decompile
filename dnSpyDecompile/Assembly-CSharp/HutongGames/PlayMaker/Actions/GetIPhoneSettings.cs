using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C67 RID: 3175
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Get various iPhone settings.")]
	public class GetIPhoneSettings : FsmStateAction
	{
		// Token: 0x06009F5C RID: 40796 RVA: 0x0032E6DF File Offset: 0x0032C8DF
		public override void Reset()
		{
			this.getScreenCanDarken = null;
			this.getUniqueIdentifier = null;
			this.getName = null;
			this.getModel = null;
			this.getSystemName = null;
			this.getGeneration = null;
		}

		// Token: 0x06009F5D RID: 40797 RVA: 0x00328883 File Offset: 0x00326A83
		public override void OnEnter()
		{
			base.Finish();
		}

		// Token: 0x040084FA RID: 34042
		[UIHint(UIHint.Variable)]
		[Tooltip("Allows device to fall into 'sleep' state with screen being dim if no touches occurred. Default value is true.")]
		public FsmBool getScreenCanDarken;

		// Token: 0x040084FB RID: 34043
		[UIHint(UIHint.Variable)]
		[Tooltip("A unique device identifier string. It is guaranteed to be unique for every device (Read Only).")]
		public FsmString getUniqueIdentifier;

		// Token: 0x040084FC RID: 34044
		[UIHint(UIHint.Variable)]
		[Tooltip("The user defined name of the device (Read Only).")]
		public FsmString getName;

		// Token: 0x040084FD RID: 34045
		[UIHint(UIHint.Variable)]
		[Tooltip("The model of the device (Read Only).")]
		public FsmString getModel;

		// Token: 0x040084FE RID: 34046
		[UIHint(UIHint.Variable)]
		[Tooltip("The name of the operating system running on the device (Read Only).")]
		public FsmString getSystemName;

		// Token: 0x040084FF RID: 34047
		[UIHint(UIHint.Variable)]
		[Tooltip("The generation of the device (Read Only).")]
		public FsmString getGeneration;
	}
}
