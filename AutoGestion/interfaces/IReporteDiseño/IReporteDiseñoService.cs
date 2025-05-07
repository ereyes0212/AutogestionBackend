using AutoGestion.Models;
using AutoGestion.Models.ReporteDisenioDto;

namespace AutoGestion.interfaces.IReporteDiseño
{
    public interface IReporteDiseñoService
    {
        Task<IEnumerable<ReporteDiseñoDto>> getReporteDiseños();
        Task<ReporteDiseñoDto> getReporteDiseñoById(string id);
        Task<ReporteDiseñoDto> postReporteDiseño(ReporteDiseño reporteDiseño);
        Task<ReporteDiseñoDto> putReporteDiseño(string id, ReporteDiseño reporteDiseño);
    }
}
 