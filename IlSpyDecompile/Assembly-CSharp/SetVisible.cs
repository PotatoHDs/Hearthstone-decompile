using UnityEngine;

public class SetVisible : MonoBehaviour
{
	public GameObject obj;

	private MeshRenderer[] renderers;

	private SkinnedMeshRenderer[] skinRenderers;

	private bool onoff;

	private void Start()
	{
		if (!(obj == null))
		{
			renderers = obj.GetComponentsInChildren<MeshRenderer>();
			skinRenderers = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
		}
	}

	public void SetOn()
	{
		onoff = true;
		SetRenderers(onoff);
	}

	public void SetOff()
	{
		onoff = false;
		SetRenderers(onoff);
	}

	private void SetRenderers(bool value)
	{
		if (!(obj == null) && renderers != null && renderers.Length != 0)
		{
			MeshRenderer[] array = renderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = value;
			}
			SkinnedMeshRenderer[] array2 = skinRenderers;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].enabled = value;
			}
		}
	}
}
