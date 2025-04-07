using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace NodeCanvas.Tasks.Actions {

	public class AttackAT : ActionTask {

		public float QTECurrentTime = 0f;
		public float QTETimeLimit = 5f;

		[Space]
		public BBParameter<GameObject> monsterQTE;
		public BBParameter<bool> whacked;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			QTECurrentTime = 0f;

			//start the QTE
			monsterQTE.value.SetActive(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			if (QTECurrentTime > QTETimeLimit)
			{
				// for now send them to the menu
				monsterQTE.value.SetActive(false);
                SceneManager.LoadScene("TitleScreen");
            }

			QTECurrentTime += 1 * Time.deltaTime;
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}