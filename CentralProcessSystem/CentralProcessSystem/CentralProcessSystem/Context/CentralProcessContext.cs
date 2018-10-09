using CentralProcessSystem.Models.CPS;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CentralProcessSystem.Context
{
    public class CentralProcessContext : DbContext
    {
        public CentralProcessContext() : base("name=CentralProcessContext") { }
        #region MyCompanyInformation
        //everything related to the company data is stored here
        public DbSet<ApplicationInformation> ApplicationInformationDB { get; set; }
        public DbSet<AdsInformation> AdsInformationDB { get; set; }
        #endregion
        #region status reference
        public DbSet<StatusTypesReferences> StatusTypesReferencesDB { get; set; }
        #endregion
        #region user related information
        public DbSet<SignalRData> SignalRDataDB { get; set; }
        public DbSet<UserAccessLevel> UserAccessLevelDB { get; set; }
        public DbSet<Users> UsersDB { get; set; }
        #endregion
        #region GroupingsData
        public DbSet<GroupingsData> GroupingsDataDB { get; set; }
        public DbSet<GroupingsInformation> GroupingsInformationDB { get; set; }

        #endregion
        #region Image Storage
        //image link storage
        public DbSet<ImageLinkStorage> ImageLinkStorageDB { get; set; }
        #endregion
        #region StoreManagement
        public DbSet<MyStore> MyStoreDB { get; set; }
        public DbSet<StoreBranch> StoreBranchDB { get; set; }
        #endregion
        #region Report System
        public DbSet<ReportClaims> ReportClaimsDB { get; set; }
        #endregion
        #region System Logs
        public DbSet<SystemLogs> SystemLogsDB { get; set; }
        #endregion
        #region Messaging Services
        public DbSet<MessagingRoom> MessagingRoomDB { get; set; }
        public DbSet<MessagingConversation> MessagingConversationDB { get; set; }
        public DbSet<MessagingConversationReceipent> MessagingConversationReceipentDB { get; set; }

        #endregion
        #region Notification Services
        //all notification related stuff are stored in here
        public DbSet<NotificationManager> NotificationManagerDB { get; set; }

        #endregion
        #region LocationStorage
        public DbSet<LocationStorage> LocationStorageDB { get; set; }
        #endregion
        #region ReviewStorages
        public DbSet<ReviewStorages> ReviewStoragesDB { get; set; }
        #endregion
        #region InventorySystem
        public DbSet<IS_Item> IS_ItemDB { get; set; }
        public DbSet<IS_ItemStock> IS_ItemStockDB { get; set; }
        public DbSet<IS_ItemColor> IS_ItemColorDB { get; set; }
        #endregion
        #region DateTimeStorage
        public DbSet<DateTimeStorage> DateTimeStorageDB { get; set; }
        #endregion
        #region QuizMaker
        public DbSet<QuizInfo> QuizInfoDB { get; set; }
        public DbSet<QuizQuestions> QuizQuestionsDB { get; set; }
        public DbSet<QuizQuestionAnswer> QuizQuestionAnswerDB { get; set; }
        public DbSet<QuizTakers> QuizTakersDB { get; set; }
        public DbSet<QuizUserAnswer> QuizUserAnswerDB { get; set; }
        #endregion
        #region data collection
        public DbSet<EmailList> EmailListDB { get; set; }
        public DbSet<SearchInputsData> SearchInputDataDB { get; set; }
        #endregion
        #region customerOrder
        public DbSet<CustomerOrder> CustomerOrderDB { get; set; }
        public DbSet<CustomerOrderItem> CustomerOrderItemDB { get; set; }
        #endregion
        #region ShopManagement
        public DbSet<ShopManagement> ShopManagementDB { get; set; }
        #endregion
        #region DescriptionStorage
        public DbSet<DescriptionStorage> DescriptionStorageDB { get; set; }
        #endregion
        #region InvitationStorage
        public DbSet<InvitationStorage> InvitationStorageDB{ get; set; }

        #endregion
        #region ContentManagement
        public DbSet<LeadPages> LeadPagesDB { get; set; }
        #endregion
        #region URL Storage
        public DbSet<URLStorage> URLStorageDB { get; set; }

        #endregion
        #region SecurityLinks
        public DbSet<SecurityLinks> SecurityLinksDB { get; set; }
        #endregion
        #region scoreStorage
        public DbSet<ScoreStorage> ScoreStorageDB { get; set; }
        #endregion
    }
}