using System;

namespace Hydrogen.Repo.Abstractions.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class AutoGenerateFieldAttribute : Attribute
{
    
}