using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerV3.MVVM
{
    /// <summary>
    /// An interface which should be implemented by ViewModels which are controlled by a controller
    /// </summary>
    /// <typeparam name="TController">The interface type of the Controller</typeparam>
    public interface IControllable<TController> where TController : class
    {
        /// <summary>The controller</summary>
        TController Controller { get; set; }
    }
}
