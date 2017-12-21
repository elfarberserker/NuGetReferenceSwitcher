using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGetReferenceSwitcher.Presentation.Models;
using VSLangProj;

namespace NuGetReferenceSwitcher.Presentation.Tests
{
    [TestClass]
    public class TestingNugetFilterExtension
    {
        [TestMethod]
        public void ShouldReturnAllThree()
        {
            var list = Models();
            var pattern = "";

            var results = list.FilterByRegex(pattern);

            Assert.AreEqual(5, results.Count());
        }
        [TestMethod]
        public void ShouldReturnNone()
        {
            var list = Models();
            var pattern = "^None$";

            var results = list.FilterByRegex(pattern);

            Assert.AreEqual(0, results.Count());
        }
        [TestMethod]
        public void ShouldReturnFour()
        {
            var list = Models();
            var pattern = @"^XAP(\.[^$]*?)?$";

            var results = list.FilterByRegex(pattern);

            Assert.AreEqual(4, results.Count());
        }

        private List<ReferenceModel> Models()
        {
            return new List<ReferenceModel>()
            {
                new ReferenceModel(new TestReference("XAP")),
                new ReferenceModel(new TestReference("XAP.Poet")),
                new ReferenceModel(new TestReference("XAP.Sql")),
                new ReferenceModel(new TestReference("XAP.Vinnustund.Connector")),
                new ReferenceModel(new TestReference("System.Web"))
            };
        }
    }
}
