using System;


[Serializable]
public abstract class BaseUpgrade
{
    public virtual object Clone()
    {
        return null;
    }

    public abstract void Execute(PlayerController player , WeaponBase weapon);


}

