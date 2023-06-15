// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using System.Reflection;

Console.WriteLine("Hello, World!");

int i = 10;
var typeInfo = typeof(int);

//Console.WriteLine(typeInfo.DeclaringType);
//Console.WriteLine(typeInfo.BaseType);
//Console.WriteLine(typeInfo.FullName);

//var objectFromActivator = Activator.CreateInstance(typeInfo);
//if (typeInfo.Name == "Test")
//{
//    var newTest1 = objectFromActivator as Test;
//}

//var newTest = objectFromActivator as Test;
//if (newTest != null)
//{

//}


var test = new Test();

var privateMethods = Type.GetType("ConsoleApp1.Test")
                          .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static);

foreach (var property in Type.GetType("ConsoleApp1.Test")
                          .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static))
{
    var hasDefaultValueAttr = false;
    int? defaultValueAttr = 0;
    foreach (var attr in property.CustomAttributes)
    {
        hasDefaultValueAttr = attr.AttributeType == typeof(DefaultValue);
        defaultValueAttr = attr.ConstructorArguments[0].Value as int?;

        if (hasDefaultValueAttr)
        {
            break;
        }
    }

    if (hasDefaultValueAttr && property.CanWrite)
    {
        property.SetValue(test, defaultValueAttr);
    }
}



Console.ReadLine();

// Create new instance
//obj = (MyFancyObject)Activator.CreateInstance("MyAssembly", ClassName))

// Get all public methods
//MethodInfo[] methodInfos = Type.GetType(selectedObjcClass)
//                           .GetMethods(BindingFlags.Public | BindingFlags.Instance);


// get methods with attributes
//var methods = assembly.GetTypes()
//                      .SelectMany(t => t.GetMethods())
//                      .Where(m => m.GetCustomAttributes(typeof(MenuItemAttribute), false).Length > 0)
//                      .ToArray();