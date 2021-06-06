using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001004 RID: 4100
	[ExecuteAlways]
	[AddComponentMenu("")]
	public class MeshGeometry : Geometry
	{
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600B255 RID: 45653 RVA: 0x0036F351 File Offset: 0x0036D551
		protected override WeakAssetReference[] ModelReferences
		{
			get
			{
				return new WeakAssetReference[]
				{
					this.m_model
				};
			}
		}

		// Token: 0x0600B256 RID: 45654 RVA: 0x0036F368 File Offset: 0x0036D568
		protected override void OnInstancesReady(PrefabInstance[] instances)
		{
			base.OnInstancesReady(instances);
			foreach (PrefabInstance prefabInstance in instances)
			{
				if (prefabInstance.Instance != null)
				{
					prefabInstance.Instance.transform.localRotation = Quaternion.Euler(this.m_rotation);
					prefabInstance.Instance.transform.localPosition = Vector3.zero;
					prefabInstance.Instance.transform.localScale = Vector3.one;
				}
			}
		}

		// Token: 0x04009606 RID: 38406
		[CustomEditField(T = EditType.GAME_OBJECT)]
		[SerializeField]
		private WeakAssetReference m_model;

		// Token: 0x04009607 RID: 38407
		[SerializeField]
		private Vector3 m_rotation;
	}
}
