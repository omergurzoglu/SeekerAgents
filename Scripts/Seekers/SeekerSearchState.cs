
public  class SeekerSearchState : SeekerBaseState
{
    public SeekerSearchState(SeekerManager seekerManager) : base(seekerManager)
    {
    }
    
    public override void EnterState() => SeekerManager.StartPatrol();

    public override void Tick() { }

    public override void ExitState()
    {
        SeekerManager.StopPatrol();
    }
}


