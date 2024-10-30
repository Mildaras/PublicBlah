using UnityEngine;

public class PauseGameCommand : ICommand
{
    private bool previousState;

    public void Execute()
    {
        previousState = Time.timeScale == 0;
        Time.timeScale = 0;
    }

    public void Undo()
    {
        Time.timeScale = previousState ? 0 : 1;
    }
}
