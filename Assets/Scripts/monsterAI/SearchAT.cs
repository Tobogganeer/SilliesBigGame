using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Tobo.Audio;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class SearchAT : ActionTask {

		public BBParameter<bool> foundPlayer = false;

		public BBParameter<GameObject> monsterSearchAnim;

		public bool animationPlayed = false;

		public BBParameter<bool> searching;

		public BBParameter<GameObject> player;

        public Sound searchingSound;

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

            if (!HidingSpot.IsPlayerHidden)
            {
                foundPlayer.value = true;
            }

            if (foundPlayer.value == true)
			{
                searching.value = false;
                monsterSearchAnim.value.SetActive(false);
				EndAction(true);
			}
			else if (animationPlayed == true && !monsterSearchAnim.value.activeInHierarchy)
			{
				animationPlayed = false;
                searching.value = false;

                EndAction(true);
			}
            else
            {
                if (animationPlayed == false && !monsterSearchAnim.value.activeInHierarchy && !foundPlayer.value)
                {
                    Debug.Log("playing");
					searching.value = true;
                    animationPlayed = true;
					searchingSound.MaybeNull().PlayAtPosition(player.value.transform.position);
                    monsterSearchAnim.value.SetActive(true);
                    

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