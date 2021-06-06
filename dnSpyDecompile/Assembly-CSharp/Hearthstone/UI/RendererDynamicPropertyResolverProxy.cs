using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200100B RID: 4107
	public class RendererDynamicPropertyResolverProxy : IDynamicPropertyResolverProxy, IDynamicPropertyResolver
	{
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x0600B2C9 RID: 45769 RVA: 0x003712C9 File Offset: 0x0036F4C9
		public ICollection<DynamicPropertyInfo> DynamicProperties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B2CA RID: 45770 RVA: 0x003712D4 File Offset: 0x0036F4D4
		public void SetTarget(object target)
		{
			this.m_renderer = (Renderer)target;
			this.m_properties.Clear();
			this.m_properties.Add(new DynamicPropertyInfo
			{
				Id = "enabled",
				Name = "Enabled",
				Type = typeof(bool),
				Value = this.m_renderer.enabled
			});
			List<Material> sharedMaterials = this.m_renderer.GetSharedMaterials();
			for (int i = 0; i < sharedMaterials.Count; i++)
			{
				string text = "Materials[" + i + "]";
				Material material = sharedMaterials[i];
				this.m_properties.Add(new DynamicPropertyInfo
				{
					Id = text,
					Name = text,
					Type = typeof(Material),
					Value = material
				});
				if (material != null && material.HasProperty("_MainTex"))
				{
					this.m_properties.Add(new DynamicPropertyInfo
					{
						Id = text + ".texture",
						Name = text + ".texture",
						Type = typeof(Texture2D),
						Value = material.mainTexture
					});
					this.m_properties.Add(new DynamicPropertyInfo
					{
						Id = text + ".textureTiling",
						Name = text + ".textureTiling",
						Type = typeof(Vector2),
						Value = material.mainTextureScale
					});
					this.m_properties.Add(new DynamicPropertyInfo
					{
						Id = text + ".textureOffset",
						Name = text + ".textureOffset",
						Type = typeof(Vector2),
						Value = material.mainTextureOffset
					});
					this.m_properties.Add(new DynamicPropertyInfo
					{
						Id = text + ".textureOffset_x",
						Name = text + ".textureOffset_X",
						Type = typeof(float),
						Value = material.mainTextureOffset.x
					});
					this.m_properties.Add(new DynamicPropertyInfo
					{
						Id = text + ".textureOffset_y",
						Name = text + ".textureOffset_Y",
						Type = typeof(float),
						Value = material.mainTextureOffset.y
					});
				}
				if (material != null && material.HasProperty("_Color"))
				{
					this.m_properties.Add(new DynamicPropertyInfo
					{
						Id = text + ".color",
						Name = text + ".color",
						Type = typeof(Color),
						Value = material.color
					});
				}
			}
		}

		// Token: 0x0600B2CB RID: 45771 RVA: 0x003715E4 File Offset: 0x0036F7E4
		public bool GetDynamicPropertyValue(string id, out object value)
		{
			value = null;
			if (id == "enabled")
			{
				value = this.m_renderer.enabled;
				return true;
			}
			if (id == "sharedMaterial")
			{
				value = this.m_renderer.GetSharedMaterial();
				return true;
			}
			int materialIndex = -1;
			if (id.Contains("Materials") && this.GetIndex(id, out materialIndex))
			{
				Material sharedMaterial = this.m_renderer.GetSharedMaterial(materialIndex);
				value = sharedMaterial;
				if (sharedMaterial != null)
				{
					string materialPropertyId;
					if (id.EndsWith(".color"))
					{
						value = sharedMaterial.color;
					}
					else if (id.EndsWith(".texture"))
					{
						value = sharedMaterial.mainTexture;
					}
					else if (id.EndsWith(".textureOffset"))
					{
						value = sharedMaterial.mainTextureOffset;
					}
					else if (id.EndsWith(".textureOffset_x"))
					{
						value = sharedMaterial.mainTextureOffset.x;
					}
					else if (id.EndsWith(".textureOffset_y"))
					{
						value = sharedMaterial.mainTextureOffset.y;
					}
					else if (id.EndsWith(".textureTiling"))
					{
						value = sharedMaterial.mainTextureScale;
					}
					else if (!string.IsNullOrEmpty(materialPropertyId = this.GetMaterialPropertyId(id)) && sharedMaterial.HasProperty(materialPropertyId))
					{
						value = sharedMaterial.GetFloat(materialPropertyId);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600B2CC RID: 45772 RVA: 0x00371754 File Offset: 0x0036F954
		public bool SetDynamicPropertyValue(string id, object value)
		{
			if (id == "enabled")
			{
				this.m_renderer.enabled = (bool)value;
				return true;
			}
			if (id == "sharedMaterial")
			{
				this.m_renderer.SetSharedMaterial((Material)value);
				return true;
			}
			int num = -1;
			List<Material> sharedMaterials = this.m_renderer.GetSharedMaterials();
			if (id.Contains("Materials") && this.GetIndex(id, out num) && num < sharedMaterials.Count)
			{
				if (id.EndsWith("]"))
				{
					List<Material> list = sharedMaterials;
					Material value2 = (Material)value;
					list[num] = value2;
					this.m_renderer.SetSharedMaterials(list);
				}
				else if (value != null)
				{
					Material materialInstance = this.GetMaterialInstance(this.m_renderer, num);
					if (materialInstance != null)
					{
						string materialPropertyId;
						if (id.EndsWith(".color"))
						{
							materialInstance.color = (Color)value;
						}
						else if (id.EndsWith(".texture"))
						{
							materialInstance.mainTexture = (value as Texture);
						}
						else if (id.EndsWith(".textureOffset"))
						{
							materialInstance.mainTextureOffset = (Vector4)value;
						}
						else if (id.EndsWith(".textureOffset_x"))
						{
							materialInstance.mainTextureOffset = new Vector2((float)value, materialInstance.mainTextureOffset.y);
						}
						else if (id.EndsWith(".textureOffset_y"))
						{
							materialInstance.mainTextureOffset = new Vector2(materialInstance.mainTextureOffset.x, (float)value);
						}
						else if (id.EndsWith(".textureTiling"))
						{
							materialInstance.mainTextureScale = (Vector4)value;
						}
						else if (!string.IsNullOrEmpty(materialPropertyId = this.GetMaterialPropertyId(id)) && materialInstance.HasProperty(materialPropertyId))
						{
							materialInstance.SetFloat(materialPropertyId, (value is float) ? ((float)value) : ((value is double) ? ((float)((double)value)) : 0f));
						}
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600B2CD RID: 45773 RVA: 0x0037195C File Offset: 0x0036FB5C
		private Material GetMaterialInstance(Renderer renderer, int index)
		{
			Material sharedMaterial = renderer.GetSharedMaterial(index);
			Material material = sharedMaterial;
			if (sharedMaterial.name[0] != '?')
			{
				material = new Material(sharedMaterial);
				material.name = "?" + material.name;
				renderer.SetSharedMaterial(index, material);
			}
			return material;
		}

		// Token: 0x0600B2CE RID: 45774 RVA: 0x003719AC File Offset: 0x0036FBAC
		private string GetMaterialPropertyId(string idPath)
		{
			string[] array = idPath.Split(new char[]
			{
				'.'
			});
			if (array.Length != 2)
			{
				return "";
			}
			return array[1];
		}

		// Token: 0x0600B2CF RID: 45775 RVA: 0x003719DC File Offset: 0x0036FBDC
		private bool GetIndex(string id, out int index)
		{
			index = 0;
			int num = id.IndexOf('[') + 1;
			if (num == 0)
			{
				return false;
			}
			int num2 = id.IndexOf(']', num) - 1;
			if (num2 < num)
			{
				return false;
			}
			int num3 = num2 - num;
			for (int i = num; i <= num2; i++)
			{
				int num4 = (int)(id[i] - '0');
				index += num4 * (int)Mathf.Pow(10f, (float)num3);
				num3--;
			}
			return true;
		}

		// Token: 0x04009640 RID: 38464
		private const string MaterialTexturePath = ".texture";

		// Token: 0x04009641 RID: 38465
		private const string MaterialTextureTilingPath = ".textureTiling";

		// Token: 0x04009642 RID: 38466
		private const string MaterialTextureOffsetPath = ".textureOffset";

		// Token: 0x04009643 RID: 38467
		private const string MaterialColorPath = ".color";

		// Token: 0x04009644 RID: 38468
		private Renderer m_renderer;

		// Token: 0x04009645 RID: 38469
		private List<DynamicPropertyInfo> m_properties = new List<DynamicPropertyInfo>();
	}
}
