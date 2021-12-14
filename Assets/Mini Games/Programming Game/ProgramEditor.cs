using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProgramEditor : MonoBehaviour
{
    public static ProgramEditor Instance;

    [HideInInspector]
    public Command[] commands;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RunProgram()
    {
        StopAllCoroutines();

        GridProgram.Instance.SetRobotOnStartPosition();
        foreach (var point in GridProgram.Instance.Points)
        {
            point.Restart();
        }

        StartCoroutine(ProgramProcessing(commands));
    }

    private IEnumerator ProgramProcessing(Command[] commands)
    {
        for (int i = 0; i < commands.Length; i++)
        {
            if (commands[i] is null)
                continue;

            if (commands[i] is EndCommand)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(1);
            }

            if (commands[i].GetType() == typeof(MoveForvardCommand) ||
                commands[i].GetType() == typeof(TurnRightCommand) ||
                commands[i].GetType() == typeof(TurnLeftCommand) ||
                commands[i].GetType() == typeof(PickUpCommand) ||
                commands[i].GetType() == typeof(PutDownCommand) )
            {
                commands[i].Action();
            }
            else if (commands[i].GetType() == typeof(CycleCommand))
            {
                CycleCommand cycle = commands[i] as CycleCommand;
                Command[] subCommands = GetSlice(i + 1, commands);

                if (subCommands is null)
                    continue;

                for (int j = 0; j < cycle.Iterations; j++)
                {
                    IEnumerator sub = ProgramProcessing(subCommands);
                    while (sub.MoveNext()) yield return new WaitForSeconds(1);
                }
                i += subCommands.Length;
            }
        }
    }

    private Command[] GetSlice(int startIndex, Command[] commands)
    {
        int counter = 1;
        for (int i = startIndex; i < commands.Length; i++)
        {
            if (commands[i] is null)
                continue;

            if (commands[i].GetType() == typeof(CycleCommand))
            {
                counter++;
            }
            else if (commands[i].GetType() == typeof(EndCommand))
            {
                counter--;
            }

            if (counter == 0)
            {
                Command[] slice = new Command[i - startIndex];
                int k = 0;

                for (int j = startIndex; j < i; j++)
                {
                    slice[k] = commands[j];
                    k++;
                }

                return slice;
            }
        }

        return null;
    }
}

public abstract class Command
{
    public string ForWrite = "";

    public abstract void Action();
}

class EndCommand : Command
{
    public override void Action() {}
    
    public EndCommand()
    {
        ForWrite = "}";
    }
}

class MoveForvardCommand : Command
{
    public override void Action()
    {
        GridProgram.Instance.TryMoveRobot();
    }

    public MoveForvardCommand()
    {
        ForWrite = "<color=yellow>MoveForward</color>();";
    }
}

class TurnLeftCommand : Command
{
    public override void Action()
    {
        GridProgram.Instance.RotateRobot(GridProgram.Rotation.Left);
    }

    public TurnLeftCommand()
    {
        ForWrite = "<color=yellow>TurnLeft</color>();";
    }
}

class TurnRightCommand : Command
{
    public override void Action()
    {
        GridProgram.Instance.RotateRobot(GridProgram.Rotation.Right);
    }

    public TurnRightCommand()
    {
        ForWrite = "<color=yellow>TurnRight</color>();";
    }
}

class CycleCommand : Command
{
    public int Iterations;

    public CycleCommand(int iterations) 
    {
        Iterations = iterations;
        ForWrite = string.Format("<color=magenta>do</color> <color=green>{0}</color> {{", iterations);
    }

    public override void Action() {}
}

class PickUpCommand : Command
{
    public override void Action()
    {
        foreach (var point in GridProgram.Instance.Points)
        {
            if (point.PointPos == GridProgram.Instance.RobotPosition)
            {
                point.PointOnGround = false;
                point.PointObject.transform.parent = GridProgram.Instance.Robot.transform;
                point.PointObject.GetComponent<SpriteRenderer>().sortingOrder = 5;
            }
        }
    }

    public PickUpCommand()
    {
        ForWrite = "<color=yellow>PickUp</color>();";
    }
}

class PutDownCommand : Command
{
    public override void Action()
    {
        int activePoints = 0;
        foreach (var point in GridProgram.Instance.Points)
        {
            if (!point.PointOnGround)
            {
                point.PointObject.transform.parent = null;
                point.PointObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                point.PointPos = GridProgram.Instance.RobotPosition;
                point.PointOnGround = true;
            }

            if (point.Active)
            {
                activePoints++;
            }
        }

        if (activePoints == GridProgram.Instance.Points.Count)
        {
            ScenesManager.Instance.LoseAction = null;
            ScenesManager.Instance?.TryLoadScene(0);
        }
    }

    public PutDownCommand()
    {
        ForWrite = "<color=yellow>PutDown</color>();";
    }
}

class FunctionCommand : Command
{
    List<Command> commands = new List<Command>();

    public override void Action()
    {
        Debug.Log("Function");
    }

    public FunctionCommand()
    {
        ForWrite = "Func();";
    }
}

class IfCommand : Command
{
    public override void Action()
    {
        Debug.Log("If");
    }

    public IfCommand()
    {
        ForWrite = "if() {;";
    }
}

