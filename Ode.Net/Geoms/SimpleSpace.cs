﻿using Ode.Net.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ode.Net.Geoms
{
    /// <summary>
    /// Represents a simple collision space.
    /// </summary>
    public sealed class SimpleSpace : Space
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSpace"/> class.
        /// </summary>
        public SimpleSpace()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSpace"/> class
        /// inside the specified space.
        /// </summary>
        /// <param name="space">The space which will contain the new simple space.</param>
        public SimpleSpace(Space space)
            : base(NativeMethods.dSimpleSpaceCreate(space != null ? space.Id : dSpaceID.Null))
        {
        }
    }
}
