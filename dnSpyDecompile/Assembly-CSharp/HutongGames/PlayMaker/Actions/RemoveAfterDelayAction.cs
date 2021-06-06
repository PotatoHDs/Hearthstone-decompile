using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000F68 RID: 3944
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Remove a list of GameObjects after a delay")]
	public class RemoveAfterDelayAction : FsmStateAction
	{
		// Token: 0x0600AD28 RID: 44328 RVA: 0x00360258 File Offset: 0x0035E458
		public override void OnEnter()
		{
			if (this.gameObjectList.Length == 0)
			{
				return;
			}
			GameObject gameObject = new GameObject();
			this.coroutineParent = gameObject.AddComponent<EmptyScript>();
			this.coroutineParent.StartCoroutine(this.WaitAndRemove());
		}

		// Token: 0x0600AD29 RID: 44329 RVA: 0x00360297 File Offset: 0x0035E497
		private IEnumerator WaitAndRemove()
		{
			yield return new WaitForSecondsRealtime(this.waitTime.Value);
			object[] values = this.gameObjectList.Values;
			for (int i = 0; i < values.Length; i++)
			{
				UnityEngine.Object.Destroy((GameObject)values[i]);
			}
			UnityEngine.Object.Destroy(this.coroutineParent.gameObject);
			yield break;
		}

		// Token: 0x0400940F RID: 37903
		[Tooltip("GameObjects to remove")]
		[UIHint(UIHint.Variable)]
		[ArrayEditor(VariableType.GameObject, "", 0, 0, 65536)]
		public FsmArray gameObjectList;

		// Token: 0x04009410 RID: 37904
		[Tooltip("Wait time before removal")]
		[RequiredField]
		public FsmFloat waitTime;

		// Token: 0x04009411 RID: 37905
		private MonoBehaviour coroutineParent;
	}
}
