using System;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200100A RID: 4106
	public class PlayMakerDynamicPropertyResolverProxy : IDynamicPropertyResolverProxy, IDynamicPropertyResolver
	{
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600B2C3 RID: 45763 RVA: 0x00370C19 File Offset: 0x0036EE19
		public ICollection<DynamicPropertyInfo> DynamicProperties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600B2C4 RID: 45764 RVA: 0x00370C24 File Offset: 0x0036EE24
		public void SetTarget(object target)
		{
			this.m_fsm = (PlayMakerFSM)target;
			this.m_properties.Clear();
			if (this.m_fsm != null && this.m_fsm.FsmVariables != null)
			{
				foreach (NamedVariable namedVariable in this.m_fsm.FsmVariables.GetAllNamedVariables())
				{
					string name = namedVariable.Name;
					switch (namedVariable.VariableType)
					{
					case VariableType.Float:
						this.m_properties.Add(new DynamicPropertyInfo
						{
							Id = name,
							Name = name,
							Type = typeof(float),
							Value = namedVariable.RawValue
						});
						break;
					case VariableType.Int:
						this.m_properties.Add(new DynamicPropertyInfo
						{
							Id = name,
							Name = name,
							Type = typeof(int),
							Value = namedVariable.RawValue
						});
						break;
					case VariableType.Bool:
						this.m_properties.Add(new DynamicPropertyInfo
						{
							Id = name,
							Name = name,
							Type = typeof(bool),
							Value = namedVariable.RawValue
						});
						break;
					case VariableType.String:
						this.m_properties.Add(new DynamicPropertyInfo
						{
							Id = name,
							Name = name,
							Type = typeof(string),
							Value = namedVariable.RawValue
						});
						break;
					case VariableType.Vector2:
					{
						Vector2 vector = (Vector2)namedVariable.RawValue;
						this.m_properties.Add(new DynamicPropertyInfo
						{
							Id = name + ".x",
							Name = name + ".x",
							Type = typeof(float),
							Value = vector.x
						});
						this.m_properties.Add(new DynamicPropertyInfo
						{
							Id = name + ".y",
							Name = name + ".y",
							Type = typeof(float),
							Value = vector.y
						});
						break;
					}
					case VariableType.Vector3:
					{
						Vector3 vector2 = (Vector3)namedVariable.RawValue;
						this.m_properties.Add(new DynamicPropertyInfo
						{
							Id = name + ".x",
							Name = name + ".x",
							Type = typeof(float),
							Value = vector2.x
						});
						this.m_properties.Add(new DynamicPropertyInfo
						{
							Id = name + ".y",
							Name = name + ".y",
							Type = typeof(float),
							Value = vector2.y
						});
						this.m_properties.Add(new DynamicPropertyInfo
						{
							Id = name + ".z",
							Name = name + ".z",
							Type = typeof(float),
							Value = vector2.z
						});
						break;
					}
					case VariableType.Color:
						this.m_properties.Add(new DynamicPropertyInfo
						{
							Id = name,
							Name = name,
							Type = typeof(Color),
							Value = namedVariable.RawValue
						});
						break;
					}
				}
			}
		}

		// Token: 0x0600B2C5 RID: 45765 RVA: 0x00370FCC File Offset: 0x0036F1CC
		public bool GetDynamicPropertyValue(string id, out object value)
		{
			value = null;
			if (this.m_fsm != null && this.m_fsm.FsmVariables != null && id != null)
			{
				char c;
				PlayMakerDynamicPropertyResolverProxy.GetIdAndDimension(ref id, out c);
				NamedVariable variable = this.m_fsm.FsmVariables.GetVariable(id);
				if (variable != null)
				{
					value = variable.RawValue;
					VariableType variableType = variable.VariableType;
					if (variableType != VariableType.Vector2)
					{
						if (variableType == VariableType.Vector3)
						{
							switch (c)
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
						}
					}
					else if (c != 'x')
					{
						if (c == 'y')
						{
							value = ((Vector2)variable.RawValue).y;
						}
					}
					else
					{
						value = ((Vector2)variable.RawValue).x;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600B2C6 RID: 45766 RVA: 0x003710E4 File Offset: 0x0036F2E4
		public bool SetDynamicPropertyValue(string id, object value)
		{
			if (this.m_fsm != null && Application.IsPlaying(this.m_fsm) && this.m_fsm.FsmVariables != null && id != null)
			{
				char c;
				PlayMakerDynamicPropertyResolverProxy.GetIdAndDimension(ref id, out c);
				NamedVariable variable = this.m_fsm.FsmVariables.GetVariable(id);
				if (variable != null)
				{
					switch (variable.VariableType)
					{
					case VariableType.Float:
						((FsmFloat)variable).RawValue = value;
						return true;
					case VariableType.Int:
						((FsmInt)variable).RawValue = value;
						return true;
					case VariableType.Bool:
						((FsmBool)variable).RawValue = value;
						return true;
					case VariableType.String:
						((FsmString)variable).RawValue = value;
						return true;
					case VariableType.Vector2:
					{
						Vector2 value2 = (Vector2)variable.RawValue;
						if (c != 'x')
						{
							if (c == 'y')
							{
								value2.y = (float)value;
							}
						}
						else
						{
							value2.x = (float)value;
						}
						((FsmVector2)variable).Value = value2;
						return true;
					}
					case VariableType.Vector3:
					{
						Vector3 value3 = (Vector3)variable.RawValue;
						switch (c)
						{
						case 'x':
							value3.x = (float)value;
							break;
						case 'y':
							value3.y = (float)value;
							break;
						case 'z':
							value3.z = (float)value;
							break;
						}
						((FsmVector3)variable).Value = value3;
						return true;
					}
					case VariableType.Color:
						((FsmColor)variable).RawValue = value;
						return true;
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x0600B2C7 RID: 45767 RVA: 0x00371278 File Offset: 0x0036F478
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

		// Token: 0x0400963E RID: 38462
		private PlayMakerFSM m_fsm;

		// Token: 0x0400963F RID: 38463
		private List<DynamicPropertyInfo> m_properties = new List<DynamicPropertyInfo>();
	}
}
