using System;
using System.Collections.Generic;
using System.IO;

namespace SingleResponsibilityPrinciple
{
  public class Journal
  {
    private readonly List<string> _entries;
    private int _count;

    public Journal()
    {
      _entries = new List<string>();
      _count = 0;
    }

    public int AddEntry(string entry)
    { 
      _entries.Add($"{++_count}: {entry}");
      return _count;
    }

    public void RemoveEntry(int index)
    {
      _entries.RemoveAt(index);
    }

    public override string ToString()
    {
      return string.Join(Environment.NewLine, _entries);
    }

    public void Save(string fileName)
    {
      File.WriteAllText(fileName, ToString());
    }

    public static Journal Load(string fileName)
    {
      throw new NotImplementedException();
    }

    public void Load(Uri uri)
    {
      throw new NotImplementedException();
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      Journal journal = new Journal();
      journal.AddEntry("Today was cold");
      journal.AddEntry("Today was a good day for ice cream");
      Console.WriteLine(journal);
    }
  }
}
