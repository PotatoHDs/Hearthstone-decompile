using System;
using System.Collections.Generic;
using Blizzard.Telemetry.WTCG.Client;

// Token: 0x02000931 RID: 2353
public static class TelemetryWatcher
{
	// Token: 0x060081DE RID: 33246 RVA: 0x002A4274 File Offset: 0x002A2474
	public static void WatchFor(TelemetryWatcherWatchType watchType)
	{
		Action action;
		if (!TelemetryWatcher.s_watchTypeSetupActions.TryGetValue(watchType, out action))
		{
			Log.Telemetry.Print("Watching for type={0} is not currently supported", Array.Empty<object>());
			return;
		}
		object obj = TelemetryWatcher.s_watchLock;
		lock (obj)
		{
			if (TelemetryWatcher.s_currentlyWatching.Contains(watchType))
			{
				Log.Telemetry.Print("Already watching for type={0}", new object[]
				{
					watchType
				});
				return;
			}
			TelemetryWatcher.s_currentlyWatching.Add(watchType);
		}
		action();
		Log.Telemetry.Print("Watching for type={0}", new object[]
		{
			watchType
		});
	}

	// Token: 0x060081DF RID: 33247 RVA: 0x002A4330 File Offset: 0x002A2530
	public static void StopWatchingFor(TelemetryWatcherWatchType watchType)
	{
		object obj = TelemetryWatcher.s_watchLock;
		lock (obj)
		{
			if (!TelemetryWatcher.s_currentlyWatching.Remove(watchType))
			{
				Log.Telemetry.Print("Was not watching for type={0}", new object[]
				{
					watchType
				});
				return;
			}
		}
		Action action;
		if (TelemetryWatcher.s_watchTypeTeardownActions.TryGetValue(watchType, out action))
		{
			action();
		}
		Log.Telemetry.Print("No longer watching for type={0}", new object[]
		{
			watchType
		});
	}

	// Token: 0x060081E0 RID: 33248 RVA: 0x002A43CC File Offset: 0x002A25CC
	private static void OnScenePreloadedExclusiveWatch(SceneMgr.Mode nextMode, SceneMgr.Mode targetMode, TelemetryWatcherWatchType watchType)
	{
		Log.Telemetry.Print("Scene change detected while watching for type={0}.  Next scene={1}, Target={2}", new object[]
		{
			watchType,
			nextMode,
			targetMode
		});
		if (nextMode == targetMode)
		{
			return;
		}
		TelemetryWatcher.StopWatchingFor(watchType);
	}

	// Token: 0x060081E1 RID: 33249 RVA: 0x002A440C File Offset: 0x002A260C
	private static void OnBoxButtonPressedExclusiveWatch(Box.ButtonType buttonPressed, Box.ButtonType targetType, TelemetryWatcherWatchType watchType, Action onTargetPressed)
	{
		Log.Telemetry.Print("Button pressed on Box while watching for type={0}.  Button pressed={1}, target={2}", new object[]
		{
			watchType,
			buttonPressed,
			targetType
		});
		if (buttonPressed == targetType)
		{
			onTargetPressed();
		}
		TelemetryWatcher.StopWatchingFor(watchType);
	}

