namespace HitechCraft.Common.DI
{
    #region Using Directives

    using System;
    using Ninject;

    #endregion

    public class BaseContainer : IContainer
    {
        private readonly IKernel _kernel;

        public BaseContainer(IKernel kernel)
        {
            this._kernel = kernel;
        }

        public T Resolve<T>()
        {
            return (T) this._kernel.Get(typeof (T));
        }

        public object Resolve(Type type)
        {
            return this._kernel.Get(type);
        }
    }
}
