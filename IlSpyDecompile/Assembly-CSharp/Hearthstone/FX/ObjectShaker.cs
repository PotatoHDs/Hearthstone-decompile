using System;
using System.Collections;
using UnityEngine;

namespace Hearthstone.FX
{
	public class ObjectShaker : MonoBehaviour
	{
		public enum TweenType
		{
			Linear,
			Sine,
			DoubleSine,
			Constant
		}

		private const int RANDOM_TRANFORMS_COUNT = 10;

		private const float HALF_PI = (float)Math.PI / 2f;

		private Vector3 m_originalPosition;

		private Vector3 m_originalRotation;

		private Space m_space = Space.Self;

		public static void Shake(GameObject target, Vector3 position, Vector3 rotation, Space space, AnimationCurve falloff, float duration, float interval, TweenType tweenType, bool destroyHelperOnComplete = false)
		{
			ObjectShaker objectShaker = target.GetComponent<ObjectShaker>();
			if (objectShaker != null)
			{
				objectShaker.CancelShake();
			}
			else
			{
				objectShaker = target.AddComponent<ObjectShaker>();
			}
			objectShaker.StartCoroutine(objectShaker.UpdateShake(position, rotation, space, falloff, duration, interval, tweenType, destroyHelperOnComplete));
		}

		public static void Cancel(GameObject target, bool andDestroy = false)
		{
			ObjectShaker component = target.GetComponent<ObjectShaker>();
			if (component != null)
			{
				component.CancelShake();
				if (andDestroy)
				{
					UnityEngine.Object.Destroy(component);
				}
			}
		}

		private IEnumerator UpdateShake(Vector3 position, Vector3 rotation, Space space, AnimationCurve falloff, float duration, float interval, TweenType tweenType, bool destroyHelperOnComplete)
		{
			Vector3[] positions = new Vector3[10];
			Vector3[] rotations = new Vector3[10];
			for (int i = 0; i < 10; i++)
			{
				int num = i;
				Vector3 vector = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
				positions[num] = vector.normalized;
				positions[i].x *= position.x;
				positions[i].y *= position.y;
				positions[i].z *= position.z;
				vector = (rotations[i] = new Vector3
				{
					x = UnityEngine.Random.Range(0f - rotation.x, rotation.x),
					y = UnityEngine.Random.Range(0f - rotation.y, rotation.y),
					z = UnityEngine.Random.Range(0f - rotation.z, rotation.z)
				});
			}
			int randomTransformIndex = 0;
			float shakeStartTime = Time.time;
			float intervalStartTime2 = Time.time;
			float num2 = falloff.Evaluate(0f);
			m_space = space;
			m_originalPosition = ((m_space == Space.Self) ? base.transform.localPosition : base.transform.position);
			m_originalRotation = ((m_space == Space.Self) ? base.transform.localEulerAngles : base.transform.eulerAngles);
			Vector3 previousPosition2 = positions[positions.Length - 1] * num2;
			Vector3 nextPosition2 = positions[0] * num2;
			Vector3 previousRotation2 = rotations[rotations.Length - 1] * num2;
			Vector3 nextRotation2 = rotations[0] * num2;
			while (Time.time - shakeStartTime < duration)
			{
				float num3 = (Time.time - intervalStartTime2) / interval;
				if (num3 >= 1f)
				{
					randomTransformIndex = (randomTransformIndex + 1) % 10;
					num2 = falloff.Evaluate((Time.time - shakeStartTime) / duration);
					previousPosition2 = nextPosition2;
					nextPosition2 = positions[randomTransformIndex] * num2;
					previousRotation2 = nextRotation2;
					nextRotation2 = rotations[randomTransformIndex] * num2;
					num3 = 0f;
					intervalStartTime2 = Time.time;
				}
				num3 = ConvertTween(num3, tweenType);
				UpdateTransform(previousPosition2, nextPosition2, previousRotation2, nextRotation2, num3);
				yield return new WaitForEndOfFrame();
			}
			previousPosition2 = nextPosition2;
			nextPosition2 = Vector3.zero;
			previousRotation2 = nextRotation2;
			nextRotation2 = Vector3.zero;
			intervalStartTime2 = Time.time;
			while (Time.time - intervalStartTime2 < interval)
			{
				float num3 = (Time.time - intervalStartTime2) / interval;
				num3 = ConvertTween(num3, tweenType);
				UpdateTransform(previousPosition2, nextPosition2, previousRotation2, nextRotation2, num3);
				yield return new WaitForEndOfFrame();
			}
			SetPosition(m_originalPosition);
			SetRotation(m_originalRotation);
			if (destroyHelperOnComplete)
			{
				UnityEngine.Object.Destroy(this);
			}
		}

		private void CancelShake()
		{
			StopAllCoroutines();
			SetPosition(m_originalPosition);
			SetRotation(m_originalRotation);
		}

		private void UpdateTransform(Vector3 previousPosition, Vector3 nextPosition, Vector3 previousRotation, Vector3 nextRotation, float time)
		{
			SetPosition(m_originalPosition + Vector3.Lerp(previousPosition, nextPosition, time));
			SetRotation(m_originalRotation + LerpVector3Angles(previousRotation, nextRotation, time));
		}

		private void SetPosition(Vector3 position)
		{
			if (m_space == Space.Self)
			{
				base.transform.localPosition = position;
			}
			else
			{
				base.transform.position = position;
			}
		}

		private void SetRotation(Vector3 eulerAngles)
		{
			if (m_space == Space.Self)
			{
				base.transform.localEulerAngles = eulerAngles;
			}
			else
			{
				base.transform.eulerAngles = eulerAngles;
			}
		}

		private static Vector3 LerpVector3Angles(Vector3 a, Vector3 b, float t)
		{
			Vector3 result = default(Vector3);
			result.x = Mathf.LerpAngle(a.x, b.x, t);
			result.y = Mathf.LerpAngle(a.y, b.y, t);
			result.z = Mathf.LerpAngle(a.z, b.z, t);
			return result;
		}

		private static float ConvertTween(float time, TweenType tweenType)
		{
			switch (tweenType)
			{
			case TweenType.Constant:
				return 0f;
			case TweenType.Sine:
				return Mathf.Sin(time * (float)Math.PI - (float)Math.PI / 2f) * 0.5f + 0.5f;
			case TweenType.DoubleSine:
				time = Mathf.Sin(time * (float)Math.PI - (float)Math.PI / 2f) * 0.5f + 0.5f;
				return Mathf.Sin(time * (float)Math.PI - (float)Math.PI / 2f) * 0.5f + 0.5f;
			default:
				return time;
			}
		}
	}
}
