using System;
using System.Collections.Generic;
using System.Diagnostics;
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
  }

  public class Persistance
  {
    public void SaveToFile(Journal journal, string fileName, bool overwrite = false)
    {
      if (overwrite || !File.Exists(fileName))
      {
        File.WriteAllText(fileName, journal.ToString());
      }
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

      Persistance persistance = new Persistance();
      string fileName = @"c:\temp\journal.txt";
      persistance.SaveToFile(journal, fileName, true );
      Process.Start(fileName);
    }
  }
}
