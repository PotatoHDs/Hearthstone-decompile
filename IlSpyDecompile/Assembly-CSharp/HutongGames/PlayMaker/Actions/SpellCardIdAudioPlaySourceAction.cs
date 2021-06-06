using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Pegasus Audio")]
	[Tooltip("Plays an audio source dependent on a Spell's Card's ID.")]
	public class SpellCardIdAudioPlaySourceAction : SpellCardIdAudioAction
	{
		public FsmOwnerDefault m_SpellObject;

		[Tooltip("Which Card to check on the Spell.")]
		public Which m_WhichCard;

		[RequiredField]
		[CompoundArray("Sources", "Card Id", "Source")]
		public string[] m_CardIds;

		[CheckForComponent(typeof(AudioSource))]
		public FsmGameObject[] m_Sources;

		[CheckForComponent(typeof(AudioSource))]
		public FsmGameObject m_DefaultSource;

		[Tooltip("Optional. The source that gets picked will be put into this variable so you can track it.")]
		[CheckForComponent(typeof(AudioSource))]
		public FsmGameObject m_PickedSource;

		[Tooltip("Scales the volume of the AudioSource just for this Play call.")]
		[HasFloatSlider(0f, 1f)]
		public FsmFloat m_VolumeScale;

		[Tooltip("Wait for the Audio Source to finish playing before moving on.")]
		public FsmBool m_WaitForFinish;

		public float m_Delay;

		private float m_DelayTime;

		private AudioSource m_source;

		private float? m_originalVolume;

		protected override GameObject GetSpellOwner()
		{
			return base.Fsm.GetOwnerDefaultTarget(m_SpellObject);
		}

		public override void Reset()
		{
			m_SpellObject = null;
			m_WhichCard = Which.SOURCE;
			m_CardIds = new string[2];
			m_Sources = new FsmGameObject[2];
			m_DefaultSource = null;
			m_PickedSource = new FsmGameObject
			{
				UseVariable = true
			};
			m_VolumeScale = 1f;
			m_WaitForFinish = false;
			m_Delay = 0f;
			m_DelayTime = 0f;
		}

		public override void OnEnter()
		{
			base.OnEnter();
			if (m_Delay > 0f)
			{
				m_DelayTime = m_Delay;
				StartCoroutine(Delay());
			}
			else
			{
				Play();
			}
		}

		public override void OnExit()
		{
			if (!m_VolumeScale.IsNone && m_originalVolume.HasValue)
			{
				SoundManager.Get().SetVolume(m_source, m_originalVolume.Value);
			}
		}

		public override void OnUpdate()
		{
			if (!m_WaitForFinish.Value || !SoundManager.Get().IsActive(m_source))
			{
				Finish();
			}
		}

		private void Play()
		{
			m_source = GetSourceMatchingCardId(m_WhichCard, m_CardIds, m_Sources, m_DefaultSource);
			if (m_source == null)
			{
				Finish();
				return;
			}
			if (!m_PickedSource.IsNone)
			{
				m_PickedSource.Value = m_source.gameObject;
			}
			if (!m_VolumeScale.IsNone)
			{
				SoundManager.Get().SetVolume(m_source, m_VolumeScale.Value);
				m_originalVolume = m_VolumeScale.Value;
			}
			SoundManager.Get().Play(m_source);
			if (!m_WaitForFinish.Value || !SoundManager.Get().IsActive(m_source))
			{
				Finish();
			}
		}

		private IEnumerator Delay()
		{
			while (m_DelayTime > 0f)
			{
				m_DelayTime -= Time.deltaTime;
				yield return null;
			}
			Play();
		}
	}
}
