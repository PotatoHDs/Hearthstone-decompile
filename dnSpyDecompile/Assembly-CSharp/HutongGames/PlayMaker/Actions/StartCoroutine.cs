using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000E07 RID: 3591
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Start a Coroutine in a Behaviour on a Game Object. See Unity StartCoroutine docs.")]
	public class StartCoroutine : FsmStateAction
	{
		// Token: 0x0600A6F0 RID: 42736 RVA: 0x0034AE6F File Offset: 0x0034906F
		public override void Reset()
		{
			this.gameObject = null;
			this.behaviour = null;
			this.functionCall = null;
			this.stopOnExit = false;
		}

		// Token: 0x0600A6F1 RID: 42737 RVA: 0x0034AE8D File Offset: 0x0034908D
		public override void OnEnter()
		{
			this.DoStartCoroutine();
			base.Finish();
		}

		// Token: 0x0600A6F2 RID: 42738 RVA: 0x0034AE9C File Offset: 0x0034909C
		private void DoStartCoroutine()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			this.component = (ownerDefaultTarget.GetComponent(ReflectionUtils.GetGlobalType(this.behaviour.Value)) as MonoBehaviour);
			if (this.component == null)
			{
				base.LogWarning("StartCoroutine: " + ownerDefaultTarget.name + " missing behaviour: " + this.behaviour.Value);
				return;
			}
			string parameterType = this.functionCall.ParameterType;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(parameterType);
			if (num <= 2515107422U)
			{
				if (num <= 1796249895U)
				{
					if (num != 398550328U)
					{
						if (num != 810547195U)
						{
							if (num != 1796249895U)
							{
								return;
							}
							if (!(parameterType == "Rect"))
							{
								return;
							}
							this.component.StartCoroutine(this.functionCall.FunctionName, this.functionCall.RectParamater.Value);
							return;
						}
						else
						{
							if (!(parameterType == "None"))
							{
								return;
							}
							this.component.StartCoroutine(this.functionCall.FunctionName);
							return;
						}
					}
					else
					{
						if (!(parameterType == "string"))
						{
							return;
						}
						this.component.StartCoroutine(this.functionCall.FunctionName, this.functionCall.StringParameter.Value);
						return;
					}
				}
				else if (num != 2197844016U)
				{
					if (num != 2214621635U)
					{
						if (num != 2515107422U)
						{
							return;
						}
						if (!(parameterType == "int"))
						{
							return;
						}
						this.component.StartCoroutine(this.functionCall.FunctionName, this.functionCall.IntParameter.Value);
						return;
					}
					else
					{
						if (!(parameterType == "Vector3"))
						{
							return;
						}
						this.component.StartCoroutine(this.functionCall.FunctionName, this.functionCall.Vector3Parameter.Value);
						return;
					}
				}
				else
				{
					if (!(parameterType == "Vector2"))
					{
						return;
					}
					this.component.StartCoroutine(this.functionCall.FunctionName, this.functionCall.Vector2Parameter.Value);
					return;
				}
			}
			else if (num <= 3289806692U)
			{
				if (num != 2571916692U)
				{
					if (num != 2797886853U)
					{
						if (num != 3289806692U)
						{
							return;
						}
						if (!(parameterType == "GameObject"))
						{
							return;
						}
						this.component.StartCoroutine(this.functionCall.FunctionName, this.functionCall.GameObjectParameter.Value);
						return;
					}
					else
					{
						if (!(parameterType == "float"))
						{
							return;
						}
						this.component.StartCoroutine(this.functionCall.FunctionName, this.functionCall.FloatParameter.Value);
						return;
					}
				}
				else
				{
					if (!(parameterType == "Texture"))
					{
						return;
					}
					this.component.StartCoroutine(this.functionCall.FunctionName, this.functionCall.TextureParameter.Value);
					return;
				}
			}
			else if (num <= 3419754368U)
			{
				if (num != 3365180733U)
				{
					if (num != 3419754368U)
					{
						return;
					}
					if (!(parameterType == "Material"))
					{
						return;
					}
					this.component.StartCoroutine(this.functionCall.FunctionName, this.functionCall.MaterialParameter.Value);
					return;
				}
				else
				{
					if (!(parameterType == "bool"))
					{
						return;
					}
					this.component.StartCoroutine(this.functionCall.FunctionName, this.functionCall.BoolParameter.Value);
					return;
				}
			}
			else if (num != 3731074221U)
			{
				if (num != 3851314394U)
				{
					return;
				}
				if (!(parameterType == "Object"))
				{
					return;
				}
				this.component.StartCoroutine(this.functionCall.FunctionName, this.functionCall.ObjectParameter.Value);
				return;
			}
			else
			{
				if (!(parameterType == "Quaternion"))
				{
					return;
				}
				this.component.StartCoroutine(this.functionCall.FunctionName, this.functionCall.QuaternionParameter.Value);
				return;
			}
		}

		// Token: 0x0600A6F3 RID: 42739 RVA: 0x0034B2D0 File Offset: 0x003494D0
		public override void OnExit()
		{
			if (this.component == null)
			{
				return;
			}
			if (this.stopOnExit)
			{
				this.component.StopCoroutine(this.functionCall.FunctionName);
			}
		}

		// Token: 0x04008D7B RID: 36219
		[RequiredField]
		[Tooltip("The game object that owns the Behaviour.")]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008D7C RID: 36220
		[RequiredField]
		[UIHint(UIHint.Behaviour)]
		[Tooltip("The Behaviour that contains the method to start as a coroutine.")]
		public FsmString behaviour;

		// Token: 0x04008D7D RID: 36221
		[RequiredField]
		[UIHint(UIHint.Coroutine)]
		[Tooltip("The name of the coroutine method.")]
		public FunctionCall functionCall;

		// Token: 0x04008D7E RID: 36222
		[Tooltip("Stop the coroutine when the state is exited.")]
		public bool stopOnExit;

		// Token: 0x04008D7F RID: 36223
		private MonoBehaviour component;
	}
}
