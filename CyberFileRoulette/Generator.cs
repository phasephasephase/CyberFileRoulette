using System.Net;
using System.Text;

namespace CyberFileRoulette;

public static class Generator
{
	public static bool Running { get; set; }
	
	private static readonly HttpClient _client = new();
	private static readonly List<string> _links = new();

	public static async Task GenerateAndCache()
	{
		var random = new Random();
		Running = true;
		
		while (Running)
		{
			// keep a limit of like 100 links in memory
			if (_links.Count > 100) continue;
			
			var sb = new StringBuilder();
			for (var i = 0; i < 4; i++)
			{
				sb.Append((char) random.Next(97, 123));
			}

			var link = $"https://cyberfile.me/{sb}";
			
			var request = new HttpRequestMessage(HttpMethod.Head, link);
			var response = await _client.SendAsync(request);
			if (response.StatusCode == HttpStatusCode.OK && response.RequestMessage.RequestUri.ToString() == link)
			{
				_links.Add(link);
			}
			
			// wait a bit as to not spam the servers
			await Task.Delay(1000);
			
			// update title
			var plural = _links.Count == 1 ? "link" : "links";
			Console.Title = $"CyberFileRoulette - {_links.Count} {plural} cached";
		}
		
		Console.Title = "CyberFileRoulette - Done";
	}

	public static string GetLink()
	{
		// wait for a link to show up in case there aren't any
		while (_links.Count == 0)
		{
			Thread.Sleep(100);
		}
		
		var random = new Random();
		var index = random.Next(0, _links.Count);
		var link = _links[index];
		_links.RemoveAt(index);
		return link;
	}
}