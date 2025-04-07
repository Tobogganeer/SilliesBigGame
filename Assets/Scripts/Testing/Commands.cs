using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tobo.DevConsole;
using UnityEngine;

public class Commands : MonoBehaviour
{
    ConCommand give;
    ConCommand room1code;
    ConCommand room2code;
    new ConCommand camera;

    private void Awake()
    {
        give = new ConCommand("give", Give, "Gives an item", CVarFlags.None, GetGiveArgOptions);
        room1code = new ConCommand("room1code", (args) => Debug.Log("6327"), "Prints room 1 code (6327)");
        room1code = new ConCommand("room2code", (args) => Debug.Log("7361"), "Prints room 1 code (7361)");
        camera = new ConCommand("camera", Camera, "Moves to a camera", CVarFlags.None, GetCameraArgOptions);
    }

    void Camera(CmdArgs args)
    {
        if (args.ArgC > 1 && MoveCommand.instance.cameras.TryGetValue(args.ArgsString.Trim(), out CameraPosition pos))
        {
            // Teleport instantly
            PlayerMovement.instance.Travel(pos, pos.rotations[0].facing, pos.rotations[0].customFacingTarget, 0, 0, true);
        }
        else
            Debug.Log("Unknown position");
    }

    List<string> GetCameraArgOptions(string partialFirstArg)
    {
        return MoveCommand.instance.cameras.dictionary.Keys.ToList();
    }

    #region Give
    void Give(CmdArgs args)
    {
        if (args.ArgC > 1 && ItemData.instance.itemData.ContainsKey(args.Args[1])) // Check if they put in an item
            Inventory.GiveItem(args.Args[1]);
    }

    List<string> GetGiveArgOptions(string partialFirstArg)
    {
        //return new List<string>() { "flashlight" };
        return ItemData.instance.itemData.Keys.ToList();
    }
    #endregion
}
