using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Http;
using UnityEngine;

// Token: 0x02000879 RID: 2169
public class CheatRequest
{
	// Token: 0x06007608 RID: 30216 RVA: 0x0025E3B8 File Offset: 0x0025C5B8
	private static HttpStatusCode GetStatusCode(IDictionary<string, string> headers)
	{
		if (headers == null)
		{
			return HttpStatusCode.NotFound;
		}
		string text;
		if (!headers.TryGetValue("STATUS", out text))
		{
			return HttpStatusCode.NotFound;
		}
		string[] array = text.Split(new char[]
		{
			' '
		});
		if (array.Length < 3)
		{
			return HttpStatusCode.NotFound;
		}
		int result;
		if (!int.TryParse(array[1], out result))
		{
			return HttpStatusCode.NotFound;
		}
		return (HttpStatusCode)result;
	}

	// Token: 0x06007609 RID: 30217 RVA: 0x0025E414 File Offset: 0x0025C614
	private IEnumerator SendGetRequestCoroutine(string url)
	{
		IHttpRequest request = HttpRequestFactory.Get().CreateGetRequest(url);
		yield return request.SendRequest();
		if (request.IsNetworkError || request.IsHttpError)
		{
			if (request.ErrorString.StartsWith("Failed to connect"))
			{
				CheatRequest.LogError("Failed to initiate cheat request. Cheat server is unreachable.");
				yield break;
			}
			CheatRequest.LogError(string.IsNullOrEmpty(request.ResponseAsString) ? request.ErrorString : request.ResponseAsString);
			yield break;
		}
		else
		{
			HttpStatusCode statusCode = CheatRequest.GetStatusCode(request.ResponseHeaders);
			if (statusCode != HttpStatusCode.OK)
			{
				CheatRequest.LogError(statusCode, request.ResponseAsString);
				this.IsSuccessful = false;
				yield break;
			}
			this.IsSuccessful = true;
			UIStatus.Get().AddInfo(request.ResponseAsString);
			yield break;
		}
	}

	// Token: 0x0600760A RID: 30218 RVA: 0x0025E42A File Offset: 0x0025C62A
	private IEnumerator SendDeleteRequestCoroutine(string url)
	{
		IHttpRequest request = HttpRequestFactory.Get().CreateDeleteRequest(url);
		yield return request.SendRequest();
		if (request.IsNetworkError)
		{
			if (request.ErrorString.StartsWith("Failed to connect"))
			{
				CheatRequest.LogError("Failed to initiate cheat request. Cheat server is unreachable.");
				yield break;
			}
			CheatRequest.LogError(request.ErrorString);
			yield break;
		}
		else
		{
			string responseAsString = request.ResponseAsString;
			HttpStatusCode responseStatusCode = (HttpStatusCode)request.ResponseStatusCode;
			if (responseStatusCode != HttpStatusCode.OK)
			{
				CheatRequest.LogError(responseStatusCode, responseAsString);
				this.IsSuccessful = false;
				yield break;
			}
			this.IsSuccessful = true;
			UIStatus.Get().AddInfo(responseAsString);
			yield break;
		}
	}

	// Token: 0x0600760B RID: 30219 RVA: 0x0025E440 File Offset: 0x0025C640
	public Coroutine SendGetRequest(string url)
	{
		return Processor.RunCoroutine(this.SendGetRequestCoroutine(url), null);
	}

	// Token: 0x0600760C RID: 30220 RVA: 0x0025E44F File Offset: 0x0025C64F
	public Coroutine SendDeleteRequest(string url)
	{
		return Processor.RunCoroutine(this.SendDeleteRequestCoroutine(url), null);
	}

	// Token: 0x0600760D RID: 30221 RVA: 0x0025E45E File Offset: 0x0025C65E
	public static void LogError(string message)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			UIStatus.Get().AddError(message, -1f);
			Debug.LogError(message);
		}
	}

	// Token: 0x0600760E RID: 30222 RVA: 0x0025E47D File Offset: 0x0025C67D
	public static void LogError(HttpStatusCode statusCode, string message)
	{
		CheatRequest.LogError(string.Format("{0} (status code: {1})", message, (int)statusCode));
	}

	// Token: 0x04005D49 RID: 23881
	private const string LOCATE_FAILURE_ERR_MSG = "Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.";

	// Token: 0x04005D4A RID: 23882
	private const string CONNECT_FAILURE_ERR_MSG = "Failed to initiate cheat request. Cheat server is unreachable.";

	// Token: 0x04005D4B RID: 23883
	public bool IsSuccessful;
}
