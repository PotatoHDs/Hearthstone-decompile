using System;
using Blizzard.T5.AssetManager;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x02001006 RID: 4102
	[ExecuteAlways]
	[RequireComponent(typeof(WidgetTransform))]
	[AddComponentMenu("")]
	public class ThreeSliceGeometry : Geometry, IBoundsDependent
	{
		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x0600B27E RID: 45694 RVA: 0x0036FE06 File Offset: 0x0036E006
		protected override WeakAssetReference[] ModelReferences
		{
			get
			{
				return new WeakAssetReference[]
				{
					this.m_left,
					this.m_middle,
					this.m_right
				};
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x0600B27F RID: 45695 RVA: 0x000052EC File Offset: 0x000034EC
		public bool NeedsBounds
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B280 RID: 45696 RVA: 0x0036FE35 File Offset: 0x0036E035
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this.m_widgetTransform = base.GetComponent<WidgetTransform>();
			this.UpdateSlices();
		}

		// Token: 0x0600B281 RID: 45697 RVA: 0x0036FE4F File Offset: 0x0036E04F
		protected override void OnDestroy()
		{
			AssetHandle.SafeDispose<GameObject>(ref this.m_leftPrefab);
			AssetHandle.SafeDispose<GameObject>(ref this.m_middlePrefab);
			AssetHandle.SafeDispose<GameObject>(ref this.m_rightPrefab);
			base.OnDestroy();
		}

		// Token: 0x0600B282 RID: 45698 RVA: 0x0036FE78 File Offset: 0x0036E078
		protected void Update()
		{
			this.UpdateSlices();
		}

		// Token: 0x0600B283 RID: 45699 RVA: 0x0036FE80 File Offset: 0x0036E080
		protected override void OnInstancesReady(PrefabInstance[] instances)
		{
			base.OnInstancesReady(instances);
			this.m_leftInstance = instances[0].Instance;
			this.m_middleInstance = instances[1].Instance;
			this.m_rightInstance = instances[2].Instance;
			AssetHandle.Set<GameObject>(ref this.m_leftPrefab, instances[0].Prefab);
			AssetHandle.Set<GameObject>(ref this.m_middlePrefab, instances[1].Prefab);
			AssetHandle.Set<GameObject>(ref this.m_rightPrefab, instances[2].Prefab);
			this.CalculateRects();
		}

		// Token: 0x0600B284 RID: 45700 RVA: 0x0036FF00 File Offset: 0x0036E100
		private void CalculateRects()
		{
			this.m_leftRect = this.GetRectForInstance(this.m_leftPrefab);
			this.m_middleRect = this.GetRectForInstance(this.m_middlePrefab);
			this.m_rightRect = this.GetRectForInstance(this.m_rightPrefab);
		}

		// Token: 0x0600B285 RID: 45701 RVA: 0x0036FF54 File Offset: 0x0036E154
		private Rect GetRectForInstance(GameObject go)
		{
			if (go == null)
			{
				return Rect.zero;
			}
			Bounds bounds = default(Bounds);
			Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (i == 0)
				{
					bounds = componentsInChildren[i].bounds;
				}
				else
				{
					bounds.Encapsulate(componentsInChildren[i].bounds);
				}
			}
			Rect result = new Rect(bounds.center, new Vector3(bounds.size.x, bounds.size.z, 0f));
			result.min = go.transform.worldToLocalMatrix.MultiplyPoint(result.min);
			result.max = go.transform.worldToLocalMatrix.MultiplyPoint(result.max);
			return result;
		}

		// Token: 0x0600B286 RID: 45702 RVA: 0x00370040 File Offset: 0x0036E240
		private void UpdateSlices()
		{
			if (this.m_leftInstance == null || this.m_rightInstance == null || this.m_middleInstance == null)
			{
				return;
			}
			Vector3 localPosition = this.m_leftInstance.transform.localPosition;
			localPosition.x = this.m_widgetTransform.Rect.xMin - this.m_leftRect.xMin + this.m_leftRect.size.x * 0.5f;
			localPosition.z = this.m_widgetTransform.Rect.center.y;
			this.m_leftInstance.transform.localPosition = localPosition;
			Vector3 localPosition2 = this.m_middleInstance.transform.localPosition;
			Vector3 localScale = this.m_middleInstance.transform.localScale;
			float num = this.m_widgetTransform.Rect.xMin + this.m_leftRect.size.x;
			float num2 = this.m_widgetTransform.Rect.xMax - this.m_rightRect.size.x;
			float width = this.m_middleRect.width;
			float num3 = num2 - num - width;
			float num4 = width + num3;
			localScale.x = num4 / width;
			localPosition2.x = this.m_widgetTransform.Rect.center.x - this.m_middleRect.x * localScale.x;
			localPosition2.z = this.m_widgetTransform.Rect.center.y;
			this.m_middleInstance.transform.localPosition = localPosition2;
			this.m_middleInstance.transform.localScale = localScale;
			Vector3 localPosition3 = this.m_rightInstance.transform.localPosition;
			localPosition3.x = this.m_widgetTransform.Rect.xMax - this.m_rightRect.xMin - this.m_rightRect.size.x * 0.5f;
			localPosition3.z = this.m_widgetTransform.Rect.center.y;
			this.m_rightInstance.transform.localPosition = localPosition3;
		}

		// Token: 0x0400961F RID: 38431
		[SerializeField]
		private WeakAssetReference m_left;

		// Token: 0x04009620 RID: 38432
		[SerializeField]
		private WeakAssetReference m_middle;

		// Token: 0x04009621 RID: 38433
		[SerializeField]
		private WeakAssetReference m_right;

		// Token: 0x04009622 RID: 38434
		private WidgetTransform m_widgetTransform;

		// Token: 0x04009623 RID: 38435
		private Rect m_leftRect;

		// Token: 0x04009624 RID: 38436
		private Rect m_middleRect;

		// Token: 0x04009625 RID: 38437
		private Rect m_rightRect;

		// Token: 0x04009626 RID: 38438
		private GameObject m_leftInstance;

		// Token: 0x04009627 RID: 38439
		private GameObject m_middleInstance;

		// Token: 0x04009628 RID: 38440
		private GameObject m_rightInstance;

		// Token: 0x04009629 RID: 38441
		private AssetHandle<GameObject> m_leftPrefab;

		// Token: 0x0400962A RID: 38442
		private AssetHandle<GameObject> m_middlePrefab;

		// Token: 0x0400962B RID: 38443
		private AssetHandle<GameObject> m_rightPrefab;
	}
}
