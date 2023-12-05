

namespace RecruitmentTask.Logic.Interfaces.Dtos
{
    public class VProductionInfoDto
    {
        public string? NazwaProduktu { get; set; }

        public string? Ean { get; set; }

        public string? Kategoria { get; set; }

        public string? NazwaProducenta { get; set; }

        public string? Url { get; set; }

        public decimal? StanMagazynowy { get; set; }

        public string? JednostkaLogistyczna { get; set; }

        public decimal? CenaNetto { get; set; }

        public decimal? KosztDostawy { get; set; }
    }
}
