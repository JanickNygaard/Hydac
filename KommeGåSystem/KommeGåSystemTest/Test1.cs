using KommeGåSystem;

namespace KommeGåSystemTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void CheckDetectNewDayWorks()
        {
            //ARRANGE
            DataHandler handler = new DataHandler($"Registrations_{DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy")}");

            // ACT
            bool newDay = handler.DetectNewDay();

            // ASSERT
            Assert.AreEqual( true, newDay );



        }
    }
}
