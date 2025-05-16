namespace TrueStory.Models;

public sealed record Data(
    string? Color,
    string? Capacity,
    int? CapacityGb,
    double? Price,
    string? Generation,
    int? Year,
    string? CpuModel,
    string? HardDiskSize,
    string? StrapColour,
    string? CaseSize,
    string? Description,
    double? ScreenSize
);