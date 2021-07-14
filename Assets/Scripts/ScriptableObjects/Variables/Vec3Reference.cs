using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Vec3Reference
{
    public bool UseConstant = true;
    public Vector3 ConstantValue;
    public Vec3VariableSO Variable;

    public Vec3Reference()
    { }

    public Vec3Reference(Vector3 value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public Vector3 Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator Vector3(Vec3Reference reference)
    {
        return reference.Value;
    }



}
