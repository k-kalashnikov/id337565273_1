using System.Linq;
using SP.Market.Identity.Common.Interfaces;

namespace SP.Contract.Application.Common.Extensions
{
    public static class CurrentUserServiceExtension
    {
        // Мастер-пользователь
        private const string SuperuserMnemonic = "superuser.module.platform";

        // Категорийный менеджер
        private const string ManagerMnemonic = "manager.module.market";

        // Администратор логистики
        private const string AdminLogisticMnemonic = "admin.module.logistic";

        // Заказчик
        private const string CustomerBidCenterMnemonic = "customer.module.bidCenter";

        // Исполнитель (Биржа)
        private const string ExecutorBidCenterMnemonic = "executor.module.bidCenter";

        // Менеджер по работе с исполнителями
        private const string PerformerManagerMnemonic = "PERF_MAN";

        // Исполнитель
        private const string PerformerMnemonic = "PERFORMER";

        // Логист
        private const string LogistMnemonic = "LOGIST";

        // Водитель
        private const string DriverMnemonic = "DRIVER";

        // Супервайзер
        private const string SupervisorMnemonic = "SUPERVISOR";

        // Диспетчер перевозки
        private const string DispRouteMnemonic = "DISP_ROUTE";

        // Диспетчер потребности
        private const string DispReqMnemonic = "DISP_REQ";

        // Приемщик
        private const string InspectorPlatformMnemonic = "inspector.module.platform";

        // Техподдержка
        private const string SupportPlatformMnemonic = "support.module.platform";

        // Проектант
        private const string ProjectantPlatformMnemonic = "projectant.module.platform";

        // Агрегатор
        private const string AggregatorPlatformMnemonic = "aggregator.module.platform";

        // Закупщик
        private const string СustomerPlatformMnemonic = "customer.module.platform";

        // Поставщик
        private const string ContractorPlatformMnemonic = "contractor.module.platform";

        // Администратор ДО
        private const string AdmCustomerPlatformMnemonic = "adm.customer.module.platform";

        public static bool IsSuperuser(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == SuperuserMnemonic);
        }

        public static bool IsManager(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == ManagerMnemonic);
        }

        public static bool IsAdminLogistic(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == AdminLogisticMnemonic);
        }

        public static bool IsCustomerBidCenter(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == CustomerBidCenterMnemonic);
        }

        public static bool IsExecutorBidCenter(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == ExecutorBidCenterMnemonic);
        }

        public static bool IsPerformerManager(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == PerformerManagerMnemonic);
        }

        public static bool IsPerformer(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == PerformerMnemonic);
        }

        public static bool IsLogist(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == LogistMnemonic);
        }

        public static bool IsDriver(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == DriverMnemonic);
        }

        public static bool IsSupervisor(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == SupervisorMnemonic);
        }

        public static bool IsDispRoute(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == DispRouteMnemonic);
        }

        public static bool IsDispReq(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == DispReqMnemonic);
        }

        public static bool IsInspectorPlatform(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == InspectorPlatformMnemonic);
        }

        public static bool IsSupportPlatform(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == SupportPlatformMnemonic);
        }

        public static bool IsProjectantPlatform(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == ProjectantPlatformMnemonic);
        }

        public static bool IsAggregatorPlatform(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == AggregatorPlatformMnemonic);
        }

        public static bool IsСustomerPlatform(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == СustomerPlatformMnemonic);
        }

        public static bool IsContractorPlatform(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == ContractorPlatformMnemonic);
        }

        public static bool IsAdmCustomerPlatform(this ICurrentUserService currentUserService)
        {
            var user = currentUserService.GetCurrentUser();
            return user.Roles.Any(r => r == AdmCustomerPlatformMnemonic);
        }
    }
}
