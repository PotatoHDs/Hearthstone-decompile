using System;
using UnityEngine;

// Token: 0x02000825 RID: 2085
public class SpellImpl : Spell
{
	// Token: 0x0600701E RID: 28702 RVA: 0x002429D2 File Offset: 0x00240BD2
	protected void InitActorVariables()
	{
		this.m_actor = SpellUtils.GetParentActor(this);
		this.m_rootObject = SpellUtils.GetParentRootObject(this);
		this.m_rootObjectRenderer = SpellUtils.GetParentRootObjectMesh(this);
	}

	// Token: 0x0600701F RID: 28703 RVA: 0x002429F8 File Offset: 0x00240BF8
	protected void SetActorVisibility(bool visible, bool ignoreSpells)
	{
		if (this.m_actor != null)
		{
			if (visible)
			{
				this.m_actor.Show(ignoreSpells);
				return;
			}
			this.m_actor.Hide(ignoreSpells);
		}
	}

	// Token: 0x06007020 RID: 28704 RVA: 0x00242A24 File Offset: 0x00240C24
	protected void SetVisibility(GameObject go, bool visible)
	{
		go.GetComponent<Renderer>().enabled = visible;
	}

	// Token: 0x06007021 RID: 28705 RVA: 0x00242A34 File Offset: 0x00240C34
	protected void SetVisibilityRecursive(GameObject go, bool visible)
	{
		if (go == null)
		{
			return;
		}
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>();
		if (componentsInChildren != null)
		{
			Renderer[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = visible;
			}
		}
	}

	// Token: 0x06007022 RID: 28706 RVA: 0x00242A6E File Offset: 0x00240C6E
	protected void SetAnimationSpeed(GameObject go, string animName, float speed)
	{
		if (go == null)
		{
			return;
		}
		go.GetComponent<Animation>()[animName].speed = speed;
	}

	// Token: 0x06007023 RID: 28707 RVA: 0x00242A8C File Offset: 0x00240C8C
	protected void SetAnimationTime(GameObject go, string animName, float time)
	{
		if (go == null)
		{
			return;
		}
		go.GetComponent<Animation>()[animName].time = time;
	}

	// Token: 0x06007024 RID: 28708 RVA: 0x00242AAA File Offset: 0x00240CAA
	protected void PlayAnimation(GameObject go, string animName, PlayMode playMode, float crossFade = 0f)
	{
		if (go == null)
		{
			return;
		}
		if (crossFade <= Mathf.Epsilon)
		{
			go.GetComponent<Animation>().Play(animName, playMode);
			return;
		}
		go.GetComponent<Animation>().CrossFade(animName, crossFade, playMode);
	}

	// Token: 0x06007025 RID: 28709 RVA: 0x00242ADD File Offset: 0x00240CDD
	protected void PlayParticles(GameObject go, bool includeChildren)
	{
		if (go == null)
		{
			return;
		}
		go.GetComponent<ParticleSystem>().Play(includeChildren);
	}

	// Token: 0x06007026 RID: 28710 RVA: 0x00242AF5 File Offset: 0x00240CF5
	protected GameObject GetActorObject(string name)
	{
		if (this.m_actor == null)
		{
			return null;
		}
		return SceneUtils.FindChildBySubstring(this.m_actor.gameObject, name);
	}

	// Token: 0x06007027 RID: 28711 RVA: 0x00242B18 File Offset: 0x00240D18
	protected void SetMaterialColor(GameObject go, Material material, string colorName, Color color, int materialIndex = 0)
	{
		if (colorName == "")
		{
			colorName = "_Color";
		}
		if (material != null)
		{
			material.SetColor(colorName, color);
			return;
		}
		if (go == null)
		{
			return;
		}
		Renderer component = go.GetComponent<Renderer>();
		if (component == null)
		{
			return;
		}
		Material material2 = component.GetMaterial();
		if (material2 == null)
		{
			return;
		}
		if (materialIndex == 0)
		{
			material2.SetColor(colorName, color);
			return;
		}
		if (component.GetMaterials().Count > materialIndex)
		{
			component.GetMaterial(materialIndex).SetColor(colorName, color);
		}
	}

	// Token: 0x06007028 RID: 28712 RVA: 0x00242BA8 File Offset: 0x00240DA8
	protected Material GetMaterial(GameObject go, Material material, bool getSharedMaterial = false, int materialIndex = 0)
	{
		if (go == null)
		{
			return null;
		}
		Renderer component = go.GetComponent<Renderer>();
		if (component == null)
		{
			return null;
		}
		if (materialIndex == 0 && !getSharedMaterial)
		{
			return component.GetMaterial();
		}
		if (materialIndex == 0 && getSharedMaterial)
		{
			return component.GetSharedMaterial();
		}
		if (component.GetMaterials().Count <= materialIndex)
		{
			return null;
		}
		if (!getSharedMaterial)
		{
			return component.GetMaterial(materialIndex);
		}
		return component.GetSharedMaterial(materialIndex);
	}

	// Token: 0x040059E6 RID: 23014
	protected Actor m_actor;

	// Token: 0x040059E7 RID: 23015
	protected GameObject m_rootObject;

	// Token: 0x040059E8 RID: 23016
	protected MeshRenderer m_rootObjectRenderer;
}
