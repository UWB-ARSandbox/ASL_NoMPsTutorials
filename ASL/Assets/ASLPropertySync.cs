using ASL;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASLPropertySync : MonoBehaviour
{
    public delegate float[] ObjToFloatArray(object obj);
    public delegate object ObjFromFloatArray(float[] obj);
    public delegate float[] ToFloatArray<T>(T obj);
    public delegate T FromFloatArray<T>(float[] obj);

    public delegate object GetLocalObject();
    public delegate void SetLocalObject(object obj);
    public delegate T GetLocalValue<T>();
    public delegate void SetLocalValue<T>();

    private class SynchronizedProperty
    {
        public object remote;
        public object previous;
        public bool changedRemotely;
        public ObjToFloatArray toFloats;
        public ObjFromFloatArray fromFloats;
        public GetLocalObject get;
        public SetLocalObject set;
    }

    private List<SynchronizedProperty> props;

    public void Synchronize<T>(GetLocalValue<T> get, SetLocalValue<T> set,
        ToFloatArray<T> toFloats, FromFloatArray<T> fromFloats)
    {
        SynchronizedProperty newProp = new SynchronizedProperty();
        newProp.remote = get();
        newProp.previous = get();
        newProp.changedRemotely = false;
        newProp.toFloats = toFloats as ObjToFloatArray;
        newProp.fromFloats = fromFloats as ObjFromFloatArray;
        newProp.get = get as GetLocalObject;
        newProp.set = set as SetLocalObject;
        props.Add(newProp);
    }

    public void Synchronize(GetLocalValue<Vector3> get, SetLocalValue<Vector3> set) {
        Synchronize<Vector3>(get, set, ConvertToFloats, (float[] f) =>
        {
            Vector3 v;
            ConvertFromFloats(out v, f);
            return v;
        });
    }
    public void Synchronize(GetLocalValue<Quaternion> get, SetLocalValue<Quaternion> set)
    {
        Synchronize<Quaternion>(get, set, ConvertToFloats, (float[] f) =>
        {
            Quaternion v;
            ConvertFromFloats(out v, f);
            return v;
        });
    }
    public void Synchronize(GetLocalValue<float> get, SetLocalValue<float> set)
    {
        Synchronize<float>(get, set, ConvertToFloats, (float[] f) =>
        {
            float v;
            ConvertFromFloats(out v, f);
            return v;
        });
    }

    public float[] ConvertToFloats(Vector3 vec)
    {
        float[] floats = new float[3];
        floats[0] = vec.x;
        floats[1] = vec.y;
        floats[2] = vec.z;
        return floats;
    }
    public void ConvertFromFloats(out Vector3 res, float[] f)
    {
        res = new Vector3(f[0], f[1], f[2]);
    }
    public float[] ConvertToFloats(Quaternion q)
    {
        float[] floats = new float[4];
        floats[0] = q.x;
        floats[1] = q.y;
        floats[2] = q.z;
        floats[3] = q.w;
        return floats;
    }
    public void ConvertFromFloats(out Quaternion res, float[] f)
    {
        res = new Quaternion(f[0], f[1], f[2], f[3]);
    }
    public float[] ConvertToFloats(float f)
    {
        float[] floats = new float[1];
        floats[0] = f;
        return floats;
    }
    public void ConvertFromFloats(out float res, float[] f)
    {
        res = f[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        props = new List<SynchronizedProperty>();

        GetComponent<ASLObject>()._LocallySetFloatCallback((string _id, float[] f) =>
        {
            if (ASLUserID.ID() != f[0])
            {
                int propIndex = (int) f[1];
                props[propIndex].remote = props[propIndex].fromFloats(f);
                props[propIndex].changedRemotely = true;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        for (int propID = 0; propID < props.Count; ++propID)
        {
            SynchronizedProperty prop = props[propID];
            if (!prop.get().Equals(prop.previous))
            {
                GetComponent<ASLObject>().SendAndSetClaim(() =>
                {
                    float[] propFloats = prop.toFloats(prop.get());
                    float[] f = new float[propFloats.Length + 2];
                    propFloats.CopyTo(f, 2);
                    f[0] = ASLUserID.ID();
                    f[1] = propID;
                });
            }

            if (prop.changedRemotely)
            {
                prop.changedRemotely = false;
                prop.set(prop.remote);
            }
            prop.previous = prop.get();
        }
    }
}
