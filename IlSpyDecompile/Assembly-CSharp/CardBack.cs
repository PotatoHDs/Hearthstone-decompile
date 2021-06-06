using UnityEngine;

public class CardBack : MonoBehaviour
{
	public enum cardBackHelpers
	{
		None,
		CardBackHelperBubbleLevel,
		CardBackHelperFlipbook
	}

	public Mesh m_CardBackMesh;

	public Material m_CardBackMaterial;

	public Material m_CardBackMaterial1;

	public Texture2D m_CardBackTexture;

	public Texture2D m_HiddenCardEchoTexture;

	public GameObject m_DragEffect;

	public float m_EffectMinVelocity = 2f;

	public float m_EffectMaxVelocity = 40f;

	public cardBackHelpers cardBackHelper;
}
