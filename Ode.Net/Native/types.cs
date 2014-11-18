﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using dReal = System.Single;

namespace Ode.Net.Native
{
    [StructLayout(LayoutKind.Sequential)]
    struct dWorldStepReserveInfo
    {
        internal uint struct_size;
        internal float reserve_factor;
        internal uint reserve_minimum;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct dWorldStepMemoryFunctionsInfo
    {
        internal uint struct_size;
        internal IntPtr alloc_block;
        internal IntPtr shrink_block;
        internal IntPtr free_block;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct dThreadingFunctionsInfo
    {
        internal uint struct_size;
        IntPtr alloc_mutex_group;
        IntPtr free_mutex_group;
        IntPtr lock_group_mutex;
        IntPtr unlock_group_mutex;
        IntPtr alloc_call_wait;
        IntPtr reset_call_wait;
        IntPtr free_call_wait;
        IntPtr post_call;
        IntPtr alter_call_dependencies_count;
        IntPtr wait_call;
        IntPtr retrieve_thread_count;
        IntPtr preallocate_resources_for_calls;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void dMovedCallback(IntPtr b);

    [StructLayout(LayoutKind.Sequential)]
    struct dJointFeedback
    {
        internal Vector3 f1;
        internal Vector3 t1;
        internal Vector3 f2;
        internal Vector3 t2;
    }

    enum dJointParam
    {
        dParamLoStop = 0x000,
        dParamHiStop,
        dParamVel,
        dParamLoVel,
        dParamHiVel,
        dParamFMax,
        dParamFudgeFactor,
        dParamBounce,
        dParamCFM,
        dParamStopERP,
        dParamStopCFM,
        dParamSuspensionERP,
        dParamSuspensionCFM,
        dParamERP
    }

    enum dTriMeshDataType
    {
        FaceNormals
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void dNearCallback(IntPtr data, IntPtr o1, IntPtr o2);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate dReal dHeightfieldGetHeight(IntPtr p_user_data, int x, int z);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int dTriCallback(IntPtr TriMesh, IntPtr RefObject, int TriangleIndex);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void dTriArrayCallback(IntPtr TriMesh, IntPtr RefObject, IntPtr TriIndices, int TriCount);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int dTriRayCallback(IntPtr TriMesh, IntPtr Ray, int TriangleIndex, dReal u, dReal v);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int dTriTriMergeCallback(IntPtr TriMesh, int FirstTriangleIndex, int SecondTriangleIndex);
}
