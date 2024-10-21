using UnityEngine;

namespace QFramework.Mofelor
{
	public partial class HealthBar04 : ViewController
	{
		public float AddPoint = 100;
		public float SubPoint = 100;

		public float MaxPoint = 200;
		public float ChangePoint = 5;

		public bool CanChang = true;

		void Start()
		{
			ChangeAddPoint(AddPoint);
			ChangeSubPoint(SubPoint);

			ChangePoint = 5;

			Btn1.onClick.AddListener(()=>{
				ChangeAddPoint(100);
				ChangeSubPoint(100);
			});

			Btn2.onClick.AddListener(()=>{
				CanChang = false;
				ActionKit.Delay(3,()=>{CanChang=true;}).Start(this);
			});
		}

		void FixedUpdate(){

			if(CanChang){
				if((AddPoint + ChangePoint * Time.deltaTime) <= MaxPoint)
					ChangeAddPoint(AddPoint += ChangePoint * Time.deltaTime);
				if((SubPoint - ChangePoint * Time.deltaTime) >= 0)	
				ChangeSubPoint(SubPoint -= ChangePoint * Time.deltaTime);
			}
		}

		public void ChangeAddPoint(float value){
			AddPoint = value;
			HealthAdd.fillAmount = AddPoint/MaxPoint;
			TextAdd.text = AddPoint.ToString("0.0")+" ";
		}

		public void ChangeSubPoint(float value){
			SubPoint = value;
			HealthPSub.fillAmount = SubPoint/MaxPoint;
			TextSub.text = SubPoint.ToString("0.0")+" ";
		}
	}
}
