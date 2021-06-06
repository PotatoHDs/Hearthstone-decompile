using System;
using System.Collections.Generic;
using Blizzard.Telemetry.WTCG.Client;

public static class TelemetryWatcher
{
	private static List<TelemetryWatcherWatchType> s_currentlyWatching = new List<TelemetryWatcherWatchType>();

	private static readonly object s_watchLock = new object();

	private static readonly Map<TelemetryWatcherWatchType, Action> s_watchTypeSetupActions = new Map<TelemetryWatcherWatchType, Action>
	{
		{
			TelemetryWatcherWatchType.CollectionManagerFromDeckPicker,
			WatchForCollectionVisitFromDeckPicker
		},
		{
			TelemetryWatcherWatchType.StoreFromPackOpening,
			WatchForStoreVisitFromPackOpening
		}
	};

	private static readonly Map<TelemetryWatcherWatchType, Action> s_watchTypeTeardownActions = new Map<TelemetryWatcherWatchType, Action>
	{
		{
			TelemetryWatcherWatchType.CollectionManagerFromDeckPicker,
			StopWatchingForCollectionVisitFromDeckPicker
		},
		{
			TelemetryWatcherWatchType.StoreFromPackOpening,
			StopWatchingForStoreVisitFromPackOpening
		}
	};

	public static void WatchFor(TelemetryWatcherWatchType watchType)
	{
		if (!s_watchTypeSetupActions.TryGetValue(watchType, out var value))
		{
			Log.Telemetry.Print("Watching for type={0} is not currently supported");
			return;
		}
		lock (s_watchLock)
		{
			if (s_currentlyWatching.Contains(watchType))
			{
				Log.Telemetry.Print("Already watching for type={0}", watchType);
				return;
			}
			s_currentlyWatching.Add(watchType);
		}
		value();
		Log.Telemetry.Print("Watching for type={0}", watchType);
	}

	public static void StopWatchingFor(TelemetryWatcherWatchType watchType)
	{
		lock (s_watchLock)
		{
			if (!s_currentlyWatching.Remove(watchType))
			{
				Log.Telemetry.Print("Was not watching for type={0}", watchType);
				return;
			}
		}
		if (s_watchTypeTeardownActions.TryGetValue(watchType, out var value))
		{
			value();
		}
		Log.Telemetry.Print("No longer watching for type={0}", watchType);
	}

	private static void OnScenePreloadedExclusiveWatch(SceneMgr.Mode nextMode, SceneMgr.Mode targetMode, TelemetryWatcherWatchType watchType)
	{
		Log.Telemetry.Print("Scene change detected while watching for type={0}.  Next scene={1}, Target={2}", watchType, nextMode, targetMode);
		if (nextMode != targetMode)
		{
			StopWatchingFor(watchType);
		}
	}

	private static void OnBoxButtonPressedExclusiveWatch(Box.ButtonType buttonPressed, Box.ButtonType targetType, TelemetryWatcherWatchType watchType, Action onTargetPressed)
	{
		Log.Telemetry.Print("Button pressed on Box while watching for type={0}.  Button pressed={1}, target={2}", watchType, buttonPressed, targetType);
		if (buttonPressed == targetType)
		{
			onTargetPressed();
		}
		StopWatchingFor(watchType);
	}

	private static void WatchForStoreVisitFromPackOpening()
	{
		Box.Get().AddButtonPressListener(OnBoxButtonPressStoreWatcher);
		SceneMgr.Get().RegisterScenePreLoadEvent(OnScenePreloadedStoreWatcher);
	}

	private static void StopWatchingForStoreVisitFromPackOpening()
	{
		Box box = Box.Get();
		if (box != null)
		{
			box.RemoveButtonPressListener(OnBoxButtonPressStoreWatcher);
		}
		SceneMgr.Get().UnregisterScenePreLoadEvent(OnScenePreloadedStoreWatcher);
	}

	private static void OnBoxButtonPressStoreWatcher(Box.ButtonType type, object userData)
	{
		OnBoxButtonPressedExclusiveWatch(type, Box.ButtonType.STORE, TelemetryWatcherWatchType.StoreFromPackOpening, delegate
		{
			TelemetryManager.Client().SendPackOpenToStore(PackOpenToStore.Path.BACK_TO_BOX);
		});
	}

	private static void OnScenePreloadedStoreWatcher(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
	{
		OnScenePreloadedExclusiveWatch(nextMode, SceneMgr.Mode.HUB, TelemetryWatcherWatchType.StoreFromPackOpening);
	}

	private static void WatchForCollectionVisitFromDeckPicker()
	{
		SceneMgr.Get().RegisterScenePreLoadEvent(OnScenePreLoadedCollectionWatcher);
		Box.Get().AddButtonPressListener(OnBoxButtonPressCollectionWatcher);
	}

	private static void StopWatchingForCollectionVisitFromDeckPicker()
	{
		Box box = Box.Get();
		if (box != null)
		{
			box.RemoveButtonPressListener(OnBoxButtonPressCollectionWatcher);
		}
		SceneMgr.Get().UnregisterScenePreLoadEvent(OnScenePreLoadedCollectionWatcher);
	}

	private static void OnBoxButtonPressCollectionWatcher(Box.ButtonType type, object userData)
	{
		OnBoxButtonPressedExclusiveWatch(type, Box.ButtonType.COLLECTION, TelemetryWatcherWatchType.CollectionManagerFromDeckPicker, delegate
		{
			TelemetryManager.Client().SendDeckPickerToCollection(DeckPickerToCollection.Path.BACK_TO_BOX);
		});
	}

	private static void OnScenePreLoadedCollectionWatcher(SceneMgr.Mode prevMode, SceneMgr.Mode nextMode, object userData)
	{
		OnScenePreloadedExclusiveWatch(nextMode, SceneMgr.Mode.HUB, TelemetryWatcherWatchType.CollectionManagerFromDeckPicker);
	}
}
