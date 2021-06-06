using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DB5 RID: 3509
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Sets Event Data before sending an event. Get the Event Data, along with sender information, using Get Event Info action.")]
	public class SetEventData : FsmStateAction
	{
		// Token: 0x0600A57F RID: 42367 RVA: 0x00346B54 File Offset: 0x00344D54
		public override void Reset()
		{
			this.setGameObjectData = new FsmGameObject
			{
				UseVariable = true
			};
			this.setIntData = new FsmInt
			{
				UseVariable = true
			};
			this.setFloatData = new FsmFloat
			{
				UseVariable = true
			};
			this.setStringData = new FsmString
			{
				UseVariable = true
			};
			this.setBoolData = new FsmBool
			{
				UseVariable = true
			};
			this.setVector2Data = new FsmVector2
			{
				UseVariable = true
			};
			this.setVector3Data = new FsmVector3
			{
				UseVariable = true
			};
			this.setRectData = new FsmRect
			{
				UseVariable = true
			};
			this.setQuaternionData = new FsmQuaternion
			{
				UseVariable = true
			};
			this.setColorData = new FsmColor
			{
				UseVariable = true
			};
			this.setMaterialData = new FsmMaterial
			{
				UseVariable = true
			};
			this.setTextureData = new FsmTexture
			{
				UseVariable = true
			};
			this.setObjectData = new FsmObject
			{
				UseVariable = true
			};
		}

		// Token: 0x0600A580 RID: 42368 RVA: 0x00346C4C File Offset: 0x00344E4C
		public override void OnEnter()
		{
			Fsm.EventData.BoolData = this.setBoolData.Value;
			Fsm.EventData.IntData = this.setIntData.Value;
			Fsm.EventData.FloatData = this.setFloatData.Value;
			Fsm.EventData.Vector2Data = this.setVector2Data.Value;
			Fsm.EventData.Vector3Data = this.setVector3Data.Value;
			Fsm.EventData.StringData = this.setStringData.Value;
			Fsm.EventData.GameObjectData = this.setGameObjectData.Value;
			Fsm.EventData.RectData = this.setRectData.Value;
			Fsm.EventData.QuaternionData = this.setQuaternionData.Value;
			Fsm.EventData.ColorData = this.setColorData.Value;
			Fsm.EventData.MaterialData = this.setMaterialData.Value;
			Fsm.EventData.TextureData = this.setTextureData.Value;
			Fsm.EventData.ObjectData = this.setObjectData.Value;
			base.Finish();
		}

		// Token: 0x04008C02 RID: 35842
		public FsmGameObject setGameObjectData;

		// Token: 0x04008C03 RID: 35843
		public FsmInt setIntData;

		// Token: 0x04008C04 RID: 35844
		public FsmFloat setFloatData;

		// Token: 0x04008C05 RID: 35845
		public FsmString setStringData;

		// Token: 0x04008C06 RID: 35846
		public FsmBool setBoolData;

		// Token: 0x04008C07 RID: 35847
		public FsmVector2 setVector2Data;

		// Token: 0x04008C08 RID: 35848
		public FsmVector3 setVector3Data;

		// Token: 0x04008C09 RID: 35849
		public FsmRect setRectData;

		// Token: 0x04008C0A RID: 35850
		public FsmQuaternion setQuaternionData;

		// Token: 0x04008C0B RID: 35851
		public FsmColor setColorData;

		// Token: 0x04008C0C RID: 35852
		public FsmMaterial setMaterialData;

		// Token: 0x04008C0D RID: 35853
		public FsmTexture setTextureData;

		// Token: 0x04008C0E RID: 35854
		public FsmObject setObjectData;
	}
}
