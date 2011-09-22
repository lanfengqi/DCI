using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Infrastructure.CrossCutting.Ioc
{
    /// <summary>
    /// IOC 工厂
    /// </summary>
    public sealed class IoCFactory
    {
        #region Singleton

        static readonly IoCFactory instance = new IoCFactory();

        /// <summary>
        /// Get singleton instance of IoCFactory
        /// </summary>
        public static IoCFactory Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region Members

        IInstanceLocator _CurrentContainer;

        /// <summary>
        /// Get current configured IContainer
        /// <remarks>
        /// At this moment only IoCUnityContainer existss
        /// </remarks>
        /// </summary>
        public IInstanceLocator CurrentContainer
        {
            get
            {
                return _CurrentContainer;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Only for singleton pattern, remove before field init IL anotation
        /// </summary>
        static IoCFactory() { }
        IoCFactory()
        {
            _CurrentContainer = new InstanceLocator();
        }

        #endregion
    }
}
