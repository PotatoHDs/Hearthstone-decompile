using System;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C55 RID: 3157
	[ActionCategory(ActionCategory.StateMachine)]
	[Tooltip("Gets info on the last event that caused a state change. See also Set Event Data action.")]
	public class GetEventInfo : FsmStateAction
	{
		// Token: 0x06009F02 RID: 40706 RVA: 0x0032D198 File Offset: 0x0032B398
		public override void Reset()
		{
			this.sentByGameObject = null;
			this.fsmName = null;
			this.getBoolData = null;
			this.getIntData = null;
			this.getFloatData = null;
			this.getVector2Data = null;
			this.getVector3Data = null;
			this.getStringData = null;
			this.getGameObjectData = null;
			this.getRectData = null;
			this.getQuaternionData = null;
			this.getMaterialData = null;
			this.getTextureData = null;
			this.getColorData = null;
			this.getObjectData = null;
		}

		// Token: 0x06009F03 RID: 40707 RVA: 0x0032D210 File Offset: 0x0032B410
		public override void OnEnter()
		{
			if (Fsm.EventData.SentByGameObject != null)
			{
				this.sentByGameObject.Value = Fsm.EventData.SentByGameObject;
			}
			else if (Fsm.EventData.SentByFsm != null)
			{
				this.sentByGameObject.Value = Fsm.EventData.SentByFsm.GameObject;
				this.fsmName.Value = Fsm.EventData.SentByFsm.Name;
			}
			else
			{
				this.sentByGameObject.Value = null;
				this.fsmName.Value = "";
			}
			this.getBoolData.Value = Fsm.EventData.BoolData;
			this.getIntData.Value = Fsm.EventData.IntData;
			this.getFloatData.Value = Fsm.EventData.FloatData;
			this.getVector2Data.Value = Fsm.EventData.Vector2Data;
			this.getVector3Data.Value = Fsm.EventData.Vector3Data;
			this.getStringData.Value = Fsm.EventData.StringData;
			this.getGameObjectData.Value = Fsm.EventData.GameObjectData;
			this.getRectData.Value = Fsm.EventData.RectData;
			this.getQuaternionData.Value = Fsm.EventData.QuaternionData;
			this.getMaterialData.Value = Fsm.EventData.MaterialData;
			this.getTextureData.Value = Fsm.EventData.TextureData;
			this.getColorData.Value = Fsm.EventData.ColorData;
			this.getObjectData.Value = Fsm.EventData.ObjectData;
			base.Finish();
		}

		// Token: 0x04008463 RID: 33891
		[UIHint(UIHint.Variable)]
		public FsmGameObject sentByGameObject;

		// Token: 0x04008464 RID: 33892
		[UIHint(UIHint.Variable)]
		public FsmString fsmName;

		// Token: 0x04008465 RID: 33893
		[UIHint(UIHint.Variable)]
		public FsmBool getBoolData;

		// Token: 0x04008466 RID: 33894
		[UIHint(UIHint.Variable)]
		public FsmInt getIntData;

		// Token: 0x04008467 RID: 33895
		[UIHint(UIHint.Variable)]
		public FsmFloat getFloatData;

		// Token: 0x04008468 RID: 33896
		[UIHint(UIHint.Variable)]
		public FsmVector2 getVector2Data;

		// Token: 0x04008469 RID: 33897
		[UIHint(UIHint.Variable)]
		public FsmVector3 getVector3Data;

		// Token: 0x0400846A RID: 33898
		[UIHint(UIHint.Variable)]
		public FsmString getStringData;

		// Token: 0x0400846B RID: 33899
		[UIHint(UIHint.Variable)]
		public FsmGameObject getGameObjectData;

		// Token: 0x0400846C RID: 33900
		[UIHint(UIHint.Variable)]
		public FsmRect getRectData;

		// Token: 0x0400846D RID: 33901
		[UIHint(UIHint.Variable)]
		public FsmQuaternion getQuaternionData;

		// Token: 0x0400846E RID: 33902
		[UIHint(UIHint.Variable)]
		public FsmMaterial getMaterialData;

		// Token: 0x0400846F RID: 33903
		[UIHint(UIHint.Variable)]
		public FsmTexture getTextureData;

		// Token: 0x04008470 RID: 33904
		[UIHint(UIHint.Variable)]
		public FsmColor getColorData;

		// Token: 0x04008471 RID: 33905
		[UIHint(UIHint.Variable)]
		public FsmObject getObjectData;
	}
}
