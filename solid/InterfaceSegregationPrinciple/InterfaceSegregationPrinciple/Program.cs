using System;

namespace InterfaceSegregationPrinciple
{
  public class Document
  {

  }

  public interface IMachine
  {
    void Print(Document d);
    void Scan(Document d);
    void Fax(Document d);
  }

  public class MultifunctionPrinter : IMachine
  {
    public void Print(Document d)
    {
      //
    }

    public void Scan(Document d)
    {
      //
    }

    public void Fax(Document d)
    {
      //
    }
  }

  public class OldFashionedPrinter : IMachine
  {
    public void Print(Document d)
    {
      //
    }

    public void Scan(Document d)
    {
      throw new NotImplementedException();
    }

    public void Fax(Document d)
    {
      throw new NotImplementedException();
    }
  }

  public interface IPrinter
  {
    void Print(Document d);
  }

  public interface IScanner
  {
    void Scan(Document d);
  }

  public interface IFaxMachine
  {
    void Fax(Document d);
  }

  public class Photocopier : IPrinter, IScanner
  {
    public void Print(Document d)
    {
      //
    }

    public void Scan(Document d)
    {
      //
    }
  }

  public interface IMultifunctionDevice : IScanner, IPrinter, IFaxMachine
  {

  }

  public class MultifunctionMachine : IMultifunctionDevice
  {
    private readonly IPrinter _printer;
    private readonly IScanner _scanner;
    private readonly IFaxMachine _faxMachine;

    public MultifunctionMachine(IPrinter printer, IScanner scanner, IFaxMachine faxMachine)
    {
      _printer = printer ?? throw new ArgumentNullException(paramName: nameof(printer));
      _scanner = scanner ?? throw new ArgumentNullException(paramName: nameof(scanner));
      _faxMachine = faxMachine ?? throw new ArgumentNullException(paramName: nameof(faxMachine));
    }

    public void Scan(Document d)
    {
      _scanner.Scan(d);
    }

    public void Print(Document d)
    {
      _printer.Print(d);
    }

    public void Fax(Document d)
    {
      _faxMachine.Fax(d);
    }
  }


  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
    }
  }
}
