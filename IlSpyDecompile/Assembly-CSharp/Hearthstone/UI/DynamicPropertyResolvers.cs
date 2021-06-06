using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	public static class DynamicPropertyResolvers
	{
		private static Dictionary<Type, IDynamicPropertyResolverProxy> s_proxies = new Dictionary<Type, IDynamicPropertyResolverProxy>
		{
			{
				typeof(Transform),
				new TransformDynamicPropertyResolverProxy()
			},
			{
				typeof(MeshRenderer),
				new RendererDynamicPropertyResolverProxy()
			},
			{
				typeof(ParticleSystemRenderer),
				new RendererDynamicPropertyResolverProxy()
			},
			{
				typeof(SkinnedMeshRenderer),
				new RendererDynamicPropertyResolverProxy()
			},
			{
				typeof(PlayMakerFSM),
				new PlayMakerDynamicPropertyResolverProxy()
			},
			{
				typeof(ParticleSystem),
				new ParticleSystemDynamicPropertyResolverProxy()
			}
		};

		public static IDynamicPropertyResolver TryGetResolver(object target)
		{
			if (target == null)
			{
				return null;
			}
			IDynamicPropertyResolver dynamicPropertyResolver = target as IDynamicPropertyResolver;
			if (dynamicPropertyResolver != null)
			{
				return dynamicPropertyResolver;
			}
			if (s_proxies.TryGetValue(target.GetType(), out var value))
			{
				value.SetTarget(target);
				return value;
			}
			return null;
		}
	}
}
