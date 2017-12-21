using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGetReferenceSwitcher.Presentation.Models;

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
        }

        private List<ReferenceModel> Models()
        {
            return new List<ReferenceModel>()
            {
                new ReferenceModel(null) {

                }
            };
        }
    }
}
