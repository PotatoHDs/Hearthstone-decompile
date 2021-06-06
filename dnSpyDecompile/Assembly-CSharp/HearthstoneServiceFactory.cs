using System;
using System.Collections.Generic;
using Blizzard.T5.Services;
using Hearthstone.UI;

// Token: 0x02000699 RID: 1689
public static class HearthstoneServiceFactory
{
	// Token: 0x06005E55 RID: 24149 RVA: 0x001EA748 File Offset: 0x001E8948
	public static IServiceFactory CreateServiceFactory()
	{
		return new HearthstoneServiceFactory.HearthstoneServiceFactoryPrivate();
	}

	// Token: 0x04004F87 RID: 20359
	public static readonly Dictionary<Type, Func<IService>> ServiceConstructors = new Dictionary<Type, Func<IService>>
	{
		{
			typeof(UniversalInputManager),
			() => new UniversalInputManager()
		},
		{
			typeof(SoundManager),
			() => new SoundManager()
		},
		{
			typeof(FullScreenFXMgr),
			() => new FullScreenFXMgr()
		},
		{
			typeof(IAssetLoader),
			() => new AssetLoader()
		},
		{
			typeof(IAliasedAssetResolver),
			() => new AliasedAssetResolver()
		},
		{
			typeof(FontTable),
			() => new FontTable()
		},
		{
			typeof(GraphicsManager),
			() => new GraphicsManager()
		},
		{
			typeof(ShaderTime),
			() => new ShaderTime()
		},
		{
			typeof(GameDbf),
			() => new GameDbf()
		},
		{
			typeof(WidgetRunner),
			() => new WidgetRunner()
		},
		{
			typeof(SpellCache),
			() => new SpellCache()
		},
		{
			typeof(ITouchScreenService),
			() => new W8Touch()
		}
	};

	// Token: 0x020021C1 RID: 8641
	private class HearthstoneServiceFactoryPrivate : IServiceFactory
	{
		// Token: 0x060124A8 RID: 74920 RVA: 0x00503D48 File Offset: 0x00501F48
		public bool TryCreateService(Type serviceType, out IService service)
		{
			Func<IService> func;
			if (HearthstoneServiceFactory.ServiceConstructors.TryGetValue(serviceType, out func))
			{
				service = func();
				return true;
			}
			service = null;
			return false;
		}
	}
}
