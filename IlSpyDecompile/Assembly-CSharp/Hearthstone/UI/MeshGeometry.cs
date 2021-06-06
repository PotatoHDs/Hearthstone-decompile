using UnityEngine;

namespace Hearthstone.UI
{
	[ExecuteAlways]
	[AddComponentMenu("")]
	public class MeshGeometry : Geometry
	{
		[CustomEditField(T = EditType.GAME_OBJECT)]
		[SerializeField]
		private WeakAssetReference m_model;

		[SerializeField]
		private Vector3 m_rotation;

		protected override WeakAssetReference[] ModelReferences => new WeakAssetReference[1] { m_model };

		protected override void OnInstancesReady(PrefabInstance[] instances)
		{
			base.OnInstancesReady(instances);
			foreach (PrefabInstance prefabInstance in instances)
			{
				if (prefabInstance.Instance != null)
				{
					prefabInstance.Instance.transform.localRotation = Quaternion.Euler(m_rotation);
					prefabInstance.Instance.transform.localPosition = Vector3.zero;
					prefabInstance.Instance.transform.localScale = Vector3.one;
				}
			}
		}
	}
}
