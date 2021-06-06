using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AC4 RID: 2756
[ExecuteAlways]
public class UberShaderController : MonoBehaviour
{
	// Token: 0x1700086D RID: 2157
	// (get) Token: 0x06009321 RID: 37665 RVA: 0x002FB504 File Offset: 0x002F9704
	// (set) Token: 0x06009322 RID: 37666 RVA: 0x002FB50C File Offset: 0x002F970C
	public UberShaderAnimation UberShaderAnimation
	{
		get
		{
			return this.m_UberShaderAnimation;
		}
		set
		{
			this.m_UberShaderAnimation = value;
			this.UpdateShaderIDs();
		}
	}

	// Token: 0x1700086E RID: 2158
	// (get) Token: 0x06009323 RID: 37667 RVA: 0x002FB51B File Offset: 0x002F971B
	public DateTime? LastSaveTime
	{
		get
		{
			return this.m_lastSaveTime;
		}
	}

	// Token: 0x06009324 RID: 37668 RVA: 0x002FB523 File Offset: 0x002F9723
	public static bool GetAutoSaveEnabled()
	{
		return UberShaderController.s_autoSave;
	}

	// Token: 0x06009325 RID: 37669 RVA: 0x002FB52A File Offset: 0x002F972A
	public static float GetAutoSaveInterval()
	{
		return UberShaderController.s_autoSaveInterval;
	}

	// Token: 0x06009326 RID: 37670 RVA: 0x002FB534 File Offset: 0x002F9734
	private void Awake()
	{
		if (this.m_UberShaderAnimation == null)
		{
			this.m_UberShaderAnimation = ScriptableObject.CreateInstance<UberShaderAnimation>();
		}
		this.m_firstFrame = true;
		this.m_randomOffset = UnityEngine.Random.Range(0f, 10f);
		this.m_time += this.m_randomOffset;
		this.m_renderer = base.GetComponent<Renderer>();
	}

	// Token: 0x06009327 RID: 37671 RVA: 0x002FB595 File Offset: 0x002F9795
	private void OnEnable()
	{
		this.LoadUberShaderAnimation();
	}

	// Token: 0x06009328 RID: 37672 RVA: 0x002FB59D File Offset: 0x002F979D
	private void Update()
	{
		this.UpdateAnimation();
	}

	// Token: 0x06009329 RID: 37673 RVA: 0x002FB5A5 File Offset: 0x002F97A5
	[ContextMenu("Reload Animation File")]
	private void LoadUberShaderAnimation()
	{
		this.m_firstFrame = true;
		if (this.m_UberShaderAnimation == null)
		{
			this.m_UberShaderAnimation = ScriptableObject.CreateInstance<UberShaderAnimation>();
		}
		this.UpdateShaderIDs();
	}

	// Token: 0x0600932A RID: 37674 RVA: 0x002FB5CD File Offset: 0x002F97CD
	private void UpdateTime()
	{
		this.m_deltaTime = Time.deltaTime;
		this.m_time += this.m_deltaTime;
		if (this.m_time > this.m_maxTime)
		{
			this.m_time = 0.0001f;
		}
	}

	// Token: 0x0600932B RID: 37675 RVA: 0x002FB608 File Offset: 0x002F9808
	private void UpdateEditorTime()
	{
		float num = Time.realtimeSinceStartup + this.m_randomOffset;
		this.m_deltaTime = num - this.m_lastTime;
		this.m_lastTime = num;
		this.m_time += this.m_deltaTime;
		if (this.m_time > this.m_maxTime)
		{
			this.m_time = 0.0001f;
		}
	}

