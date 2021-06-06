using System;
using UnityEngine;

// Token: 0x02000B3F RID: 2879
public class DragCardRow : MonoBehaviour
{
	// Token: 0x06009896 RID: 39062 RVA: 0x003164E8 File Offset: 0x003146E8
	private void Start()
	{
		Camera main = Camera.main;
		Vector3 vector = main.ScreenToWorldPoint(Vector3.zero);
		Vector3 vector2 = main.ScreenToWorldPoint(new Vector3((float)main.pixelWidth, (float)main.pixelHeight));
		this.cameraRect = new Rect(vector.x, vector.y, vector2.x - vector.x, vector2.y - vector.y);
	}

	// Token: 0x06009897 RID: 39063 RVA: 0x00316552 File Offset: 0x00314752
	private void OnMouseDown()
	{
		this.m_CursorX = InputCollection.GetMousePosition().x;
	}

	// Token: 0x06009898 RID: 39064 RVA: 0x00316564 File Offset: 0x00314764
	private void OnMouseDrag()
	{
		float x = InputCollection.GetMousePosition().x;
		float num = x - this.m_CursorX;
		num *= 0.01f;
		base.transform.Translate(num, 0f, 0f);
		base.transform.position = new Vector3(Mathf.Clamp(base.transform.position.x, this.cameraRect.xMin, this.cameraRect.xMax), base.transform.position.y, base.transform.position.z);
		this.m_CursorX = x;
	}

	// Token: 0x04007FA5 RID: 32677
	private float m_CursorX;

	// Token: 0x04007FA6 RID: 32678
	private Rect cameraRect;
}
