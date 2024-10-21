using UnityEngine;
using QFramework;

namespace QFramework.Mofelor
{
	public partial class HealthBar06 : ViewController
	{
		public void Change (float value)
		{
			T0001.fillAmount = value;
			T0002.fillAmount = value;
			T0003.fillAmount = value;
		}
	}
}
