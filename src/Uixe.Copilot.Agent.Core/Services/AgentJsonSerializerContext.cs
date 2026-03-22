using System.Text.Json;
using System.Text.Json.Serialization;
using Uixe.Copilot.Agent.Core.Models;

namespace Uixe.Copilot.Agent.Core.Services;

[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
[JsonSerializable(typeof(AgentInstructionRequest))]
[JsonSerializable(typeof(AgentInstructionResponse))]
internal sealed partial class AgentJsonSerializerContext : JsonSerializerContext
{
}
