using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolSystem.Business.Quran.Pages;

namespace SchoolSystem.Tests.Quran
{
    [TestClass]
    public class clsQuranPagesSyncTests
    {
        [TestMethod]
        public void ExtractPageNumber_NumericFileName_ReturnsNumber()
        {
            int result = clsQuranPagesSync.ExtractPageNumber(@"C:\Quran\604.jpeg");

            Assert.AreEqual(604, result);
        }

        [TestMethod]
        public void ExtractPageNumber_PrefixedFileName_ReturnsEmbeddedNumber()
        {
            int result = clsQuranPagesSync.ExtractPageNumber(@"C:\Quran\page_021.png");

            Assert.AreEqual(21, result);
        }

        [TestMethod]
        public void ExtractPageNumber_FileNameWithoutDigits_ReturnsMinusOne()
        {
            int result = clsQuranPagesSync.ExtractPageNumber(@"C:\Quran2026\cover.jpg");

            Assert.AreEqual(-1, result);
        }
    }
}
