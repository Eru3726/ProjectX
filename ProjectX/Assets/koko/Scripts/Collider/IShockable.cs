using UnityEngine;

public interface IShockable
{
    public float shockResist { get; }

    public void TakeShock(float value, Vector3 pos);

    public void TakeStop();
}
