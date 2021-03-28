using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInversionPrinciple
{
  public enum Relationship
  {
    Parent,
    Child,
    Sibling
  }

  public class Person
  {
    public string Name { get; set; }
  }

  public interface IRelationshipBrowser
  {
    IEnumerable<Person> FindAllChildrenOf(string parentName);
  }

  // low-level
  public class RelationShips : IRelationshipBrowser
  {
    private readonly List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

    //public List<(Person, Relationship, Person)> Relations => relations;

    public void AddParentAndChild(Person parent, Person child)
    {
      relations.Add((parent, Relationship.Parent, child));
      relations.Add((child, Relationship.Child, parent));
    }

    public IEnumerable<Person> FindAllChildrenOf(string parentName)
    {
      foreach ((Person, Relationship, Person) relation in relations.Where(x =>
          x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
      {
        yield return relation.Item3;
      }
    }
  }

  // high-level
  public class Research
  {
    //public Research(RelationShips relationShips)
    //{
    //  List<(Person, Relationship, Person)> relations = relationShips.Relations;

    //  foreach ((Person, Relationship, Person) relation in relations.Where(x =>
    //    x.Item1.Name == "John" && x.Item2 == Relationship.Parent))
    //  {
    //    Console.WriteLine($"John's child is {relation.Item3.Name}");
    //  }
    //} 

    public Research(IRelationshipBrowser browser)
    {
      IEnumerable<Person> children = browser.FindAllChildrenOf("John");

      foreach (Person child in children)
      {
        Console.WriteLine($"John's child is {child.Name}");
      }
    }

    static void Main(string[] args)
    {
      Person parent = new Person {Name = "John"};
      Person child1 = new Person {Name = "Chris"};
      Person child2 = new Person {Name = "Mary"};

      RelationShips relationShips = new RelationShips();
      relationShips.AddParentAndChild(parent, child1);
      relationShips.AddParentAndChild(parent, child2);

      Research research = new Research(relationShips);
    }
  }
}
