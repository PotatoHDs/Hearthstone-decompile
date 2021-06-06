using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

namespace Hearthstone.Extensions
{
	// Token: 0x0200107B RID: 4219
	public class MaterialManagerService : IService, IHasFixedUpdate
	{
		// Token: 0x0600B63E RID: 46654 RVA: 0x0037E750 File Offset: 0x0037C950
		public MaterialManagerService()
		{
			this.m_timeProvider = new UnityTimeProvider();
		}

		// Token: 0x0600B63F RID: 46655 RVA: 0x0037E763 File Offset: 0x0037C963
		public MaterialManagerService(ITimeProvider timeProvider)
		{
			this.m_timeProvider = timeProvider;
		}

		// Token: 0x0600B640 RID: 46656 RVA: 0x0037E772 File Offset: 0x0037C972
		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			this.m_customMaterialRenderers = new Dictionary<int, MaterialManagerService.RegisteredRenderer>(500);
			this.m_customMaterials = new Dictionary<int, MaterialManagerService.MaterialUsages>(500);
			this.m_unusedMaterials = new List<int>(20);
			this.m_unusedRenderers = new List<int>(20);
			this.m_tempRemoveMaterial = new List<int>(500);
			this.m_isAppPlaying = Application.isPlaying;
			yield break;
		}

		// Token: 0x0600B641 RID: 46657 RVA: 0x00090064 File Offset: 0x0008E264
		public Type[] GetDependencies()
		{
			return null;
		}

		// Token: 0x0600B642 RID: 46658 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void Shutdown()
		{
		}

		// Token: 0x0600B643 RID: 46659 RVA: 0x0037E781 File Offset: 0x0037C981
		public void FixedUpdate()
		{
			this.CleanupDestroyedRenderers();
			this.CleanupDestroyedMaterials();
		}

		// Token: 0x0600B644 RID: 46660 RVA: 0x000C3B6A File Offset: 0x000C1D6A
		public float GetSecondsBetweenUpdates()
		{
			return 1f;
		}

		// Token: 0x0600B645 RID: 46661 RVA: 0x0037E78F File Offset: 0x0037C98F
		public bool HasCustomMaterial(Renderer renderer)
		{
			return this.m_customMaterialRenderers.ContainsKey(renderer.GetHashCode());
		}

		// Token: 0x0600B646 RID: 46662 RVA: 0x0037E7A4 File Offset: 0x0037C9A4
		public void RegisterNewMaterialsUsages(Renderer renderer, IEnumerable<Material> materials)
		{
			int hashCode = renderer.GetHashCode();
			MaterialManagerService.RegisteredRenderer registeredRenderer;
			if (!this.m_customMaterialRenderers.TryGetValue(hashCode, out registeredRenderer))
			{
				registeredRenderer = new MaterialManagerService.RegisteredRenderer(renderer);
				this.m_customMaterialRenderers.Add(hashCode, registeredRenderer);
			}
			foreach (Material material in materials)
			{
				if (material)
				{
					this.RegisterMaterialUsage(registeredRenderer, material);
				}
			}
		}

		// Token: 0x0600B647 RID: 46663 RVA: 0x0037E824 File Offset: 0x0037CA24
		public void RegisterNewMaterialUsage(Renderer renderer, Material material)
		{
			if (!material)
			{
				return;
			}
			int hashCode = renderer.GetHashCode();
			if (material.GetHashCode() > 0)
			{
				return;
			}
			MaterialManagerService.RegisteredRenderer registeredRenderer;
			if (!this.m_customMaterialRenderers.TryGetValue(hashCode, out registeredRenderer))
			{
				registeredRenderer = new MaterialManagerService.RegisteredRenderer(renderer);
				this.m_customMaterialRenderers.Add(hashCode, registeredRenderer);
			}
			this.RegisterMaterialUsage(registeredRenderer, material);
		}

		// Token: 0x0600B648 RID: 46664 RVA: 0x0037E878 File Offset: 0x0037CA78
		public void StopUsingMaterial(Renderer renderer, Material material)
		{
			if (!material)
			{
				return;
			}
			int hashCode = material.GetHashCode();
			MaterialManagerService.RegisteredRenderer registeredRenderer;
			if (this.m_customMaterialRenderers.TryGetValue(renderer.GetHashCode(), out registeredRenderer))
			{
				int num = -1;
				for (int i = 0; i < registeredRenderer.Materials.Count; i++)
				{
					if (registeredRenderer.Materials[i] == hashCode)
					{
						num = i;
						break;
					}
				}
				if (num != -1)
				{
					registeredRenderer.Materials.RemoveAt(num);
				}
			}
			this.DecrementMaterialUsage(hashCode);
		}

