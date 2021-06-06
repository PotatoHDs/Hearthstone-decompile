using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class UberShaderController : MonoBehaviour
{
	private const int GUI_PROPERTY_LABEL_WIDTH = 130;

	[SerializeField]
	private UberShaderAnimation m_UberShaderAnimation;

	public int m_MaterialIndex = -1;

	private bool m_firstFrame;

	private float m_time;

	private float m_deltaTime;

	private Renderer m_renderer;

	private float m_lastTime;

	private float m_randomOffset;

	private string m_copyBuffer;

	private UberShaderAnimation.PropertyType m_copyBufferType;

	private string m_copyBufferLayer;

	private int m_copyBufferLayerCount;

	private DateTime? m_lastSaveTime;

	private float m_maxTime = 65535f;

	private static bool s_autoSave = false;

	private static float s_autoSaveInterval = 30f;

	public UberShaderAnimation UberShaderAnimation
	{
		get
		{
			return m_UberShaderAnimation;
		}
		set
		{
			m_UberShaderAnimation = value;
			UpdateShaderIDs();
		}
	}

	public DateTime? LastSaveTime => m_lastSaveTime;

	public static bool GetAutoSaveEnabled()
	{
		return s_autoSave;
	}

	public static float GetAutoSaveInterval()
	{
		return s_autoSaveInterval;
	}

	private void Awake()
	{
		if (m_UberShaderAnimation == null)
		{
			m_UberShaderAnimation = ScriptableObject.CreateInstance<UberShaderAnimation>();
		}
		m_firstFrame = true;
		m_randomOffset = UnityEngine.Random.Range(0f, 10f);
		m_time += m_randomOffset;
		m_renderer = GetComponent<Renderer>();
	}

	private void OnEnable()
	{
		LoadUberShaderAnimation();
	}

	private void Update()
	{
		UpdateAnimation();
	}

	[ContextMenu("Reload Animation File")]
	private void LoadUberShaderAnimation()
	{
		m_firstFrame = true;
		if (m_UberShaderAnimation == null)
		{
			m_UberShaderAnimation = ScriptableObject.CreateInstance<UberShaderAnimation>();
		}
		UpdateShaderIDs();
	}

	private void UpdateTime()
	{
		m_deltaTime = Time.deltaTime;
		m_time += m_deltaTime;
		if (m_time > m_maxTime)
		{
			m_time = 0.0001f;
		}
	}

	private void UpdateEditorTime()
	{
		float num = Time.realtimeSinceStartup + m_randomOffset;
		m_deltaTime = num - m_lastTime;
		m_lastTime = num;
		m_time += m_deltaTime;
		if (m_time > m_maxTime)
		{
			m_time = 0.0001f;
		}
	}

	private void UpdateAnimation()
	{
		UpdateTime();
		if (m_renderer == null)
		{
			return;
		}
		List<Material> sharedMaterials = m_renderer.GetSharedMaterials();
		if (sharedMaterials == null || sharedMaterials.Count < 1 || (object)m_UberShaderAnimation == null || m_UberShaderAnimation.animations == null)
		{
			return;
		}
		for (int i = 0; i < m_UberShaderAnimation.animations.Count; i++)
		{
			UberShaderAnimation.UberAnimation uberAnimation = m_UberShaderAnimation.animations[i];
			int nameID = m_UberShaderAnimation.materialPropertyIDs[i];
			int materialIndex = uberAnimation.materialIndex;
			if (m_MaterialIndex > -1 && m_MaterialIndex < sharedMaterials.Count)
			{
				materialIndex = m_MaterialIndex;
			}
			Material material = sharedMaterials[materialIndex];
			if (material == null)
			{
				continue;
			}
			if (uberAnimation.propertyType == UberShaderAnimation.PropertyType.Color)
			{
				UberShaderAnimation.UberAnimationElement uberAnimationElement = uberAnimation.animationElement[0];
				if (uberAnimationElement == null)
				{
					continue;
				}
				UberShaderAnimation.UberAnimationColor colorAnimation = uberAnimationElement.colorAnimation;
				if (colorAnimation != null && !colorAnimation.enabled)
				{
					continue;
				}
			}
			if (!material.HasProperty(nameID))
			{
				continue;
			}
			Vector4 vector = Vector4.zero;
			if (uberAnimation.propertyType == UberShaderAnimation.PropertyType.Vector)
			{
				vector = material.GetVector(nameID);
			}
			else if (uberAnimation.propertyType == UberShaderAnimation.PropertyType.Float)
			{
				vector[0] = material.GetFloat(nameID);
			}
			Vector4 value = vector;
			for (int j = 0; j < uberAnimation.animationElement.Count; j++)
			{
				UberShaderAnimation.UberAnimationElement uberAnimationElement2 = uberAnimation.animationElement[j];
				UberShaderAnimation.UberAnimationCurve animationCurve = uberAnimationElement2.animationCurve;
				UberShaderAnimation.UberAnimationRandom randomAnimation = uberAnimationElement2.randomAnimation;
				int element = uberAnimationElement2.element;
				float num = 0f;
				if (!uberAnimationElement2.incrementingValue)
				{
					switch (element)
					{
					case 0:
						num = vector.x;
						break;
					case 1:
						num = vector.y;
						break;
					case 2:
						num = vector.z;
						break;
					case 3:
						num = vector.w;
						break;
					}
				}
				if (animationCurve.animationCurve != null && animationCurve.enabled)
				{
					num = (animationCurve.animationCurve.Evaluate(m_time * animationCurve.speed) + animationCurve.offset) * animationCurve.scale;
				}
				if (randomAnimation != null && randomAnimation.enabled)
				{
					if (animationCurve.animationCurve == null || !animationCurve.enabled)
					{
						num = 0f;
					}
					float num2 = 1f;
					if (randomAnimation.intensityCurve != null)
					{
						num2 = randomAnimation.intensityCurve.Evaluate(m_time * randomAnimation.intensitySpeed);
					}
					num += Mathf.Lerp(randomAnimation.minValue, randomAnimation.maxValue, (UberMath.SimplexNoise(m_time * randomAnimation.speed + randomAnimation.seed, 0.5f) + 1f) * 0.5f * num2) * randomAnimation.scale;
				}
				if (uberAnimationElement2.incrementingValue)
				{
					if (m_firstFrame)
					{
						uberAnimationElement2.incrementingLastValue = 0f;
					}
					if (uberAnimationElement2.incrementingLastValue > m_maxTime)
					{
						uberAnimationElement2.incrementingLastValue = 0.0001f;
					}
					float num3 = num + uberAnimationElement2.incrementingSpeed;
					float num4 = m_deltaTime * num3;
					num = (uberAnimationElement2.incrementingLastValue += num4);
				}
				switch (element)
				{
				case 0:
					value.x = num;
					break;
				case 1:
					value.y = num;
					break;
				case 2:
					value.z = num;
					break;
				case 3:
					value.w = num;
					break;
				}
			}
			if (uberAnimation.propertyType == UberShaderAnimation.PropertyType.Color)
			{
				Color value2 = uberAnimation.animationElement[0].colorAnimation.gradient.Evaluate(value.x);
				material.SetColor(nameID, value2);
			}
			else
			{
				material.SetVector(nameID, value);
			}
		}
		m_firstFrame = false;
	}

	private void UpdateShaderIDs()
	{
		if (m_renderer == null)
		{
			return;
		}
		List<Material> sharedMaterials = m_renderer.GetSharedMaterials();
		if (sharedMaterials != null && sharedMaterials.Count >= 1 && (object)m_UberShaderAnimation != null && m_UberShaderAnimation.animations != null)
		{
			m_UberShaderAnimation.materialPropertyIDs = new List<int>(m_UberShaderAnimation.animations.Count);
			for (int i = 0; i < m_UberShaderAnimation.animations.Count; i++)
			{
				int item = Shader.PropertyToID(m_UberShaderAnimation.animations[i].materialPropertyName);
				UberShaderAnimation.materialPropertyIDs.Add(item);
			}
		}
	}
}
