﻿using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ode.Net.Native
{
    class dBodyID : SafeHandleZeroOrMinusOneIsInvalid
    {
        World owner;
        internal static readonly dBodyID Null = new NulldBodyID();

        internal dBodyID()
            : base(true)
        {
        }

        private dBodyID(bool ownsHandle)
            : base(ownsHandle)
        {
        }

        internal World Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        protected override bool ReleaseHandle()
        {
            if (owner == null || !owner.Id.IsClosed)
            {
                NativeMethods.dBodyDestroy(handle);
            }
            owner = null;
            return true;
        }

        class NulldBodyID : dBodyID
        {
            public NulldBodyID()
                : base(false)
            {
            }

            protected override bool ReleaseHandle()
            {
                return false;
            }
        }
    }
}
