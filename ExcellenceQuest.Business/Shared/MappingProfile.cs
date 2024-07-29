namespace ExcellenceQuest.Business.Shared
{
    using AutoMapper;
    using ExcellenceQuest.Models.Badge;
    using ExcellenceQuest.Models.Employee;
    using ExcellenceQuest.Models.KPI;
    using ExcellenceQuest.Repository.Models;

    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeModel, Employee>().ReverseMap();
            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<UserRoleModel, UserRole>().ReverseMap();
            CreateMap<CompetencyModel, Competency>().ReverseMap();
            CreateMap<SubCompetencyModel, SubCompetency>().ReverseMap();
            CreateMap<GradeModel, Grade>().ReverseMap();
            CreateMap<RoleModel, Role>().ReverseMap();
            CreateMap<KeyPerformanceIndexModel, KeyPerformanceIndex>().ReverseMap();
            CreateMap<StreamModel, Stream>().ReverseMap();
            CreateMap<EmployeeAchievementModel, EmployeeAchievement>().ReverseMap();
            CreateMap<KPISuccessCriteriaModel, KPISuccessCriterion>().ReverseMap();
            CreateMap<BadgeConfigurationModel, BadgeConfiguration>().ReverseMap();
            CreateMap<BadgeModel, Badge>().ReverseMap();



        }
    }
}
