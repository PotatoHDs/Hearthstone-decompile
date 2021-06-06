using System;
using System.Collections.Generic;
using HearthstoneTelemetry;
using UnityEngine;

namespace Hearthstone.Telemetry
{
	// Token: 0x0200106C RID: 4204
	public class TelemetryManagerComponentAudio : ITelemetryManagerComponent
	{
		// Token: 0x0600B56F RID: 46447 RVA: 0x0037C309 File Offset: 0x0037A509
		public TelemetryManagerComponentAudio(ITelemetryClient telemetryClient)
		{
			this.m_telemetryClient = telemetryClient;
		}

		// Token: 0x0600B570 RID: 46448 RVA: 0x0037C324 File Offset: 0x0037A524
		public void Initialize()
		{
			this.m_deviceAudioSettingsProvider = new DeviceAudioSettingsProviderMock();
			this.m_wasDeviceMuted = this.m_deviceAudioSettingsProvider.IsMuted;
			this.m_lastDeviceVolume = this.m_deviceAudioSettingsProvider.Volume;
			this.m_lastMasterVolume = Options.Get().GetFloat(Option.SOUND_VOLUME);
			this.m_lastMusicVolume = Options.Get().GetFloat(Option.MUSIC_VOLUME);
			this.m_telemetryClient.SendStartupAudioSettings(this.m_wasDeviceMuted, this.m_lastDeviceVolume, this.m_lastMasterVolume, this.m_lastMusicVolume);
			GameState.RegisterGameStateInitializedListener(new GameState.GameStateInitializedCallback(this.OnGameCreated), null);
			Options.Get().RegisterChangedListener(Option.SOUND_VOLUME, new Options.ChangedCallback(this.OnMasterVolumeChanged));
			Options.Get().RegisterChangedListener(Option.MUSIC_VOLUME, new Options.ChangedCallback(this.OnMusicVolumeChanged));
		}

		// Token: 0x0600B571 RID: 46449 RVA: 0x0037C3E6 File Offset: 0x0037A5E6
		private void OnMasterVolumeChanged(Option option, object prevValue, bool existed, object userData)
		{
			this.QueueDelayedAction(TelemetryManagerComponentAudio.ActionType.MasterVolume, delegate
			{
				float @float = Options.Get().GetFloat(Option.SOUND_VOLUME);
				this.m_telemetryClient.SendMasterVolumeChanged(this.m_lastMasterVolume, @float);
				this.m_lastMasterVolume = @float;
			});
		}

		// Token: 0x0600B572 RID: 46450 RVA: 0x0037C3FB File Offset: 0x0037A5FB
		private void OnMusicVolumeChanged(Option option, object prevValue, bool existed, object userData)
		{
			this.QueueDelayedAction(TelemetryManagerComponentAudio.ActionType.MusicVolume, delegate
			{
				float @float = Options.Get().GetFloat(Option.MUSIC_VOLUME);
				this.m_telemetryClient.SendMusicVolumeChanged(this.m_lastMusicVolume, @float);
				this.m_lastMusicVolume = @float;
			});
		}

		// Token: 0x0600B573 RID: 46451 RVA: 0x0037C410 File Offset: 0x0037A610
		private void OnGameCreated(GameState instance, object userData)
		{
			this.m_telemetryClient.SendGameRoundStartAudioSettings(this.m_wasDeviceMuted, this.m_lastDeviceVolume, this.m_lastMasterVolume, this.m_lastMusicVolume);
		}

		// Token: 0x0600B574 RID: 46452 RVA: 0x0037C438 File Offset: 0x0037A638
		private void QueueDelayedAction(TelemetryManagerComponentAudio.ActionType audioType, Action action)
		{
			this.m_queuedActionsMap[audioType] = new TelemetryManagerComponentAudio.QueuedAction
			{
				Action = action,
				ProcessTime = Time.realtimeSinceStartup + 1f
			};
		}

		// Token: 0x0600B575 RID: 46453 RVA: 0x0037C474 File Offset: 0x0037A674
		public void Update()
		{
			this.ProcessNativeAudioEvents();
			this.ProcessQueuedActions();
		}

