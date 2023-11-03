using System.Diagnostics.CodeAnalysis;
using static Csharp11.RequiredMembersAfter;
using static Csharp11.RequiredMembersPreviouslyUsingClasses;

namespace Csharp11;


public enum PersonTitle
{
    Mr,
    Mrs,
    Ms,
    Dr
}

/*
Title:          Required Members
Description:    Specify that a property or a field is required during initialization
Link:           https://github.com/dotnet/csharplang/blob/main/proposals/csharp-11.0/required-members.md
*/
public class RequiredMembers
{
    public void DemonstratePrevious()
    {
        var person = new Person(PersonTitle.Mr, "Foo", "Bar");
        var student = new Student(PersonTitle.Mr, "Foo", "Bar", Guid.NewGuid());
        Console.WriteLine(person);
        Console.WriteLine(student);
    }

    public void DemonstrateAfter()
    {
        var person = new RequiredPerson
        {
            FirstName = "Foo",
            LastName = "Bar",
            Title = PersonTitle.Mr,
        };

        var student = new RequiredStudent()
        {
            FirstName = "Foo",
            LastName = "Bar",
            Title = PersonTitle.Mr,
            StudentId = Guid.NewGuid()
        };

        Console.WriteLine(person);
        Console.WriteLine(student);


        // ------------------------------------
        
        // var animation = new DanceMoveAnimation();
        // Console.WriteLine(animation);
        // // Console.WriteLine(animation.ToDanceMoveIdentifier());


        // ------------------------------------

        // var another = new AnotherClass();
        // Console.WriteLine(another);
    }
}

/// <summary>
/// Loads of repetitiveness
/// </summary>
public class RequiredMembersPreviouslyUsingClasses
{

    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string? MiddleName { get; }
        public PersonTitle Title { get; }

        public Person(PersonTitle title, string firstName, string lastName, string? middleName = null)
        {
            Title = title;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName ?? string.Empty;
        }

        public override string ToString() => $"{Title} {FirstName} {MiddleName} {LastName}";
    }

    public class Student : Person
    {
        public Guid StudentId { get; }
        public Student(PersonTitle title, string firstName, string lastName, Guid studentId, string? middleName = null) : base(title, firstName, lastName, middleName)
        {
            StudentId = studentId;
        }

        public override string ToString() => $"Student ({StudentId}): {base.ToString()}";
    }
}


/// <summary>
/// Better, but still a lot of repetitiveness
/// </summary>
public class RequiredMembersPreviouslyUsingRecords
{
    record Person(PersonTitle Title, string FirstName, string LastName, string MiddleName = "");
    record Student(int ID, PersonTitle Title, string FirstName, string LastName, string MiddleName = "") : Person(Title, FirstName, LastName, MiddleName);
}

/// <summary>
/// Much less boilerplate. No need to have constructors, it's compiler problem now
/// 
/// Bonus points:
/// - Same for class, structs and records
/// - Doesn't work with interfaces
/// - Cannot use with fixed, ref readonly, ref, const, static. Required must be settable.
/// - All values are considered 'default' by analyzer. 
/// </summary>
public class RequiredMembersAfter
{
    public class RequiredPerson
    {
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public string? MiddleName { get; }
        public required PersonTitle Title { get; set; }

        public override string ToString() => $"{Title} {FirstName} {MiddleName} {LastName}";
        
        // Watchout 1: Required members can't have constructors
        //
        // public RequiredPerson(string firstName) {
        //     FirstName = firstName;
        // }

        // Watchout 2: Required can only be used on settable properties/fields
        //
        // public required readonly bool IsInitialized;

        // Watchout 3: _field is not at least as visible as Base.
        //
        // protected required int _field; 
    
        // Watchout 4: PropInner cannot be set inside Base or Derived
        // 
        // protected class Inner
        // {
        //     protected required int PropInner { get; set; }
        // }

        // Watchout 5: Required member must have a setter or initer
        //
        // public required int Prop3 { get; }
        
        // Watchout 6: Required member setter must be at least as visible as the constructor of Derived
        //
        // public required int Prop4 { get; internal set; } 

    }
    
    public class RequiredStudent : RequiredPerson
    {
        public required Guid StudentId { get; init; }
        public override string ToString() => $"{Title} {FirstName} {MiddleName} {LastName}";
    }
}


/// <summary>
/// Fancy messing up with required members? Use this attribute
/// 
/// Bonus points:
/// - Any parent class must also have this attribute
/// </summary>
// public class DanceMoveAnimation
// {
//     public required string DanceMoveName { get;set;}

//     // [SetsRequiredMembers]
//     public DanceMoveAnimation()
//     {

//     }

//     // public string ToDanceMoveIdentifier() => DanceMoveName[..4];

//     public override string ToString() => $"Dance move: {DanceMoveName}";
// }


///
/// Differnece between init and required.
/// 
/// With required you are shifting responsibility to the left (i.e. the caller)
/// and you are also telling the compiler that the value will never be null whilst avoiding the need for a constructor
///
// public class AnotherClass
// {
//     public string Name { get; init; }
//     override public string ToString() => Name;
// }