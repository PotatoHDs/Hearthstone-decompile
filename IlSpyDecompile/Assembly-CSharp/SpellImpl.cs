using UnityEngine;

public class SpellImpl : Spell
{
	protected Actor m_actor;

	protected GameObject m_rootObject;

	protected MeshRenderer m_rootObjectRenderer;

	protected void InitActorVariables()
	{
		m_actor = SpellUtils.GetParentActor(this);
		m_rootObject = SpellUtils.GetParentRootObject(this);
		m_rootObjectRenderer = SpellUtils.GetParentRootObjectMesh(this);
	}

	protected void SetActorVisibility(bool visible, bool ignoreSpells)
	{
		if (m_actor != null)
		{
			if (visible)
			{
				m_actor.Show(ignoreSpells);
			}
			else
			{
				m_actor.Hide(ignoreSpells);
			}
		}
	}

	protected void SetVisibility(GameObject go, bool visible)
	{
		go.GetComponent<Renderer>().enabled = visible;
	}

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

	protected void SetAnimationSpeed(GameObject go, string animName, float speed)
	{
		if (!(go == null))
		{
			go.GetComponent<Animation>()[animName].speed = speed;
		}
	}

	protected void SetAnimationTime(GameObject go, string animName, float time)
	{
		if (!(go == null))
		{
			go.GetComponent<Animation>()[animName].time = time;
		}
	}

	protected void PlayAnimation(GameObject go, string animName, PlayMode playMode, float crossFade = 0f)
	{
		if (!(go == null))
		{
			if (crossFade <= Mathf.Epsilon)
			{
				go.GetComponent<Animation>().Play(animName, playMode);
			}
			else
			{
				go.GetComponent<Animation>().CrossFade(animName, crossFade, playMode);
			}
		}
	}

	protected void PlayParticles(GameObject go, bool includeChildren)
	{
		if (!(go == null))
		{
			go.GetComponent<ParticleSystem>().Play(includeChildren);
		}
	}

	protected GameObject GetActorObject(string name)
	{
		if (m_actor == null)
		{
			return null;
		}
		return SceneUtils.FindChildBySubstring(m_actor.gameObject, name);
	}

	protected void SetMaterialColor(GameObject go, Material material, string colorName, Color color, int materialIndex = 0)
	{
		if (colorName == "")
		{
			colorName = "_Color";
		}
		if (material != null)
		{
			material.SetColor(colorName, color);
		}
		else
		{
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
			if (!(material2 == null))
			{
				if (materialIndex == 0)
				{
					material2.SetColor(colorName, color);
				}
				else if (component.GetMaterials().Count > materialIndex)
				{
					component.GetMaterial(materialIndex).SetColor(colorName, color);
				}
			}
		}
	}

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
		if (component.GetMaterials().Count > materialIndex)
		{
			if (!getSharedMaterial)
			{
				return component.GetMaterial(materialIndex);
			}
			return component.GetSharedMaterial(materialIndex);
		}
		return null;
	}
}
