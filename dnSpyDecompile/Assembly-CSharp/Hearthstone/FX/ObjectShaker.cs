using System;
using System.Collections;
using UnityEngine;

namespace Hearthstone.FX
{
	// Token: 0x02001173 RID: 4467
	public class ObjectShaker : MonoBehaviour
	{
		// Token: 0x0600C40C RID: 50188 RVA: 0x003B2920 File Offset: 0x003B0B20
		public static void Shake(GameObject target, Vector3 position, Vector3 rotation, Space space, AnimationCurve falloff, float duration, float interval, ObjectShaker.TweenType tweenType, bool destroyHelperOnComplete = false)
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

		// Token: 0x0600C40D RID: 50189 RVA: 0x003B2968 File Offset: 0x003B0B68
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

		// Token: 0x0600C40E RID: 50190 RVA: 0x003B2994 File Offset: 0x003B0B94
		private IEnumerator UpdateShake(Vector3 position, Vector3 rotation, Space space, AnimationCurve falloff, float duration, float interval, ObjectShaker.TweenType tweenType, bool destroyHelperOnComplete)
		{
			Vector3[] positions = new Vector3[10];
			Vector3[] rotations = new Vector3[10];
			for (int i = 0; i < 10; i++)
			{
				positions[i] = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
				Vector3[] array = positions;
				int num = i;
				array[num].x = array[num].x * position.x;
				Vector3[] array2 = positions;
				int num2 = i;
				array2[num2].y = array2[num2].y * position.y;
				Vector3[] array3 = positions;
				int num3 = i;
				array3[num3].z = array3[num3].z * position.z;
				rotations[i] = new Vector3
				{
					x = UnityEngine.Random.Range(-rotation.x, rotation.x),
					y = UnityEngine.Random.Range(-rotation.y, rotation.y),
					z = UnityEngine.Random.Range(-rotation.z, rotation.z)
				};
			}
			int randomTransformIndex = 0;
			float shakeStartTime = Time.time;
			float intervalStartTime = Time.time;
			float d = falloff.Evaluate(0f);
			this.m_space = space;
			this.m_originalPosition = ((this.m_space == Space.Self) ? base.transform.localPosition : base.transform.position);
			this.m_originalRotation = ((this.m_space == Space.Self) ? base.transform.localEulerAngles : base.transform.eulerAngles);
			Vector3 previousPosition = positions[positions.Length - 1] * d;
			Vector3 nextPosition = positions[0] * d;
			Vector3 previousRotation = rotations[rotations.Length - 1] * d;
			Vector3 nextRotation = rotations[0] * d;
			while (Time.time - shakeStartTime < duration)
			{
				float num4 = (Time.time - intervalStartTime) / interval;
				if (num4 >= 1f)
				{
					randomTransformIndex = (randomTransformIndex + 1) % 10;
					d = falloff.Evaluate((Time.time - shakeStartTime) / duration);
					previousPosition = nextPosition;
					nextPosition = positions[randomTransformIndex] * d;
					previousRotation = nextRotation;
					nextRotation = rotations[randomTransformIndex] * d;
					num4 = 0f;
					intervalStartTime = Time.time;
				}
				num4 = ObjectShaker.ConvertTween(num4, tweenType);
				this.UpdateTransform(previousPosition, nextPosition, previousRotation, nextRotation, num4);
				yield return new WaitForEndOfFrame();
			}
			previousPosition = nextPosition;
			nextPosition = Vector3.zero;
			previousRotation = nextRotation;
			nextRotation = Vector3.zero;
			intervalStartTime = Time.time;
			while (Time.time - intervalStartTime < interval)
			{
				float num4 = (Time.time - intervalStartTime) / interval;
				num4 = ObjectShaker.ConvertTween(num4, tweenType);
				this.UpdateTransform(previousPosition, nextPosition, previousRotation, nextRotation, num4);
				yield return new WaitForEndOfFrame();
			}
			this.SetPosition(this.m_originalPosition);
			this.SetRotation(this.m_originalRotation);
			if (destroyHelperOnComplete)
			{
				UnityEngine.Object.Destroy(this);
			}
			yield break;
		}

		// Token: 0x0600C40F RID: 50191 RVA: 0x003B29EB File Offset: 0x003B0BEB
		private void CancelShake()
		{
			base.StopAllCoroutines();
			this.SetPosition(this.m_originalPosition);
			this.SetRotation(this.m_originalRotation);
		}

		// Token: 0x0600C410 RID: 50192 RVA: 0x003B2A0B File Offset: 0x003B0C0B
		private void UpdateTransform(Vector3 previousPosition, Vector3 nextPosition, Vector3 previousRotation, Vector3 nextRotation, float time)
		{
			this.SetPosition(this.m_originalPosition + Vector3.Lerp(previousPosition, nextPosition, time));
			this.SetRotation(this.m_originalRotation + ObjectShaker.LerpVector3Angles(previousRotation, nextRotation, time));
		}

		// Token: 0x0600C411 RID: 50193 RVA: 0x003B2A42 File Offset: 0x003B0C42
		private void SetPosition(Vector3 position)
		{
			if (this.m_space == Space.Self)
			{
				base.transform.localPosition = position;
				return;
			}
			base.transform.position = position;
		}

		// Token: 0x0600C412 RID: 50194 RVA: 0x003B2A66 File Offset: 0x003B0C66
		private void SetRotation(Vector3 eulerAngles)
		{
			if (this.m_space == Space.Self)
			{
				base.transform.localEulerAngles = eulerAngles;
				return;
			}
			base.transform.eulerAngles = eulerAngles;
		}

		// Token: 0x0600C413 RID: 50195 RVA: 0x003B2A8C File Offset: 0x003B0C8C
		private static Vector3 LerpVector3Angles(Vector3 a, Vector3 b, float t)
		{
			return new Vector3
			{
				x = Mathf.LerpAngle(a.x, b.x, t),
				y = Mathf.LerpAngle(a.y, b.y, t),
				z = Mathf.LerpAngle(a.z, b.z, t)
			};
		}

		// Token: 0x0600C414 RID: 50196 RVA: 0x003B2AF0 File Offset: 0x003B0CF0
		private static float ConvertTween(float time, ObjectShaker.TweenType tweenType)
		{
			switch (tweenType)
			{
			case ObjectShaker.TweenType.Sine:
				return Mathf.Sin(time * 3.1415927f - 1.5707964f) * 0.5f + 0.5f;
			case ObjectShaker.TweenType.DoubleSine:
				time = Mathf.Sin(time * 3.1415927f - 1.5707964f) * 0.5f + 0.5f;
				return Mathf.Sin(time * 3.1415927f - 1.5707964f) * 0.5f + 0.5f;
			case ObjectShaker.TweenType.Constant:
				return 0f;
			default:
				return time;
			}
		}

		// Token: 0x04009D09 RID: 40201
		private const int RANDOM_TRANFORMS_COUNT = 10;

		// Token: 0x04009D0A RID: 40202
		private const float HALF_PI = 1.5707964f;

		// Token: 0x04009D0B RID: 40203
		private Vector3 m_originalPosition;

		// Token: 0x04009D0C RID: 40204
		private Vector3 m_originalRotation;

		// Token: 0x04009D0D RID: 40205
		private Space m_space = Space.Self;

		// Token: 0x02002939 RID: 10553
		public enum TweenType
		{
			// Token: 0x0400FC20 RID: 64544
			Linear,
			// Token: 0x0400FC21 RID: 64545
			Sine,
			// Token: 0x0400FC22 RID: 64546
			DoubleSine,
			// Token: 0x0400FC23 RID: 64547
			Constant
		}
	}
}
