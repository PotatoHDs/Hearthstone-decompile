using System;
using System.Collections.Generic;
using System.Linq;
using Hearthstone.UI.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hearthstone.UI
{
	// Token: 0x02000FF2 RID: 4082
	public static class NestedReferenceUtils
	{
		// Token: 0x0600B16A RID: 45418 RVA: 0x0036B99C File Offset: 0x00369B9C
		public static Scene FindSceneThatGameObjectBelongsTo(GameObject gameObject)
		{
			Func<GameObject, bool> <>9__0;
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				Scene sceneAt = SceneManager.GetSceneAt(i);
				if (sceneAt.isLoaded)
				{
					IEnumerable<GameObject> rootGameObjects = sceneAt.GetRootGameObjects();
					Func<GameObject, bool> predicate;
					if ((predicate = <>9__0) == null)
					{
						predicate = (<>9__0 = ((GameObject a) => gameObject.transform.IsChildOf(a.transform) || a == gameObject.transform));
					}
					if (rootGameObjects.Any(predicate))
					{
						return sceneAt;
					}
				}
			}
			return default(Scene);
		}

		// Token: 0x0600B16B RID: 45419 RVA: 0x0036BA10 File Offset: 0x00369C10
		public static INestedReferenceResolver FindParentNestedResolver(Component component)
		{
			Transform transform = component.transform;
			while (transform != null)
			{
				INestedReferenceResolver nestedReferenceResolver = transform.GetComponents<Component>().FirstOrDefault((Component a) => a is INestedReferenceResolver) as INestedReferenceResolver;
				if (nestedReferenceResolver != null)
				{
					return nestedReferenceResolver;
				}
				transform = transform.parent;
			}
			return null;
		}

		// Token: 0x0600B16C RID: 45420 RVA: 0x0036BA6C File Offset: 0x00369C6C
		public static bool IsSupportedType(Type type)
		{
			return type.IsEnum || NestedReferenceUtils.s_targetTypeMap.ContainsKey(type);
		}

		// Token: 0x0600B16D RID: 45421 RVA: 0x0036BA84 File Offset: 0x00369C84
		public static NestedReference.TargetTypes ResolveTargetType(Type type)
		{
			if (type.IsEnum)
			{
				return NestedReference.TargetTypes.Enum;
			}
			NestedReference.TargetTypes result = NestedReference.TargetTypes.Unknown;
			if (NestedReferenceUtils.s_targetTypeMap.TryGetValue(type, out result))
			{
				return result;
			}
			return NestedReference.TargetTypes.Unknown;
		}

		// Token: 0x040095A5 RID: 38309
		private static readonly Map<Type, NestedReference.TargetTypes> s_targetTypeMap = new Map<Type, NestedReference.TargetTypes>
		{
			{
				typeof(string),
				NestedReference.TargetTypes.String
			},
			{
				typeof(Color),
				NestedReference.TargetTypes.Color
			},
			{
				typeof(float),
				NestedReference.TargetTypes.Float
			},
			{
				typeof(double),
				NestedReference.TargetTypes.Double
			},
			{
				typeof(long),
				NestedReference.TargetTypes.Long
			},
			{
				typeof(int),
				NestedReference.TargetTypes.Int
			},
			{
				typeof(bool),
				NestedReference.TargetTypes.Bool
			},
			{
				typeof(Material),
				NestedReference.TargetTypes.Material
			},
			{
				typeof(Texture2D),
				NestedReference.TargetTypes.Texture
			},
			{
				typeof(Vector2),
				NestedReference.TargetTypes.Vector2
			},
			{
				typeof(Vector3),
				NestedReference.TargetTypes.Vector3
			},
			{
				typeof(Vector4),
				NestedReference.TargetTypes.Vector4
			},
			{
				typeof(Mesh),
				NestedReference.TargetTypes.Mesh
			}
		};
	}
}
