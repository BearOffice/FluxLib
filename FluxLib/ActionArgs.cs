﻿namespace FluxLib;

public class ActionArgs
{
    public Enum Type { get; }
    private readonly Dictionary<Enum, object?> _dataDic;
    private object? _anonymousData;

    public ActionArgs(Enum actionType)
    {
        Type = actionType;
        _dataDic = [];
    }

    public ActionArgs(Enum actionType, object anonymousData)
    {
        Type = actionType;
        _dataDic = [];
        _anonymousData = anonymousData;
    }

    public void AddAnonymousData(object data)
    {
        _anonymousData = data;
    }

    public void AddData(Enum tag, object data)
    {
        _dataDic.Add(tag, data);
    }

    public void AddEmptyData(Enum tag)
    {
        _dataDic.Add(tag, null);
    }

    public object? GetAnonymousData()
    {
        return _anonymousData;
    }

    public object? GetData(Enum tag)
    {
        return _dataDic[tag];
    }

    public bool TryGetData(Enum tag, out object? data)
    {
        return _dataDic.TryGetValue(tag, out data);
    }

    public Enum[] GetDataTags()
    {
        return [.. _dataDic.Keys];
    }

    public void RemoveAnonymousData()
    {
        _anonymousData = null;
    }

    public void RemoveData(Enum tag)
    {
        _dataDic.Remove(tag);
    }

    public void Clear()
    {
        _anonymousData = null;
        _dataDic.Clear();
    }
}
