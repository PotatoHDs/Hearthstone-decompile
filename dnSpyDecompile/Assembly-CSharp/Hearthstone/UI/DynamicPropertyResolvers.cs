using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200100D RID: 4109
	public static class DynamicPropertyResolvers
	{
		// Token: 0x0600B2D6 RID: 45782 RVA: 0x00372660 File Offset: 0x00370860
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
			IDynamicPropertyResolverProxy dynamicPropertyResolverProxy;
			if (DynamicPropertyResolvers.s_proxies.TryGetValue(target.GetType(), out dynamicPropertyResolverProxy))
			{
				dynamicPropertyResolverProxy.SetTarget(target);
				return dynamicPropertyResolverProxy;
			}
			return null;
		}

		// Token: 0x04009648 RID: 38472
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
	}
}
