using UnityEngine;

public class CardBackHelperBubbleLevel : MonoBehaviour
{
	private Material m_material2;

	private float m_bubbleLiteralPosition;

	private float m_objectTilt;

	private float m_tiltRangeMin = -2f;

	private float m_tiltRangeMax = 2f;

	private float m_bubbleMomentum;

	private float m_bubblePosition;

	private float m_bubbleDistanceFromLiteral;

	public int TargetMaterialIndex;

	public float TiltSensitivity = 5f;

	public float BubbleMomentumBase = 0.05f;

	public float BubbleOffsetY = -1.22f;

	public string TexturePropertyName = "_AddTex";

	public void Awake()
	{
		m_material2 = GetComponent<Renderer>().GetMaterial(TargetMaterialIndex);
	}

	private void Update()
	{
		float num = NormalizeRotation(base.gameObject.transform.eulerAngles.x);
		float num2 = NormalizeRotation(base.gameObject.transform.eulerAngles.y - 180f);
		float num3 = NormalizeRotation(base.gameObject.transform.eulerAngles.z);
		m_objectTilt = num + num2 + num3;
		m_objectTilt = Mathf.Clamp(m_objectTilt / TiltSensitivity, m_tiltRangeMin, m_tiltRangeMax);
		m_objectTilt = (m_objectTilt + 2f) * 0.5f;
		m_bubbleLiteralPosition = m_objectTilt * 0.5f;
		m_bubbleDistanceFromLiteral = (m_bubblePosition - m_bubbleLiteralPosition) * -1f;
		m_bubbleMomentum = m_bubbleDistanceFromLiteral * BubbleMomentumBase;
		m_bubblePosition = Mathf.Clamp(m_bubblePosition + m_bubbleMomentum, 0f, 1f);
		m_material2.SetTextureOffset(TexturePropertyName, new Vector2(m_bubblePosition * -2f, BubbleOffsetY));
	}

	private static float NormalizeRotation(float inputRotation)
	{
		if (inputRotation > 180f)
		{
			return inputRotation - 360f;
		}
		return inputRotation;
	}
}
