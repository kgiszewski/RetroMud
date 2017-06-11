﻿using System;

namespace RetroMud.Tcp.Serialization
{
    public interface IHandleSerialization
    {
        string Serialize(object input);
        object Deserialize(string input);
        object Deserialize(string input, Type type);
        T Deserialize<T>(string input);
    }
}
