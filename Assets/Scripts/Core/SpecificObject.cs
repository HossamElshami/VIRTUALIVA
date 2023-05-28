public class SpecificObject : SaveableObject
{
    public override void Save(int id)
    {
        base.Save(id);
    }
    public override void Load(string[] values)
    {
        base.Load(values);
    }
    public override void DestroySaveable()
    {
        base.DestroySaveable();
    }

}
