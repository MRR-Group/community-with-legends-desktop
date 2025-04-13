using System.Collections.Generic;
using System.Linq;

namespace Presentation.Utils;

public class FormExceptions
{
    public Dictionary<string, List<string>> Exceptions { get; private set;  } = new();

    public Dictionary<string, string> LimitToFirst()
    {
        var query = from pair in Exceptions 
            where pair.Value.Count >= 1 
            select (pair.Key.ToString(), pair.Value.First());

        return query.ToDictionary(item => item.Item1, item => item.Item2);
    }

    public void Clear()
    {
        Exceptions.Clear();
    }

    public void Add(string formName, string exception)
    {
        if (!Exceptions.ContainsKey(formName))
        {
            Exceptions.Add(formName, []);
        }
        
        Exceptions[formName].Add(exception);
    }
    
    public void AddRange(Dictionary<string, List<string>> exceptions)
    {
        foreach (var exception in exceptions)
        {
            AddRange(exception.Key, exception.Value);
        }
    }
    
    public void AddRange(string formName, List<string> exceptions)
    {
        if (!Exceptions.ContainsKey(formName))
        {
            Exceptions.Add(formName, []);
        }

        Exceptions[formName].AddRange(exceptions);
    }
}