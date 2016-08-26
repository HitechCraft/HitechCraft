namespace HitechCraft.DAL.NHibernate.Helper
{
    #region Using Directives

    using System;
    using FluentNHibernate.Automapping;

    #endregion

    /// <summary>
    /// Store mappings settings
    /// </summary>
    public class AutomappingHelper : DefaultAutomappingConfiguration
    {
        /// <summary>
        /// Namespace of mapping domains
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "HitechCraft.DAL.Domain";
        }
    }
}
