namespace Example
{
	using System;
	using UnityEngine;

	/// <summary>
	/// Component to control scene and quality configuration.
	/// </summary>
	public sealed class SceneConfig : MonoBehaviour
	{
		// PRIVATE MEMBERS

		[Header("Post-Processing")]

		[SerializeField]
		private Camera[]      _cameras;
		[SerializeField]
		private QualityConfig _pcLow;
		[SerializeField]
		private QualityConfig _pcMedium;
		[SerializeField]
		private QualityConfig _pcHigh;
		[SerializeField]
		private QualityConfig _mobileLow;
		[SerializeField]
		private QualityConfig _mobileMedium;
		[SerializeField]
		private QualityConfig _mobileHigh;
		[SerializeField]
		private QualityConfig _vrLow;
		[SerializeField]
		private QualityConfig _vrMedium;
		[SerializeField]
		private QualityConfig _vrHigh;

		// PUBLIC METHODS

		public int GetQualityLevel()
		{
			return QualitySettings.GetQualityLevel() % 3;
		}

		public void SetQualityLevel(int level)
		{
			level %= 3;

		if (Application.isMobilePlatform == true && Application.isEditor == false)
			{
				level += 3;
			}

			QualitySettings.SetQualityLevel(level, true);

			QualityConfig config;

		}

		// MonoBehaviour INTERFACE

		private void Awake()
		{
			SetQualityLevel(GetQualityLevel());
		}

		[Serializable]
		private sealed class QualityConfig
		{
			public bool             EnableShadows;
			public bool             EnablePostProcessing;
		}
	}
}
