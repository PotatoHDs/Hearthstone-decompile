using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200075B RID: 1883
public class LightningCtrl : MonoBehaviour
{
	// Token: 0x060069D1 RID: 27089 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x060069D2 RID: 27090 RVA: 0x00228173 File Offset: 0x00226373
	private void Update()
	{
		if (UniversalInputManager.Get().GetMouseButtonDown(0))
		{
			this.Spawn(this.target.transform, this.destination.transform);
		}
	}

	// Token: 0x060069D3 RID: 27091 RVA: 0x002281A0 File Offset: 0x002263A0
	public void Spawn(Transform targetTransform, Transform destinationTransform)
	{
		this.lightningObj = UnityEngine.Object.Instantiate<GameObject>(this.mylightning, new Vector3(this.position_X, this.position_Y, this.position_Z), new Quaternion(0f, 0f, 0f, 0f));
		this.lightningObj.transform.localScale = new Vector3(this.scale, this.scale, this.scale);
		ElectroScript component = this.lightningObj.GetComponent<ElectroScript>();
		component.timers.timeToPowerUp = this.speed;
		component.prefabs.target.position = targetTransform.position;
		component.prefabs.destination.position = destinationTransform.position;
		base.StartCoroutine(this.DestroyLightning());
	}

	// Token: 0x060069D4 RID: 27092 RVA: 0x00228269 File Offset: 0x00226469
	private IEnumerator DestroyLightning()
	{
		yield return new WaitForSeconds(this.lifetime);
		UnityEngine.Object.Destroy(this.lightningObj);
		yield break;
	}

	// Token: 0x04005697 RID: 22167
	public GameObject mylightning;

	// Token: 0x04005698 RID: 22168
	private GameObject lightningObj;

	// Token: 0x04005699 RID: 22169
	public float lifetime = 1f;

	// Token: 0x0400569A RID: 22170
	public float position_X;

	// Token: 0x0400569B RID: 22171
	public float position_Y;

	// Token: 0x0400569C RID: 22172
	public float position_Z;

	// Token: 0x0400569D RID: 22173
	public float scale = 0.1f;

	// Token: 0x0400569E RID: 22174
	public float speed = 1f;

	// Token: 0x0400569F RID: 22175
	public GameObject target;

	// Token: 0x040056A0 RID: 22176
	public GameObject destination;
}
