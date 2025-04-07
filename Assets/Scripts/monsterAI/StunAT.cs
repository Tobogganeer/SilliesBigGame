using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Tobo.Audio;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class StunAT : ActionTask {

		public float stunDuration;
		public float stunTime;

		[Space]
		public float stunDurationMin;
		public float stunDurationMax;

		[Space]
		public BBParameter<bool> foundPlayer;
		public BBParameter<bool> whacked;

		public Sound runSound;
        public BBParameter<GameObject> player;


        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            PopUp.Show("She'll be back soon...", 1.5f);
			runSound.MaybeNull().PlayAtPosition(player.value.transform.position);
            foundPlayer.value = false;
			whacked.value = false;
			stunTime = 0f;
			stunDuration = Random.Range(stunDurationMin+1, stunDurationMax+1);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			if (stunTime > stunDuration)
			{
				EndAction(true);
			}

			stunTime += 1 * Time.deltaTime;
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}