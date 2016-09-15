namespace HitechCraft.Core.Models.Json
{
    /// <summary>
    /// User textures data
    /// </summary>
    public class JsonTextureData
    {
        #region Properties
        
        /// <summary>
        /// User skin data
        /// </summary>
        //TODO Реализовать загрузку плаща и добавить сюда параметр CAPE с ссылой на плащ
        public JsonUserSkinData SKIN { get; set; }

        #endregion
    }
}