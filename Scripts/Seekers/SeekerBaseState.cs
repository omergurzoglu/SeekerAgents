

public abstract class SeekerBaseState
{
    protected readonly SeekerManager SeekerManager;

    protected SeekerBaseState(SeekerManager seekerManager) => SeekerManager = seekerManager;
    
    public abstract void EnterState();
    public abstract void Tick();
    public abstract void ExitState();
    
}
