using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BE7 RID: 3047
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Call a static method in a class.")]
	public class CallStaticMethod : FsmStateAction
	{
		// Token: 0x06009D0B RID: 40203 RVA: 0x0032771C File Offset: 0x0032591C
		public override void OnEnter()
		{
			this.parametersArray = new object[this.parameters.Length];
			this.DoMethodCall();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D0C RID: 40204 RVA: 0x00327745 File Offset: 0x00325945
		public override void OnUpdate()
		{
			this.DoMethodCall();
		}

		// Token: 0x06009D0D RID: 40205 RVA: 0x00327750 File Offset: 0x00325950
		private void DoMethodCall()
		{
			if (this.className == null || string.IsNullOrEmpty(this.className.Value))
			{
				base.Finish();
				return;
			}
			if (this.cachedClassName != this.className.Value || this.cachedMethodName != this.methodName.Value)
			{
				this.errorString = string.Empty;
				if (!this.DoCache())
				{
					Debug.LogError(this.errorString);
					base.Finish();
					return;
				}
			}
			object value;
			if (this.cachedParameterInfo.Length == 0)
			{
				value = this.cachedMethodInfo.Invoke(null, null);
			}
			else
			{
				for (int i = 0; i < this.parameters.Length; i++)
				{
					FsmVar fsmVar = this.parameters[i];
					fsmVar.UpdateValue();
					this.parametersArray[i] = fsmVar.GetValue();
				}
				value = this.cachedMethodInfo.Invoke(null, this.parametersArray);
			}
			this.storeResult.SetValue(value);
		}

		// Token: 0x06009D0E RID: 40206 RVA: 0x00327840 File Offset: 0x00325A40
		private bool DoCache()
		{
			this.cachedType = ReflectionUtils.GetGlobalType(this.className.Value);
			if (this.cachedType == null)
			{
				this.errorString = this.errorString + "Class is invalid: " + this.className.Value + "\n";
				base.Finish();
				return false;
			}
			this.cachedClassName = this.className.Value;
			List<Type> list = new List<Type>(this.parameters.Length);
			foreach (FsmVar fsmVar in this.parameters)
			{
				list.Add(fsmVar.RealType);
			}
			this.cachedMethodInfo = this.cachedType.GetMethod(this.methodName.Value, list.ToArray());
			if (this.cachedMethodInfo == null)
			{
				this.errorString = this.errorString + "Invalid Method Name or Parameters: " + this.methodName.Value + "\n";
				base.Finish();
				return false;
			}
			this.cachedMethodName = this.methodName.Value;
			this.cachedParameterInfo = this.cachedMethodInfo.GetParameters();
			return true;
		}

		// Token: 0x06009D0F RID: 40207 RVA: 0x00327964 File Offset: 0x00325B64
		public override string ErrorCheck()
		{
			this.errorString = string.Empty;
			this.DoCache();
			if (!string.IsNullOrEmpty(this.errorString))
			{
				return this.errorString;
			}
			if (this.parameters.Length != this.cachedParameterInfo.Length)
			{
				return string.Concat(new object[]
				{
					"Parameter count does not match method.\nMethod has ",
					this.cachedParameterInfo.Length,
					" parameters.\nYou specified ",
					this.parameters.Length,
					" paramaters."
				});
			}
			for (int i = 0; i < this.parameters.Length; i++)
			{
				Type realType = this.parameters[i].RealType;
				Type parameterType = this.cachedParameterInfo[i].ParameterType;
				if (realType != parameterType)
				{
					return string.Concat(new object[]
					{
						"Parameters do not match method signature.\nParameter ",
						i + 1,
						" (",
						realType,
						") should be of type: ",
						parameterType
					});
				}
			}
			if (this.cachedMethodInfo.ReturnType == typeof(void))
			{
				if (!string.IsNullOrEmpty(this.storeResult.variableName))
				{
					return "Method does not have return.\nSpecify 'none' in Store Result.";
				}
			}
			else if (this.cachedMethodInfo.ReturnType != this.storeResult.RealType)
			{
				return "Store Result is of the wrong type.\nIt should be of type: " + this.cachedMethodInfo.ReturnType;
			}
			return string.Empty;
		}

		// Token: 0x04008286 RID: 33414
		[Tooltip("Full path to the class that contains the static method.")]
		public FsmString className;

		// Token: 0x04008287 RID: 33415
		[Tooltip("The static method to call.")]
		public FsmString methodName;

		// Token: 0x04008288 RID: 33416
		[Tooltip("Method parameters. NOTE: these must match the method's signature!")]
		public FsmVar[] parameters;

		// Token: 0x04008289 RID: 33417
		[ActionSection("Store Result")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result of the method call.")]
		public FsmVar storeResult;

		// Token: 0x0400828A RID: 33418
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x0400828B RID: 33419
		private Type cachedType;

		// Token: 0x0400828C RID: 33420
		private string cachedClassName;

		// Token: 0x0400828D RID: 33421
		private string cachedMethodName;

		// Token: 0x0400828E RID: 33422
		private MethodInfo cachedMethodInfo;

		// Token: 0x0400828F RID: 33423
		private ParameterInfo[] cachedParameterInfo;

		// Token: 0x04008290 RID: 33424
		private object[] parametersArray;

		// Token: 0x04008291 RID: 33425
		private string errorString;
	}
}
