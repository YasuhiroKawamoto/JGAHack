
using UnityEngine;

public enum Direction
{
    Front,
    Back,
    Left,
    Right
}

public static class DirectionEx
{
    public static Vector3 Vector(this Direction dir)
    {
        float z = 0.0f;
        switch (dir)
        {
            case Direction.Back:
                z = 180.0f;
                break;
            case Direction.Left:
                z = 90.0f;
                break;
            case Direction.Right:
                z = 270.0f;
                break;
        }
        return new Vector3(0.0f, 0.0f, z);
    }
}

public enum EffectID
{
    Console, DestoryEnemy, DestoryObj, EnemyDistinction, EnemyRecovery, EnemyRespown, GoalPoint, LookOn, Numparent, ParticleLight, Wave
}

public static class EffectIDEx
{
    public static bool Rect(this EffectID ID)
    {
        bool isRect = false;
        switch (ID)
        {
            case EffectID.Console:
                isRect = true;
                break;
            case EffectID.DestoryEnemy:
                isRect = false;
                break;
            case EffectID.DestoryObj:
                isRect = false;
                break; 
            case EffectID.EnemyDistinction:
                isRect = false;
                break;
            case EffectID.EnemyRecovery:
                isRect = true;
                break;
            case EffectID.EnemyRespown:
                isRect = false;
                break;
            case EffectID.GoalPoint:
                isRect = false;
                break;
            case EffectID.LookOn:
                isRect = true;
                break;
            case EffectID.Numparent:
                isRect = false;
                break;
            case EffectID.ParticleLight:
                isRect = false;
                break;
            case EffectID.Wave:
                isRect = false;
                break;
        }
        return isRect;
    }
}