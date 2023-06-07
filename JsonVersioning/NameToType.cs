using ModelsGeneration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonVersioning
{
    public class NameToType
    {
        public Dictionary<string, Type> NameToTypeDict = new Dictionary<string, Type>()
        {
            {"AdminUsers", typeof(AdminUser)},
            {"AppUsers", typeof(AppUser)},
            {"AsaGlobalPreConditionRules", typeof(AsaGlobalPreConditionRule)},
            {"AsaGlobalRules", typeof(AsaGlobalRule)},
            {"AuditLogs", typeof(AuditLog)},
            {"BackupServerSettings", typeof(BackupServerSetting)},
            {"BgsDataPoints", typeof(BgsDataPoint)},
            {"BgsWebsiteInfos", typeof(BgsWebsiteInfo)},
            {"Bgswaypoints", typeof(Bgswaypoint)},
            {"BgswaypointsSummaries", typeof(BgswaypointsSummary)},
            {"CorrectedSurveys", typeof(CorrectedSurvey)},
            {"CorrectedSurveyValues", typeof(CorrectedSurveyValue)},
            {"CustomerImages", typeof(CustomerImage)},
            {"CustomerReports", typeof(CustomerReport)},
            {"CustomerReportData", typeof(CustomerReportDatum)},
            {"CustomerReportTemplates", typeof(CustomerReportTemplate)},
            {"CustomerReportTemplateData", typeof(CustomerReportTemplateDatum)},
            {"DpatrialLogs", typeof(DpatrialLog)},
            {"ExportImportTemplateInformations", typeof(ExportImportTemplateInformation)},
            {"Hs2TgTrialLogs", typeof(Hs2TgTrialLog)},
            {"HypercubeWebsiteInfos", typeof(HypercubeWebsiteInfo)},
            {"MaxSurveyActionResults", typeof(MaxSurveyActionResult)},
            {"MaxSurveyPreConditionRules", typeof(MaxSurveyPreConditionRule)},
            {"MaxSurveyRules", typeof(MaxSurveyRule)},
            {"MaxSurveyRuleResponses", typeof(MaxSurveyRuleResponse)},
            {"MaxSurveyRuleSetResponses", typeof(MaxSurveyRuleSetResponse)},
            {"Messages0s", typeof(Messages0)},
            {"Messages0Ids", typeof(Messages0Id)},
            {"MigrationHistories", typeof(MigrationHistory)},
            {"ObservatoryReadings", typeof(ObservatoryReading)},
            {"ObservatoryStations", typeof(ObservatoryStation)},
            {"OdisseusToolCodes", typeof(OdisseusToolCode)},
            {"OdisseusToolCodeData", typeof(OdisseusToolCodeDatum)},
            {"OdisseusToolCodeParams", typeof(OdisseusToolCodeParam)},
            {"OdisseusToolCodeSections", typeof(OdisseusToolCodeSection)},
            {"Plansurveys", typeof(Plansurvey)},
            {"Pmrresults", typeof(Pmrresult)},
            {"PmrsurveyBases", typeof(PmrsurveyBase)},
            {"RangingDeterminations", typeof(RangingDetermination)},
            {"RawSurveys", typeof(RawSurvey)},
            {"ResourceGroups", typeof(ResourceGroup)},
            {"Runs", typeof(Run)},
            {"Schemas", typeof(Schema)},
            {"Shortsurveys", typeof(Shortsurvey)},
            {"Solutions", typeof(Solution)},
            {"SpatrialLogs", typeof(SpatrialLog)},
            {"Templates", typeof(Template)},
            {"TraceLogs", typeof(TraceLog)},
            {"Uncertainties", typeof(Uncertainty)},
            {"WaitObservatoryCorrectedSurveys", typeof(WaitObservatoryCorrectedSurvey)},
            {"Waypoints", typeof(Waypoint)},
            {"Wells", typeof(Well)}
        };
    }
}
