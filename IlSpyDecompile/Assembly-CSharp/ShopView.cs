using System;
using System.Collections.Generic;
using System.Linq;

public class ShopView
{
	public interface IComponent
	{
		bool IsLoaded { get; }

		bool IsShown { get; }

		event Action OnComponentReady;

		void Load(IAssetLoader assetLoader);

		void Unload();

		void Hide();
	}

	private readonly List<IComponent> m_components;

	public PurchaseAuthView PurchaseAuth => FindComponent<PurchaseAuthView>();

	public SummaryView Summary => FindComponent<SummaryView>();

	public SendToBamView SendToBam => FindComponent<SendToBamView>();

	public LegalBamView LegalBam => FindComponent<LegalBamView>();

	public DoneWithBamView DoneWithBam => FindComponent<DoneWithBamView>();

	public ChallengePromptView ChallengePrompt => FindComponent<ChallengePromptView>();

	public bool HasStartedLoading { get; private set; }

	public bool IsLoaded => m_components.All((IComponent component) => component.IsLoaded);

	public bool IsPromptShowing => m_components.Any((IComponent component) => component.IsShown);

	public event Action OnComponentReady = delegate
	{
	};

	public ShopView()
	{
		m_components = new List<IComponent>
		{
			InitializeComponent<PurchaseAuthView>(),
			InitializeComponent<SummaryView>(),
			InitializeComponent<SendToBamView>(),
			InitializeComponent<LegalBamView>(),
			InitializeComponent<DoneWithBamView>(),
			InitializeComponent<ChallengePromptView>()
		};
	}

	public void LoadAssets()
	{
		if (!HasStartedLoading)
		{
			IAssetLoader assetLoader = AssetLoader.Get();
			m_components.ForEach(delegate(IComponent component)
			{
				component.Load(assetLoader);
			});
			HasStartedLoading = true;
		}
	}

	public void UnloadAssets()
	{
		m_components.ForEach(delegate(IComponent component)
		{
			component.Unload();
		});
		HasStartedLoading = false;
	}

	public void Hide()
	{
		m_components.ForEach(delegate(IComponent component)
		{
			component.Hide();
		});
	}

	private T InitializeComponent<T>() where T : IComponent, new()
	{
		T result = new T();
		result.OnComponentReady += HandleComponentReady;
		return result;
	}

	private void HandleComponentReady()
	{
		this.OnComponentReady();
	}

	private T FindComponent<T>() where T : class, IComponent
	{
		return m_components.FirstOrDefault((IComponent component) => component is T) as T;
	}
}
