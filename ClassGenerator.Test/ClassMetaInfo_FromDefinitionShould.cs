using System;
using Xunit;
using ClassGenerator;

namespace ClassGenerator.Test
{
    public class ClassMetaInfo_FromDefinitionShould
    {
        [Fact]
        public void ReturnNullIfDefinitionIsMalformed()
        {
        	var definition = "foo/bar:namespace::";
        	var metaInfo = ClassMetaInfo.FromDefinition(definition);

        	Assert.Null(metaInfo);
        }

        [Fact]
        public void ReturnCorrectMetaInfoIfDefinitionIsValid()
        {
        	var definition = "path/to/some/location:ThisIsNamespace::ThisIsClassName";

        	var metaInfo = ClassMetaInfo.FromDefinition(definition);

        	Assert.Equal("path/to/some/location", metaInfo.Location);
        	Assert.Equal("ThisIsClassName", metaInfo.ClassName);
        	Assert.Equal("ThisIsNamespace", metaInfo.Namespace);
        }
    }
}
