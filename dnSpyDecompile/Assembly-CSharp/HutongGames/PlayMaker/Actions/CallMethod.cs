using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000BE6 RID: 3046
	[ActionCategory(ActionCategory.ScriptControl)]
	[Tooltip("Call a method in a component on a GameObject.")]
	public class CallMethod : FsmStateAction
	{
		// Token: 0x06009D02 RID: 40194 RVA: 0x00327223 File Offset: 0x00325423
		public override void Reset()
		{
			this.behaviour = null;
			this.methodName = null;
			this.parameters = null;
			this.storeResult = null;
			this.everyFrame = false;
		}

		// Token: 0x06009D03 RID: 40195 RVA: 0x00327248 File Offset: 0x00325448
		public override void OnEnter()
		{
			this.parametersArray = new object[this.parameters.Length];
			this.DoMethodCall();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x06009D04 RID: 40196 RVA: 0x00327271 File Offset: 0x00325471
		public override void OnUpdate()
		{
			this.DoMethodCall();
		}

		// Token: 0x06009D05 RID: 40197 RVA: 0x0032727C File Offset: 0x0032547C
		private void DoMethodCall()
		{
			if (this.behaviour.Value == null)
			{
				base.Finish();
				return;
			}
			if (this.NeedToUpdateCache() && !this.DoCache())
			{
				Debug.LogError(this.errorString);
				base.Finish();
				return;
			}
			object value;
			if (this.cachedParameterInfo.Length == 0)
			{
				value = this.cachedMethodInfo.Invoke(this.cachedBehaviour.Value, null);
			}
			else
			{
				for (int i = 0; i < this.parameters.Length; i++)
				{
					FsmVar fsmVar = this.parameters[i];
					fsmVar.UpdateValue();
					if (fsmVar.Type == VariableType.Array)
					{
						fsmVar.UpdateValue();
						object[] array = fsmVar.GetValue() as object[];
						Array array2 = Array.CreateInstance(this.cachedParameterInfo[i].ParameterType.GetElementType(), array.Length);
						for (int j = 0; j < array.Length; j++)
						{
							array2.SetValue(array[j], j);
						}
						this.parametersArray[i] = array2;
					}
					else
					{
						fsmVar.UpdateValue();
						this.parametersArray[i] = fsmVar.GetValue();
					}
				}
				value = this.cachedMethodInfo.Invoke(this.cachedBehaviour.Value, this.parametersArray);
			}
			if (this.storeResult != null && !this.storeResult.IsNone && this.storeResult.Type != VariableType.Unknown)
			{
				this.storeResult.SetValue(value);
			}
		}

		// Token: 0x06009D06 RID: 40198 RVA: 0x003273D8 File Offset: 0x003255D8
		private bool NeedToUpdateCache()
		{
			return this.cachedBehaviour == null || this.cachedMethodName == null || this.cachedBehaviour.Value != this.behaviour.Value || this.cachedBehaviour.Name != this.behaviour.Name || this.cachedMethodName.Value != this.methodName.Value || this.cachedMethodName.Name != this.methodName.Name;
		}

		// Token: 0x06009D07 RID: 40199 RVA: 0x00327469 File Offset: 0x00325669
		private void ClearCache()
		{
			this.cachedBehaviour = null;
			this.cachedMethodName = null;
			this.cachedType = null;
			this.cachedMethodInfo = null;
			this.cachedParameterInfo = null;
		}

		// Token: 0x06009D08 RID: 40200 RVA: 0x00327490 File Offset: 0x00325690
		private bool DoCache()
		{
			this.ClearCache();
			this.errorString = string.Empty;
			this.cachedBehaviour = new FsmObject(this.behaviour);
			this.cachedMethodName = new FsmString(this.methodName);
			if (this.cachedBehaviour.Value == null)
			{
				if (!this.behaviour.UsesVariable || Application.isPlaying)
				{
					this.errorString += "Behaviour is invalid!\n";
				}
				base.Finish();
				return false;
			}
			this.cachedType = this.behaviour.Value.GetType();
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
			this.cachedParameterInfo = this.cachedMethodInfo.GetParameters();
			return true;
		}

		// Token: 0x06009D09 RID: 40201 RVA: 0x003275D0 File Offset: 0x003257D0
		public override string ErrorCheck()
		{
			if (Application.isPlaying)
			{
				return this.errorString;
			}
			if (!this.DoCache())
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

		// Token: 0x04008279 RID: 33401
		[ObjectType(typeof(Component))]
		[Tooltip("The behaviour on a GameObject that has the method you want to call. Drag the script component from the Unity inspector into this slot. HINT: Use Lock if the script is on another GameObject.\n\nNOTE: Unity Object fields only show the GameObject name, so for clarity we show the Behaviour name in a readonly field below.")]
		public FsmObject behaviour;

		// Token: 0x0400827A RID: 33402
		[Tooltip("Name of the method to call on the component")]
		public FsmString methodName;

		// Token: 0x0400827B RID: 33403
		[Tooltip("Method parameters. NOTE: these must match the method's signature!")]
		public FsmVar[] parameters;

		// Token: 0x0400827C RID: 33404
		[ActionSection("Store Result")]
		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result of the method call.")]
		public FsmVar storeResult;

		// Token: 0x0400827D RID: 33405
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		// Token: 0x0400827E RID: 33406
		[Tooltip("Use the old manual editor UI.")]
		public bool manualUI;

		// Token: 0x0400827F RID: 33407
		private FsmObject cachedBehaviour;

		// Token: 0x04008280 RID: 33408
		private FsmString cachedMethodName;

		// Token: 0x04008281 RID: 33409
		private Type cachedType;

		// Token: 0x04008282 RID: 33410
		private MethodInfo cachedMethodInfo;

		// Token: 0x04008283 RID: 33411
		private ParameterInfo[] cachedParameterInfo;

		// Token: 0x04008284 RID: 33412
		private object[] parametersArray;

		// Token: 0x04008285 RID: 33413
		private string errorString;
	}
}
