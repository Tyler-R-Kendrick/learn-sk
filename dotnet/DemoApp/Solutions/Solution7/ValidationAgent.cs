using Microsoft.SemanticKernel;
using Core.Utilities.Agents;
using System.ComponentModel;

namespace Solution7;

public class ValidationAgent : BaseAgent
{
    private readonly Random _random = new();

    [KernelFunction, Description("Gets the executives schedule.")]
    public string GetExecutivesScheduleData()
    {
        var next = _random.Next(1, 4);
        return next != 1
            ? "Schedule is full for this day"
            : "Schedule is open for this day";
    }
}
