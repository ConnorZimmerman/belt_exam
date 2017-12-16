using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

[AttributeUsage(AttributeTargets.All)]
public class PasswordValidation : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        string newVal = (string)value;
        if (String.IsNullOrEmpty(newVal))
        {
            return false;
        }
        string passwordPattern = @"^(?=.*[0-9])(?=.*[!@#$%^&*])[0-9a-zA-Z!@#$%^&*0-9]{10,}$";
        if (!Regex.IsMatch(newVal, passwordPattern))
        {
            return false;
        }
        return true;
    }

    public override string FormatErrorMessage(string name)
    {
        return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
    }
}