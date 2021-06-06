namespace Hearthstone.Login
{
	public class TokenPromise
	{
		public enum ResultType
		{
			Unknown,
			Success,
			Failure,
			Canceled
		}

		public string Token { get; private set; }

		public ResultType Result { get; private set; }

		public bool IsReady()
		{
			return Result != ResultType.Unknown;
		}

		public void SetResult(ResultType status, string token = null)
		{
			Token = token;
			Result = status;
		}
	}
}
