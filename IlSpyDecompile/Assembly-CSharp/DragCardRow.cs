using UnityEngine;

public class DragCardRow : MonoBehaviour
{
	private float m_CursorX;

	private Rect cameraRect;

	private void Start()
	{
		Camera main = Camera.main;
		Vector3 vector = main.ScreenToWorldPoint(Vector3.zero);
		Vector3 vector2 = main.ScreenToWorldPoint(new Vector3(main.pixelWidth, main.pixelHeight));
		cameraRect = new Rect(vector.x, vector.y, vector2.x - vector.x, vector2.y - vector.y);
	}

	private void OnMouseDown()
	{
		m_CursorX = InputCollection.GetMousePosition().x;
	}

	private void OnMouseDrag()
	{
		float x = InputCollection.GetMousePosition().x;
		float num = x - m_CursorX;
		num *= 0.01f;
		base.transform.Translate(num, 0f, 0f);
		base.transform.position = new Vector3(Mathf.Clamp(base.transform.position.x, cameraRect.xMin, cameraRect.xMax), base.transform.position.y, base.transform.position.z);
		m_CursorX = x;
	}
}
