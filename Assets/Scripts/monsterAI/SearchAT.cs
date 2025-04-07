using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class SearchAT : ActionTask {

		public BBParameter<bool> foundPlayer = false;

		public BBParameter<GameObject> monsterSearchAnim;

		public bool animationPlayed = false;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            animationPlayed = false;
        }


		//Called once per frame while the action is active.
		protected override void OnUpdate() {

			if (foundPlayer.value == true)
			{
				monsterSearchAnim.value.SetActive(false);
				EndAction(true);
			}
			else if (animationPlayed == true && !monsterSearchAnim.value.activeInHierarchy)
			{
				animationPlayed = false;
				EndAction(true);
			}

                if (!HidingSpot.IsPlayerHidden)
			{
				foundPlayer.value = true;
			}
			else
			{
				if (animationPlayed == false)
				{
                    monsterSearchAnim.value.SetActive(true);
                    animationPlayed = true;
					
				}
			}


		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}