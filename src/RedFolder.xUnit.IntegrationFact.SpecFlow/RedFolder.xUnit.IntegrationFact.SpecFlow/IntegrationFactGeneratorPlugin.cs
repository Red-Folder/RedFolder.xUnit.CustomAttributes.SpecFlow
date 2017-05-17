using TechTalk.SpecFlow.Generator.Plugins;
using TechTalk.SpecFlow.Generator.UnitTestProvider;
using TechTalk.SpecFlow.Infrastructure;

[assembly: GeneratorPlugin(typeof(RedFolder.xUnit.IntegrationFact.SpecFlowPlugin.IntegrationTestAttributeGeneratorPlugin))]

namespace RedFolder.xUnit.IntegrationFact.SpecFlowPlugin
{
    public class IntegrationTestAttributeGeneratorPlugin: IGeneratorPlugin
    {
        public void Initialize(GeneratorPluginEvents generatorPluginEvents, GeneratorPluginParameters generatorPluginParameters)
        {
            generatorPluginEvents.CustomizeDependencies += CustomizeDependencies;
        }

        public void CustomizeDependencies(object sender, CustomizeDependenciesEventArgs eventArgs)
        {
            eventArgs.ObjectContainer.RegisterTypeAs<IntegrationFactGeneratorProvider, IUnitTestGeneratorProvider>();
        }
    }
}
