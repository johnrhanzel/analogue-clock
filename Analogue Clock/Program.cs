namespace Analogue_Clock;
using System;

//This is a class for creating branches in a binary tree
public class Branch
{
    //Declare left and right branches
    internal Branch leftBranch;
    internal Branch rightBranch;

    //Declare variables for the program
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public Branch LeftBranch { get; set; }
    public Branch RightBranch { get; set; }

    //Constructor for creating a branch with given hours and minutes
    public Branch(int hours, int minutes)
    {
        Hours = hours;
        Minutes = minutes;
        LeftBranch = null;
        RightBranch = null;
    }

    //Method for adding a left branch with given hours and minutes
    public void AddLeftBranch(int hours, int minutes)
    {
        LeftBranch = new Branch(hours, minutes);
    }

    //Method for adding a right branch with given hours and minutes
    public void AddRightBranch(int hours, int minutes)
    {
        RightBranch = new Branch(hours, minutes);
    }
}

class Program
{
    //This is the main method of the program
    static void Main(string[] args)
    {
        //Creating a binary tree of branches to represent the clock
        Branch root = new Branch(12, 0);
        root.AddLeftBranch(11, 30);
        root.AddRightBranch(1, 0);

        root.LeftBranch.AddLeftBranch(10, 15);
        root.LeftBranch.AddRightBranch(6, 45);

        root.RightBranch.AddLeftBranch(3, 15);
        root.RightBranch.AddRightBranch(6, 0);

        root.LeftBranch.LeftBranch.AddLeftBranch(9, 0);
        root.LeftBranch.LeftBranch.AddRightBranch(4, 30);

        root.LeftBranch.RightBranch.AddLeftBranch(7, 15);
        root.LeftBranch.RightBranch.AddRightBranch(11, 45);

        root.RightBranch.LeftBranch.AddLeftBranch(2, 0);
        root.RightBranch.LeftBranch.AddRightBranch(5, 45);

        root.RightBranch.RightBranch.AddLeftBranch(10, 0);
        root.RightBranch.RightBranch.AddRightBranch(9, 30);

        //Ask for user input to get the hours and minutes seperated by space used for the calculation of the lesser angle
        Console.WriteLine("Enter hours and minutes for the clock (separated by a space): ");
        string input = Console.ReadLine();
        string[] timeValues = input.Split(' ');

        //To validate user input
        if (timeValues.Length != 2)
        {
            Console.WriteLine("Invalid input. Please enter hours and minutes separated by a space.");
            return;
        }

        int hours;
        int minutes;

        if (!int.TryParse(timeValues[0], out hours) || !int.TryParse(timeValues[1], out minutes))
        {
            Console.WriteLine("Invalid input. Please enter valid integers for hours and minutes.");
            return;
        }
        //Calculate the lesser angle between the hours and minutes arrows
        double lesserAngle = GetLesserAngle(root, hours, minutes);
        //Displat the result of the lesser angle in degrees
        Console.WriteLine("The lesser angle in degrees between the hours arrow and minutes arrow is: " + lesserAngle);
    }

    //To calculate lesser angle between the hour and minute for each branch
    static double GetLesserAngle(Branch current, int hours, int minutes)
    {
        //If branch is null it will return the maximum value of a double to ignore the null branches in the calculation of lesser angles
        if (current == null)
        {
            return double.MaxValue;
        }
        // Calculates the angle between the hour arrow and 12 o'clock
        double currentAngle = GetAngle(current.Hours, current.Minutes);

        // Calculates the angle between the hour arrow and the current hour
        double hoursAngle = GetAngle(hours, minutes) - 30 * hours - 0.5 * minutes;

        // Calculates the angle between the minute arrow and 12 o'clock
        double minutesAngle = 6 * minutes;

        // Calculates the lesser angle between the hour arrow and minute arrow
        double angle1 = Math.Abs(currentAngle - hoursAngle);
        double angle2 = Math.Abs(currentAngle - minutesAngle);
        double lesserAngle = Math.Min(angle1, angle2);

        // Calculates the lesser angle for the left and right branches recursively
        double leftBranchAngle = GetLesserAngle(current.leftBranch, hours, minutes);
        double rightBranchAngle = GetLesserAngle(current.rightBranch, hours, minutes);

        // Returns the minimum of the lesser angle for the current branch and the lesser angles of the left and right branches
        return Math.Min(lesserAngle, Math.Min(leftBranchAngle, rightBranchAngle));

        //To calculate the angle between hour and 12 o'clock for the given hours and minutes
        static double GetAngle(int hours, int minutes)
        {
            //Returns the angle in degrees
            return Math.Abs(0.5 * (60 * hours - 11 * minutes));
        }
    }
}