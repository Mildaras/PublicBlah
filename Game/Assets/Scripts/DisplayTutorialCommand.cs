using UnityEngine;

public class DisplayTutorialCommand : ICommand
{
    private Animator animator;

    public DisplayTutorialCommand(Animator animator)
    {
        this.animator = animator;
    }

    public void Execute()
    {
        animator.SetTrigger("displayTutorial");
        Debug.Log("Tutorial displayed");
    }

    public void Undo()
    {
        animator.ResetTrigger("displayTutorial");
        Debug.Log("Tutorial hidden");
    }
}
