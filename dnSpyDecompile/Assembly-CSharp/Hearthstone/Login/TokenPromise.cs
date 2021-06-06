using System;

namespace Hearthstone.Login
{
	// Token: 0x02001135 RID: 4405
	public class TokenPromise
	{
		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x0600C0F8 RID: 49400 RVA: 0x003AAF79 File Offset: 0x003A9179
		// (set) Token: 0x0600C0F9 RID: 49401 RVA: 0x003AAF81 File Offset: 0x003A9181
		public string Token { get; private set; }

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600C0FA RID: 49402 RVA: 0x003AAF8A File Offset: 0x003A918A
		// (set) Token: 0x0600C0FB RID: 49403 RVA: 0x003AAF92 File Offset: 0x003A9192
		public TokenPromise.ResultType Result { get; private set; }

		// Token: 0x0600C0FC RID: 49404 RVA: 0x003AAF9B File Offset: 0x003A919B
		public bool IsReady()
		{
			return this.Result > TokenPromise.ResultType.Unknown;
		}

		// Token: 0x0600C0FD RID: 49405 RVA: 0x003AAFA6 File Offset: 0x003A91A6
		public void SetResult(TokenPromise.ResultType status, string token = null)
		{
			this.Token = token;
			this.Result = status;
		}

		// Token: 0x0200290B RID: 10507
		public enum ResultType
		{
			// Token: 0x0400FB9A RID: 64410
			Unknown,
			// Token: 0x0400FB9B RID: 64411
			Success,
			// Token: 0x0400FB9C RID: 64412
			Failure,
			// Token: 0x0400FB9D RID: 64413
			Canceled
		}
	}
}
