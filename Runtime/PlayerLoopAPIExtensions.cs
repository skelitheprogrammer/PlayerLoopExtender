﻿using System;
using UnityEngine.LowLevel;

namespace PlayerLoopCustomizationAPI.Runtime
{
    public static partial class PlayerLoopAPIExtensions
    {
        public static ref PlayerLoopSystem GetLoopSystem<T>(this ref PlayerLoopSystem loopSystem) where T : struct
        {
            return ref PlayerLoopAPI.GetLoopSystem<T>(ref loopSystem);
        }

        public static ref PlayerLoopSystem InsertSystemBefore<T>(this ref PlayerLoopSystem parentSystem, in PlayerLoopSystem newSystem) where T : struct
        {
            ref PlayerLoopSystem[] systems = ref parentSystem.subSystemList;

            for (int i = 0; i < systems.Length; i++)
            {
                if (systems[i].type == typeof(T))
                {
                    return ref PlayerLoopAPI.InsertSystemAt(ref parentSystem, newSystem, i);
                }
            }

            throw new ArgumentException($"System {typeof(T).Name} is not presented in {(parentSystem.type != null ? parentSystem.type : "MainPlayerLoop")} system");
        }

        public static ref PlayerLoopSystem InsertSystemAfter<T>(this ref PlayerLoopSystem parentSystem, in PlayerLoopSystem newSystem) where T : struct
        {
            ref PlayerLoopSystem[] systems = ref parentSystem.subSystemList;

            for (int i = 0; i < systems.Length; i++)
            {
                if (systems[i].type == typeof(T))
                {
                    return ref PlayerLoopAPI.InsertSystemAt(ref parentSystem, newSystem, i + 1);
                }
            }

            throw new ArgumentException($"System {typeof(T).Name} is not presented in {(parentSystem.type != null ? parentSystem.type : "MainPlayerLoop")} system");
        }

        public static ref PlayerLoopSystem InsertAtBeginning(this ref PlayerLoopSystem parentSystem, in PlayerLoopSystem newSystem)
        {
            return ref PlayerLoopAPI.InsertSystemAt(ref parentSystem, newSystem, 0);
        }

        public static ref PlayerLoopSystem InsertAtEnd(this ref PlayerLoopSystem parentSystem, in PlayerLoopSystem newSystem)
        {
            return ref PlayerLoopAPI.InsertSystemAt(ref parentSystem, newSystem, parentSystem.subSystemList.Length);
        }
    }
}