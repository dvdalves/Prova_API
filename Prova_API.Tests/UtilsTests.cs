namespace Prova_API.Tests
{
    public class UtilsTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
            InicializarContexto();
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void Converter_Data_Hora_Brasilia()
        {
            // Arrange
            var dataHoraUtc = new DateTime(2021, 10, 10, 10, 10, 10, DateTimeKind.Utc);
            var esperadoBrasilia = TimeZoneInfo.ConvertTimeFromUtc(dataHoraUtc, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            // Act
            var dataHoraConvertida = DateTimeConverter.ConvertToBrasiliaTimeZone(dataHoraUtc);

            // Assert
            Assert.That(dataHoraConvertida, Is.EqualTo(esperadoBrasilia));
        }

        [Test]
        public void Converter_Data_Hora_Local_Para_Brasilia()
        {
            // Arrange
            var dataHoraLocal = new DateTime(2021, 10, 10, 10, 10, 10, DateTimeKind.Local);
            var esperadoBrasilia = TimeZoneInfo.ConvertTime(dataHoraLocal, TimeZoneInfo.Local, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

            // Act
            var dataHoraConvertida = DateTimeConverter.ConvertToBrasiliaTimeZone(dataHoraLocal);

            // Assert
            Assert.That(dataHoraConvertida, Is.EqualTo(esperadoBrasilia));
        }
    }
}
