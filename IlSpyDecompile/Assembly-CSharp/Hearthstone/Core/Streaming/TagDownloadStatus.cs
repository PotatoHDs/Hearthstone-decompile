using System;
using UnityEngine;

namespace Hearthstone.Core.Streaming
{
	[Serializable]
	public class TagDownloadStatus
	{
		public string[] Tags;

		public long BytesTotal;

		public long BytesDownloaded;

		public float StartProgress;

		public float progressVal;

		private bool completed;

		public long BytesRemaining => BytesTotal - BytesDownloaded;

		public float Progress
		{
			get
			{
				if (BytesTotal == 0L)
				{
					return 0f;
				}
				if (progressVal != 0f)
				{
					return progressVal;
				}
				return Mathf.Lerp(StartProgress, 1f, (float)((double)BytesDownloaded / (double)BytesTotal));
			}
			set
			{
				progressVal = value;
			}
		}

		public bool Complete
		{
			get
			{
				if (!completed)
				{
					if (BytesRemaining == 0L)
					{
						return BytesTotal > 0;
					}
					return false;
				}
				return true;
			}
			set
			{
				completed = value;
			}
		}
	}
}
