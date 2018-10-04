﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    public class DIModule : Ninject.Modules.NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            Bind<IPaging>().To<Paging>();
            Bind<ISearch>().To<Search>();
            Bind<ISorting>().To<Sorting>();
        }
    }
}
