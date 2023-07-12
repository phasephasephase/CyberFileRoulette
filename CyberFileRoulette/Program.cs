using System.Diagnostics;
using CyberFileRoulette;

// fat warning when you open this
Console.ForegroundColor = ConsoleColor.Red;
Console.Title = "CyberFileRoulette - Warning";
Console.WriteLine("!! WARNING !!");
Console.WriteLine("Due to CyberFile's lack of restrictions on files uploaded, you may encounter malware, porn, or other disturbing content.");
Console.WriteLine("If you are not comfortable with this, please close this program now.");
Console.WriteLine("This program will generate links in the background. Press any key to continue.");
Console.ReadKey();

Console.ForegroundColor = ConsoleColor.White;

// run Generator.GenerateAndCache on a separate thread
async void Start() => await Generator.GenerateAndCache();
var thread = new Thread(Start);
thread.Start();

// main loop
while (true)
{
	Console.Clear();
	Console.WriteLine("Press any key to open a random link...");
	Console.ReadKey();
	
	var info = new ProcessStartInfo
	{
		FileName = Generator.GetLink(),
		UseShellExecute = true
	};
	
	Process.Start(info);
	
	Console.WriteLine("Would you like another link? (say no to exit)");
	if (Console.ReadLine() == "no")
	{
		break;
	}
}

Generator.Running = false;
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("I guess we're done for the day.\n");
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Press any key to leave.");
Console.ReadKey();