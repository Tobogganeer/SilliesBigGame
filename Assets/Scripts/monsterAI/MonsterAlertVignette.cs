using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Tobo.Audio;
using UnityEngine;
using UnityEngine.UI;


namespace NodeCanvas.Tasks.Actions {

	public class MonsterAlertVignette : ActionTask {


		public float timer;
		public float timeLimit;
        public Sound heartBeat;
		public Sound heartBeatClose;

        [Space]
		public GameObject vignetteCG;

		public GameObject player;

		public bool playedCloseSound;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			vignetteCG.SetActive(true);
			vignetteCG.GetComponent<Image>().color = new Vector4(1, 1, 1, 0);
			heartBeat.MaybeNull().PlayAtPosition(player.transform.position);
            playedCloseSound = false;
            timer = 0f;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			vignetteCG.GetComponent<Image>().color = new Vector4(1, 1, 1, timer / timeLimit);
			if (timer > timeLimit)
			{
				vignetteCG.SetActive(false);
				EndAction(true);
			}

			else if (timer > timeLimit / 2 && !playedCloseSound)
			{
                heartBeatClose.MaybeNull().PlayAtPosition(player.transform.position);
				playedCloseSound = true;
            }



			timer += 1 * Time.deltaTime;
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}