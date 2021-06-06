using System;
using System.Collections.Generic;
using Hearthstone.Extensions;
using UnityEngine;

// Token: 0x0200083D RID: 2109
public static class RendererExtension
{
	// Token: 0x0600708B RID: 28811 RVA: 0x00244D60 File Offset: 0x00242F60
	public static void SetMaterial(this Renderer renderer, Material material)
	{
		MaterialManagerService service = RendererExtension.GetService();
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

	// Token: 0x0600708C RID: 28812 RVA: 0x00244D9C File Offset: 0x00242F9C
	public static void SetMaterial(this Renderer renderer, int materialIndex, Material material)
	{
		if (materialIndex == 0)
		{
			renderer.SetMaterial(material);
			return;
		}
		Material[] materials = renderer.materials;
		MaterialManagerService service = RendererExtension.GetService();
		if (service != null)
		{
			service.RegisterNewMaterialsUsages(renderer, materials);
		}
		if (materials.Length < materialIndex)
		{
			return;
		}
		Material material2 = materials[materialIndex];
		materials[materialIndex] = material;
		renderer.materials = materials;
		if (service != null)
		{
			service.StopUsingMaterial(renderer, material2);
			service.RegisterNewMaterialUsage(renderer, material);
		}
	}

	// Token: 0x0600708D RID: 28813 RVA: 0x00244DF8 File Offset: 0x00242FF8
	public static Material GetMaterial(this Renderer renderer)
	{
		Material material = renderer.material;
		MaterialManagerService service = RendererExtension.GetService();
		if (service != null)
		{
			service.RegisterNewMaterialUsage(renderer, material);
		}
		return material;
	}

	// Token: 0x0600708E RID: 28814 RVA: 0x00244E20 File Offset: 0x00243020
	public static Material GetMaterial(this Renderer renderer, int materialIndex)
	{
		if (materialIndex == 0)
		{
			return renderer.GetMaterial();
		}
		Material[] materials = renderer.materials;
		MaterialManagerService service = RendererExtension.GetService();
		if (service != null)
		{
			service.RegisterNewMaterialsUsages(renderer, materials);
		}
		if (materials.Length < materialIndex)
		{
			return null;
		}
		return materials[materialIndex];
	}

	// Token: 0x0600708F RID: 28815 RVA: 0x00244E5C File Offset: 0x0024305C
	public static void SetMaterials(this Renderer renderer, Material[] materials)
	{
		MaterialManagerService service = RendererExtension.GetService();
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

	// Token: 0x06007090 RID: 28816 RVA: 0x00244E98 File Offset: 0x00243098
	public static void SetMaterials(this Renderer renderer, List<Material> materials)
	{
		Material[] materials2 = materials.ToArray();
		renderer.SetMaterials(materials2);
	}

	// Token: 0x06007091 RID: 28817 RVA: 0x00244EB4 File Offset: 0x002430B4
	public static List<Material> GetMaterials(this Renderer renderer)
	{
		List<Material> list = new List<Material>();
		renderer.GetMaterials(list);
		MaterialManagerService service = RendererExtension.GetService();
		if (service != null)
		{
			service.RegisterNewMaterialsUsages(renderer, list);
		}
		return list;
	}

	// Token: 0x06007092 RID: 28818 RVA: 0x00244EE4 File Offset: 0x002430E4
	public static void SetSharedMaterial(this Renderer renderer, Material material)
	{
		MaterialManagerService service = RendererExtension.GetService();
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

	// Token: 0x06007093 RID: 28819 RVA: 0x00244F20 File Offset: 0x00243120
	public static void SetSharedMaterial(this Renderer renderer, int materialIndex, Material material)
	{
		if (materialIndex == 0)
		{
			renderer.SetSharedMaterial(material);
			return;
		}
		Material[] sharedMaterials = renderer.sharedMaterials;
		MaterialManagerService service = RendererExtension.GetService();
		if (service != null)
		{
			service.RegisterNewMaterialsUsages(renderer, sharedMaterials);
		}
		if (sharedMaterials.Length < materialIndex)
		{
			return;
		}
		Material material2 = sharedMaterials[materialIndex];
		sharedMaterials[materialIndex] = material;
		renderer.sharedMaterials = sharedMaterials;
		if (service != null)
		{
			service.StopUsingMaterial(renderer, material2);
			service.RegisterNewMaterialUsage(renderer, material);
		}
	}

	// Token: 0x06007094 RID: 28820 RVA: 0x00244F7C File Offset: 0x0024317C
	public static void SetSharedMaterials(this Renderer renderer, Material[] materials)
	{
		MaterialManagerService service = RendererExtension.GetService();
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

	// Token: 0x06007095 RID: 28821 RVA: 0x00244FB8 File Offset: 0x002431B8
	public static void SetSharedMaterials(this Renderer renderer, List<Material> materials)
	{
		Material[] materials2 = materials.ToArray();
		renderer.SetSharedMaterials(materials2);
	}

	// Token: 0x06007096 RID: 28822 RVA: 0x00244FD4 File Offset: 0x002431D4
	public static Material GetSharedMaterial(this Renderer renderer)
	{
		Material sharedMaterial = renderer.sharedMaterial;
		MaterialManagerService service = RendererExtension.GetService();
		if (service != null)
		{
			service.RegisterNewMaterialUsage(renderer, sharedMaterial);
		}
		return sharedMaterial;
	}

	// Token: 0x06007097 RID: 28823 RVA: 0x00244FFC File Offset: 0x002431FC
	public static Material GetSharedMaterial(this Renderer renderer, int materialIndex)
	{
		if (materialIndex == 0)
		{
			return renderer.GetSharedMaterial();
		}
		Material[] sharedMaterials = renderer.sharedMaterials;
		MaterialManagerService service = RendererExtension.GetService();
		if (service != null)
		{
			service.RegisterNewMaterialsUsages(renderer, sharedMaterials);
		}
		if (materialIndex < sharedMaterials.Length)
		{
			return sharedMaterials[materialIndex];
		}
		return null;
	}

	// Token: 0x06007098 RID: 28824 RVA: 0x00245038 File Offset: 0x00243238
	public static List<Material> GetSharedMaterials(this Renderer renderer)
	{
		List<Material> list = new List<Material>();
		renderer.GetSharedMaterials(list);
		return list;
	}

	// Token: 0x06007099 RID: 28825 RVA: 0x00245053 File Offset: 0x00243253
	private static MaterialManagerService GetService()
	{
		if (RendererExtension.m_service == null)
		{
			RendererExtension.m_service = HearthstoneServices.Get<MaterialManagerService>();
		}
		return RendererExtension.m_service;
	}

	// Token: 0x04005A69 RID: 23145
	private static MaterialManagerService m_service;
}
