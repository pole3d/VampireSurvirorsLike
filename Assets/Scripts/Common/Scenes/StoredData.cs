using System;
using System.Collections.Generic;



[Serializable]
public class StoredData 
{
    public List<StoredDataValue> Values = new List<StoredDataValue>();
    public string Name;

    public StoredData(string name)
    {
        Name = name;
    }

    public void SetValue(string name , int value)
    {
        StoredDataValue storedData = GetValueInternal(name);

        if ( storedData == null )
        {
            storedData = new StoredDataValue();
            storedData.Name = name;
            Values.Add(storedData);
        }

        storedData.Value = value;
    }

    public void SetValue(string name, string value)
    {
        StoredDataValue storedData = GetValueInternal(name);

        if (storedData == null)
        {
            storedData = new StoredDataValue();
            storedData.Name = name;
            Values.Add(storedData);
        }

        storedData.ValueStr = value;
    }

    public int GetValue(string name)
    {
        StoredDataValue value = GetValueInternal(name);
        if (value == null)
        {
            return -1;
        }

        return value.Value;
    }

    public string GetStringValue(string name)
    {
        StoredDataValue value = GetValueInternal(name);
        if (value == null)
        {
            return String.Empty;
        }

        return value.ValueStr;
    }


    StoredDataValue GetValueInternal(string name)
    {
        return Values.Find((item) => name == item.Name);
    }

    internal void DeleteValues()
    {
        Values.Clear();
    }
}

[Serializable]
public class StoredDataValue
{
    public string Name;
    public int Value = -1;
    public string ValueStr;
}
