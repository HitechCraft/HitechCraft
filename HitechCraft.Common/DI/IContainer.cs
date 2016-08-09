namespace HitechCraft.Common.DI
{
    #region Using Directives

    using System;

    #endregion

    public interface IContainer
    {
        T Resolve<T>();

        object Resolve(Type type);
    }
}
