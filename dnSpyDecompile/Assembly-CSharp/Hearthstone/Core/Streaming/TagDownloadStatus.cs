using System;
using UnityEngine;

namespace Hearthstone.Core.Streaming
{
	// Token: 0x02001090 RID: 4240
	[Serializable]
	public class TagDownloadStatus
	{
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x0600B7E9 RID: 47081 RVA: 0x003859A6 File Offset: 0x00383BA6
		public long BytesRemaining
		{
			get
			{
				return this.BytesTotal - this.BytesDownloaded;
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x0600B7EA RID: 47082 RVA: 0x003859B8 File Offset: 0x00383BB8
		// (set) Token: 0x0600B7EB RID: 47083 RVA: 0x00385A07 File Offset: 0x00383C07
		public float Progress
		{
			get
			{
				if (this.BytesTotal == 0L)
				{
					return 0f;
				}
				if (this.progressVal != 0f)
				{
					return this.progressVal;
				}
				return Mathf.Lerp(this.StartProgress, 1f, (float)((double)this.BytesDownloaded / (double)this.BytesTotal));
			}
			set
			{
				this.progressVal = value;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x0600B7EC RID: 47084 RVA: 0x00385A10 File Offset: 0x00383C10
		// (set) Token: 0x0600B7ED RID: 47085 RVA: 0x00385A30 File Offset: 0x00383C30
		public bool Complete
		{
			get
			{
				return this.completed || (this.BytesRemaining == 0L && this.BytesTotal > 0L);
			}
			set
			{
				this.completed = value;
			}
		}

		// Token: 0x04009833 RID: 38963
		public string[] Tags;

		// Token: 0x04009834 RID: 38964
		public long BytesTotal;

		// Token: 0x04009835 RID: 38965
		public long BytesDownloaded;

		// Token: 0x04009836 RID: 38966
		public float StartProgress;

		// Token: 0x04009837 RID: 38967
		public float progressVal;

		// Token: 0x04009838 RID: 38968
		private bool completed;
	}
}
