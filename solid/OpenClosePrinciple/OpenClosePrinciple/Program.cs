using System;
using System.Collections.Generic;

namespace OpenClosePrinciple
{
  public enum Color
  {
    Red, Green, Blue
  }

  public enum Size
  {
    Small, Medium, Large, Huge
  }

  public class Product
  {
    public string Name;
    public Color Color;
    public Size Size;

    public Product(string name, Color color, Size size)
    {

      Name = name??throw new ArgumentNullException(paramName: nameof(name));
      Color = color;
      Size = size;
    }

    public override string ToString()
    {
      return $"{Name} {Color} {Size}";
    }
  }

  public class ProductFilter
  {
    public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
    {
      foreach (Product product in products)
      {
        if (product.Size == size)
        {
          yield return product;
        }
      }
    }

    public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
    {
      foreach (Product product in products)
      {
        if (product.Color == color)
        {
          yield return product;
        }
      }
    }

    public IEnumerable<Product> FilterByColorAndSize(IEnumerable<Product> products, Color color, Size size)
    {
      foreach (Product product in products)
      {
        if (product.Color == color && product.Size == size)
        {
          yield return product;
        }
      }
    }
  }

  public interface ISpecification<T>
  {
    bool IsSatisfied(T t);
  }

  public class ColorSpecification : ISpecification<Product>
  {
    private readonly Color _color;

    public ColorSpecification(Color color)
    {
      this._color = color;
    }

    public bool IsSatisfied(Product t)
    {
      return t.Color == _color;
    }
  }

  public class SizeSpecification : ISpecification<Product>
  {
    private readonly Size _size;

    public SizeSpecification(Size size)
    {
      _size = size;
    }

    public bool IsSatisfied(Product t)
    {
      return t.Size == _size;
    }
  }

  public class AndSpecification<T> : ISpecification<T>
  {
    private readonly ISpecification<T> _firstSpec;
    private readonly ISpecification<T> _secondSpec;

    public AndSpecification(ISpecification<T> firstSpec, ISpecification<T> secondSpec)
    {
      _firstSpec = firstSpec ?? throw new ArgumentNullException(paramName: nameof(firstSpec));
      _secondSpec = secondSpec ?? throw new ArgumentNullException(paramName: nameof(secondSpec));
    }

    public bool IsSatisfied(T t)
    {
      return _firstSpec.IsSatisfied(t) && _secondSpec.IsSatisfied(t);
    }
  }

  public interface IFilter<T>
  {
    IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
  }

  public class BetterFilter : IFilter<Product>
  {
    public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
    {
      foreach (Product product in items)
      {
        if (spec.IsSatisfied(product))
        {
          yield return product;
        }
      }
    }
  }
    
  class Program
  {
    static void Main(string[] args)
    {
      Product apple = new Product("Apple", Color.Red, Size.Small);
      Product tree = new Product("Tree", Color.Green, Size.Large);
      Product house = new Product("House", Color.Blue, Size.Huge);
      Product[] products = {apple, tree, house};
      
      Console.WriteLine("Green products(old)");
      ProductFilter pf = new ProductFilter();

      foreach (Product product in pf.FilterByColor(products, Color.Green))
      {
        Console.WriteLine(product);
      }

      Console.WriteLine("Green products(new)");
      ColorSpecification greenSpecification = new ColorSpecification(Color.Green);
      BetterFilter betterFilter = new BetterFilter();
      IEnumerable<Product> greenProducts = betterFilter.Filter(products, greenSpecification);
      
      foreach (Product product in greenProducts)
      {
        Console.WriteLine(product);
      }

      Console.WriteLine("Green and Large products");
      SizeSpecification largeSpecification = new SizeSpecification(Size.Large);
      AndSpecification<Product> andSpecification =
        new AndSpecification<Product>(greenSpecification, largeSpecification);

      IEnumerable<Product> andProducts = betterFilter.Filter(products, andSpecification);

      foreach (Product product in andProducts)
      {
        Console.WriteLine(product);
      }
    }
  }
}
