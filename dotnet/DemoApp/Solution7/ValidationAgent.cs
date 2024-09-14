﻿#pragma warning disable SKEXP0110
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0101
using Microsoft.SemanticKernel;
using Core.Utilities.Agents;
using System.ComponentModel;

namespace Solution7
{
    public class ValidationAgent : BaseAgent
    {
        [KernelFunction, Description("Gets the executives schedule.")]
        public async Task<string> GetExecutivesScheduleData()
        {
            var random = new Random();
            var next = random.Next(1, 3);

            if (next == 1)
            {
                return "Schedule is full for this day";
            }

            return "Schedule is open for this day";
        }
    }
}
