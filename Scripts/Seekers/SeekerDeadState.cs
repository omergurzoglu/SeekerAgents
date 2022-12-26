
public class SeekerDeadState : SeekerBaseState
{
    public SeekerDeadState(SeekerManager seekerManager) : base(seekerManager)
    {
    }
    
    public override void EnterState()
    {
        
        SeekerManager.SeekerHit += SeekerManager.KillSeeker;
    }

    public override void Tick() { }

    public override void ExitState()
    {
        SeekerManager.SeekerHit -= SeekerManager.KillSeeker;
    }
}
