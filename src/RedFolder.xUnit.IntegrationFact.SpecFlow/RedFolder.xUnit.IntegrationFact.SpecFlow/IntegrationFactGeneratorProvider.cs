using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow.Generator;
using TechTalk.SpecFlow.Generator.UnitTestProvider;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.Utils;

namespace RedFolder.xUnit.IntegrationFact.SpecFlowPlugin
{
    public class IntegrationFactGeneratorProvider : IUnitTestGeneratorProvider
    {
        private const string FEATURE_TITLE_PROPERTY_NAME = "FeatureTitle";
        private const string DESCRIPTION_PROPERTY_NAME = "Description";
        private const string FACT_ATTRIBUTE = "RedFolder.Xunit.IntegrationTestAttribute";
        private const string FACT_ATTRIBUTE_SKIP_PROPERTY_NAME = "Skip";
        internal const string SKIP_REASON = "Ignored";

        internal const string THEORY_ATTRIBUTE = "Xunit.Extensions.TheoryAttribute";
        internal const string THEORY_ATTRIBUTE_SKIP_PROPERTY_NAME = "Skip";

        private const string TRAIT_ATTRIBUTE = "Xunit.TraitAttribute";

        private IUnitTestGeneratorProvider _innerGenerator;
        protected CodeDomHelper CodeDomHelper { get; set; }

        public IntegrationFactGeneratorProvider(CodeDomHelper codeDomHelper)
        {
            CodeDomHelper = codeDomHelper;
            _innerGenerator = new XUnit2TestGeneratorProvider(codeDomHelper);
        }

        public void FinalizeTestClass(TestClassGenerationContext generationContext)
        {
            _innerGenerator.FinalizeTestClass(generationContext);
        }

        public UnitTestGeneratorTraits GetTraits()
        {
            return _innerGenerator.GetTraits();
        }

        public void SetRow(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, IEnumerable<string> arguments, IEnumerable<string> tags, bool isIgnored)
        {
            _innerGenerator.SetRow(generationContext, testMethod, arguments, tags, isIgnored);
        }

        public void SetRowTest(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, string scenarioTitle)
        {
            _innerGenerator.SetRowTest(generationContext, testMethod, scenarioTitle);
        }

        public void SetTestClass(TestClassGenerationContext generationContext, string featureTitle, string featureDescription)
        {
            _innerGenerator.SetTestClass(generationContext, featureTitle, featureDescription);
        }

        public void SetTestClassCategories(TestClassGenerationContext generationContext, IEnumerable<string> featureCategories)
        {
            _innerGenerator.SetTestClassCategories(generationContext, featureCategories);
        }

        public void SetTestClassCleanupMethod(TestClassGenerationContext generationContext)
        {
            _innerGenerator.SetTestClassCleanupMethod(generationContext);
        }

        public void SetTestClassIgnore(TestClassGenerationContext generationContext)
        {
            _innerGenerator.SetTestClassIgnore(generationContext);
        }

        public void SetTestClassInitializeMethod(TestClassGenerationContext generationContext)
        {
            _innerGenerator.SetTestClassInitializeMethod(generationContext);
        }

        public void SetTestCleanupMethod(TestClassGenerationContext generationContext)
        {
            _innerGenerator.SetTestCleanupMethod(generationContext);
        }

        public void SetTestInitializeMethod(TestClassGenerationContext generationContext)
        {
            _innerGenerator.SetTestInitializeMethod(generationContext);
        }

        public void SetTestMethod(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, string friendlyTestName)
        {
            CodeDomHelper.AddAttribute(testMethod, FACT_ATTRIBUTE, new CodeAttributeArgument("DisplayName", new CodePrimitiveExpression(friendlyTestName)));

            SetProperty(testMethod, FEATURE_TITLE_PROPERTY_NAME, generationContext.Feature.Name);
            SetDescription(testMethod, friendlyTestName);
        }

        public void SetTestMethodAsRow(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, string scenarioTitle, string exampleSetName, string variantName, IEnumerable<KeyValuePair<string, string>> arguments)
        {
            _innerGenerator.SetTestMethodAsRow(generationContext, testMethod, scenarioTitle, exampleSetName, variantName, arguments);
        }

        public void SetTestMethodCategories(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, IEnumerable<string> scenarioCategories)
        {
            _innerGenerator.SetTestMethodCategories(generationContext, testMethod, scenarioCategories);
        }

        public void SetTestMethodIgnore(TestClassGenerationContext generationContext, CodeMemberMethod testMethod)
        {
            var factAttr = testMethod.CustomAttributes.OfType<CodeAttributeDeclaration>()
                            .FirstOrDefault(codeAttributeDeclaration => codeAttributeDeclaration.Name == FACT_ATTRIBUTE);

            if (factAttr != null)
            {
                // set [FactAttribute(Skip="reason")]
                factAttr.Arguments.Add
                    (
                        new CodeAttributeArgument(FACT_ATTRIBUTE_SKIP_PROPERTY_NAME, new CodePrimitiveExpression(SKIP_REASON))
                    );
            }

            var theoryAttr = testMethod.CustomAttributes.OfType<CodeAttributeDeclaration>()
                .FirstOrDefault(codeAttributeDeclaration => codeAttributeDeclaration.Name == THEORY_ATTRIBUTE);

            if (theoryAttr != null)
            {
                // set [TheoryAttribute(Skip="reason")]
                theoryAttr.Arguments.Add
                    (
                        new CodeAttributeArgument(THEORY_ATTRIBUTE_SKIP_PROPERTY_NAME, new CodePrimitiveExpression(SKIP_REASON))
                    );
            }
        }

        protected void SetProperty(CodeTypeMember codeTypeMember, string name, string value)
        {
            CodeDomHelper.AddAttribute(codeTypeMember, TRAIT_ATTRIBUTE, name, value);
        }

        protected void SetDescription(CodeTypeMember codeTypeMember, string description)
        {
            // xUnit doesn't have a DescriptionAttribute so using a TraitAttribute instead
            SetProperty(codeTypeMember, DESCRIPTION_PROPERTY_NAME, description);
        }
    }
}
