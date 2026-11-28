using System;

[Serializable]
public struct Pair<K, V>
{
    public K Key;
    public V Value;

    public Pair(K key, V value)
    {
        Key = key;
        Value = value;
    }

    public void Deconstruct(out K key, out V value)
    {
        key = Key;
        value = Value;
    }
}