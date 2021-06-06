using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CustomEditClass]
public class LightningAnimator : MonoBehaviour
{
	public bool m_StartOnEnable;

	public bool m_SetAlphaToZeroOnStart = true;

	public float m_StartDelayMin;

	public float m_StartDelayMax;

	public string m_MatFrameProperty = "_Frame";

	public float m_FrameTime = 0.01f;

	public List<int> m_FrameList;

	public Transform m_SourceJount;

	public Vector3 m_SourceMinRotation = new Vector3(0f, -10f, 0f);

	public Vector3 m_SourceMaxRotation = new Vector3(0f, 10f, 0f);

	public Transform m_TargetJoint;

	public Vector3 m_TargetMinRotation = new Vector3(0f, -20f, 0f);

	public Vector3 m_TargetMaxRotation = new Vector3(0f, 20f, 0f);

	private Material m_material;

	private float m_matGlowIntensity;

	private void Start()
	{
		m_material = GetComponent<Renderer>().GetMaterial();
		if (m_material == null)
		{
			base.enabled = false;
		}
		if (m_SetAlphaToZeroOnStart)
		{
			Color color = m_material.color;
			color.a = 0f;
			m_material.color = color;
		}
		if (m_material.HasProperty("_GlowIntensity"))
		{
			m_matGlowIntensity = m_material.GetFloat("_GlowIntensity");
		}
	}

	private void OnEnable()
	{
		if (m_StartOnEnable)
		{
			StartAnimation();
		}
	}

	public void StartAnimation()
	{
		StartCoroutine(AnimateMaterial());
	}

	private IEnumerator AnimateMaterial()
	{
		RandomJointRotation();
		Color matColor = m_material.color;
		matColor.a = 0f;
		m_material.color = matColor;
		yield return new WaitForSeconds(Random.Range(m_StartDelayMin, m_StartDelayMax));
		matColor = m_material.color;
		matColor.a = 1f;
		m_material.color = matColor;
		if (m_material.HasProperty("_GlowIntensity"))
		{
			m_material.SetFloat("_GlowIntensity", m_matGlowIntensity);
		}
		foreach (int frame in m_FrameList)
		{
			m_material.SetFloat(m_MatFrameProperty, frame);
			yield return new WaitForSeconds(m_FrameTime);
		}
		matColor.a = 0f;
		m_material.color = matColor;
		if (m_material.HasProperty("_GlowIntensity"))
		{
			m_material.SetFloat("_GlowIntensity", 0f);
		}
	}

	private void RandomJointRotation()
	{
		if (m_SourceJount != null)
		{
			Vector3 eulers = Vector3.Lerp(m_SourceMinRotation, m_SourceMaxRotation, Random.value);
			m_SourceJount.Rotate(eulers);
		}
		if (m_TargetJoint != null)
		{
			Vector3 eulers2 = Vector3.Lerp(m_TargetMinRotation, m_TargetMaxRotation, Random.value);
			m_TargetJoint.Rotate(eulers2);
		}
	}
}