		// Token: 0x0600B649 RID: 46665 RVA: 0x0037E8EC File Offset: 0x0037CAEC
		public void StopUsingMaterials(Renderer renderer, IEnumerable<Material> materials)
		{
			foreach (Material material in materials)
			{
				this.StopUsingMaterial(renderer, material);
			}
		}

		// Token: 0x0600B64A RID: 46666 RVA: 0x0037E938 File Offset: 0x0037CB38
		public void IgnoreMaterial(Material material)
		{
			material.name += "[IGNORE]";
		}

		// Token: 0x0600B64B RID: 46667 RVA: 0x0037E950 File Offset: 0x0037CB50
		public void KeepMaterial(Material material)
		{
			if (material)
			{
				this.IncrementMaterialUsage(material);
			}
		}

		// Token: 0x0600B64C RID: 46668 RVA: 0x0037E961 File Offset: 0x0037CB61
		public void DropMaterial(Material material)
		{
			if (material)
			{
				this.DecrementMaterialUsage(material.GetInstanceID());
			}
		}

		// Token: 0x0600B64D RID: 46669 RVA: 0x0037E978 File Offset: 0x0037CB78
		public bool CanIgnoreMaterial(Material material)
		{
			if (material != null && material.GetHashCode() <= 0)
			{
				string name = material.name;
				if (!string.IsNullOrEmpty(name))
				{
					return !name.Contains("(") || name.Contains("[IGNORE]");
				}
			}
			return true;
		}

		// Token: 0x0600B64E RID: 46670 RVA: 0x0037E9C4 File Offset: 0x0037CBC4
		private void IncrementMaterialUsage(Material material)
		{
			int instanceID = material.GetInstanceID();
			MaterialManagerService.MaterialUsages value;
			if (!this.m_customMaterials.TryGetValue(instanceID, out value))
			{
				this.m_customMaterials.Add(instanceID, new MaterialManagerService.MaterialUsages
				{
					Material = material,
					HashCode = instanceID,
					TimesUsed = 1
				});
				return;
			}
			value.TimesUsed++;
			value.TimeToRemove = 0f;
			this.m_customMaterials[instanceID] = value;
		}

		// Token: 0x0600B64F RID: 46671 RVA: 0x0037EA3C File Offset: 0x0037CC3C
		private void DecrementMaterialUsage(int materialHashCode)
		{
			MaterialManagerService.MaterialUsages materialUsages;
			if (!this.m_customMaterials.TryGetValue(materialHashCode, out materialUsages))
			{
				return;
			}
			materialUsages.TimesUsed--;
			if (materialUsages.TimesUsed <= 0 || !materialUsages.Material)
			{
				this.m_unusedMaterials.Add(materialHashCode);
				materialUsages.TimeToRemove = this.m_timeProvider.TimeSinceStartup + 10f;
			}
			this.m_customMaterials[materialHashCode] = materialUsages;
		}

