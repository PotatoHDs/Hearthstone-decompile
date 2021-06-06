using System.Collections.Generic;
using Hearthstone.Extensions;
using UnityEngine;

public static class RendererExtension
{
	private static MaterialManagerService m_service;

	public static void SetMaterial(this Renderer renderer, Material material)
	{
		MaterialManagerService service = GetService();
		if (service != null)
		{
			if (service.HasCustomMaterial(renderer))
			{
				service.StopUsingMaterial(renderer, renderer.material);
			}
			service.RegisterNewMaterialUsage(renderer, material);
		}
		renderer.material = material;
	}

	public static void SetMaterial(this Renderer renderer, int materialIndex, Material material)
	{
		if (materialIndex == 0)
		{
			renderer.SetMaterial(material);
			return;
		}
		Material[] materials = renderer.materials;
		MaterialManagerService service = GetService();
		service?.RegisterNewMaterialsUsages(renderer, materials);
		if (materials.Length >= materialIndex)
		{
			Material material2 = materials[materialIndex];
			materials[materialIndex] = material;
			renderer.materials = materials;
			if (service != null)
			{
				service.StopUsingMaterial(renderer, material2);
				service.RegisterNewMaterialUsage(renderer, material);
			}
		}
	}

	public static Material GetMaterial(this Renderer renderer)
	{
		Material material = renderer.material;
		GetService()?.RegisterNewMaterialUsage(renderer, material);
		return material;
	}

	public static Material GetMaterial(this Renderer renderer, int materialIndex)
	{
		if (materialIndex == 0)
		{
			return renderer.GetMaterial();
		}
		Material[] materials = renderer.materials;
		GetService()?.RegisterNewMaterialsUsages(renderer, materials);
		if (materials.Length < materialIndex)
		{
			return null;
		}
		return materials[materialIndex];
	}

	public static void SetMaterials(this Renderer renderer, Material[] materials)
	{
		MaterialManagerService service = GetService();
		if (service != null)
		{
			if (service.HasCustomMaterial(renderer))
			{
				service.StopUsingMaterials(renderer, renderer.materials);
			}
			service.RegisterNewMaterialsUsages(renderer, materials);
		}
		renderer.materials = materials;
	}

	public static void SetMaterials(this Renderer renderer, List<Material> materials)
	{
		Material[] materials2 = materials.ToArray();
		renderer.SetMaterials(materials2);
	}

	public static List<Material> GetMaterials(this Renderer renderer)
	{
		List<Material> list = new List<Material>();
		renderer.GetMaterials(list);
		GetService()?.RegisterNewMaterialsUsages(renderer, list);
		return list;
	}

	public static void SetSharedMaterial(this Renderer renderer, Material material)
	{
		MaterialManagerService service = GetService();
		if (service != null)
		{
			if (service.HasCustomMaterial(renderer))
			{
				service.StopUsingMaterial(renderer, renderer.sharedMaterial);
			}
			service.RegisterNewMaterialUsage(renderer, material);
		}
		renderer.sharedMaterial = material;
	}

	public static void SetSharedMaterial(this Renderer renderer, int materialIndex, Material material)
	{
		if (materialIndex == 0)
		{
			renderer.SetSharedMaterial(material);
			return;
		}
		Material[] sharedMaterials = renderer.sharedMaterials;
		MaterialManagerService service = GetService();
		service?.RegisterNewMaterialsUsages(renderer, sharedMaterials);
		if (sharedMaterials.Length >= materialIndex)
		{
			Material material2 = sharedMaterials[materialIndex];
			sharedMaterials[materialIndex] = material;
			renderer.sharedMaterials = sharedMaterials;
			if (service != null)
			{
				service.StopUsingMaterial(renderer, material2);
				service.RegisterNewMaterialUsage(renderer, material);
			}
		}
	}

	public static void SetSharedMaterials(this Renderer renderer, Material[] materials)
	{
		MaterialManagerService service = GetService();
		if (service != null)
		{
			if (service.HasCustomMaterial(renderer))
			{
				service.StopUsingMaterials(renderer, renderer.sharedMaterials);
			}
			service.RegisterNewMaterialsUsages(renderer, materials);
		}
		renderer.sharedMaterials = materials;
	}

	public static void SetSharedMaterials(this Renderer renderer, List<Material> materials)
	{
		Material[] materials2 = materials.ToArray();
		renderer.SetSharedMaterials(materials2);
	}

	public static Material GetSharedMaterial(this Renderer renderer)
	{
		Material sharedMaterial = renderer.sharedMaterial;
		GetService()?.RegisterNewMaterialUsage(renderer, sharedMaterial);
		return sharedMaterial;
	}

	public static Material GetSharedMaterial(this Renderer renderer, int materialIndex)
	{
		if (materialIndex == 0)
		{
			return renderer.GetSharedMaterial();
		}
		Material[] sharedMaterials = renderer.sharedMaterials;
		GetService()?.RegisterNewMaterialsUsages(renderer, sharedMaterials);
		if (materialIndex < sharedMaterials.Length)
		{
			return sharedMaterials[materialIndex];
		}
		return null;
	}

	public static List<Material> GetSharedMaterials(this Renderer renderer)
	{
		List<Material> list = new List<Material>();
		renderer.GetSharedMaterials(list);
		return list;
	}

	private static MaterialManagerService GetService()
	{
		if (m_service == null)
		{
			m_service = HearthstoneServices.Get<MaterialManagerService>();
		}
		return m_service;
	}
}
