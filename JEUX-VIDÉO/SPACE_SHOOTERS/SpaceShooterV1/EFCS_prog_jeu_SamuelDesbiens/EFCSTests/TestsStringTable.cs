using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFCS;

namespace EFCSTests
{
    [TestClass]
    public class TestsStringTable
    {
        // **************************************
        // N'oubliez pas de nommer vos tests de manière adéquate (ShouldXYZ, CanXYZ, etc)
        // **************************************
        const string EmptyText = @"";
        const string CorrectText = @"ID_THUNDER=tonerre-thunder
                                     ID_Test=teste-test
                                     ID_LOL=riretouthaut-laughingoutloud";
        const string CompletlyWrong = @"pejfngvpoqokldsfnv^qpierôgqnepefjvqojdf=p;kjdfdnvpoqjnfpvq efpov q---q^fvnq=";
        const string MissingId = @"=tonerre-thunder
                                   =teste-test
                                   =riretouthaut-laughingoutloud";
        const string MissingEN = @"ID_THUNDER=tonerre-
                                     ID_Test=teste-
                                     ID_LOL=riretouthaut-";
        const string MissingFR = @"ID_THUNDER=-thunder
                                     ID_Test=-test
                                     ID_LOL=-laughingoutloud";


        [TestMethod]
        public void ShouldntWorkIfEmptyTextFile()
        {
            // Mise en place des données
            // Appel de la méthode à tester
            ErrorCode result = StringTable.GetInstance().Parse(EmptyText);
            // Validation des résultats
            Assert.AreEqual(ErrorCode.BAD_FILE_FORMAT, result);
            // Clean-up
            StringTable.GetInstance().Clean();
        }

        [TestMethod]
        public void ShouldWorkIfCorrectTextFileLoaded()
        {
            // Mise en place des données
            // Appel de la méthode à tester
            ErrorCode result = StringTable.GetInstance().Parse(CorrectText);
            // Validation des résultats
            Assert.AreEqual(ErrorCode.OK, result);
            // Clean-up
            StringTable.GetInstance().Clean();
        }

        [TestMethod]
        public void ShouldntWorkIfCompletelywrongTextFileLoaded()
        {
            // Mise en place des données
            // Appel de la méthode à tester
            ErrorCode result = StringTable.GetInstance().Parse(CompletlyWrong);
            // Validation des résultats
            Assert.AreEqual(ErrorCode.BAD_FILE_FORMAT, result);
            // Clean-up
            StringTable.GetInstance().Clean();
        }
        [TestMethod]
        public void ShouldntWorkIfTextFileMissingTheID()
        {
            // Mise en place des données
            // Appel de la méthode à tester
            ErrorCode result = StringTable.GetInstance().Parse(MissingId);
            // Validation des résultats
            Assert.AreEqual(ErrorCode.MISSING_FIELD, result);
            // Clean-up
            StringTable.GetInstance().Clean();
        }
        [TestMethod]
        public void ShouldWorkIfTextFileMissingTheEnglish()
        {
            // Mise en place des données
            // Appel de la méthode à tester
            ErrorCode result = StringTable.GetInstance().Parse(MissingEN);
            // Validation des résultats
            Assert.AreEqual(ErrorCode.OK, result);
            // Clean-up
            StringTable.GetInstance().Clean();
        }
        [TestMethod]
        public void ShouldWorkIfTextFileMissingTheFrench()
        {
            // Mise en place des données
            // Appel de la méthode à tester
            ErrorCode result = StringTable.GetInstance().Parse(MissingFR);
            // Validation des résultats
            Assert.AreEqual(ErrorCode.OK, result);
            // Clean-up
            StringTable.GetInstance().Clean();
        }
        [TestMethod]
        public void ShouldLoadValueCorrectly()
        {
            // Mise en place des données
            // Appel de la méthode à tester
            StringTable.GetInstance().Parse(CorrectText);
            string result = StringTable.GetInstance().GetValue(Language.English, "ID_THUNDER");
            // Validation des résultats
            Assert.AreEqual("thunder", result);
            // Clean-up
            StringTable.GetInstance().Clean();
        }
        [TestMethod]
        [ExpectedException(typeof(System.Collections.Generic.KeyNotFoundException))]
        public void ShouldPanicIfTheKeyIsNotPresent()
        {
            // Mise en place des données
            // Appel de la méthode à tester
            StringTable.GetInstance().Parse(CorrectText);
            string result = StringTable.GetInstance().GetValue(Language.English, "ID_NotThere");
            // Validation des résultats
            
            // Clean-up
            StringTable.GetInstance().Clean();
        }
    }
}
