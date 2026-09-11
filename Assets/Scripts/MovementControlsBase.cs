

public abstract class MovementControlsBase : IMovementControls
{
  public abstract float yawAmount {  get; }

  public abstract float pitchAmount {  get; }

  public abstract float rollAmount { get; }

  public abstract float thrustAmount {  get; }

}
