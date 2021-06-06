using System;
using Blizzard.T5.Jobs;
using Hearthstone;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x020008E9 RID: 2281
[Serializable]
public class MaterialReference
{
	// Token: 0x06007E8B RID: 32395 RVA: 0x0028F257 File Offset: 0x0028D457
	public MaterialReference(string materialRef, string mainTextureRef)
	{
		this.m_materialRef = materialRef;
		this.m_mainTextureRef = mainTextureRef;
	}

	// Token: 0x1700073F RID: 1855
	// (get) Token: 0x06007E8C RID: 32396 RVA: 0x0028F26D File Offset: 0x0028D46D
	public string MaterialRef
	{
		get
		{
			return this.m_materialRef;
		}
	}

	// Token: 0x17000740 RID: 1856
	// (get) Token: 0x06007E8D RID: 32397 RVA: 0x0028F275 File Offset: 0x0028D475
	public string MainTextureRef
	{
		get
		{
			return this.m_mainTextureRef;
		}
	}

	// Token: 0x06007E8E RID: 32398 RVA: 0x0028F280 File Offset: 0x0028D480
	public Material GetMaterial()
	{
		if (string.IsNullOrWhiteSpace(this.m_materialRef))
		{
			Debug.LogWarning(string.Format("Material Reference used with no value", Array.Empty<object>()));
			return null;
		}
		if (AssetLoader.Get() == null)
		{
			return null;
		}
		Material material = AssetLoader.Get().LoadMaterial(this.m_materialRef, false, false);
		if (material == null)
		{
			return null;
		}
		if (!string.IsNullOrWhiteSpace(this.m_mainTextureRef))
		{
			if (material.mainTexture == null)
			{
				material.mainTexture = AssetLoader.Get().LoadTexture(this.m_mainTextureRef, false, false);
			}
			if (material.mainTexture == null)
			{
				Debug.LogWarning(string.Format("Material Reference attempted to load texture and failed: \"{0}\"", this.m_mainTextureRef));
			}
		}
		return material;
	}

	// Token: 0x06007E8F RID: 32399 RVA: 0x0028F33C File Offset: 0x0028D53C
	public void GetMaterialAsync(Action<Material> callback)
	{
		if (!HearthstoneServices.IsAvailable<IAssetLoader>())
		{
			IJobDependency[] dependencies;
			HearthstoneServices.InitializeDynamicServicesIfEditor(out dependencies, new Type[]
			{
				typeof(IAssetLoader)
			});
			Processor.QueueJob(HearthstoneJobs.CreateJobFromAction("MaterialReference.GetMaterialAsync", delegate()
			{
				callback(this.GetMaterial());
			}, JobFlags.StartImmediately, dependencies));
			return;
		}
		callback(this.GetMaterial());
	}

	// Token: 0x04006635 RID: 26165
	[SerializeField]
	private string m_materialRef;

	// Token: 0x04006636 RID: 26166
	[SerializeField]
	private string m_mainTextureRef;
}
