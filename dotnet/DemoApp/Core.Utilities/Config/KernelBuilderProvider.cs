using Core.Utilities.Models;
using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Config
{
  public static class KernelBuilderProvider
  {
    public static IKernelBuilder CreateKernelWithChatCompletion()
    {
      var applicationSettings = AISettingsProvider.GetSettings();

      var builder = Kernel.CreateBuilder();

      builder.AddAzureOpenAIChatCompletion(
          applicationSettings.OpenAI.ModelName,
          applicationSettings.OpenAI.Endpoint,
          applicationSettings.OpenAI.Key);

      return builder;
    }

  }
}
