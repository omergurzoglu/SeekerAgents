

public class SeekerAlertState:SeekerBaseState
{
    public SeekerAlertState(SeekerManager seekerManager) : base(seekerManager)
    {
    }

    public override void EnterState()
    {
        SeekerManager.AlertCoroutine();
    }

    public override void Tick()
    {
        
    }

    public override void ExitState()
    {
        
    }
}