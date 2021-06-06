using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DA1 RID: 3489
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Sends a Message to a Game Object. See Unity docs for SendMessage.")]
	public class SendMessage : FsmStateAction
	{
		// Token: 0x0600A524 RID: 42276 RVA: 0x00345B5E File Offset: 0x00343D5E
		public override void Reset()
		{
			this.gameObject = null;
			this.delivery = SendMessage.MessageType.SendMessage;
			this.options = SendMessageOptions.DontRequireReceiver;
			this.functionCall = null;
		}

		// Token: 0x0600A525 RID: 42277 RVA: 0x00345B7C File Offset: 0x00343D7C
		public override void OnEnter()
		{
			this.DoSendMessage();
			base.Finish();
		}

		// Token: 0x0600A526 RID: 42278 RVA: 0x00345B8C File Offset: 0x00343D8C
		private void DoSendMessage()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			object obj = null;
			string parameterType = this.functionCall.ParameterType;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(parameterType);
			if (num <= 2571916692U)
			{
				if (num <= 1796249895U)
				{
					if (num <= 398550328U)
					{
						if (num != 382270662U)
						{
							if (num == 398550328U)
							{
								if (parameterType == "string")
								{
									obj = this.functionCall.StringParameter.Value;
								}
							}
						}
						else if (parameterType == "Array")
						{
							obj = this.functionCall.ArrayParameter.Values;
						}
					}
					else if (num != 810547195U)
					{
						if (num == 1796249895U)
						{
							if (parameterType == "Rect")
							{
								obj = this.functionCall.RectParamater.Value;
							}
						}
					}
					else if (!(parameterType == "None"))
					{
					}
				}
				else if (num <= 2214621635U)
				{
					if (num != 2197844016U)
					{
						if (num == 2214621635U)
						{
							if (parameterType == "Vector3")
							{
								obj = this.functionCall.Vector3Parameter.Value;
							}
						}
					}
					else if (parameterType == "Vector2")
					{
						obj = this.functionCall.Vector2Parameter.Value;
					}
				}
				else if (num != 2515107422U)
				{
					if (num == 2571916692U)
					{
						if (parameterType == "Texture")
						{
							obj = this.functionCall.TextureParameter.Value;
						}
					}
				}
				else if (parameterType == "int")
				{
					obj = this.functionCall.IntParameter.Value;
				}
			}
			else if (num <= 3419754368U)
			{
				if (num <= 3289806692U)
				{
					if (num != 2797886853U)
					{
						if (num == 3289806692U)
						{
							if (parameterType == "GameObject")
							{
								obj = this.functionCall.GameObjectParameter.Value;
							}
						}
					}
					else if (parameterType == "float")
					{
						obj = this.functionCall.FloatParameter.Value;
					}
				}
				else if (num != 3365180733U)
				{
					if (num == 3419754368U)
					{
						if (parameterType == "Material")
						{
							obj = this.functionCall.MaterialParameter.Value;
						}
					}
				}
				else if (parameterType == "bool")
				{
					obj = this.functionCall.BoolParameter.Value;
				}
			}
			else if (num <= 3851314394U)
			{
				if (num != 3731074221U)
				{
					if (num == 3851314394U)
					{
						if (parameterType == "Object")
						{
							obj = this.functionCall.ObjectParameter.Value;
						}
					}
				}
				else if (parameterType == "Quaternion")
				{
					obj = this.functionCall.QuaternionParameter.Value;
				}
			}
			else if (num != 3853794552U)
			{
				if (num == 3897416224U)
				{
					if (parameterType == "Enum")
					{
						obj = this.functionCall.EnumParameter.Value;
					}
				}
			}
			else if (parameterType == "Color")
			{
				obj = this.functionCall.ColorParameter.Value;
			}
			switch (this.delivery)
			{
			case SendMessage.MessageType.SendMessage:
				ownerDefaultTarget.SendMessage(this.functionCall.FunctionName, obj, this.options);
				return;
			case SendMessage.MessageType.SendMessageUpwards:
				ownerDefaultTarget.SendMessageUpwards(this.functionCall.FunctionName, obj, this.options);
				return;
			case SendMessage.MessageType.BroadcastMessage:
				ownerDefaultTarget.BroadcastMessage(this.functionCall.FunctionName, obj, this.options);
				return;
			default:
				return;
			}
		}

		// Token: 0x04008BBF RID: 35775
		[RequiredField]
		[Tooltip("GameObject that sends the message.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008BC0 RID: 35776
		[Tooltip("Where to send the message.\nSee Unity docs.")]
		public SendMessage.MessageType delivery;

		// Token: 0x04008BC1 RID: 35777
		[Tooltip("Send options.\nSee Unity docs.")]
		public SendMessageOptions options;

		// Token: 0x04008BC2 RID: 35778
		[RequiredField]
		public FunctionCall functionCall;

		// Token: 0x020027AB RID: 10155
		public enum MessageType
		{
			// Token: 0x0400F526 RID: 62758
			SendMessage,
			// Token: 0x0400F527 RID: 62759
			SendMessageUpwards,
			// Token: 0x0400F528 RID: 62760
			BroadcastMessage
		}
	}
}