		// Token: 0x0600B650 RID: 46672 RVA: 0x0037EAB0 File Offset: 0x0037CCB0
		private void RegisterMaterialUsage(MaterialManagerService.RegisteredRenderer registeredRenderer, Material material)
		{
			int hashCode = material.GetHashCode();
			if (this.CanIgnoreMaterial(material))
			{
				return;
			}
			using (List<int>.Enumerator enumerator = registeredRenderer.Materials.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == hashCode)
					{
						return;
					}
				}
			}
			registeredRenderer.Materials.Add(hashCode);
			this.IncrementMaterialUsage(material);
		}

		// Token: 0x0600B651 RID: 46673 RVA: 0x0037EB24 File Offset: 0x0037CD24
		private void CleanupDestroyedRenderers()
		{
			this.m_unusedRenderers.Clear();
			foreach (KeyValuePair<int, MaterialManagerService.RegisteredRenderer> keyValuePair in this.m_customMaterialRenderers)
			{
				MaterialManagerService.RegisteredRenderer value = keyValuePair.Value;
				if (value.Materials.Count <= 0 || !value.Renderer)
				{
					this.m_unusedRenderers.Add(keyValuePair.Key);
					foreach (int materialHashCode in value.Materials)
					{
						this.DecrementMaterialUsage(materialHashCode);
					}
				}
			}
			foreach (int key in this.m_unusedRenderers)
			{
				this.m_customMaterialRenderers.Remove(key);
			}
		}

		// Token: 0x0600B652 RID: 46674 RVA: 0x0037EC40 File Offset: 0x0037CE40
		private void CleanupDestroyedMaterials()
		{
			int num = 0;
			float timeSinceStartup = this.m_timeProvider.TimeSinceStartup;
			this.m_tempRemoveMaterial.Clear();
			int num2 = 0;
			foreach (int num3 in this.m_unusedMaterials)
			{
				MaterialManagerService.MaterialUsages materialUsages;
				if (this.m_customMaterials.TryGetValue(num3, out materialUsages))
				{
					if (materialUsages.TimesUsed == 0 && materialUsages.TimeToRemove != 0f && materialUsages.TimeToRemove < timeSinceStartup)
					{
						if (materialUsages.Material)
						{
							if (this.m_isAppPlaying)
							{
								UnityEngine.Object.Destroy(materialUsages.Material);
							}
							else
							{
								UnityEngine.Object.DestroyImmediate(materialUsages.Material);
							}
							num++;
						}
						this.m_customMaterials.Remove(num3);
					}
					else if (materialUsages.TimesUsed == 0)
					{
						this.m_tempRemoveMaterial.Add(num3);
					}
					num2++;
				}
			}
			this.m_unusedMaterials.Clear();
			if (this.m_tempRemoveMaterial.Count > 0)
			{
				foreach (int item in this.m_tempRemoveMaterial)
				{
					this.m_unusedMaterials.Add(item);
				}
			}
		}

		// Token: 0x0400979A RID: 38810
		protected const int RENDERER_MATERIAL_LIST_SIZE = 2;

		// Token: 0x0400979B RID: 38811
		protected const int CUSTOM_MATERIAL_LIST_SIZE = 500;

		// Token: 0x0400979C RID: 38812
		protected const int UNUSED_OBJECTS_LIST_SIZE = 20;

		// Token: 0x0400979D RID: 38813
		protected const int TTL_SECONDS_AFTER_UNUSED = 10;

		// Token: 0x0400979E RID: 38814
		protected const string IGNORE_KEYWORD = "[IGNORE]";

		// Token: 0x0400979F RID: 38815
		private Dictionary<int, MaterialManagerService.RegisteredRenderer> m_customMaterialRenderers;

		// Token: 0x040097A0 RID: 38816
		private ITimeProvider m_timeProvider;

		// Token: 0x040097A1 RID: 38817
		private Dictionary<int, MaterialManagerService.MaterialUsages> m_customMaterials;

		// Token: 0x040097A2 RID: 38818
		private List<int> m_unusedMaterials;

		// Token: 0x040097A3 RID: 38819
		private List<int> m_unusedRenderers;

		// Token: 0x040097A4 RID: 38820
		private List<int> m_tempRemoveMaterial;

		// Token: 0x040097A5 RID: 38821
		private bool m_isAppPlaying;

		// Token: 0x02002882 RID: 10370
		private struct RegisteredRenderer
		{
			// Token: 0x06013C20 RID: 80928 RVA: 0x0053C0BF File Offset: 0x0053A2BF
			public RegisteredRenderer(Renderer renderer)
			{
				this.Renderer = renderer;
				this.Materials = new List<int>(2);
			}

			// Token: 0x0400F9D0 RID: 63952
			public Renderer Renderer;

			// Token: 0x0400F9D1 RID: 63953
			public List<int> Materials;
		}

		// Token: 0x02002883 RID: 10371
		private struct MaterialUsages
		{
			// Token: 0x0400F9D2 RID: 63954
			public Material Material;

			// Token: 0x0400F9D3 RID: 63955
			public int HashCode;

			// Token: 0x0400F9D4 RID: 63956
			public int TimesUsed;

			// Token: 0x0400F9D5 RID: 63957
			public float TimeToRemove;
		}
	}
}
