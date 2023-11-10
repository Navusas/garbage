using System.ComponentModel.DataAnnotations;

namespace Csharp11;


public static class ExtendedNameOfScopeDemo
{
    public static void Demonstrate()
    {
        var extendedNameOfScope = new ExtendedNameOfScope();
        extendedNameOfScope.DemonstrateBefore(10);
        extendedNameOfScope.DemonstrateAfter(20);
    }

    public static void DemonstrateAdvanced()
    {
        var extendedNameOfScopeAdvanced = new ExtendedNameOfScopeAdvanced();
        extendedNameOfScopeAdvanced.Demonstrate();
    }
}

/*
Title:          Extended Nameof Scope
Description:    Allow nameof(parameter) inside attribute on method or parameter
Link:           https://github.com/dotnet/csharplang/blob/main/proposals/csharp-11.0/extended-nameof-scope.md
*/
public class ExtendedNameOfScope
{
    public void DemonstrateBefore([LogParameter("value")] int value)
    {
        Console.WriteLine($"Before C# 11: Logging parameter 'value'");
    }

    /// <summary>
    /// After C#12 you can use nameof() inside attribute on method or parameter
    /// </summary>
    /// <param name="value"></param>    
    public void DemonstrateAfter([LogParameter(nameof(value))] int value)
    {
        Console.WriteLine($"After C# 11: Logging parameter '{nameof(value)}'");
    }
}


public class LogParameterAttribute : Attribute
{
    public string ParameterName { get; }

    public LogParameterAttribute(string parameterName)
    {
        ParameterName = parameterName;
    }
}




public class UserRegistrationModel
{
    [Required]
    public string Username { get; set; }

    // [RequiredIf("IsNewsletterSubscribed")]
    [RequiredIf(nameof(IsNewsletterSubscribed))]
    public string Email {get;set;}

    public bool IsNewsletterSubscribed { get; set; }
}

public class RequiredIfAttribute : ValidationAttribute
{
    private readonly string _otherPropertyName;

    public RequiredIfAttribute(string otherPropertyName)
    {
        _otherPropertyName = otherPropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var otherProperty = validationContext.ObjectType.GetProperty(_otherPropertyName);
        var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

        if (otherPropertyValue is bool && (bool)otherPropertyValue && value == null)
        {
            return new ValidationResult($"The {validationContext.DisplayName} field is required when {_otherPropertyName} is true.");
        }

        return ValidationResult.Success;
    }
}



public class ExtendedNameOfScopeAdvanced
{
    public void Demonstrate()
    {
        var userRegistrationRequest = new UserRegistrationModel
        {
            Username = "Foo",
            IsNewsletterSubscribed = true
        };

        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(userRegistrationRequest, new ValidationContext(userRegistrationRequest), validationResults, true);

        if (!isValid)
        {
            foreach (var validationResult in validationResults)
            {
                Console.WriteLine(validationResult.ErrorMessage);
            }
        }
    }
}