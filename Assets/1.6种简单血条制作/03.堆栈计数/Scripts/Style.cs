using UnityEngine;
using System.Collections.Generic;

namespace QFramework.Mofelor
{
	public partial class Style : ViewController
	{
		public List<Sprite> Sprites = new List<Sprite>();

		public void ChangeSprite(int index)
		{
			Image.sprite = Sprites[index];
		}
	}
}
