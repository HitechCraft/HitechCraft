namespace HitechCraft.Common.Models.PermissionsEx
{
    public interface IPexInheritance
    {
        #region Properties
        
        string Child { get; set; }
        
        string Parent { get; set; }
        
        int Type { get; set; }
        
        string World { get; set; }

        #endregion
    }
}