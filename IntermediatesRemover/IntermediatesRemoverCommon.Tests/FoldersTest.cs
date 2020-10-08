using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntermediatesRemoverCommon.Tests
{
    [TestClass]
    public class FoldersTest
    {
        [TestMethod]
        public void ConstructionTest()
        {
            var folders = new Folders(CancellationToken.None);
            Assert.IsNotNull(folders);
        }
    }
}
