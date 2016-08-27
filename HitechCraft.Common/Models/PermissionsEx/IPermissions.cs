namespace HitechCraft.Common.Models.PermissionsEx
{
    public interface IPermissions
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