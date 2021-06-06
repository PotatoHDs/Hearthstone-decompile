using System;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;

namespace Hearthstone.UI
{
	public class PlayMakerDynamicPropertyResolverProxy : IDynamicPropertyResolverProxy, IDynamicPropertyResolver
	{
		private PlayMakerFSM m_fsm;

		private List<DynamicPropertyInfo> m_properties = new List<DynamicPropertyInfo>();

		public ICollection<DynamicPropertyInfo> DynamicProperties => m_properties;

		public void SetTarget(object target)
		{
			m_fsm = (PlayMakerFSM)target;
			m_properties.Clear();
			if (!(m_fsm != null) || m_fsm.FsmVariables == null)
			{
				return;
			}
			NamedVariable[] allNamedVariables = m_fsm.FsmVariables.GetAllNamedVariables();
			foreach (NamedVariable namedVariable in allNamedVariables)
			{
				string name = namedVariable.Name;
				switch (namedVariable.VariableType)
				{
				case VariableType.Int:
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = name,
						Name = name,
						Type = typeof(int),
						Value = namedVariable.RawValue
					});
					break;
				case VariableType.Float:
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = name,
						Name = name,
						Type = typeof(float),
						Value = namedVariable.RawValue
					});
					break;
				case VariableType.Color:
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = name,
						Name = name,
						Type = typeof(Color),
						Value = namedVariable.RawValue
					});
					break;
				case VariableType.String:
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = name,
						Name = name,
						Type = typeof(string),
						Value = namedVariable.RawValue
					});
					break;
				case VariableType.Bool:
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = name,
						Name = name,
						Type = typeof(bool),
						Value = namedVariable.RawValue
					});
					break;
				case VariableType.Vector3:
				{
					Vector3 vector2 = (Vector3)namedVariable.RawValue;
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = name + ".x",
						Name = name + ".x",
						Type = typeof(float),
						Value = vector2.x
					});
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = name + ".y",
						Name = name + ".y",
						Type = typeof(float),
						Value = vector2.y
					});
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = name + ".z",
						Name = name + ".z",
						Type = typeof(float),
						Value = vector2.z
					});
					break;
				}
				case VariableType.Vector2:
				{
					Vector2 vector = (Vector2)namedVariable.RawValue;
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = name + ".x",
						Name = name + ".x",
						Type = typeof(float),
						Value = vector.x
					});
					m_properties.Add(new DynamicPropertyInfo
					{
						Id = name + ".y",
						Name = name + ".y",
						Type = typeof(float),
						Value = vector.y
					});
					break;
				}
				}
			}
		}

		public bool GetDynamicPropertyValue(string id, out object value)
		{
			value = null;
			if (m_fsm != null && m_fsm.FsmVariables != null && id != null)
			{
				GetIdAndDimension(ref id, out var dimension);
				NamedVariable variable = m_fsm.FsmVariables.GetVariable(id);
				if (variable != null)
				{
					value = variable.RawValue;
					switch (variable.VariableType)
					{
					case VariableType.Vector2:
						switch (dimension)
						{
						case 'x':
							value = ((Vector2)variable.RawValue).x;
							break;
						case 'y':
							value = ((Vector2)variable.RawValue).y;
							break;
						}
						break;
					case VariableType.Vector3:
						switch (dimension)
						{
						case 'x':
							value = ((Vector3)variable.RawValue).x;
							break;
						case 'y':
							value = ((Vector3)variable.RawValue).y;
							break;
						case 'z':
							value = ((Vector3)variable.RawValue).z;
							break;
						}
						break;
					}
					return true;
				}
			}
			return false;
		}

		public bool SetDynamicPropertyValue(string id, object value)
		{
			if (m_fsm != null && Application.IsPlaying(m_fsm) && m_fsm.FsmVariables != null && id != null)
			{
				GetIdAndDimension(ref id, out var dimension);
				NamedVariable variable = m_fsm.FsmVariables.GetVariable(id);
				if (variable != null)
				{
					switch (variable.VariableType)
					{
					case VariableType.Int:
						((FsmInt)variable).RawValue = value;
						break;
					case VariableType.Float:
						((FsmFloat)variable).RawValue = value;
						break;
					case VariableType.String:
						((FsmString)variable).RawValue = value;
						break;
					case VariableType.Bool:
						((FsmBool)variable).RawValue = value;
						break;
					case VariableType.Color:
						((FsmColor)variable).RawValue = value;
						break;
					case VariableType.Vector2:
					{
						Vector2 value3 = (Vector2)variable.RawValue;
						switch (dimension)
						{
						case 'x':
							value3.x = (float)value;
							break;
						case 'y':
							value3.y = (float)value;
							break;
						}
						((FsmVector2)variable).Value = value3;
						break;
					}
					case VariableType.Vector3:
					{
						Vector3 value2 = (Vector3)variable.RawValue;
						switch (dimension)
						{
						case 'x':
							value2.x = (float)value;
							break;
						case 'y':
							value2.y = (float)value;
							break;
						case 'z':
							value2.z = (float)value;
							break;
						}
						((FsmVector3)variable).Value = value2;
						break;
					}
					default:
						return false;
					}
					return true;
				}
			}
			return false;
		}

		private static void GetIdAndDimension(ref string id, out char dimension)
		{
			dimension = '\0';
			int num = id.IndexOf(".", StringComparison.Ordinal);
			if (num > 0)
			{
				dimension = id[id.Length - 1];
				id = id.Remove(num);
			}
		}
	}
}
