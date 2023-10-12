using FakeApi.Abstractions;

namespace FakeApi.Dto;

public class VehicleRecord : IId
{
	public string Vin { get; set; }
	public string Manufacturer { get; set; }
	public string Model { get; set; }
	public string Type { get; set; }
	public string Fuel { get; set; }
	public int Id { get; set; }
}