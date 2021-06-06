using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using UnityEngine;

namespace Hearthstone.Extensions
{
	public class MaterialManagerService : IService, IHasFixedUpdate
	{
		private struct RegisteredRenderer
		{
			public Renderer Renderer;

			public List<int> Materials;

			public RegisteredRenderer(Renderer renderer)
			{
				Renderer = renderer;
				Materials = new List<int>(2);
			}
		}

		private struct MaterialUsages
		{
			public Material Material;

			public int HashCode;

			public int TimesUsed;

			public float TimeToRemove;
		}

		protected const int RENDERER_MATERIAL_LIST_SIZE = 2;

		protected const int CUSTOM_MATERIAL_LIST_SIZE = 500;

		protected const int UNUSED_OBJECTS_LIST_SIZE = 20;

		protected const int TTL_SECONDS_AFTER_UNUSED = 10;

		protected const string IGNORE_KEYWORD = "[IGNORE]";

		private Dictionary<int, RegisteredRenderer> m_customMaterialRenderers;

		private ITimeProvider m_timeProvider;

		private Dictionary<int, MaterialUsages> m_customMaterials;

		private List<int> m_unusedMaterials;

		private List<int> m_unusedRenderers;

		private List<int> m_tempRemoveMaterial;

		private bool m_isAppPlaying;

		public MaterialManagerService()
		{
			m_timeProvider = new UnityTimeProvider();
		}

		public MaterialManagerService(ITimeProvider timeProvider)
		{
			m_timeProvider = timeProvider;
		}

		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			m_customMaterialRenderers = new Dictionary<int, RegisteredRenderer>(500);
			m_customMaterials = new Dictionary<int, MaterialUsages>(500);
			m_unusedMaterials = new List<int>(20);
			m_unusedRenderers = new List<int>(20);
			m_tempRemoveMaterial = new List<int>(500);
			m_isAppPlaying = Application.isPlaying;
			yield break;
		}

		public Type[] GetDependencies()
		{
			return null;
		}

		public void Shutdown()
		{
		}

		public void FixedUpdate()
		{
			CleanupDestroyedRenderers();
			CleanupDestroyedMaterials();
		}

		public float GetSecondsBetweenUpdates()
		{
			return 1f;
		}

		public bool HasCustomMaterial(Renderer renderer)
		{
			return m_customMaterialRenderers.ContainsKey(renderer.GetHashCode());
		}

		public void RegisterNewMaterialsUsages(Renderer renderer, IEnumerable<Material> materials)
		{
			int hashCode = renderer.GetHashCode();
			if (!m_customMaterialRenderers.TryGetValue(hashCode, out var value))
			{
				value = new RegisteredRenderer(renderer);
				m_customMaterialRenderers.Add(hashCode, value);
			}
			foreach (Material material in materials)
			{
				if ((bool)material)
				{
					RegisterMaterialUsage(value, material);
				}
			}
		}

		public void RegisterNewMaterialUsage(Renderer renderer, Material material)
		{
			if (!material)
			{
				return;
			}
			int hashCode = renderer.GetHashCode();
			if (material.GetHashCode() <= 0)
			{
				if (!m_customMaterialRenderers.TryGetValue(hashCode, out var value))
				{
					value = new RegisteredRenderer(renderer);
					m_customMaterialRenderers.Add(hashCode, value);
				}
				RegisterMaterialUsage(value, material);
			}
		}

		public void StopUsingMaterial(Renderer renderer, Material material)
		{
			if (!material)
			{
				return;
			}
			int hashCode = material.GetHashCode();
			if (m_customMaterialRenderers.TryGetValue(renderer.GetHashCode(), out var value))
			{
				int num = -1;
				for (int i = 0; i < value.Materials.Count; i++)
				{
					if (value.Materials[i] == hashCode)
					{
						num = i;
						break;
					}
				}
				if (num != -1)
				{
					value.Materials.RemoveAt(num);
				}
			}
			DecrementMaterialUsage(hashCode);
		}

		public void StopUsingMaterials(Renderer renderer, IEnumerable<Material> materials)
		{
			foreach (Material material in materials)
			{
				StopUsingMaterial(renderer, material);
			}
		}

