using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
[AddComponentMenu("Hearthstone/Gizmos/Transform Locator")]
public class TransformLocator : MonoBehaviour
{
	// Token: 0x06000090 RID: 144 RVA: 0x000039AC File Offset: 0x00001BAC
	private void OnDrawGizmos()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		Gizmos.color = Color.red;
		Gizmos.DrawLine(Vector3.zero, Vector3.right * this.scale);
		Gizmos.color = Color.red * 0.5f;
		Gizmos.DrawLine(Vector3.zero, Vector3.left * this.scale);
		Gizmos.color = Color.green;
		Gizmos.DrawLine(Vector3.zero, Vector3.up * this.scale);
		Gizmos.color = Color.green * 0.5f;
		Gizmos.DrawLine(Vector3.zero, Vector3.down * this.scale);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(Vector3.zero, Vector3.forward * this.scale);
		Gizmos.color = Color.blue * 0.5f;
		Gizmos.DrawLine(Vector3.zero, Vector3.back * this.scale);
	}

	// Token: 0x0400005F RID: 95
	public float scale = 1f;
}