		// Token: 0x0600B576 RID: 46454 RVA: 0x0037C484 File Offset: 0x0037A684
		private void ProcessNativeAudioEvents()
		{
			if (Time.realtimeSinceStartup < this.m_nextPollingTime)
			{
				return;
			}
			this.m_nextPollingTime = Time.realtimeSinceStartup + 10f;
			float volume = this.m_deviceAudioSettingsProvider.Volume;
			bool isMuted = this.m_deviceAudioSettingsProvider.IsMuted;
			if (Math.Abs(volume - this.m_lastDeviceVolume) > 1E-45f)
			{
				Log.Telemetry.Print("AudioTelemetry: Device volume changed from {0} to {1}", new object[]
				{
					this.m_lastDeviceVolume,
					volume
				});
				this.m_telemetryClient.SendDeviceVolumeChanged(this.m_lastDeviceVolume, volume);
			}
			if (isMuted != this.m_wasDeviceMuted)
			{
				Log.Telemetry.Print("AudioTelemetry: Device mute state changed to {0}", new object[]
				{
					isMuted
				});
				this.m_telemetryClient.SendDeviceMuteChanged(isMuted);
			}
			this.m_lastDeviceVolume = volume;
			this.m_wasDeviceMuted = isMuted;
		}

		// Token: 0x0600B577 RID: 46455 RVA: 0x0037C55C File Offset: 0x0037A75C
		private void ProcessQueuedActions()
		{
			List<TelemetryManagerComponentAudio.ActionType> list = null;
			foreach (KeyValuePair<TelemetryManagerComponentAudio.ActionType, TelemetryManagerComponentAudio.QueuedAction> keyValuePair in this.m_queuedActionsMap)
			{
				TelemetryManagerComponentAudio.QueuedAction value = keyValuePair.Value;
				TelemetryManagerComponentAudio.ActionType key = keyValuePair.Key;
				if (Time.realtimeSinceStartup > value.ProcessTime)
				{
					if (list == null)
					{
						list = new List<TelemetryManagerComponentAudio.ActionType>();
					}
					Log.Telemetry.Print("AudioTelemetry: Sending event for {0}", new object[]
					{
						key
					});
					value.Action();
					list.Add(key);
				}
			}
			if (list != null)
			{
				foreach (TelemetryManagerComponentAudio.ActionType key2 in list)
				{
					this.m_queuedActionsMap.Remove(key2);
				}
			}
		}

		// Token: 0x0600B578 RID: 46456 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public void Shutdown()
		{
		}

		// Token: 0x04009754 RID: 38740
		private const float DEVICE_POLLING_INTERVAL_SECONDS = 10f;

		// Token: 0x04009755 RID: 38741
		private const float OPERATION_DELAY_SECONDS = 1f;

		// Token: 0x04009756 RID: 38742
		private IDeviceAudioSettingsProvider m_deviceAudioSettingsProvider;

		// Token: 0x04009757 RID: 38743
		private readonly ITelemetryClient m_telemetryClient;

		// Token: 0x04009758 RID: 38744
		private float m_nextPollingTime;

		// Token: 0x04009759 RID: 38745
		private float m_lastMasterVolume;

		// Token: 0x0400975A RID: 38746
		private float m_lastMusicVolume;

		// Token: 0x0400975B RID: 38747
		private float m_lastDeviceVolume;

		// Token: 0x0400975C RID: 38748
		private bool m_wasDeviceMuted;

		// Token: 0x0400975D RID: 38749
		private Dictionary<TelemetryManagerComponentAudio.ActionType, TelemetryManagerComponentAudio.QueuedAction> m_queuedActionsMap = new Dictionary<TelemetryManagerComponentAudio.ActionType, TelemetryManagerComponentAudio.QueuedAction>();

		// Token: 0x0200286E RID: 10350
		private struct QueuedAction
		{
			// Token: 0x0400F9B1 RID: 63921
			public Action Action;

			// Token: 0x0400F9B2 RID: 63922
			public float ProcessTime;
		}

		// Token: 0x0200286F RID: 10351
		private enum ActionType
		{
			// Token: 0x0400F9B4 RID: 63924
			MasterVolume,
			// Token: 0x0400F9B5 RID: 63925
			MusicVolume
		}
	}
}
