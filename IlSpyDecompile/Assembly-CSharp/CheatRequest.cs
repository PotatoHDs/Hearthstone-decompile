using System.Collections;
using System.Collections.Generic;
using System.Net;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.Http;
using UnityEngine;

public class CheatRequest
{
	private const string LOCATE_FAILURE_ERR_MSG = "Failed to locate cheat server. Please ensure that the server has Config.Util.Cheat=true enabled.";

	private const string CONNECT_FAILURE_ERR_MSG = "Failed to initiate cheat request. Cheat server is unreachable.";

	public bool IsSuccessful;

	private static HttpStatusCode GetStatusCode(IDictionary<string, string> headers)
	{
		if (headers == null)
		{
			return HttpStatusCode.NotFound;
		}
		if (!headers.TryGetValue("STATUS", out var value))
		{
			return HttpStatusCode.NotFound;
		}
		string[] array = value.Split(' ');
		if (array.Length < 3)
		{
			return HttpStatusCode.NotFound;
		}
		if (!int.TryParse(array[1], out var result))
		{
			return HttpStatusCode.NotFound;
		}
		return (HttpStatusCode)result;
	}

	private IEnumerator SendGetRequestCoroutine(string url)
	{
		IHttpRequest request = HttpRequestFactory.Get().CreateGetRequest(url);
		yield return request.SendRequest();
		if (request.IsNetworkError || request.IsHttpError)
		{
			if (request.ErrorString.StartsWith("Failed to connect"))
			{
				LogError("Failed to initiate cheat request. Cheat server is unreachable.");
			}
			else
			{
				LogError(string.IsNullOrEmpty(request.ResponseAsString) ? request.ErrorString : request.ResponseAsString);
			}
			yield break;
		}
		HttpStatusCode statusCode = GetStatusCode(request.ResponseHeaders);
		if (statusCode != HttpStatusCode.OK)
		{
			LogError(statusCode, request.ResponseAsString);
			IsSuccessful = false;
		}
		else
		{
			IsSuccessful = true;
			UIStatus.Get().AddInfo(request.ResponseAsString);
		}
	}

	private IEnumerator SendDeleteRequestCoroutine(string url)
	{
		IHttpRequest request = HttpRequestFactory.Get().CreateDeleteRequest(url);
		yield return request.SendRequest();
		if (request.IsNetworkError)
		{
			if (request.ErrorString.StartsWith("Failed to connect"))
			{
				LogError("Failed to initiate cheat request. Cheat server is unreachable.");
			}
			else
			{
				LogError(request.ErrorString);
			}
			yield break;
		}
		string responseAsString = request.ResponseAsString;
		HttpStatusCode responseStatusCode = (HttpStatusCode)request.ResponseStatusCode;
		if (responseStatusCode != HttpStatusCode.OK)
		{
			LogError(responseStatusCode, responseAsString);
			IsSuccessful = false;
		}
		else
		{
			IsSuccessful = true;
			UIStatus.Get().AddInfo(responseAsString);
		}
	}

	public Coroutine SendGetRequest(string url)
	{
		return Processor.RunCoroutine(SendGetRequestCoroutine(url));
	}

	public Coroutine SendDeleteRequest(string url)
	{
		return Processor.RunCoroutine(SendDeleteRequestCoroutine(url));
	}

	public static void LogError(string message)
	{
		if (!HearthstoneApplication.IsPublic())
		{
			UIStatus.Get().AddError(message);
			Debug.LogError(message);
		}
	}

	public static void LogError(HttpStatusCode statusCode, string message)
	{
		LogError($"{message} (status code: {(int)statusCode})");
	}
}
