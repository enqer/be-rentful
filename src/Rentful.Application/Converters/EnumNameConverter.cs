using Rentful.Domain.ConstantValues;
using Rentful.Domain.Entities.Enums;

namespace Rentful.Application.Converters
{
    public class EnumNameConverter
    {
        public static string GetTenantRatingName(TenantRatingEnum rating)
        {
            return rating switch
            {
                TenantRatingEnum.NotRated => TenantRatingName.NotRated,
                TenantRatingEnum.Unsatisfied => TenantRatingName.Unsatisfied,
                TenantRatingEnum.Neutral => TenantRatingName.Neutral,
                TenantRatingEnum.Satisfied => TenantRatingName.Satisfied,
                TenantRatingEnum.Excellent => TenantRatingName.Excellent,
                _ => TenantRatingName.NotRated
            };
        }

        public static string GetReportTypeName(ReportTypeEnum type)
        {
            return type switch
            {
                ReportTypeEnum.Any => ReportTypeName.Any,
                ReportTypeEnum.Failure => ReportTypeName.Failure,
                ReportTypeEnum.Payment => ReportTypeName.Payment,
                _ => ReportTypeName.Any
            };
        }

        public static string GetReportStatusName(ReportStatusEnum status)
        {
            return status switch
            {
                ReportStatusEnum.Unresolved => ReportStatusName.Unresolved,
                ReportStatusEnum.Resolved => ReportStatusName.Resolved,
                ReportStatusEnum.Rejected => ReportStatusName.Rejected,
                _ => ReportStatusName.Unresolved
            };
        }
    }
}
