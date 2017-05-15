using TechTalk.SpecFlow.Generator.Plugins;
using TechTalk.SpecFlow.Generator.UnitTestProvider;

namespace RedFolder.xUnit.IntegrationFact.SpecFlow
{
    public class IntegrationTestAttributeGeneratorPlugin
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
