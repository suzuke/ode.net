﻿using Ode.Net.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
#if SINGLE_PRECISION
using dReal = System.Single;
#elif DOUBLE_PRECISION
using dReal = System.Double;
#else
#error You must define SINGLE_PRECISION or DOUBLE_PRECISION
#endif

namespace Ode.Net.Collision
{
    /// <summary>
    /// Represents a data object which is used to store triangle mesh data.
    /// </summary>
    public sealed class TriMeshData : IDisposable
    {
        readonly dTriMeshDataID id;
        DataHandle verticesData;
        DataHandle indicesData;
        DataHandle normalsData;

        /// <summary>
        /// Initializes a new instance of the <see cref="TriMeshData"/> class.
        /// </summary>
        public TriMeshData()
        {
            id = NativeMethods.dGeomTriMeshDataCreate();
        }

        internal dTriMeshDataID Id
        {
            get { return id; }
        }

        /// <summary>
        /// Builds the triangle mesh data object with single precision vertex data.
        /// </summary>
        /// <param name="vertices">The array of mesh vertices.</param>
        /// <param name="indices">
        /// The array of indices forming the triangle mesh. Each element in the array
        /// represents the index of one of the vertices.
        /// </param>
        public void BuildSingle(float[] vertices, int[] indices)
        {
            BuildSingle(vertices, indices, null);
        }

        /// <summary>
        /// Builds the triangle mesh data object with single precision vertex data
        /// and pre-calculated normals.
        /// </summary>
        /// <param name="vertices">The array of mesh vertices.</param>
        /// <param name="indices">
        /// The array of indices forming the triangle mesh. Each element in the array
        /// represents the index of one of the vertices.
        /// </param>
        /// <param name="normals">The array of pre-calculated normals.</param>
        public void BuildSingle(float[] vertices, int[] indices, dReal[] normals)
        {
            int vertexCount = vertices.Length / 3;
            int vertexStride = 3 * Marshal.SizeOf(typeof(float));
            int indexCount = indices.Length;
            int triStride = 3 * Marshal.SizeOf(typeof(int));
            StoreMeshData(vertices, indices, normals);
            if (normals != null)
            {
                NativeMethods.dGeomTriMeshDataBuildSingle1(
                    id, verticesData, vertexStride, vertexCount,
                    indicesData, indexCount, triStride, normalsData);
            }
            else
            {
                NativeMethods.dGeomTriMeshDataBuildSingle(
                id, verticesData, vertexStride, vertexCount,
                indicesData, indexCount, triStride);
            }
        }

        /// <summary>
        /// Builds the triangle mesh data object with double precision vertex data.
        /// </summary>
        /// <param name="vertices">The array of mesh vertices.</param>
        /// <param name="indices">
        /// The array of indices forming the triangle mesh. Each element in the array
        /// represents the index of one of the vertices.
        /// </param>
        public void BuildDouble(double[] vertices, int[] indices)
        {
            BuildDouble(vertices, indices, null);
        }

        /// <summary>
        /// Builds the triangle mesh data object with double precision vertex data
        /// and pre-calculated normals.
        /// </summary>
        /// <param name="vertices">The array of mesh vertices.</param>
        /// <param name="indices">
        /// The array of indices forming the triangle mesh. Each element in the array
        /// represents the index of one of the vertices.
        /// </param>
        /// <param name="normals">The array of pre-calculated normals.</param>
        public void BuildDouble(double[] vertices, int[] indices, dReal[] normals)
        {
            int vertexCount = vertices.Length / 3;
            int vertexStride = 3 * Marshal.SizeOf(typeof(double));
            int indexCount = indices.Length;
            int triStride = 3 * Marshal.SizeOf(typeof(int));
            StoreMeshData(vertices, indices, normals);
            if (normals != null)
            {
                NativeMethods.dGeomTriMeshDataBuildDouble1(
                    id, verticesData, vertexStride, vertexCount,
                    indicesData, indexCount, triStride, normalsData);
            }
            else
            {
                NativeMethods.dGeomTriMeshDataBuildDouble(
                id, verticesData, vertexStride, vertexCount,
                indicesData, indexCount, triStride);
            }
        }

        /// <summary>
        /// Builds the triangle mesh data object with vertex data.
        /// </summary>
        /// <param name="vertices">The array of mesh vertices.</param>
        /// <param name="indices">
        /// The array of indices forming the triangle mesh. Each element in the array
        /// is an index into the vertices array.
        /// </param>
        public void BuildSimple(Vector3[] vertices, int[] indices)
        {
            BuildSimple(vertices, indices, null);
        }

        /// <summary>
        /// Builds the triangle mesh data object with vertex data and
        /// pre-calculated normals.
        /// </summary>
        /// <param name="vertices">The array of mesh vertices.</param>
        /// <param name="indices">
        /// The array of indices forming the triangle mesh. Each element in the array
        /// is an index into the vertices array.
        /// </param>
        /// <param name="normals">The array of pre-calculated normals.</param>
        public void BuildSimple(Vector3[] vertices, int[] indices, dReal[] normals)
        {
            StoreMeshData(vertices, indices, normals);
            if (normals != null)
            {
                NativeMethods.dGeomTriMeshDataBuildSimple1(
                    id, verticesData, vertices.Length,
                    indicesData, indices.Length, normalsData);
            }
            else
            {
                NativeMethods.dGeomTriMeshDataBuildSimple(
                    id, verticesData, vertices.Length,
                    indicesData, indices.Length);
            }
        }

        /// <summary>
        /// Preprocesses the trimesh data to remove unnecessary edges and vertices.
        /// </summary>
        public void Preprocess()
        {
            NativeMethods.dGeomTriMeshDataPreprocess(id);
        }

        /// <summary>
        /// Efficiently updates the internal triangle representation when dynamically
        /// deforming mesh vertices.
        /// </summary>
        public void Update()
        {
            NativeMethods.dGeomTriMeshDataUpdate(id);
        }

        private static void ReleaseDataStore(ref DataHandle storeHandle)
        {
            if (storeHandle != null)
            {
                storeHandle.Close();
            }

            storeHandle = null;
        }

        private void ReleaseDataStores()
        {
            ReleaseDataStore(ref verticesData);
            ReleaseDataStore(ref indicesData);
            ReleaseDataStore(ref normalsData);
        }

        private void StoreMeshData<TVertex>(TVertex[] vertices, int[] indices, dReal[] normals)
        {
            ReleaseDataStores();
            verticesData = new DataHandle(vertices.Length * Marshal.SizeOf(typeof(TVertex)));
            indicesData = new DataHandle(indices.Length * Marshal.SizeOf(typeof(int)));

            float[] floatVertices = vertices as float[];
            if (floatVertices != null) verticesData.Copy(floatVertices);

            double[] doubleVertices = vertices as double[];
            if (doubleVertices != null) verticesData.Copy(doubleVertices);

            indicesData.Copy(indices);
            if (normals != null)
            {
                normalsData = new DataHandle(normals.Length * Marshal.SizeOf(typeof(int)));
                normalsData.Copy(normals);
            }
        }

        /// <summary>
        /// Destroys the triangle mesh data.
        /// </summary>
        public void Dispose()
        {
            if (!id.IsClosed)
            {
                ReleaseDataStores();
                id.Close();
            }
        }
    }
}
