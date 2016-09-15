namespace HitechCraft.Core.Entity
{
    internal interface IPermissions
    {
        #region Properties
        
        string Name { get; set; }
        
        int Type { get; set; }
        
        string Permission { get; set; }
        
        string World { get; set; }
        
        string Value { get; set; }

        #endregion
    }
}