	// Token: 0x060081E2 RID: 33250 RVA: 0x002A4459 File Offset: 0x002A2659
	private static void WatchForStoreVisitFromPackOpening()
	{
		Box.Get().AddButtonPressListener(new Box.ButtonPressCallback(TelemetryWatcher.OnBoxButtonPressStoreWatcher));
		SceneMgr.Get().RegisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(TelemetryWatcher.OnScenePreloadedStoreWatcher));
	}

	// Token: 0x060081E3 RID: 33251 RVA: 0x002A4488 File Offset: 0x002A2688
	private static void StopWatchingForStoreVisitFromPackOpening()
	{
		Box box = Box.Get();
		if (box != null)
		{
			box.RemoveButtonPressListener(new Box.ButtonPressCallback(TelemetryWatcher.OnBoxButtonPressStoreWatcher));
		}
		SceneMgr.Get().UnregisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(TelemetryWatcher.OnScenePreloadedStoreWatcher));
	}

	// Token: 0x060081E4 RID: 33252 RVA: 0x002A44CE File Offset: 0x002A26CE
	private static void OnBoxButtonPressStoreWatcher(Box.ButtonType type, object userData)
	{
		TelemetryWatcher.OnBoxButtonPressedExclusiveWatch(type, Box.ButtonType.STORE, TelemetryWatcherWatchType.StoreFromPackOpening, delegate
		{
			TelemetryManager.Client().SendPackOpenToStore(PackOpenToStore.Path.BACK_TO_BOX);
		});
	}

	// Token: 0x060081E5 RID: 33253 RVA: 0x002A44F8 File Offset: 0x002A26F8
	private static void OnScenePreloadedStoreWatcher(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
	{
		TelemetryWatcher.OnScenePreloadedExclusiveWatch(nextMode, SceneMgr.Mode.HUB, TelemetryWatcherWatchType.StoreFromPackOpening);
	}

	// Token: 0x060081E6 RID: 33254 RVA: 0x002A4502 File Offset: 0x002A2702
	private static void WatchForCollectionVisitFromDeckPicker()
	{
		SceneMgr.Get().RegisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(TelemetryWatcher.OnScenePreLoadedCollectionWatcher));
		Box.Get().AddButtonPressListener(new Box.ButtonPressCallback(TelemetryWatcher.OnBoxButtonPressCollectionWatcher));
	}

	// Token: 0x060081E7 RID: 33255 RVA: 0x002A4530 File Offset: 0x002A2730
	private static void StopWatchingForCollectionVisitFromDeckPicker()
	{
		Box box = Box.Get();
		if (box != null)
		{
			box.RemoveButtonPressListener(new Box.ButtonPressCallback(TelemetryWatcher.OnBoxButtonPressCollectionWatcher));
		}
		SceneMgr.Get().UnregisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(TelemetryWatcher.OnScenePreLoadedCollectionWatcher));
	}

	// Token: 0x060081E8 RID: 33256 RVA: 0x002A4576 File Offset: 0x002A2776
	private static void OnBoxButtonPressCollectionWatcher(Box.ButtonType type, object userData)
	{
		TelemetryWatcher.OnBoxButtonPressedExclusiveWatch(type, Box.ButtonType.COLLECTION, TelemetryWatcherWatchType.CollectionManagerFromDeckPicker, delegate
		{
			TelemetryManager.Client().SendDeckPickerToCollection(DeckPickerToCollection.Path.BACK_TO_BOX);
		});
	}

	// Token: 0x060081E9 RID: 33257 RVA: 0x002A459F File Offset: 0x002A279F
	private static void OnScenePreLoadedCollectionWatcher(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
	{
		TelemetryWatcher.OnScenePreloadedExclusiveWatch(nextMode, SceneMgr.Mode.HUB, TelemetryWatcherWatchType.CollectionManagerFromDeckPicker);
	}

	// Token: 0x04006CF4 RID: 27892
	private static List<TelemetryWatcherWatchType> s_currentlyWatching = new List<TelemetryWatcherWatchType>();

	// Token: 0x04006CF5 RID: 27893
	private static readonly object s_watchLock = new object();

	// Token: 0x04006CF6 RID: 27894
	private static readonly Map<TelemetryWatcherWatchType, Action> s_watchTypeSetupActions = new Map<TelemetryWatcherWatchType, Action>
	{
		{
			TelemetryWatcherWatchType.CollectionManagerFromDeckPicker,
			new Action(TelemetryWatcher.WatchForCollectionVisitFromDeckPicker)
		},
		{
			TelemetryWatcherWatchType.StoreFromPackOpening,
			new Action(TelemetryWatcher.WatchForStoreVisitFromPackOpening)
		}
	};

	// Token: 0x04006CF7 RID: 27895
	private static readonly Map<TelemetryWatcherWatchType, Action> s_watchTypeTeardownActions = new Map<TelemetryWatcherWatchType, Action>
	{
		{
			TelemetryWatcherWatchType.CollectionManagerFromDeckPicker,
			new Action(TelemetryWatcher.StopWatchingForCollectionVisitFromDeckPicker)
		},
		{
			TelemetryWatcherWatchType.StoreFromPackOpening,
			new Action(TelemetryWatcher.StopWatchingForStoreVisitFromPackOpening)
		}
	};
}
