# RedFolder.xUnit.IntegrationFact.SpecFlow
SpecFlow generator to add RedFolder.xUnit.IntegrationFact.IntegrationFact rather than the standard xUnit Fact.

See https://github.com/Red-Folder/RedFolder.xUnit.IntegrationFact for more details on the IntegrationFact

## Work in Progress
While it all seems to work, this is currently very much work in progress - thus the alpha state.

Feel free to use - I'd appreciate any feedback.

## To Install
Install into your project with:

```
Install-Package RedFolder.xUnit.IntegrationFact.SpecFlow -Pre
```

When SpecFlow next generates, it will generate tests using the IntegrationFact rather than the standard xUnit Fact.

## Why would I use this
The custom xUnit attribute is intended to be used with integration tests.  By nature, integration test will be slower (and possible have side-effects) compared to unit tests.

Normally, for developer productivity, you will want to run you quick unit test very regulary.  Slower a test suite runs, less likely the tests are used.

As such, common practice is to keep integration (and over long running) tests separate.

IntegrationFact allows you to do this by selectively enabling/ disabling while still using the xUnit tool you are familar with.

## Limitations
This is project-wide - so will generate all SpecFlow features with the IntegrationFact attribute.

