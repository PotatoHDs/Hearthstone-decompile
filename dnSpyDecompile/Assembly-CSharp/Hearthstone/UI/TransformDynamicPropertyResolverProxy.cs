using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200100C RID: 4108
	public class TransformDynamicPropertyResolverProxy : IDynamicPropertyResolverProxy, IDynamicPropertyResolver
	{
		// Token: 0x0600B2D1 RID: 45777 RVA: 0x00371A58 File Offset: 0x0036FC58
		public void SetTarget(object target)
		{
			this.m_transform = (Transform)target;
			this.m_dynamicProperties[0].Value = this.m_transform.localPosition;
			this.m_dynamicProperties[1].Value = this.m_transform.localRotation.eulerAngles;
			this.m_dynamicProperties[2].Value = this.m_transform.localScale;
			this.m_dynamicProperties[3].Value = this.m_transform.localPosition.x;
			this.m_dynamicProperties[4].Value = this.m_transform.localPosition.y;
			this.m_dynamicProperties[5].Value = this.m_transform.localPosition.z;
			this.m_dynamicProperties[6].Value = this.m_transform.localRotation.eulerAngles.x;
			this.m_dynamicProperties[7].Value = this.m_transform.localRotation.eulerAngles.y;
			this.m_dynamicProperties[8].Value = this.m_transform.localRotation.eulerAngles.z;
			this.m_dynamicProperties[9].Value = this.m_transform.localScale.x;
			this.m_dynamicProperties[10].Value = this.m_transform.localScale.y;
			this.m_dynamicProperties[11].Value = this.m_transform.localScale.z;
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x0600B2D2 RID: 45778 RVA: 0x00371C4D File Offset: 0x0036FE4D
		public ICollection<DynamicPropertyInfo> DynamicProperties
		{
			get
			{
				return this.m_dynamicProperties;
			}
		}

		// Token: 0x0600B2D3 RID: 45779 RVA: 0x00371C58 File Offset: 0x0036FE58
		public bool GetDynamicPropertyValue(string id, out object value)
		{
			value = null;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(id);
			if (num <= 1485142914U)
			{
				if (num <= 1204023382U)
				{
					if (num != 564937055U)
					{
						if (num != 1170468144U)
						{
							if (num == 1204023382U)
							{
								if (id == "scale_x")
								{
									value = this.m_transform.localScale.x;
									return true;
								}
							}
						}
						else if (id == "scale_z")
						{
							value = this.m_transform.localScale.z;
							return true;
						}
					}
					else if (id == "rotation")
					{
						value = this.m_transform.localRotation.eulerAngles;
						return true;
					}
				}
				else if (num != 1220801001U)
				{
					if (num != 1468365295U)
					{
						if (num == 1485142914U)
						{
							if (id == "position_y")
							{
								value = this.m_transform.localPosition.y;
								return true;
							}
						}
					}
					else if (id == "position_z")
					{
						value = this.m_transform.localPosition.z;
						return true;
					}
				}
				else if (id == "scale_y")
				{
					value = this.m_transform.localScale.y;
					return true;
				}
			}
			else if (num <= 2471448074U)
			{
				if (num != 1501920533U)
				{
					if (num != 2190941297U)
					{
						if (num == 2471448074U)
						{
							if (id == "position")
							{
								value = this.m_transform.localPosition;
								return true;
							}
						}
					}
					else if (id == "scale")
					{
						value = this.m_transform.localScale;
						return true;
					}
				}
				else if (id == "position_x")
				{
					value = this.m_transform.localPosition.x;
					return true;
				}
			}
			else if (num != 3768673768U)
			{
				if (num != 3785451387U)
				{
					if (num == 3802229006U)
					{
						if (id == "rotation_z")
						{
							value = this.m_transform.localRotation.z;
							return true;
						}
					}
				}
				else if (id == "rotation_y")
				{
					value = this.m_transform.localRotation.y;
					return true;
				}
			}
			else if (id == "rotation_x")
			{
				value = this.m_transform.localRotation.x;
				return true;
			}
			return false;
		}

		// Token: 0x0600B2D4 RID: 45780 RVA: 0x00371F38 File Offset: 0x00370138
		public bool SetDynamicPropertyValue(string id, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(id);
			if (num <= 1485142914U)
			{
				if (num <= 1204023382U)
				{
					if (num != 564937055U)
					{
						if (num != 1170468144U)
						{
							if (num == 1204023382U)
							{
								if (id == "scale_x")
								{
									Vector3 localScale = this.m_transform.localScale;
									localScale.x = (float)value;
									this.m_transform.localScale = localScale;
									return true;
								}
							}
						}
						else if (id == "scale_z")
						{
							Vector3 localScale2 = this.m_transform.localScale;
							localScale2.z = (float)value;
							this.m_transform.localScale = localScale2;
							return true;
						}
					}
					else if (id == "rotation")
					{
						Vector3 euler = new Vector3(((Vector4)value).x, ((Vector4)value).y, ((Vector4)value).z);
						this.m_transform.localRotation = Quaternion.Euler(euler);
						return true;
					}
				}
				else if (num != 1220801001U)
				{
					if (num != 1468365295U)
					{
						if (num == 1485142914U)
						{
							if (id == "position_y")
							{
								Vector3 localPosition = this.m_transform.localPosition;
								localPosition.y = (float)value;
								this.m_transform.localPosition = localPosition;
								return true;
							}
						}
					}
					else if (id == "position_z")
					{
						Vector3 localPosition2 = this.m_transform.localPosition;
						localPosition2.z = (float)value;
						this.m_transform.localPosition = localPosition2;
						return true;
					}
				}
				else if (id == "scale_y")
				{
					Vector3 localScale3 = this.m_transform.localScale;
					localScale3.y = (float)value;
					this.m_transform.localScale = localScale3;
					return true;
				}
			}
			else if (num <= 2471448074U)
			{
				if (num != 1501920533U)
				{
					if (num != 2190941297U)
					{
						if (num == 2471448074U)
						{
							if (id == "position")
							{
								this.m_transform.localPosition = new Vector3(((Vector4)value).x, ((Vector4)value).y, ((Vector4)value).z);
								return true;
							}
						}
					}
					else if (id == "scale")
					{
						this.m_transform.localScale = new Vector3(((Vector4)value).x, ((Vector4)value).y, ((Vector4)value).z);
						return true;
					}
				}
				else if (id == "position_x")
				{
					Vector3 localPosition3 = this.m_transform.localPosition;
					localPosition3.x = (float)value;
					this.m_transform.localPosition = localPosition3;
					return true;
				}
			}
			else if (num != 3768673768U)
			{
				if (num != 3785451387U)
				{
					if (num == 3802229006U)
					{
						if (id == "rotation_z")
						{
							Quaternion localRotation = this.m_transform.localRotation;
							Vector3 eulerAngles = localRotation.eulerAngles;
							eulerAngles.z = (float)value;
							localRotation.eulerAngles = eulerAngles;
							this.m_transform.localRotation = localRotation;
							return true;
						}
					}
				}
				else if (id == "rotation_y")
				{
					Quaternion localRotation2 = this.m_transform.localRotation;
					Vector3 eulerAngles2 = localRotation2.eulerAngles;
					eulerAngles2.y = (float)value;
					localRotation2.eulerAngles = eulerAngles2;
					this.m_transform.localRotation = localRotation2;
					return true;
				}
			}
			else if (id == "rotation_x")
			{
				Quaternion localRotation3 = this.m_transform.localRotation;
				Vector3 eulerAngles3 = localRotation3.eulerAngles;
				eulerAngles3.x = (float)value;
				localRotation3.eulerAngles = eulerAngles3;
				this.m_transform.localRotation = localRotation3;
				return true;
			}
			return false;
		}

		// Token: 0x04009646 RID: 38470
		private List<DynamicPropertyInfo> m_dynamicProperties = new List<DynamicPropertyInfo>
		{
			new DynamicPropertyInfo
			{
				Id = "position",
				Name = "Position",
				Type = typeof(Vector3),
				Value = Vector3.zero
			},
			new DynamicPropertyInfo
			{
				Id = "rotation",
				Name = "Rotation",
				Type = typeof(Vector3),
				Value = Vector3.zero
			},
			new DynamicPropertyInfo
			{
				Id = "scale",
				Name = "Scale",
				Type = typeof(Vector3),
				Value = Vector3.zero
			},
			new DynamicPropertyInfo
			{
				Id = "position_x",
				Name = "Position_X",
				Type = typeof(float),
				Value = 0f
			},
			new DynamicPropertyInfo
			{
				Id = "position_y",
				Name = "Position_Y",
				Type = typeof(float),
				Value = 0f
			},
			new DynamicPropertyInfo
			{
				Id = "position_z",
				Name = "Position_Z",
				Type = typeof(float),
				Value = 0f
			},
			new DynamicPropertyInfo
			{
				Id = "rotation_x",
				Name = "Rotation_X",
				Type = typeof(float),
				Value = 0f
			},
			new DynamicPropertyInfo
			{
				Id = "rotation_y",
				Name = "Rotation_Y",
				Type = typeof(float),
				Value = 0f
			},
			new DynamicPropertyInfo
			{
				Id = "rotation_z",
				Name = "Rotation_Z",
				Type = typeof(float),
				Value = 0f
			},
			new DynamicPropertyInfo
			{
				Id = "scale_x",
				Name = "Scale_X",
				Type = typeof(float),
				Value = 0f
			},
			new DynamicPropertyInfo
			{
				Id = "scale_y",
				Name = "Scale_Y",
				Type = typeof(float),
				Value = 0f
			},
			new DynamicPropertyInfo
			{
				Id = "scale_z",
				Name = "Scale_Z",
				Type = typeof(float),
				Value = 0f
			}
		};

		// Token: 0x04009647 RID: 38471
		private Transform m_transform;
	}
}
