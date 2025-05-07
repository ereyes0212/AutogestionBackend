using AutoGestion.Models;

namespace AutoGestion.interfaces.IReporteDiseño
{
    public interface IReporteDiseñoReporsitory
    {
        Task<IEnumerable<ReporteDiseño>> getReportesDiseño();
        Task<ReporteDiseño> getReporteDiseñoById(string id);
        Task<ReporteDiseño> postReporteDiseño(ReporteDiseño reporteDiseño);
        Task<ReporteDiseño> putReporteDiseño(ReporteDiseño reporteDiseño);
    }
}