	// Token: 0x0600932C RID: 37676 RVA: 0x002FB664 File Offset: 0x002F9864
	private void UpdateAnimation()
	{
		this.UpdateTime();
		if (this.m_renderer == null)
		{
			return;
		}
		List<Material> sharedMaterials = this.m_renderer.GetSharedMaterials();
		if (sharedMaterials == null || sharedMaterials.Count < 1)
		{
			return;
		}
		if (this.m_UberShaderAnimation == null || this.m_UberShaderAnimation.animations == null)
		{
			return;
		}
		for (int i = 0; i < this.m_UberShaderAnimation.animations.Count; i++)
		{
			UberShaderAnimation.UberAnimation uberAnimation = this.m_UberShaderAnimation.animations[i];
			int nameID = this.m_UberShaderAnimation.materialPropertyIDs[i];
			int materialIndex = uberAnimation.materialIndex;
			if (this.m_MaterialIndex > -1 && this.m_MaterialIndex < sharedMaterials.Count)
			{
				materialIndex = this.m_MaterialIndex;
			}
			Material material = sharedMaterials[materialIndex];
			if (!(material == null))
			{
				if (uberAnimation.propertyType == UberShaderAnimation.PropertyType.Color)
				{
					UberShaderAnimation.UberAnimationElement uberAnimationElement = uberAnimation.animationElement[0];
					if (uberAnimationElement == null)
					{
						goto IL_398;
					}
					UberShaderAnimation.UberAnimationColor colorAnimation = uberAnimationElement.colorAnimation;
					if (colorAnimation != null && !colorAnimation.enabled)
					{
						goto IL_398;
					}
				}
				if (material.HasProperty(nameID))
				{
					Vector4 vector = Vector4.zero;
					if (uberAnimation.propertyType == UberShaderAnimation.PropertyType.Vector)
					{
						vector = material.GetVector(nameID);
					}
					else if (uberAnimation.propertyType == UberShaderAnimation.PropertyType.Float)
					{
						vector[0] = material.GetFloat(nameID);
					}
					Vector4 vector2 = vector;
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
							num = (animationCurve.animationCurve.Evaluate(this.m_time * animationCurve.speed) + animationCurve.offset) * animationCurve.scale;
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
								num2 = randomAnimation.intensityCurve.Evaluate(this.m_time * randomAnimation.intensitySpeed);
							}
							num += Mathf.Lerp(randomAnimation.minValue, randomAnimation.maxValue, (UberMath.SimplexNoise(this.m_time * randomAnimation.speed + randomAnimation.seed, 0.5f) + 1f) * 0.5f * num2) * randomAnimation.scale;
						}
						if (uberAnimationElement2.incrementingValue)
						{
							if (this.m_firstFrame)
							{
								uberAnimationElement2.incrementingLastValue = 0f;
							}
							if (uberAnimationElement2.incrementingLastValue > this.m_maxTime)
							{
								uberAnimationElement2.incrementingLastValue = 0.0001f;
							}
							float num3 = num + uberAnimationElement2.incrementingSpeed;
							float num4 = this.m_deltaTime * num3;
							float num5 = uberAnimationElement2.incrementingLastValue + num4;
							num = num5;
							uberAnimationElement2.incrementingLastValue = num5;
						}
						switch (element)
						{
						case 0:
							vector2.x = num;
							break;
						case 1:
							vector2.y = num;
							break;
						case 2:
							vector2.z = num;
							break;
						case 3:
							vector2.w = num;
							break;
						}
					}
					if (uberAnimation.propertyType == UberShaderAnimation.PropertyType.Color)
					{
						Color value = uberAnimation.animationElement[0].colorAnimation.gradient.Evaluate(vector2.x);
						material.SetColor(nameID, value);
					}
					else
					{
						material.SetVector(nameID, vector2);
					}
				}
			}
			IL_398:;
		}
		this.m_firstFrame = false;
	}

	// Token: 0x0600932D RID: 37677 RVA: 0x002FBA2C File Offset: 0x002F9C2C
	private void UpdateShaderIDs()
	{
		if (this.m_renderer == null)
		{
			return;
		}
		List<Material> sharedMaterials = this.m_renderer.GetSharedMaterials();
		if (sharedMaterials == null || sharedMaterials.Count < 1)
		{
			return;
		}
		if (this.m_UberShaderAnimation == null || this.m_UberShaderAnimation.animations == null)
		{
			return;
		}
		this.m_UberShaderAnimation.materialPropertyIDs = new List<int>(this.m_UberShaderAnimation.animations.Count);
		for (int i = 0; i < this.m_UberShaderAnimation.animations.Count; i++)
		{
			int item = Shader.PropertyToID(this.m_UberShaderAnimation.animations[i].materialPropertyName);
			this.UberShaderAnimation.materialPropertyIDs.Add(item);
		}
	}

	// Token: 0x04007B45 RID: 31557
	private const int GUI_PROPERTY_LABEL_WIDTH = 130;

	// Token: 0x04007B46 RID: 31558
	[SerializeField]
	private UberShaderAnimation m_UberShaderAnimation;

	// Token: 0x04007B47 RID: 31559
	public int m_MaterialIndex = -1;

	// Token: 0x04007B48 RID: 31560
	private bool m_firstFrame;

	// Token: 0x04007B49 RID: 31561
	private float m_time;

	// Token: 0x04007B4A RID: 31562
	private float m_deltaTime;

	// Token: 0x04007B4B RID: 31563
	private Renderer m_renderer;

	// Token: 0x04007B4C RID: 31564
	private float m_lastTime;

	// Token: 0x04007B4D RID: 31565
	private float m_randomOffset;

	// Token: 0x04007B4E RID: 31566
	private string m_copyBuffer;

	// Token: 0x04007B4F RID: 31567
	private UberShaderAnimation.PropertyType m_copyBufferType;

	// Token: 0x04007B50 RID: 31568
	private string m_copyBufferLayer;

	// Token: 0x04007B51 RID: 31569
	private int m_copyBufferLayerCount;

	// Token: 0x04007B52 RID: 31570
	private DateTime? m_lastSaveTime;

	// Token: 0x04007B53 RID: 31571
	private float m_maxTime = 65535f;

	// Token: 0x04007B54 RID: 31572
	private static bool s_autoSave = false;

	// Token: 0x04007B55 RID: 31573
	private static float s_autoSaveInterval = 30f;
}
