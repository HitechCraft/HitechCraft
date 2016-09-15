namespace HitechCraft.Core.Entity
{
    internal interface IPexEntity
    {
        #region Properties

        string Name { get; set; }

        int Type { get; set; }

        int Default { get; set; }

        #endregion
    }
}