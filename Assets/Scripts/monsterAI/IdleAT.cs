using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class IdleAT : ActionTask {


		public float timer;
		public float timeLimit;
		public BBParameter<GameObject> player;
		public BBParameter<int> currentRoom;
		public BBParameter<bool> sameRoom;


		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			timer += 1 * Time.deltaTime;

			if (timer > timeLimit)
			{
				timer = 0;
				EndAction(true);
			}

            if (player.value.GetComponent<PlayerMovement>().currentRoom == currentRoom.value)
            {
                sameRoom.value = true;
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