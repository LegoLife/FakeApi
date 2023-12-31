﻿using System.ComponentModel.DataAnnotations.Schema;
using FakeApi.Abstractions;

namespace FakeApi.Dto;

[Table("Computer")]
public class ComputerRecord
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string IpAddress { get; set; }
	public string Mac { get; set; }
	public string Avatar { get; set; }
	public string DomainSuffix { get; set; }
	public string Url { get; set; }
}