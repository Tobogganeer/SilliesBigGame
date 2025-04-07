using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Tobo.Audio;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class MoveRoomAT : ActionTask {


        public BBParameter<GameObject> player;
        public BBParameter<int> currentRoom;
        public BBParameter<bool> sameRoom;
        public Sound movementSound;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			//int oldValue = currentRoom.value;
			//currentRoom.value = Random.Range(1, 4);
			if (currentRoom.value == 1)
			{
				int nextRoom = Random.Range(1, 3);
				currentRoom.value = nextRoom;
			}
			else if (currentRoom.value == 4)
			{
                int nextRoom = Random.Range(3, 5);
                currentRoom.value = nextRoom;
            }
			else
			{
				int nextRoom = Random.Range(currentRoom.value - 1, currentRoom.value + 2);
				currentRoom.value = nextRoom;
			}


            if (player.value.GetComponent<PlayerMovement>().currentRoom == currentRoom.value)
            {
                sameRoom.value = true;
            }

			EndAction(true);

        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}