		public void IgnoreMaterial(Material material)
		{
			material.name += "[IGNORE]";
		}

		public void KeepMaterial(Material material)
		{
			if ((bool)material)
			{
				IncrementMaterialUsage(material);
			}
		}

		public void DropMaterial(Material material)
		{
			if ((bool)material)
			{
				DecrementMaterialUsage(material.GetInstanceID());
			}
		}

		public bool CanIgnoreMaterial(Material material)
		{
			if (material != null && material.GetHashCode() <= 0)
			{
				string name = material.name;
				if (!string.IsNullOrEmpty(name))
				{
					if (name.Contains("("))
					{
						return name.Contains("[IGNORE]");
					}
					return true;
				}
			}
			return true;
		}

		private void IncrementMaterialUsage(Material material)
		{
			int instanceID = material.GetInstanceID();
			if (!m_customMaterials.TryGetValue(instanceID, out var value))
			{
				m_customMaterials.Add(instanceID, new MaterialUsages
				{
					Material = material,
					HashCode = instanceID,
					TimesUsed = 1
				});
			}
			else
			{
				value.TimesUsed++;
				value.TimeToRemove = 0f;
				m_customMaterials[instanceID] = value;
			}
		}

		private void DecrementMaterialUsage(int materialHashCode)
		{
			if (m_customMaterials.TryGetValue(materialHashCode, out var value))
			{
				value.TimesUsed--;
				if (value.TimesUsed <= 0 || !value.Material)
				{
					m_unusedMaterials.Add(materialHashCode);
					value.TimeToRemove = m_timeProvider.TimeSinceStartup + 10f;
				}
				m_customMaterials[materialHashCode] = value;
			}
		}

		private void RegisterMaterialUsage(RegisteredRenderer registeredRenderer, Material material)
		{
			int hashCode = material.GetHashCode();
			if (CanIgnoreMaterial(material))
			{
				return;
			}
			foreach (int material2 in registeredRenderer.Materials)
			{
				if (material2 == hashCode)
				{
					return;
				}
			}
			registeredRenderer.Materials.Add(hashCode);
			IncrementMaterialUsage(material);
		}

		private void CleanupDestroyedRenderers()
		{
			m_unusedRenderers.Clear();
			foreach (KeyValuePair<int, RegisteredRenderer> customMaterialRenderer in m_customMaterialRenderers)
			{
				RegisteredRenderer value = customMaterialRenderer.Value;
				if (value.Materials.Count > 0 && (bool)value.Renderer)
				{
					continue;
				}
				m_unusedRenderers.Add(customMaterialRenderer.Key);
				foreach (int material in value.Materials)
				{
					DecrementMaterialUsage(material);
				}
			}
			foreach (int unusedRenderer in m_unusedRenderers)
			{
				m_customMaterialRenderers.Remove(unusedRenderer);
			}
		}

		private void CleanupDestroyedMaterials()
		{
			int num = 0;
			float timeSinceStartup = m_timeProvider.TimeSinceStartup;
			m_tempRemoveMaterial.Clear();
			int num2 = 0;
			foreach (int unusedMaterial in m_unusedMaterials)
			{
				if (!m_customMaterials.TryGetValue(unusedMaterial, out var value))
				{
					continue;
				}
				if (value.TimesUsed == 0 && value.TimeToRemove != 0f && value.TimeToRemove < timeSinceStartup)
				{
					if ((bool)value.Material)
					{
						if (m_isAppPlaying)
						{
							UnityEngine.Object.Destroy(value.Material);
						}
						else
						{
							UnityEngine.Object.DestroyImmediate(value.Material);
						}
						num++;
					}
					m_customMaterials.Remove(unusedMaterial);
				}
				else if (value.TimesUsed == 0)
				{
					m_tempRemoveMaterial.Add(unusedMaterial);
				}
				num2++;
			}
			m_unusedMaterials.Clear();
			if (m_tempRemoveMaterial.Count <= 0)
			{
				return;
			}
			foreach (int item in m_tempRemoveMaterial)
			{
				m_unusedMaterials.Add(item);
			}
		}
	}
}
