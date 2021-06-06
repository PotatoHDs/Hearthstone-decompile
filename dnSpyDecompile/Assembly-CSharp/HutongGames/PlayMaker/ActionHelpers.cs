using System;
using HutongGames.PlayMaker.AnimationEnums;
using UnityEngine;

namespace HutongGames.PlayMaker
{
	// Token: 0x02000B8C RID: 2956
	public static class ActionHelpers
	{
		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06009B2D RID: 39725 RVA: 0x0031E9E0 File Offset: 0x0031CBE0
		public static Texture2D WhiteTexture
		{
			get
			{
				return Texture2D.whiteTexture;
			}
		}

		// Token: 0x06009B2E RID: 39726 RVA: 0x0031E9E8 File Offset: 0x0031CBE8
		public static Color BlendColor(ColorBlendMode blendMode, Color c1, Color c2)
		{
			switch (blendMode)
			{
			case ColorBlendMode.Normal:
				return Color.Lerp(c1, c2, c2.a);
			case ColorBlendMode.Multiply:
				return Color.Lerp(c1, c1 * c2, c2.a);
			case ColorBlendMode.Screen:
			{
				Color b = Color.white - (Color.white - c1) * (Color.white - c2);
				return Color.Lerp(c1, b, c2.a);
			}
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06009B2F RID: 39727 RVA: 0x0031EA64 File Offset: 0x0031CC64
		public static bool IsVisible(GameObject go)
		{
			if (go == null)
			{
				return false;
			}
			Renderer component = go.GetComponent<Renderer>();
			return component != null && component.isVisible;
		}

		// Token: 0x06009B30 RID: 39728 RVA: 0x0031EA94 File Offset: 0x0031CC94
		public static GameObject GetOwnerDefault(FsmStateAction action, FsmOwnerDefault ownerDefault)
		{
			return action.Fsm.GetOwnerDefaultTarget(ownerDefault);
		}

		// Token: 0x06009B31 RID: 39729 RVA: 0x0031EAA4 File Offset: 0x0031CCA4
		public static PlayMakerFSM GetGameObjectFsm(GameObject go, string fsmName)
		{
			if (!string.IsNullOrEmpty(fsmName))
			{
				foreach (PlayMakerFSM playMakerFSM in go.GetComponents<PlayMakerFSM>())
				{
					if (playMakerFSM.FsmName == fsmName)
					{
						return playMakerFSM;
					}
				}
				Debug.LogWarning("Could not find FSM: " + fsmName);
			}
			return go.GetComponent<PlayMakerFSM>();
		}

		// Token: 0x06009B32 RID: 39730 RVA: 0x0031EAF8 File Offset: 0x0031CCF8
		public static int GetRandomWeightedIndex(FsmFloat[] weights)
		{
			float num = 0f;
			foreach (FsmFloat fsmFloat in weights)
			{
				num += fsmFloat.Value;
			}
			float num2 = UnityEngine.Random.Range(0f, num);
			for (int j = 0; j < weights.Length; j++)
			{
				if (num2 < weights[j].Value)
				{
					return j;
				}
				num2 -= weights[j].Value;
			}
			return -1;
		}

		// Token: 0x06009B33 RID: 39731 RVA: 0x0031EB64 File Offset: 0x0031CD64
		public static void AddAnimationClip(GameObject go, AnimationClip animClip)
		{
			if (animClip == null)
			{
				return;
			}
			Animation component = go.GetComponent<Animation>();
			if (component != null)
			{
				component.AddClip(animClip, animClip.name);
			}
		}

		// Token: 0x06009B34 RID: 39732 RVA: 0x0031EB98 File Offset: 0x0031CD98
		public static bool HasAnimationFinished(AnimationState anim, float prevTime, float currentTime)
		{
			return anim.wrapMode != WrapMode.Loop && anim.wrapMode != WrapMode.PingPong && (((anim.wrapMode == WrapMode.Default || anim.wrapMode == WrapMode.Once) && prevTime > 0f && currentTime.Equals(0f)) || (prevTime < anim.length && currentTime >= anim.length));
		}

		// Token: 0x06009B35 RID: 39733 RVA: 0x0031EBFC File Offset: 0x0031CDFC
		public static Vector3 GetPosition(FsmGameObject fsmGameObject, FsmVector3 fsmVector3)
		{
			Vector3 result;
			if (fsmGameObject.Value != null)
			{
				result = ((!fsmVector3.IsNone) ? fsmGameObject.Value.transform.TransformPoint(fsmVector3.Value) : fsmGameObject.Value.transform.position);
			}
			else
			{
				result = fsmVector3.Value;
			}
			return result;
		}

		// Token: 0x06009B36 RID: 39734 RVA: 0x0031EC54 File Offset: 0x0031CE54
		public static Quaternion GetTargetRotation(RotationOptions option, Transform owner, Transform target, Vector3 rotation)
		{
			if (owner == null)
			{
				return Quaternion.identity;
			}
			switch (option)
			{
			case RotationOptions.CurrentRotation:
				return owner.rotation;
			case RotationOptions.WorldRotation:
				return Quaternion.Euler(rotation);
			case RotationOptions.LocalRotation:
				if (owner.parent == null)
				{
					return Quaternion.Euler(rotation);
				}
				return owner.parent.rotation * Quaternion.Euler(rotation);
			case RotationOptions.WorldOffsetRotation:
				return Quaternion.Euler(rotation) * owner.rotation;
			case RotationOptions.LocalOffsetRotation:
				return owner.rotation * Quaternion.Euler(rotation);
			case RotationOptions.MatchGameObjectRotation:
				if (target == null)
				{
					return owner.rotation;
				}
				return target.rotation * Quaternion.Euler(rotation);
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06009B37 RID: 39735 RVA: 0x0031ED18 File Offset: 0x0031CF18
		public static bool GetTargetRotation(RotationOptions option, Transform owner, FsmVector3 rotation, FsmGameObject target, out Quaternion targetRotation)
		{
			targetRotation = Quaternion.identity;
			if (owner == null || !ActionHelpers.CanEditTargetRotation(option, rotation, target))
			{
				return false;
			}
			targetRotation = ActionHelpers.GetTargetRotation(option, owner, (target.Value != null) ? target.Value.transform : null, rotation.Value);
			return true;
		}

		// Token: 0x06009B38 RID: 39736 RVA: 0x0031ED77 File Offset: 0x0031CF77
		private static bool CanEditTargetRotation(RotationOptions option, NamedVariable rotation, FsmGameObject target)
		{
			switch (option)
			{
			case RotationOptions.CurrentRotation:
				return false;
			case RotationOptions.WorldRotation:
			case RotationOptions.LocalRotation:
			case RotationOptions.WorldOffsetRotation:
			case RotationOptions.LocalOffsetRotation:
				return !rotation.IsNone;
			case RotationOptions.MatchGameObjectRotation:
				return target.Value != null;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06009B39 RID: 39737 RVA: 0x0031EDB8 File Offset: 0x0031CFB8
		public static Vector3 GetTargetScale(ScaleOptions option, Transform owner, Transform target, Vector3 scale)
		{
			if (owner == null)
			{
				return Vector3.one;
			}
			switch (option)
			{
			case ScaleOptions.CurrentScale:
				return owner.localScale;
			case ScaleOptions.LocalScale:
				return scale;
			case ScaleOptions.MultiplyScale:
				return new Vector3(owner.localScale.x * scale.x, owner.localScale.y * scale.y, owner.localScale.z * scale.z);
			case ScaleOptions.AddToScale:
				return new Vector3(owner.localScale.x + scale.x, owner.localScale.y + scale.y, owner.localScale.z + scale.z);
			case ScaleOptions.MatchGameObject:
				if (target == null)
				{
					return owner.localScale;
				}
				return target.localScale;
			default:
				return owner.localScale;
			}
		}

		// Token: 0x06009B3A RID: 39738 RVA: 0x0031EE94 File Offset: 0x0031D094
		public static bool GetTargetPosition(PositionOptions option, Transform owner, FsmVector3 position, FsmGameObject target, out Vector3 targetPosition)
		{
			targetPosition = Vector3.zero;
			if (owner == null || !ActionHelpers.IsValidTargetPosition(option, position, target))
			{
				return false;
			}
			targetPosition = ActionHelpers.GetTargetPosition(option, owner, (target != null && target.Value != null) ? target.Value.transform : null, position.Value);
			return true;
		}

		// Token: 0x06009B3B RID: 39739 RVA: 0x0031EEF6 File Offset: 0x0031D0F6
		private static bool IsValidTargetPosition(PositionOptions option, NamedVariable position, FsmGameObject target)
		{
			switch (option)
			{
			case PositionOptions.CurrentPosition:
				return true;
			case PositionOptions.WorldPosition:
			case PositionOptions.LocalPosition:
			case PositionOptions.WorldOffset:
			case PositionOptions.LocalOffset:
				return !position.IsNone;
			case PositionOptions.TargetGameObject:
				return target.Value != null;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06009B3C RID: 39740 RVA: 0x0031ED77 File Offset: 0x0031CF77
		public static bool CanEditTargetPosition(PositionOptions option, NamedVariable position, FsmGameObject target)
		{
			switch (option)
			{
			case PositionOptions.CurrentPosition:
				return false;
			case PositionOptions.WorldPosition:
			case PositionOptions.LocalPosition:
			case PositionOptions.WorldOffset:
			case PositionOptions.LocalOffset:
				return !position.IsNone;
			case PositionOptions.TargetGameObject:
				return target.Value != null;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06009B3D RID: 39741 RVA: 0x0031EF38 File Offset: 0x0031D138
		public static Vector3 GetTargetPosition(PositionOptions option, Transform owner, Transform target, Vector3 position)
		{
			if (owner == null)
			{
				return Vector3.zero;
			}
			switch (option)
			{
			case PositionOptions.CurrentPosition:
				return owner.position;
			case PositionOptions.WorldPosition:
				return position;
			case PositionOptions.LocalPosition:
				if (owner.parent == null)
				{
					return position;
				}
				return owner.parent.TransformPoint(position);
			case PositionOptions.WorldOffset:
				return owner.position + position;
			case PositionOptions.LocalOffset:
				return owner.TransformPoint(position);
			case PositionOptions.TargetGameObject:
				if (target == null)
				{
					return owner.position;
				}
				if (position != Vector3.zero)
				{
					return target.TransformPoint(position);
				}
				return target.position;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x06009B3E RID: 39742 RVA: 0x0031EFE0 File Offset: 0x0031D1E0
		public static bool IsMouseOver(GameObject gameObject, float distance, int layerMask)
		{
			return !(gameObject == null) && gameObject == ActionHelpers.MouseOver(distance, layerMask);
		}

		// Token: 0x06009B3F RID: 39743 RVA: 0x0031EFFA File Offset: 0x0031D1FA
		public static RaycastHit MousePick(float distance, int layerMask)
		{
			if (!ActionHelpers.mousePickRaycastTime.Equals((float)Time.frameCount) || ActionHelpers.mousePickDistanceUsed < distance || ActionHelpers.mousePickLayerMaskUsed != layerMask)
			{
				ActionHelpers.DoMousePick(distance, layerMask);
			}
			return ActionHelpers.mousePickInfo;
		}

		// Token: 0x06009B40 RID: 39744 RVA: 0x0031F02C File Offset: 0x0031D22C
		public static GameObject MouseOver(float distance, int layerMask)
		{
			if (!ActionHelpers.mousePickRaycastTime.Equals((float)Time.frameCount) || ActionHelpers.mousePickDistanceUsed < distance || ActionHelpers.mousePickLayerMaskUsed != layerMask)
			{
				ActionHelpers.DoMousePick(distance, layerMask);
			}
			if (ActionHelpers.mousePickInfo.collider != null && ActionHelpers.mousePickInfo.distance < distance)
			{
				return ActionHelpers.mousePickInfo.collider.gameObject;
			}
			return null;
		}

		// Token: 0x06009B41 RID: 39745 RVA: 0x0031F094 File Offset: 0x0031D294
		private static void DoMousePick(float distance, int layerMask)
		{
			if (Camera.main == null)
			{
				return;
			}
			Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out ActionHelpers.mousePickInfo, distance, layerMask);
			ActionHelpers.mousePickLayerMaskUsed = layerMask;
			ActionHelpers.mousePickDistanceUsed = distance;
			ActionHelpers.mousePickRaycastTime = (float)Time.frameCount;
		}

		// Token: 0x06009B42 RID: 39746 RVA: 0x0031F0E4 File Offset: 0x0031D2E4
		public static int LayerArrayToLayerMask(FsmInt[] layers, bool invert)
		{
			int num = 0;
			foreach (FsmInt fsmInt in layers)
			{
				num |= 1 << fsmInt.Value;
			}
			if (invert)
			{
				num = ~num;
			}
			if (num != 0)
			{
				return num;
			}
			return -5;
		}

		// Token: 0x06009B43 RID: 39747 RVA: 0x0031F122 File Offset: 0x0031D322
		public static bool IsLoopingWrapMode(WrapMode wrapMode)
		{
			return wrapMode == WrapMode.Loop || wrapMode == WrapMode.PingPong;
		}

		// Token: 0x06009B44 RID: 39748 RVA: 0x0031F12E File Offset: 0x0031D32E
		public static string CheckRayDistance(float rayDistance)
		{
			if (rayDistance > 0f)
			{
				return "";
			}
			return "Ray Distance should be greater than zero!\n";
		}

		// Token: 0x06009B45 RID: 39749 RVA: 0x0031F144 File Offset: 0x0031D344
		public static string CheckForValidEvent(FsmState state, string eventName)
		{
			if (state == null)
			{
				return "Invalid State!";
			}
			if (string.IsNullOrEmpty(eventName))
			{
				return "";
			}
			FsmTransition[] array = state.Fsm.GlobalTransitions;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].EventName == eventName)
				{
					return "";
				}
			}
			array = state.Transitions;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].EventName == eventName)
				{
					return "";
				}
			}
			return "Fsm will not respond to Event: " + eventName;
		}

		// Token: 0x06009B46 RID: 39750 RVA: 0x0031F1CE File Offset: 0x0031D3CE
		public static string CheckPhysicsSetup(FsmOwnerDefault ownerDefault)
		{
			if (ownerDefault == null)
			{
				return "";
			}
			return ActionHelpers.CheckPhysicsSetup(ownerDefault.GameObject.Value);
		}

		// Token: 0x06009B47 RID: 39751 RVA: 0x0031F1E9 File Offset: 0x0031D3E9
		public static string CheckOwnerPhysicsSetup(GameObject gameObject)
		{
			return ActionHelpers.CheckPhysicsSetup(gameObject);
		}

		// Token: 0x06009B48 RID: 39752 RVA: 0x0031F1F4 File Offset: 0x0031D3F4
		public static string CheckPhysicsSetup(GameObject gameObject)
		{
			string text = string.Empty;
			if (gameObject != null && gameObject.GetComponent<Collider>() == null && gameObject.GetComponent<Rigidbody>() == null)
			{
				text += "GameObject requires RigidBody/Collider!\n";
			}
			return text;
		}

		// Token: 0x06009B49 RID: 39753 RVA: 0x0031F239 File Offset: 0x0031D439
		public static string CheckPhysics2dSetup(FsmOwnerDefault ownerDefault)
		{
			if (ownerDefault == null)
			{
				return "";
			}
			return ActionHelpers.CheckPhysics2dSetup(ownerDefault.GameObject.Value);
		}

		// Token: 0x06009B4A RID: 39754 RVA: 0x0031F254 File Offset: 0x0031D454
		public static string CheckOwnerPhysics2dSetup(GameObject gameObject)
		{
			return ActionHelpers.CheckPhysics2dSetup(gameObject);
		}

		// Token: 0x06009B4B RID: 39755 RVA: 0x0031F25C File Offset: 0x0031D45C
		public static string CheckPhysics2dSetup(GameObject gameObject)
		{
			string text = string.Empty;
			if (gameObject != null && gameObject.GetComponent<Collider2D>() == null && gameObject.GetComponent<Rigidbody2D>() == null)
			{
				text += "GameObject requires a RigidBody2D or Collider2D component!\n";
			}
			return text;
		}

		// Token: 0x06009B4C RID: 39756 RVA: 0x0031F2A4 File Offset: 0x0031D4A4
		public static void DebugLog(Fsm fsm, LogLevel logLevel, string text, bool sendToUnityLog = false)
		{
			if (!Application.isEditor && sendToUnityLog)
			{
				string message = ActionHelpers.FormatUnityLogString(text);
				if (logLevel != LogLevel.Warning)
				{
					if (logLevel != LogLevel.Error)
					{
						Debug.Log(message);
					}
					else
					{
						Debug.LogError(message);
					}
				}
				else
				{
					Debug.LogWarning(message);
				}
			}
			if (!FsmLog.LoggingEnabled || fsm == null)
			{
				return;
			}
			switch (logLevel)
			{
			case LogLevel.Info:
				fsm.MyLog.LogAction(FsmLogType.Info, text, sendToUnityLog);
				return;
			case LogLevel.Warning:
				fsm.MyLog.LogAction(FsmLogType.Warning, text, sendToUnityLog);
				return;
			case LogLevel.Error:
				fsm.MyLog.LogAction(FsmLogType.Error, text, sendToUnityLog);
				return;
			default:
				return;
			}
		}

		// Token: 0x06009B4D RID: 39757 RVA: 0x0031F32E File Offset: 0x0031D52E
		public static void LogError(string text)
		{
			ActionHelpers.DebugLog(FsmExecutionStack.ExecutingFsm, LogLevel.Error, text, true);
		}

		// Token: 0x06009B4E RID: 39758 RVA: 0x0031F33D File Offset: 0x0031D53D
		public static void LogWarning(string text)
		{
			ActionHelpers.DebugLog(FsmExecutionStack.ExecutingFsm, LogLevel.Warning, text, true);
		}

		// Token: 0x06009B4F RID: 39759 RVA: 0x0031F34C File Offset: 0x0031D54C
		public static string FormatUnityLogString(string text)
		{
			if (FsmExecutionStack.ExecutingFsm == null)
			{
				return text;
			}
			string str = Fsm.GetFullFsmLabel(FsmExecutionStack.ExecutingFsm);
			if (FsmExecutionStack.ExecutingState != null)
			{
				str = str + " : " + FsmExecutionStack.ExecutingStateName;
			}
			if (FsmExecutionStack.ExecutingAction != null)
			{
				str += FsmExecutionStack.ExecutingAction.Name;
			}
			return str + " : " + text;
		}

		// Token: 0x06009B50 RID: 39760 RVA: 0x000D5239 File Offset: 0x000D3439
		public static string GetValueLabel(INamedVariable variable)
		{
			return "";
		}

		// Token: 0x06009B51 RID: 39761 RVA: 0x0031F3AB File Offset: 0x0031D5AB
		public static string GetValueLabel(Fsm fsm, FsmOwnerDefault ownerDefault)
		{
			if (ownerDefault == null)
			{
				return "[null]";
			}
			if (ownerDefault.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				return "Owner";
			}
			return ActionHelpers.GetValueLabel(ownerDefault.GameObject);
		}

		// Token: 0x06009B52 RID: 39762 RVA: 0x0031F3CF File Offset: 0x0031D5CF
		public static string AutoName(FsmStateAction action, params INamedVariable[] exposedFields)
		{
			if (action != null)
			{
				return ActionHelpers.AutoName(action.GetType().Name, exposedFields);
			}
			return null;
		}

		// Token: 0x06009B53 RID: 39763 RVA: 0x0031F3E8 File Offset: 0x0031D5E8
		public static string AutoName(string actionName, params INamedVariable[] exposedFields)
		{
			string text = actionName + " :";
			foreach (INamedVariable variable in exposedFields)
			{
				text = text + " " + ActionHelpers.GetValueLabel(variable);
			}
			return text;
		}

		// Token: 0x06009B54 RID: 39764 RVA: 0x0031F428 File Offset: 0x0031D628
		public static string AutoNameRange(FsmStateAction action, NamedVariable min, NamedVariable max)
		{
			if (action != null)
			{
				return ActionHelpers.AutoNameRange(action.GetType().Name, min, max);
			}
			return null;
		}

		// Token: 0x06009B55 RID: 39765 RVA: 0x0031F441 File Offset: 0x0031D641
		public static string AutoNameRange(string actionName, NamedVariable min, NamedVariable max)
		{
			return string.Concat(new string[]
			{
				actionName,
				" : ",
				ActionHelpers.GetValueLabel(min),
				" - ",
				ActionHelpers.GetValueLabel(max)
			});
		}

		// Token: 0x06009B56 RID: 39766 RVA: 0x0031F474 File Offset: 0x0031D674
		public static string AutoNameSetVar(FsmStateAction action, NamedVariable var, NamedVariable value)
		{
			if (action != null)
			{
				return ActionHelpers.AutoNameSetVar(action.GetType().Name, var, value);
			}
			return null;
		}

		// Token: 0x06009B57 RID: 39767 RVA: 0x0031F48D File Offset: 0x0031D68D
		public static string AutoNameSetVar(string actionName, NamedVariable var, NamedVariable value)
		{
			return string.Concat(new string[]
			{
				actionName,
				" : ",
				ActionHelpers.GetValueLabel(var),
				" = ",
				ActionHelpers.GetValueLabel(value)
			});
		}

		// Token: 0x06009B58 RID: 39768 RVA: 0x0031F4C0 File Offset: 0x0031D6C0
		public static string AutoNameConvert(FsmStateAction action, NamedVariable fromVariable, NamedVariable toVariable)
		{
			if (action != null)
			{
				return ActionHelpers.AutoNameConvert(action.GetType().Name, fromVariable, toVariable);
			}
			return null;
		}

		// Token: 0x06009B59 RID: 39769 RVA: 0x0031F4DC File Offset: 0x0031D6DC
		public static string AutoNameConvert(string actionName, NamedVariable fromVariable, NamedVariable toVariable)
		{
			return string.Concat(new string[]
			{
				actionName.Replace("Convert", ""),
				" : ",
				ActionHelpers.GetValueLabel(fromVariable),
				" to ",
				ActionHelpers.GetValueLabel(toVariable)
			});
		}

		// Token: 0x06009B5A RID: 39770 RVA: 0x0031F529 File Offset: 0x0031D729
		public static string AutoNameGetProperty(FsmStateAction action, NamedVariable property, NamedVariable store)
		{
			if (action != null)
			{
				return ActionHelpers.AutoNameGetProperty(action.GetType().Name, property, store);
			}
			return null;
		}

		// Token: 0x06009B5B RID: 39771 RVA: 0x0031F542 File Offset: 0x0031D742
		public static string AutoNameGetProperty(string actionName, NamedVariable property, NamedVariable store)
		{
			return string.Concat(new string[]
			{
				actionName,
				" : ",
				ActionHelpers.GetValueLabel(property),
				" -> ",
				ActionHelpers.GetValueLabel(store)
			});
		}

		// Token: 0x06009B5C RID: 39772 RVA: 0x0031F575 File Offset: 0x0031D775
		[Obsolete("Use LogError instead.")]
		public static void RuntimeError(FsmStateAction action, string error)
		{
			action.LogError(action + " : " + error);
		}

		// Token: 0x040080CE RID: 32974
		public static RaycastHit mousePickInfo;

		// Token: 0x040080CF RID: 32975
		private static float mousePickRaycastTime;

		// Token: 0x040080D0 RID: 32976
		private static float mousePickDistanceUsed;

		// Token: 0x040080D1 RID: 32977
		private static int mousePickLayerMaskUsed;
	}
}
