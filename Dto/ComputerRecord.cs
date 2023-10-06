using System.Net;

namespace API.Dto;

public class ComputerRecord
{
	public Location Location { get; set; } = new ();
	public string Name { get; set; }
	public string IpAddress { get; set; }
	public string Mac { get; set; }
	public string Avatar { get; set; }
	public string DomainSuffix { get; set; }
	public string Url { get; set; }
}

public class Location
{
	public string Country { get; set; }
}