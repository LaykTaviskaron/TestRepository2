// See https://aka.ms/new-console-template for more information
using ConsoleApp1;

Console.WriteLine("Hello, World!");

var struct1 = new Point(10, 12, 0);
var struct2 = new Point(12, 10, 0);

var struct3 = Point.Multiply(struct1, struct2);

var user = new User
{
    Name = "Test",
    Password = "qwerty"
};


int i = 100;
long l = 100;

int intFromLong = (int)l;

object intObj = (object)i;
int intFromObj = (int)intObj;

object longObj = (object)l;
int intFromLongObj = (int)longObj;

var userType = user.GetType();
object userObject = (object)user;
var userObjectType = userObject.GetType();

userObject = (object)1200;

if (userObject is int)
{
    var intFromObject = (int)userObject;
}

if (userObject is User)
{

    var userFromObject = (User)userObject;
}


Console.ReadLine();
