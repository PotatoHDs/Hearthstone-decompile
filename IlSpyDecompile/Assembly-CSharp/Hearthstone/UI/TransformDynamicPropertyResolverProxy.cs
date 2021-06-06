using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	public class TransformDynamicPropertyResolverProxy : IDynamicPropertyResolverProxy, IDynamicPropertyResolver
	{
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

		private Transform m_transform;

		public ICollection<DynamicPropertyInfo> DynamicProperties => m_dynamicProperties;

		public void SetTarget(object target)
		{
			m_transform = (Transform)target;
			m_dynamicProperties[0].Value = m_transform.localPosition;
			m_dynamicProperties[1].Value = m_transform.localRotation.eulerAngles;
			m_dynamicProperties[2].Value = m_transform.localScale;
			m_dynamicProperties[3].Value = m_transform.localPosition.x;
			m_dynamicProperties[4].Value = m_transform.localPosition.y;
			m_dynamicProperties[5].Value = m_transform.localPosition.z;
			m_dynamicProperties[6].Value = m_transform.localRotation.eulerAngles.x;
			m_dynamicProperties[7].Value = m_transform.localRotation.eulerAngles.y;
			m_dynamicProperties[8].Value = m_transform.localRotation.eulerAngles.z;
			m_dynamicProperties[9].Value = m_transform.localScale.x;
			m_dynamicProperties[10].Value = m_transform.localScale.y;
			m_dynamicProperties[11].Value = m_transform.localScale.z;
		}

		public bool GetDynamicPropertyValue(string id, out object value)
		{
			value = null;
			switch (id)
			{
			case "position":
				value = m_transform.localPosition;
				return true;
			case "scale":
				value = m_transform.localScale;
				return true;
			case "rotation":
				value = m_transform.localRotation.eulerAngles;
				return true;
			case "position_x":
				value = m_transform.localPosition.x;
				return true;
			case "position_y":
				value = m_transform.localPosition.y;
				return true;
			case "position_z":
				value = m_transform.localPosition.z;
				return true;
			case "rotation_x":
				value = m_transform.localRotation.x;
				return true;
			case "rotation_y":
				value = m_transform.localRotation.y;
				return true;
			case "rotation_z":
				value = m_transform.localRotation.z;
				return true;
			case "scale_x":
				value = m_transform.localScale.x;
				return true;
			case "scale_y":
				value = m_transform.localScale.y;
				return true;
			case "scale_z":
				value = m_transform.localScale.z;
				return true;
			default:
				return false;
			}
		}

		public bool SetDynamicPropertyValue(string id, object value)
		{
			switch (id)
			{
			case "position":
				m_transform.localPosition = new Vector3(((Vector4)value).x, ((Vector4)value).y, ((Vector4)value).z);
				return true;
			case "rotation":
			{
				Vector3 euler = new Vector3(((Vector4)value).x, ((Vector4)value).y, ((Vector4)value).z);
				m_transform.localRotation = Quaternion.Euler(euler);
				return true;
			}
			case "scale":
				m_transform.localScale = new Vector3(((Vector4)value).x, ((Vector4)value).y, ((Vector4)value).z);
				return true;
			case "position_x":
			{
				Vector3 localPosition3 = m_transform.localPosition;
				localPosition3.x = (float)value;
				m_transform.localPosition = localPosition3;
				return true;
			}
			case "position_y":
			{
				Vector3 localPosition2 = m_transform.localPosition;
				localPosition2.y = (float)value;
				m_transform.localPosition = localPosition2;
				return true;
			}
			case "position_z":
			{
				Vector3 localPosition = m_transform.localPosition;
				localPosition.z = (float)value;
				m_transform.localPosition = localPosition;
				return true;
			}
			case "rotation_x":
			{
				Quaternion localRotation3 = m_transform.localRotation;
				Vector3 eulerAngles3 = localRotation3.eulerAngles;
				eulerAngles3.x = (float)value;
				localRotation3.eulerAngles = eulerAngles3;
				m_transform.localRotation = localRotation3;
				return true;
			}
			case "rotation_y":
			{
				Quaternion localRotation2 = m_transform.localRotation;
				Vector3 eulerAngles2 = localRotation2.eulerAngles;
				eulerAngles2.y = (float)value;
				localRotation2.eulerAngles = eulerAngles2;
				m_transform.localRotation = localRotation2;
				return true;
			}
			case "rotation_z":
			{
				Quaternion localRotation = m_transform.localRotation;
				Vector3 eulerAngles = localRotation.eulerAngles;
				eulerAngles.z = (float)value;
				localRotation.eulerAngles = eulerAngles;
				m_transform.localRotation = localRotation;
				return true;
			}
			case "scale_x":
			{
				Vector3 localScale3 = m_transform.localScale;
				localScale3.x = (float)value;
				m_transform.localScale = localScale3;
				return true;
			}
			case "scale_y":
			{
				Vector3 localScale2 = m_transform.localScale;
				localScale2.y = (float)value;
				m_transform.localScale = localScale2;
				return true;
			}
			case "scale_z":
			{
				Vector3 localScale = m_transform.localScale;
				localScale.z = (float)value;
				m_transform.localScale = localScale;
				return true;
			}
			default:
				return false;
			}
		}
	}
}
